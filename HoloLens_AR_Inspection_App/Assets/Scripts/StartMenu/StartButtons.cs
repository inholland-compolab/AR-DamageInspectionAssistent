using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButtons : MonoBehaviour
{
    [SerializeField]
    GameObject startMenuTitle;
    [SerializeField]
    GameObject infoWindow;

    public bool startBool = true;
    public bool infoBool = false;

    //Check each frame for activation or deactivation of different windows
    public void Update()
    {
        startMenuTitle.SetActive(startBool);
        infoWindow.SetActive(infoBool);
    }

    //Function to determine if the information window is open or not
    public void ButtonInfo() {
        if (startBool == true && infoBool == false) {
            startBool = false;
            infoBool = true;
            return;
        }
        if (startBool == false && infoBool == true) {
            startBool = true;
            infoBool = false;
            return;
        }
        else {
            startBool = true;
            infoBool = false;
        }
    }

    //Functions to open scenes (Scenes were used for HoloLens 1 version)
    public void ButtonRegister() {
        SceneLoader.Load(SceneLoader.Scene.RegisterMode);
    }
    public void ButtonInspection() {
        SceneLoader.Load(SceneLoader.Scene.InspectionMode);
    }
    public void ButtonView() {
        SceneLoader.Load(SceneLoader.Scene.ViewMode);
    }
    public void ButtonStart() {
        SceneLoader.Load(SceneLoader.Scene.StartScreen);
    }
}
