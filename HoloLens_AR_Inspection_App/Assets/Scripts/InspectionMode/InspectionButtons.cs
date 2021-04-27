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
        for (int i = 0; i < inspectionButtons.Length; i++) {        //Set all inspection buttons INactive
            inspectionButtons[i].SetActive(false);
        }
        positioningButtons.SetActive(false);
    }
    
    public void Update()
    {
        inspectionMenuTitle.SetActive(startBool);
        changesWindow.SetActive(changesBool);
        projectWindow.SetActive(projectBool);
        listWindow.SetActive(listBool);
        if (selectBool == true) {
            projectButton.SetActive(false);
        }
    }

    public void ButtonChanges() {
        spawnComponent = GameObject.Find("ImageTarget/"+modelName+"(Clone)/default");     //Find GameObject within parent with script
        spawnScript = spawnComponent.GetComponent<CoordinatesInput>();                                //Assign script variable
        
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
        selectBool = true;                                          //Get button specific bool when pressed
        for (int i = 0; i < inspectionButtons.Length; i++) {        //Set all inspection buttons active
            inspectionButtons[i].SetActive(true);
        }
        positioningButtons.SetActive(true);
        startBool = true;
        projectBool = false;
    }

    public void ButtonList() {
        listBool = true;
        startBool = false;
        for (int i = 0; i < inspectionButtons.Length; i++) {        //Set all inspection buttons INactive
            inspectionButtons[i].SetActive(false);
        }
    }
    public void ListBack() {
        listBool = false;
        startBool = true;
        for (int i = 0; i < inspectionButtons.Length; i++) {        //Set all inspection buttons INactive
            inspectionButtons[i].SetActive(true);
        }
    }

    public void ButtonBack() {
        SceneLoader.Load(SceneLoader.Scene.StartScreen);
    }

    public void ButtonBackToStart() {                   //Back button for hololens 2 configuration
        GameObject startScene = GameObject.FindWithTag("Start");
        foreach (Transform i in startScene.transform) {
            i.gameObject.SetActive(true);
        }
        GameObject inspectionScene = GameObject.FindWithTag("Inspection");
        Destroy(inspectionScene);

        GameObject imageTarget = GameObject.Find("ImageTarget");
        foreach (Transform i in imageTarget.transform) {
            Destroy(i.gameObject);
        }
    }

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

    public void GetModelName(string modelName) {
        this.modelName = modelName;
    }
    
    public void GetListBool(bool listBool) {
        this.listBool = listBool;
    }
   
}
