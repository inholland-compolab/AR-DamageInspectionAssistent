using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using SimpleJSON;
using TMPro;
using System.Linq;

public class NewLoadSave : MonoBehaviour
{
    GameObject modelParent;       //Assign Image Tracker as parent of model
    Object selectedPrefab;

    [SerializeField] GameObject coordinatePrefab;
    GameObject spawnedCoordinate;

    [SerializeField] GameObject noPathPopUp;       //Assign no valid path pop up

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

    string modelName;

    string projectFileName;
    public string projectName;

    public string markingTag;
    public string colourChoice;
    string[] colour;
    string colourName;
    string typeName;

    string preLoad;
    string[] arrayLoad;
    Vector3 spawnLoad;
    GameObject spawnedModel;
    string typeLoad;

    GameObject[] coordinatesList;
    GameObject marking;
    GameObject parentPrefab;
    [SerializeField] GameObject markingPrefab;
    [SerializeField] GameObject lineRendererPrefab;
    GameObject lineRenderer;

    Material material;
    Material selectedMaterial;

    [SerializeField] GameObject tagPrefab;
    GameObject tagObject;

    int loops = 100;
    float timer = 3.0f;

    InspectionButtons inspectionButtons;

    string pathList;
    string[] projectArray;
    public List<string> projectMarkingsList = new List<string>();

    [SerializeField] GameObject dropdownList;
    [SerializeField] GameObject dropdownListValue;     //Assign dropdown label

    string selectMarking;
    string listString;
    string saveString;

    [SerializeField] GameObject listWindow;

    GameObject[] numbersObjects;
    

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
    //Also, variables are sent to other scripts.
    public void Update()
    {   
        //Send model selection to other scripts
        modelName = modelLoad;
        HierarchyManager hierarchyManager = GameObject.Find("InspectionManager").GetComponent<HierarchyManager>();
        InspectionButtons inspectionButtons = GameObject.Find("InspectionManager").GetComponent<InspectionButtons>();
        CoordinatesInput coordinatesInput = GameObject.Find("ImageTarget/"+modelName+"(Clone)/default").GetComponent<CoordinatesInput>();
        hierarchyManager.GetModelName(modelName);
        inspectionButtons.GetModelName(modelName);
        coordinatesInput.GetModelName(modelName);

        //Send model selection to scripts of number objects (responsible for creating the marking tag)
        numbersObjects = GameObject.FindGameObjectsWithTag("Number");
        for (int i = 0; i < numbersObjects.Length - 1; i++) {
            NumberValue numberValue = numbersObjects[i].GetComponent<NumberValue>();
            numberValue.GetModelName(modelName);
        }

        pathList = path;

        //Timer to let popup wait between active and inactive
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
        catch (FileNotFoundException) {
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

        spawnedModel = GameObject.Find(modelLoad+"(Clone)");                //Find spawned 3D model

        //Find saved markings
        for (int i = 0; i < loops; i++) {                                                   //Search for Marking_0 until Marking_100 within saved json file
            preLoad = loadJson["Marking_"+i].ToString();                                    //Get Marking part of saved json file (this contains the coordinates)
            arrayLoad = preLoad.Split(';');                                                 //Split the coordinates' string
            parentPrefab = GameObject.Find("ImageTarget/"+modelLoad+"(Clone)/Markings");

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
            if (i.childCount < 3) {
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
    
    //The save function is responsible for saving the markings and their information to a json file (storage method: SimpleJSON)
    public void Save() {
        string savePath = Application.persistentDataPath + "/NoseConeProject_"+projectName+".json";     //Define project json file path
        string jsonString = File.ReadAllText(savePath);                                                 //Read whole json file and convert to string

        JSONObject ProjectJson = new JSONObject();                          //Create name object in JSON File
        ProjectJson.Add("Project", projectName);                            //Add project name (why not nameLoad???)
        ProjectJson.Add("Model", modelLoad);                                //Add project model

        GameObject markingsGroup = GameObject.Find("ImageTarget/"+modelLoad+"(Clone)/Markings");        //Find Markings gameobject that contains all markings
        
        //Find all marking gameobjects within the Markings gameobject to save them
        foreach (Transform i in markingsGroup.transform) {
            JSONArray coordinatesSaved = new JSONArray();                                   //Create JSON array for coordinates
            colour = i.GetComponent<Renderer>().material.name.ToString().Split(' ');        //Get marking colour
            colourName = colour[0];                                                         //Only get the colour name
            Transform typeObject = i.Find("HoloTag(Clone)/DescriptionWindow/TypeAnswer");   //Get damage type text object
            typeName = typeObject.GetComponent<TextMeshPro>().text;                         //Get damage type string

            //Find x,y,z coordinates of all coordinates within a marking
            foreach (Transform x in i) {
                //Only search for x,y,z values when the gameobject has a "Coordinate" tag
                if (x.tag == "Coordinate") {
                    coordinatesSaved.Add(x.transform.localPosition.x);
                    coordinatesSaved.Add(x.transform.localPosition.y);
                    coordinatesSaved.Add(x.transform.localPosition.z);
                }
            }

            //Add all found markings and information to the project string
            ProjectJson.Add("Marking_"+i.name, coordinatesSaved);
            ProjectJson.Add("Colour_"+i.name, colourName);
            ProjectJson.Add("Type_"+i.name, typeName);
        }
        
        //Save project string to json file
        string path = Application.persistentDataPath + "/NoseConeProject_"+projectName+".json";         //Set file path
        File.WriteAllText(path, ProjectJson.ToString());                                                //Create or over-write json file with project string
    }

    //Function to create a list of all markings
    public void List() {
        jsonString = File.ReadAllText(pathList);        //Read project json file and convert it to a string
        projectArray = jsonString.Split('"');           //Split project string into parts (split by " symbol)
        
        projectMarkingsList.Clear();                    //Start with an empty markings list
        
        //Get the marking names of the project string (name example: Marking_1)
        int x = -1;
        for (int i = 9; i < projectArray.Length; i++) {
            x = x + 1;
            if ( ((x % 10) == 0) && (x <= projectArray.Length - 9) ) {
                projectMarkingsList.Add(projectArray[i].ToString());
            }
        }

        projectMarkingsList.Sort();                                 //Sorteer markings list
        dropdown = dropdownList.GetComponent<TMP_Dropdown>();       //Find dropdown component
        dropdown.ClearOptions();                                    //Clear dropdown
        dropdown.AddOptions(startList);                             //Set first value of dropdown list
        dropdown.AddOptions(projectMarkingsList);                   //Insert markings into the dropdown list
    }
    
    //Function to delete a marking
    public void ListDelete() {
        dropdown = dropdownList.GetComponent<TMP_Dropdown>();                           //Find dropdown component
        selectMarking = dropdownListValue.GetComponent<TextMeshProUGUI>().text;         //Get marking selection from dropdown list
        
        //Remove selected marking from json file
        for (int i = 0; i < projectArray.Length; i++) {
            if (projectArray[i].ToString() != selectMarking.ToString()) {
                listString = listString + (projectArray[i].ToString()) + '"';
            }
            else { i = i + 9; }
        }
        saveString = listString.Substring(0, listString.Length - 2) + "}";
        File.WriteAllText(path, saveString);
        
        //Send listBool to InspectionButtons script
        bool listBool = false;
        InspectionButtons inspectionButtons = GameObject.Find("InspectionManager").GetComponent<InspectionButtons>();
        inspectionButtons.GetListBool(listBool);

        //The Inspection Mode is closed after deletion, so destroy the spawned 3D model within the ImageTarget gameobject
        GameObject modelParent = GameObject.Find("ImageTarget");
        foreach (Transform i in modelParent.transform) {
            Destroy(i.gameObject);
        }
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////////
    //Functions to receive variables from other scripts

    //Receive markingTag variable
    public void GetMarkingTag(string markingTag) {
        this.markingTag = markingTag;
    }

    //Receive colourChoice variable
    public void GetColour(string colourChoice) {
        this.colourChoice = colourChoice;
    }
}
