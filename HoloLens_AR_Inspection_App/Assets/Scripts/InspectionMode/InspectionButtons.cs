using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InspectionButtons : MonoBehaviour
{
    [SerializeField]
    GameObject inspectionMenuTitle;
    [SerializeField]
    GameObject changesWindow;
    [SerializeField]
    GameObject projectWindow;
    
    public bool startBool = true;
    public bool changesBool = false;
    public bool projectBool = false;

    public void Update()
    {
        inspectionMenuTitle.SetActive(startBool);
        changesWindow.SetActive(changesBool);
        projectWindow.SetActive(projectBool);
    }

    public void ButtonChanges() {
        if (startBool == true && changesBool == false) {
            startBool = false;
            changesBool = true;
            return;
        }
        if (startBool == false && changesBool == true) {
            startBool = true;
            changesBool = false;
            return;
        }
        else {
            startBool = true;
            changesBool = false;
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
        startBool = true;
        projectBool = false;
    }

    public void ButtonList() {}                                  //Show Damages in List (Number, Type)

    public void ButtonBack() {
        SceneLoader.Load(SceneLoader.Scene.StartScreen);
    }
}
