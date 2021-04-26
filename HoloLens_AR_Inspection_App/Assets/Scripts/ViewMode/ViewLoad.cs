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

    int loops = 100;
    float timer = 3.0f;
    

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
        }
    }

    public void SetTimer() {
        timer = 3.0f;
    }

    public void Load() {
        //Get selected projectName
        projectFileName = dropdownValue.GetComponent<TextMeshProUGUI>().text;                       //Get value from dropdown
        projectName = projectFileName.Substring(0, projectFileName.Length - 5);                     //Remove .json from value to get project name
        path = Application.persistentDataPath + "/NoseConeProject_"+projectName+".json";            //Define project json file path
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
        modelParent = GameObject.Find("ImageTarget");
        Instantiate(selectedPrefab, modelParent.transform);                 //Spawn prefab (wanted nose cone) as child of Target Image

        spawnedModel = GameObject.Find(modelLoad+"(Clone)");

        for (int i = 0; i < loops; i++) {
            preLoad = loadJson["Marking_"+i].ToString();
            arrayLoad = preLoad.Split(';');
            parentPrefab = GameObject.Find("ImageTarget/"+modelLoad+"(Clone)/Markings");    //"ImageTarget/RevEng_NoseCone_Fokker100(Clone)/Markings"

            materialLoad = loadJson["Colour_"+i].ToString();
            materialLoad = materialLoad.Substring(1, materialLoad.Length - 2);
            MaterialSelection();

            typeLoad = loadJson["Type_"+i].ToString();
            
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
            
            tagObject = Instantiate(tagPrefab);
            tagObject.transform.SetParent(i.transform, false);
        }

        for (int i = 0; i < loops; i++) {
            typeLoad = loadJson["Type_"+i].ToString();
            typeName = typeLoad.Substring(1, typeLoad.Length - 2);
            GameObject typeObject = GameObject.Find("ImageTarget/"+modelLoad+"(Clone)/Markings/"+i+"/HoloTag(Clone)/DescriptionWindow/TypeAnswer");
            typeObject.GetComponent<TextMeshPro>().text = typeName;
        }
    }

    public void MaterialSelection() {
        var material = Resources.Load("Colours/"+materialLoad);
        selectedMaterial = material as Material; 
    }
}
