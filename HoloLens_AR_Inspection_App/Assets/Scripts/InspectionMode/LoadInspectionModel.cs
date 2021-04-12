using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using SimpleJSON;
using TMPro;

public class LoadInspectionModel : MonoBehaviour
{
    [SerializeField] GameObject modelParent;       //Assign Image Tracker as parent of model
    Object selectedPrefab;

    [SerializeField] GameObject inputProject;      //Assign dropdown
    [SerializeField] GameObject dropdownValue;     //Assign dropdown label
    TMP_Dropdown dropdown;                         //Assign variable

    public List<string> startList = new List<string>();
    public List<string> projectList = new List<string>();

    string nameLoad;
    string modelLoad;

    string projectFileName;
    string projectName;


    public void Awake()
    {
        DirectoryInfo dir = new DirectoryInfo(Application.persistentDataPath);
        FileInfo[] fileArray = dir.GetFiles( "*.json*" );
        
        for (int i = 0; i < fileArray.Length; i++) {          //Get all names of the prefabs in Resources folder
            string[] nameOnly = fileArray[i].ToString().Split('_');
            projectList.Add(nameOnly[nameOnly.Length - 1].ToString());
        }

        dropdown = inputProject.GetComponent<TMP_Dropdown>();
        dropdown.ClearOptions();
        startList.Add("Choose");                                    //Instruction at first value of the list
        dropdown.AddOptions(startList);                             //Set first value of dropdown
        dropdown.AddOptions(projectList);                           //Set other values of dropdown
    }


    public void Load() { 
        //Get selected projectName
        projectFileName = dropdownValue.GetComponent<TextMeshProUGUI>().text;
        projectName = projectFileName.Substring(0, projectFileName.Length - 5);
        Debug.Log(projectName);
        string path = Application.persistentDataPath + "/NoseConeProject_"+projectName+".json";
        string jsonString = File.ReadAllText(path);
        JSONObject loadJson = (JSONObject)JSON.Parse(jsonString);

        //Set values
        nameLoad = loadJson["Name"];
        modelLoad = loadJson["Model"];

        //Spawn Model
        var prefab = Resources.Load("3DModels/"+modelLoad);
        selectedPrefab = prefab as GameObject;
        Instantiate(selectedPrefab, modelParent.transform);
    }

}
