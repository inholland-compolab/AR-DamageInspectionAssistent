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
        inspectionMenuTitle.SetActive(startBool);
        projectWindow.SetActive(projectBool);
        if (selectBool == true) {
            projectButton.SetActive(false);
        }
        else { projectButton.SetActive(true); }
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
        //Reset buttons to initial state
        startBool = true;
        projectBool = false;
        selectBool = false;
        
        //SceneManagement needed?
        //SceneLoader.Load(SceneLoader.Scene.StartScreen);
    }

    public void ButtonBackToStart() {                   //Back button for hololens 2 configuration
        GameObject startScene = GameObject.FindWithTag("Start");
        foreach (Transform i in startScene.transform) {
            i.gameObject.SetActive(true);
        }
        GameObject viewScene = GameObject.FindWithTag("View");
        Destroy(viewScene);

        GameObject imageTarget = GameObject.Find("ImageTarget");
        foreach (Transform i in imageTarget.transform) {
            Destroy(i.gameObject);
        }
    }
}
