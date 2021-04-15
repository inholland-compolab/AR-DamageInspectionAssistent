using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using SimpleJSON;
using TMPro;

public class LoadSaveManager : MonoBehaviour
{
    [SerializeField] GameObject modelParent;       //Assign Image Tracker as parent of model
    Object selectedPrefab;

    [SerializeField] GameObject coordinatePrefab;
    GameObject spawnedCoordinate;

    [SerializeField] GameObject noPathPopUp;       //Assign no valid path pop up

    [SerializeField] GameObject inputProject;      //Assign dropdown
    [SerializeField] GameObject dropdownValue;     //Assign dropdown label
    TMP_Dropdown dropdown;                         //Assign variable

    public List<string> startList = new List<string>();
    public List<string> projectList = new List<string>();

    string nameLoad;
    string modelLoad;
    string markingLoad;
    string coordinatesLoad;

    string projectFileName;
    public string projectName;

    public string markingTag;

    string[] preArrayLoad;
    string[] arrayLoad;
    Vector3 spawnLoad;
    GameObject spawnedModel;

    float timer = 3.0f;

    InspectionButtons inspectionButtons;


    public void Awake()
    {
        DirectoryInfo dir = new DirectoryInfo(Application.persistentDataPath);
        FileInfo[] fileArray = dir.GetFiles( "*.json*" );
        
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

    public void Update() 
    {
        //Timer to let popup wait between active and inactive
        if (timer >= 0) {
            timer = timer - Time.deltaTime;
        }
        if (timer < 0) {
            timer = 3;
            noPathPopUp.SetActive(false);
            //inspectionButtons.PopUpToggled(false);
        }
    }

    public void Load() { 
        //Get selected projectName
        projectFileName = dropdownValue.GetComponent<TextMeshProUGUI>().text;                       //Get value from dropdown
        projectName = projectFileName.Substring(0, projectFileName.Length - 5);                     //Remove .json from value to get project name
        Debug.Log(projectName);
        string path = Application.persistentDataPath + "/NoseConeProject_"+projectName+".json";     //Define project json file path
        try {
            File.ReadAllText(path);                                                                 //Try path
        }
        catch (FileNotFoundException) {
            SetTimer();                                                                     //Set timer to 3 seconds
            noPathPopUp.SetActive(true);                                                            //Show pop up to inform user about wrong file path
        }
        string jsonString = File.ReadAllText(path);                                                 //Read whole json file and convert to string
        JSONObject loadJson = (JSONObject)JSON.Parse(jsonString);                                   //Retrieve json object

        //Set values
        nameLoad = loadJson["Project"];                                                                //Get name from name object
        modelLoad = loadJson["Model"];                                                              //Get model name from model object
        //markingLoad = loadJson["Marking"];
        //coordinatesLoad = loadJson["Coordiantes"];
        // Debug.Log(jsonString);
        // Debug.Log("project:"+nameLoad);
        // Debug.Log("model:"+modelLoad);
        // Debug.Log("marking:"+markingLoad);


        //Spawn Model
        var prefab = Resources.Load("3DModels/"+modelLoad);                 //Load nose cone prefab corresponding to the model name
        selectedPrefab = prefab as GameObject;                              //Set prefab variable
        Instantiate(selectedPrefab, modelParent.transform);                 //Spawn prefab (wanted nose cone) as child of Target Image

        //Search for spawned model gameobject
        spawnedModel = GameObject.Find(modelLoad+"(Clone)");

        //Spawn Coordinates
        // preArrayLoad = jsonString.Split('[',']');   //Select part from [ to ]
        // arrayLoad = preArrayLoad[1].ToString().Split(';');  //Split string into array
        // for (int i = 0; i <= arrayLoad.Length; i++) {
        //     if ( ((i % 3) == 0) && (i <= (arrayLoad.Length-2)) ) {
        //         spawnLoad.x = float.Parse(arrayLoad[i]);
        //         spawnLoad.y = float.Parse(arrayLoad[(i+1)]);
        //         spawnLoad.z = float.Parse(arrayLoad[(i+2)]);
        //         //Spawn coordinate, set parent with relative offset, set local position
        //         spawnedCoordinate = Instantiate(coordinatePrefab);
        //         spawnedCoordinate.transform.SetParent(spawnedModel.transform, false);
        //         spawnedCoordinate.transform.localPosition = spawnLoad;
        //     }
        // }
    }

    public void SetTimer() {
        timer = 3.0f;
    }

    public void Save() {
        JSONObject ProjectJson = new JSONObject();                          //Create name object in JSON File
        ProjectJson.Add("Project", projectName);                            //Add project name (why not nameLoad???)

        ProjectJson.Add("Model", modelLoad);                               //Add project model

        ProjectJson.Add("Marking", markingTag);                               //Add project model

        JSONArray coordinatesSaved = new JSONArray();
        GameObject[] coordinatesList = GameObject.FindGameObjectsWithTag("Coordinate");
        for (int i = 0; i < coordinatesList.Length; i++) {
            coordinatesSaved.Add(coordinatesList[i].transform.localPosition.x);
            coordinatesSaved.Add(coordinatesList[i].transform.localPosition.y);
            coordinatesSaved.Add(coordinatesList[i].transform.localPosition.z);
        }
        ProjectJson.Add("Coordinates", coordinatesSaved);
        
        string path = Application.persistentDataPath + "/NoseConeProject_"+projectName+".json";         //Set file path
        File.WriteAllText(path, ProjectJson.ToString());
    }

    public void GetMarkingTag(string markingTag) {
        this.markingTag = markingTag;
    }

}
