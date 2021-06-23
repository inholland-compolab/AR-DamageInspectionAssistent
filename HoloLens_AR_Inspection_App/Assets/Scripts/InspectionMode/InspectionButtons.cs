using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class InspectionButtons : MonoBehaviour
{
    [SerializeField] GameObject inspectionMenuTitle;
    [SerializeField] GameObject changesWindow;
    [SerializeField] GameObject projectWindow;
    [SerializeField] GameObject listWindow;

    [SerializeField] public GameObject[] inspectionButtons;
    [SerializeField] public GameObject[] nonChangesButtons;
    [SerializeField] public GameObject projectButton;

    string modelName;
    
    public bool startBool = true;
    public bool changesBool = false;
    public bool projectBool = false;
    bool selectBool = false;
    bool listBool = false;

    GameObject spawnComponent;
    CoordinatesInput spawnScript;

    string loadProjectName;

    [SerializeField] GameObject positioningPrefab;
    bool positioningBool = false;

    [SerializeField] GameObject positioningButtons;

    public void Start()
    {
        //Set all inspection buttons INactive
        for (int i = 0; i < inspectionButtons.Length; i++) {
            inspectionButtons[i].SetActive(false);
        }
        positioningButtons.SetActive(false);
    }
    
    public void Update()
    {
        //Check each frame which windows are active / INactive
        inspectionMenuTitle.SetActive(startBool);
        changesWindow.SetActive(changesBool);
        projectWindow.SetActive(projectBool);
        listWindow.SetActive(listBool);
        
        //Disable project button when a project is selected
        if (selectBool == true) {
            projectButton.SetActive(false);
        }
    }

    public void ButtonChanges() {
        spawnComponent = GameObject.Find("ImageTarget/"+modelName+"(Clone)/default");     //Find GameObject within parent with script
        spawnScript = spawnComponent.GetComponent<CoordinatesInput>();                    //Assign nose cone script variable
        
        if (startBool == true && changesBool == false) {
            startBool = false;
            changesBool = true;

            for (int i = 0; i < nonChangesButtons.Length; i++) {        //Set all non changes buttons INactive
                nonChangesButtons[i].SetActive(false);
            }

            spawnScript.ChangesToggled(changesBool);                    //Send bool variable to nose cone script
            return;
        }
        if (startBool == false && changesBool == true) {
            startBool = true;
            changesBool = false;

            for (int i = 0; i < nonChangesButtons.Length; i++) {        //Set all non changes buttons Active
                nonChangesButtons[i].SetActive(true);
            }

            spawnScript.ChangesToggled(changesBool);
            return;
        }
        else {
            startBool = true;
            changesBool = false;

            for (int i = 0; i < nonChangesButtons.Length; i++) {
                nonChangesButtons[i].SetActive(true);
            }

            spawnScript.ChangesToggled(changesBool);                    
        }
    }

    //Function to determine if bool for project window is true or false
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

    //Function to enable/disable buttons after project is selected
    public void ButtonSelect() {
        selectBool = true;                                          //Get button specific bool when pressed
        for (int i = 0; i < inspectionButtons.Length; i++) {        //Set all inspection buttons active
            inspectionButtons[i].SetActive(true);
        }
        positioningButtons.SetActive(true);
        startBool = true;
        projectBool = false;
    }

    //Function to open window with list of markings
    public void ButtonList() {
        listBool = true;
        startBool = false;
        for (int i = 0; i < inspectionButtons.Length; i++) {        //Set all inspection buttons INactive
            inspectionButtons[i].SetActive(false);
        }
    }

    //Function to close list of markings
    public void ListBack() {
        listBool = false;
        startBool = true;
        for (int i = 0; i < inspectionButtons.Length; i++) {        //Set all inspection buttons active
            inspectionButtons[i].SetActive(true);
        }
    }

    //Leave inspection mode scene (Scenes were used for HoloLens 1 version)
    public void ButtonBack() {
        SceneLoader.Load(SceneLoader.Scene.StartScreen);
    }

    //Back button to leave inspection mode (for hololens 2 version)
    public void ButtonBackToStart() {
        //Show start menu
        GameObject startScene = GameObject.FindWithTag("Start");
        foreach (Transform i in startScene.transform) {
            i.gameObject.SetActive(true);
        }

        //Remove inspection menus
        GameObject inspectionScene = GameObject.FindWithTag("Inspection");
        Destroy(inspectionScene);


        //Remove 3D model and children from ImageTarget gameobject
        GameObject imageTarget = GameObject.Find("ImageTarget");
        foreach (Transform i in imageTarget.transform) {
            Destroy(i.gameObject);
        }
    }

    //Function to open/close positioning window to manually adjust the position and rotation of the 3D model
    public void OpenPositioning() {
        if (positioningBool == false) {
            Instantiate(positioningPrefab);
            positioningBool = true;
            return;
        }
        if (positioningBool == true) {
            Destroy(GameObject.Find(positioningPrefab.name+"(Clone)"));
            positioningBool = false;
            return;
        }
    }

    ////////////////////////////////////////////////////////////////
    //Functions to receive variables from other scripts

    //Receive modelName variable
    public void GetModelName(string modelName) {
        this.modelName = modelName;
    }
    
    //Receive listBool variable
    public void GetListBool(bool listBool) {
        this.listBool = listBool;
    }
   
}
