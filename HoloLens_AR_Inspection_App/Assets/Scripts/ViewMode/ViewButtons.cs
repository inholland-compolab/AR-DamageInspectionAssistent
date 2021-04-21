using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewButtons : MonoBehaviour
{
    [SerializeField] GameObject inspectionMenuTitle;
    [SerializeField] GameObject projectWindow;

    [SerializeField] public GameObject projectButton;
    
    public bool startBool = true;
    public bool changesBool = false;
    public bool projectBool = false;
    bool selectBool = false;

    GameObject spawnComponent;
    CoordinatesInput spawnScript;

    string loadProjectName;
    
    public void Update()
    {
        Debug.Log("project: "+projectBool);
        Debug.Log("select: "+selectBool);
        inspectionMenuTitle.SetActive(startBool);
        projectWindow.SetActive(projectBool);
        if (selectBool == true) {
            projectButton.SetActive(false);
        }
    }

    public void ButtonProject() {
        if (startBool == true && projectBool == false) {
            startBool = false;
            projectBool = true;
            return;
        }
        if (startBool == false && projectBool == true) {
            startBool = true;
            projectBool = false;
            return;
        }
        else {
            startBool = true;
            projectBool = false;
        }
    }

    public void ButtonSelect() {
        selectBool = true;                  //Get button specific bool when pressed
        startBool = true;
        projectBool = false;
    }

    public void ButtonBack() {
        SceneLoader.Load(SceneLoader.Scene.StartScreen);
    }
}
