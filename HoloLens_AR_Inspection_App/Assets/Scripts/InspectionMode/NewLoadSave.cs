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

    string path;
    string jsonString;
    JSONObject loadJson;
    string nameLoad;
    string modelLoad;
    string coordinatesLoad;
    string materialLoad;

    string projectFileName;
    public string projectName;

    public string markingTag;
    public string colourChoice;
    string[] colour;
    string colourName;

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

    Material material;
    Material selectedMaterial;

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

    public void SetTimer() {
        timer = 3.0f;
    }

    public void Load() {
        //Get selected projectName
        projectFileName = dropdownValue.GetComponent<TextMeshProUGUI>().text;                       //Get value from dropdown
        projectName = projectFileName.Substring(0, projectFileName.Length - 5);                     //Remove .json from value to get project name
        path = Application.persistentDataPath + "/NoseConeProject_"+projectName+".json";     //Define project json file path
        try {
            File.ReadAllText(path);                                                                 //Try path
        }
        catch (FileNotFoundException) {
            SetTimer();                                                                             //Set timer to 3 seconds
            noPathPopUp.SetActive(true);                                                            //Show pop up to inform user about wrong file path
        }
        string jsonString = File.ReadAllText(path);                                                 //Read whole json file and convert to string
        JSONObject loadJson = (JSONObject)JSON.Parse(jsonString);                                   //Retrieve json object

        nameLoad = loadJson["Project"];                                                             //Get name from name object
        modelLoad = loadJson["Model"];                                                              //Get model name from model object

        //Spawn Model
        var prefab = Resources.Load("3DModels/"+modelLoad);                 //Load nose cone prefab corresponding to the model name
        selectedPrefab = prefab as GameObject;                              //Set prefab variable
        Instantiate(selectedPrefab, modelParent.transform);                 //Spawn prefab (wanted nose cone) as child of Target Image

        spawnedModel = GameObject.Find(modelLoad+"(Clone)");

        for (int i = 0; i < loops; i++) {
            preLoad = loadJson["Marking_"+i].ToString();
            arrayLoad = preLoad.Split(';');
            parentPrefab = GameObject.Find("ImageTarget/"+modelLoad+"(Clone)/Markings");    //"ImageTarget/RevEng_NoseCone_Fokker100(Clone)/Markings"

            materialLoad = loadJson["Colour_"+i].ToString();
            materialLoad = materialLoad.Substring(1, materialLoad.Length - 2);
            MaterialSelection();
            
            marking = Instantiate(markingPrefab);
            marking.transform.SetParent(parentPrefab.transform, false);
            marking.name = (i.ToString());
            marking.GetComponent<Renderer>().material = selectedMaterial;
            
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

    public void MaterialSelection() {
        var material = Resources.Load("Colours/"+materialLoad);
        selectedMaterial = material as Material; 
    }
    
    public void Save() {
        string savePath = Application.persistentDataPath + "/NoseConeProject_"+projectName+".json";     //Define project json file path
        string jsonString = File.ReadAllText(savePath);                                                 //Read whole json file and convert to string
        JSONObject loadJson = (JSONObject)JSON.Parse(jsonString);

        GameObject markingsGroup = GameObject.Find("ImageTarget/"+modelLoad+"(Clone)/Markings");    //"ImageTarget/RevEng_NoseCone_Fokker100(Clone)/Markings"
        foreach (Transform i in markingsGroup.transform) {
            JSONArray coordinatesSaved = new JSONArray();
            colour = i.GetComponent<Renderer>().material.name.ToString().Split(' ');
            colourName = colour[0];
            foreach (Transform x in i) {
                if (x.tag == "Coordinate") {
                    coordinatesSaved.Add(x.transform.localPosition.x);
                    coordinatesSaved.Add(x.transform.localPosition.y);
                    coordinatesSaved.Add(x.transform.localPosition.z);
                }
            }
            loadJson.Add("Marking_"+i.name, coordinatesSaved);
            loadJson.Add("Colour_"+i.name, colourName);
        }
        
        string path = Application.persistentDataPath + "/NoseConeProject_"+projectName+".json";         //Set file path
        File.WriteAllText(path, loadJson.ToString());
    }

    public void List() {
        jsonString = File.ReadAllText(pathList);
        projectArray = jsonString.Split('"');
        Debug.Log(projectArray.Length);
        projectMarkingsList.Clear();
        int x = -1;
        for (int i = 9; i < projectArray.Length; i++) {
            x = x + 1;
            if ( ((x % 6) == 0) && (x <= projectArray.Length - 9) ) {
                projectMarkingsList.Add(projectArray[i].ToString());
            }
        }
        dropdown = dropdownList.GetComponent<TMP_Dropdown>();
        dropdown.ClearOptions();
        dropdown.AddOptions(startList);
        dropdown.AddOptions(projectMarkingsList);
    }
    
    public void ListDelete() {
        dropdown = dropdownList.GetComponent<TMP_Dropdown>();
        selectMarking = dropdownListValue.GetComponent<TextMeshProUGUI>().text;
        for (int i = 0; i < projectArray.Length; i++) {
            if (projectArray[i].ToString() != selectMarking.ToString()) {
                listString = listString + (projectArray[i].ToString()) + '"';
            }
            else { 
                if ( (projectArray.Length - i) < 9 ) {
                    i = i + 5;
                    Debug.Log("close to limit");
                }
                else { i = i + 5; 
                    Debug.Log("not close to limit"); }
            }
        }
        saveString = listString.Substring(0, listString.Length - 2) + "}";
        File.WriteAllText(path, saveString);
        
        bool listBool = false;
        InspectionButtons inspectionButtons = GameObject.Find("InspectionManager").GetComponent<InspectionButtons>();
        inspectionButtons.GetListBool(listBool);

        GameObject modelParent = GameObject.Find("ImageTarget");
        foreach (Transform i in modelParent.transform) {
            Destroy(i.gameObject);
        }
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////////
    public void GetMarkingTag(string markingTag) {
        this.markingTag = markingTag;
    }

    public void GetColour(string colourChoice) {
        this.colourChoice = colourChoice;
    }
}
