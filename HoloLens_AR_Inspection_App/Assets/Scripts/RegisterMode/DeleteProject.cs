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
        //Check each frame if delete message needs to be active or not
        deleteMessageWindow.SetActive(messageBool);
    }

    //Function to set delete message bool to true when activated
    public void DeleteMessage() {
        messageBool = true;
    }

    //Function to set delete message bool back to false when the deletion is cancelled
    public void noDelete() {
        messageBool = false;
    }

    //Delete a project by typing the exact name in the projectname input field and select the delete button
    //Function to delete a project from the HoloLens
    public void DeleteFile() {
        //Get project name from input field
        projectName = inputName.GetComponent<TextMeshProUGUI>().text;

        //Set file path
        string path = Application.persistentDataPath + "/NoseConeProject_"+projectName+".json";
        //Delete file
        File.Delete(path);
        //Deactivate delete message
        messageBool = false;
    }
}
