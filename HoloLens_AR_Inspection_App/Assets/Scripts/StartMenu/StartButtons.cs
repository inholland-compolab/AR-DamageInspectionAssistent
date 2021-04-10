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

    public void Update()
    {
        startMenuTitle.SetActive(startBool);
        infoWindow.SetActive(infoBool);
    }

    public void ButtonRegister() {
        SceneLoader.Load(SceneLoader.Scene.RegisterMode);
    }

    public void ButtonInspection() {
        SceneLoader.Load(SceneLoader.Scene.InspectionMode);
    }

    public void ButtonView() {
        SceneLoader.Load(SceneLoader.Scene.ViewMode);
    }

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

    public void ButtonStart() {
        SceneLoader.Load(SceneLoader.Scene.StartScreen);
    }
}
