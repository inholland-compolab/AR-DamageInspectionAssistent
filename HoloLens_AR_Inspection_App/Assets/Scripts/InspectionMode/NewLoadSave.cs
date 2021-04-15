using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using SimpleJSON;
using TMPro;

public class NewLoadSave : MonoBehaviour
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

    string jsonString;
    JSONObject loadJson;
    string nameLoad;
    string modelLoad;
    string markingLoad;
    string coordinatesLoad;

    string projectFileName;
    public string projectName;

    public string markingTag;
    public string colourChoice;

    string[] markingLoads;
    string preLoad;
    string[] arrayLoad;
    Vector3 spawnLoad;
    GameObject spawnedModel;

    GameObject[] coordinatesList;
    GameObject marking;
    GameObject parentPrefab;
    [SerializeField] GameObject markingPrefab;
    [SerializeField] GameObject lineRendererPrefab;
    GameObject lineRenderer;

    int loops = 100;
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

    public void SetTimer() {
        timer = 3.0f;
    }

    public void Load() {
        //Get selected projectName
        projectFileName = dropdownValue.GetComponent<TextMeshProUGUI>().text;                       //Get value from dropdown
        projectName = projectFileName.Substring(0, projectFileName.Length - 5);                     //Remove .json from value to get project name
        //Debug.Log(projectName);
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

        nameLoad = loadJson["Project"];                                                                //Get name from name object
        modelLoad = loadJson["Model"];                                                              //Get model name from model object
        markingLoad = loadJson["Marking_10"].ToString();

        //Spawn Model
        var prefab = Resources.Load("3DModels/"+modelLoad);                 //Load nose cone prefab corresponding to the model name
        selectedPrefab = prefab as GameObject;                              //Set prefab variable
        Instantiate(selectedPrefab, modelParent.transform);                 //Spawn prefab (wanted nose cone) as child of Target Image

        spawnedModel = GameObject.Find(modelLoad+"(Clone)");

        for (int i = 0; i < loops; i++) {
            preLoad = loadJson["Marking_"+i].ToString();
            arrayLoad = preLoad.Split(';');
            parentPrefab = GameObject.Find("ImageTarget/RevEng_NoseCone_Fokker100(Clone)/Markings");
            
            marking = Instantiate(markingPrefab);
            marking.transform.SetParent(parentPrefab.transform, false);
            marking.name = (i.ToString());
            
            for (int x = 0; x < arrayLoad.Length; x++){
                //Debug.Log("Marking_"+i+", array: "+arrayLoad[x]);
                // if (arrayLoad[x] != null) {
                    if ( ((x % 3) == 0) && (x <= arrayLoad.Length-2) ) {
                        spawnLoad.x = float.Parse(arrayLoad[x].Trim('['));
                        spawnLoad.y = float.Parse(arrayLoad[(x+1)]);
                        spawnLoad.z = float.Parse(arrayLoad[(x+2)].Trim(']'));
                        
                        //Spawn coordinate, set parent with relative offset, set local position
                        spawnedCoordinate = Instantiate(coordinatePrefab);
                        spawnedCoordinate.transform.SetParent(marking.transform, false);
                        spawnedCoordinate.transform.localPosition = spawnLoad;
                    }
                // }
                // else {
                //     Destroy(marking.transform);
                // }
            }
        }

        foreach (Transform i in parentPrefab.transform) {
            if (i.childCount < 3) {
                Destroy(i.gameObject);
            }
        }

        foreach (Transform i in parentPrefab.transform) {
            marking = Instantiate(lineRendererPrefab);
            marking.transform.SetParent(i.transform, false);
        }

    }
    
    public void Save() {
        string savePath = Application.persistentDataPath + "/NoseConeProject_"+projectName+".json";     //Define project json file path
        string jsonString = File.ReadAllText(savePath);                                                 //Read whole json file and convert to string
        JSONObject loadJson = (JSONObject)JSON.Parse(jsonString);

        GameObject markingsGroup = GameObject.Find("ImageTarget/RevEng_NoseCone_Fokker100(Clone)/Markings");
        foreach (Transform i in markingsGroup.transform) {
            JSONArray coordinatesSaved = new JSONArray();
            foreach (Transform x in i) {
                if (x.tag == "Coordinate") {
                    coordinatesSaved.Add(x.transform.localPosition.x);
                    coordinatesSaved.Add(x.transform.localPosition.y);
                    coordinatesSaved.Add(x.transform.localPosition.z);
                }
            }
            loadJson.Add("Marking_"+i.name, coordinatesSaved);
        }
        
        string path = Application.persistentDataPath + "/NoseConeProject_"+projectName+".json";         //Set file path
        File.WriteAllText(path, loadJson.ToString());
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////////
    public void GetMarkingTag(string markingTag) {
        this.markingTag = markingTag;
    }

    public void GetColour(string colourChoice) {
        this.colourChoice = colourChoice;
    }
}
