using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;

public class DeleteProject : MonoBehaviour
{
    [SerializeField] GameObject deleteMessageWindow;
    bool messageBool = false;
    [SerializeField] GameObject inputName;      //Assign input field text
    string projectName;                         //Assign variable

    public void Update() 
    {
        deleteMessageWindow.SetActive(messageBool);
    }
    public void DeleteMessage() {
        messageBool = true;
    }

    public void noDelete() {
        messageBool = false;
    }

    public void DeleteFile() {
        projectName = inputName.GetComponent<TextMeshProUGUI>().text;           //Set project name from input field

        string path = Application.persistentDataPath + "/NoseConeProject_"+projectName+".json";         //Set file path
        File.Delete(path);
        messageBool = false;
    }
}
