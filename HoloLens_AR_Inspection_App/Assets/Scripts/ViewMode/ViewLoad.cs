using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using SimpleJSON;
using TMPro;
using System.Linq;

public class ViewLoad : MonoBehaviour
{
    GameObject modelParent;       //Assign Image Tracker as parent of model
    Object selectedPrefab;

    [SerializeField] GameObject coordinatePrefab;   //Assign prefab for the coordinate gameobject
    GameObject spawnedCoordinate;

    [SerializeField] GameObject noPathPopUp;       //Assign no valid path pop up window

    [SerializeField] GameObject inputProject;      //Assign dropdown
    [SerializeField] GameObject dropdownValue;     //Assign dropdown label
    TMP_Dropdown dropdown;                         //Assign variable

    public List<string> startList = new List<string>();
    public List<string> projectList = new List<string>();

    string path;
    string jsonString;
    JSONObject loadJson;
    string nameLoad;
    string modelLoad;
    string coordinatesLoad;
    string materialLoad;

    string projectFileName;
    public string projectName;

    string[] colour;
    string typeName;

    string preLoad;
    string[] arrayLoad;
    Vector3 spawnLoad;
    GameObject spawnedModel;
    string typeLoad;

    GameObject marking;
    GameObject parentPrefab;
    [SerializeField] GameObject markingPrefab;
    [SerializeField] GameObject lineRendererPrefab;
    GameObject lineRenderer;

    Material material;
    Material selectedMaterial;

    [SerializeField] GameObject tagPrefab;
    GameObject tagObject;

    int loops = 100;        //Set amount of loops to go through the marking numbers (loops = max markings)
    float timer = 3.0f;     //Timer to set duration of no path found error
    

    //At the start of the script, immediately search for the saved projects and populate the dropdown menu with them.
    public void Awake()
    {
        DirectoryInfo dir = new DirectoryInfo(Application.persistentDataPath);  //Set local app location (app folder path)
        FileInfo[] fileArray = dir.GetFiles( "*.json*" );                   //Get an array of all json files from folder

        for (int i = 0; i < fileArray.Length; i++) {                        //Get all names of the available projects
            string[] nameOnly = fileArray[i].ToString().Split('_');         //Only get name part
            projectList.Add(nameOnly[nameOnly.Length - 1].ToString());      //Add name to list
        }

        dropdown = inputProject.GetComponent<TMP_Dropdown>();       //Define dropdown gameobject as dropdown variable
        dropdown.ClearOptions();                                    //Clear dropdown values
        startList.Add("Choose");                                    //Instruction at first value of the list
        dropdown.AddOptions(startList);                             //Set first value of dropdown
        dropdown.AddOptions(projectList);                           //Set other values of dropdown to all available project names
    }


    //This script has to run the timer to get the right duration for the no path found error.
    public void Update()
    {   
        //Timer to let no path found popup wait between active and inactive
        if (timer >= 0) {
            timer = timer - Time.deltaTime;
        }
        if (timer < 0) {
            timer = 3;
            noPathPopUp.SetActive(false);
        }
    }


    //Function to set no path found timer back to start value
    public void SetTimer() {
        timer = 3.0f;
    }


    //The Load function has to load all information from a saved project and spawn all corresponding gameobjects.
    public void Load() {
        //Get selected projectName
        projectFileName = dropdownValue.GetComponent<TextMeshProUGUI>().text;                       //Get value from dropdown
        projectName = projectFileName.Substring(0, projectFileName.Length - 5);                     //Remove .json from value to get project name
        path = Application.persistentDataPath + "/NoseConeProject_"+projectName+".json";            //Define project json file path
        try {
            File.ReadAllText(path);                                             //Try path
        }
        catch (FileNotFoundException) {                                         //Look for no path found error
            SetTimer();                                                         //Set timer to 3 seconds
            noPathPopUp.SetActive(true);                                        //Show pop up to inform user about wrong file path
        }

        //Read project
        string jsonString = File.ReadAllText(path);                             //Read whole json file and convert to string
        JSONObject loadJson = (JSONObject)JSON.Parse(jsonString);               //Retrieve json object

        nameLoad = loadJson["Project"];                                         //Get name from name object
        modelLoad = loadJson["Model"];                                          //Get model name from model object

        //Spawn Model
        var prefab = Resources.Load("3DModels/"+modelLoad);                 //Load nose cone prefab corresponding to the model name
        selectedPrefab = prefab as GameObject;                              //Set prefab variable
        modelParent = GameObject.Find("ImageTarget");
        Instantiate(selectedPrefab, modelParent.transform);                 //Spawn prefab (wanted nose cone) as child of Target Image

        //Find saved markings
        for (int i = 0; i < loops; i++) {                                                   //Search for Marking_0 until Marking_100 within saved json file
            preLoad = loadJson["Marking_"+i].ToString();                                    //Get Marking_i part of saved json file (this contains the coordinates)
            arrayLoad = preLoad.Split(';');                                                 //Split the coordinates' string
            parentPrefab = GameObject.Find("ImageTarget/"+modelLoad+"(Clone)/Markings");    //Find Markings (group) gameobject of 3D model

            materialLoad = loadJson["Colour_"+i].ToString();                                //Get Colour_i part of saved json file
            materialLoad = materialLoad.Substring(1, materialLoad.Length - 2);              //Get only the relevant colour name
            MaterialSelection();                                                            //Perform material selection function

            typeLoad = loadJson["Type_"+i].ToString();                                  //Get Type_i part of saved json file
            
            marking = Instantiate(markingPrefab);                                   //Spawn marking gameobject
            marking.transform.SetParent(parentPrefab.transform, false);             //Make this marking child of Markings gameobject
            marking.name = (i.ToString());                                          //Set name to i (marking number)
            marking.GetComponent<Renderer>().material = selectedMaterial;           //Give marking the corresponding material
            
            //Populate marking with coordinates
            for (int x = 0; x < arrayLoad.Length; x++){
                    if ( ((x % 3) == 0) && (x <= arrayLoad.Length-2) ) {            //Divide coordinates array in groups of 3 coordinates
                        spawnLoad.x = float.Parse(arrayLoad[x].Trim('['));          //Get x coordinate and delete [ symbol
                        spawnLoad.y = float.Parse(arrayLoad[(x+1)]);                //Get y coordinate
                        spawnLoad.z = float.Parse(arrayLoad[(x+2)].Trim(']'));      //Get z coordinate and delete ] symbol
                        
                        //Spawn coordinate, set parent with relative offset, set local position
                        spawnedCoordinate = Instantiate(coordinatePrefab);
                        spawnedCoordinate.transform.SetParent(marking.transform, false);
                        spawnedCoordinate.transform.localPosition = spawnLoad;
                    }
            }
        }

        //Delete all markings without coordinates
        foreach (Transform i in parentPrefab.transform) {
            if (i.childCount < 1) {
                Destroy(i.gameObject);
            }
        }

        //Spawn LineRenderer and MarkingTag for each marking
        foreach (Transform i in parentPrefab.transform) {
            marking = Instantiate(lineRendererPrefab);
            marking.transform.SetParent(i.transform, false);
            
            tagObject = Instantiate(tagPrefab);
            tagObject.transform.SetParent(i.transform, false);
        }

        //Give every marking the corresponding damage type information
        for (int i = 0; i < loops; i++) {
            typeLoad = loadJson["Type_"+i].ToString();                          //Get Type_i part of saved json file
            typeName = typeLoad.Substring(1, typeLoad.Length - 2);              //Only get the relevant string part
            //Find Tag text component
            GameObject typeObject = GameObject.Find("ImageTarget/"+modelLoad+"(Clone)/Markings/"+i+"/HoloTag(Clone)/DescriptionWindow/TypeAnswer");
            typeObject.GetComponent<TextMeshPro>().text = typeName;             //Set tag text to damage type
        }
    }

    //Function to load the material from the Resources/Colours/ folder
    public void MaterialSelection() {
        var material = Resources.Load("Colours/"+materialLoad);
        selectedMaterial = material as Material; 
    }
}
