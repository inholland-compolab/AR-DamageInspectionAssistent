using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using SimpleJSON;
using TMPro;

public class SaveProject : MonoBehaviour
{    
    [SerializeField] GameObject inputName;      //Assign input field text
    string projectName;                         //Assign variable

    [SerializeField] GameObject inputModel;      //Assign dropdown text
    string projectModel;                         //Assign variable



    public void Save() {
        JSONObject ProjectNameJson = new JSONObject();                          //Create name object in JSON File
        projectName = inputName.GetComponent<TextMeshProUGUI>().text;           //Set project name from input field
        ProjectNameJson.Add("Project", projectName);                            //Add project name

        JSONObject ProjectModelJson = new JSONObject();                         //Create model object in JSON File
        projectModel = inputModel.GetComponent<TextMeshProUGUI>().text;         //Set project model from dropdown
        ProjectModelJson.Add("Model", projectModel);                            //Add project model

        string path = Application.persistentDataPath + "/NoseConeProject_"+projectName+".json";         //Set file path
        File.WriteAllText(path, ProjectNameJson.ToString()+                                             //Create text file / Overwrite previous text file
        ProjectModelJson.ToString());
    }

    public void ButtonBackToStart() {                                                //Button to go back to startscene for hololens 2 configuration
        GameObject startScene = GameObject.FindWithTag("Start");
        foreach (Transform i in startScene.transform) {
            i.gameObject.SetActive(true);
        }
        GameObject registerScene = GameObject.FindWithTag("Register");
        Destroy(registerScene);
        }
}
