using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class InspectionButtons : MonoBehaviour
{
    [SerializeField] GameObject inspectionMenuTitle;
    [SerializeField] GameObject changesWindow;
    [SerializeField] GameObject projectWindow;

    [SerializeField] public GameObject[] inspectionButtons;
    [SerializeField] public GameObject[] nonChangesButtons;
    
    public bool startBool = true;
    public bool changesBool = false;
    public bool projectBool = false;

    GameObject spawnComponent;
    CoordinatesInput spawnScript;
    LoadSaveManager loadSaveManager;

    string loadProjectName;

    public void Start()
    {
        for (int i = 0; i < inspectionButtons.Length; i++) {        //Set all inspection buttons INactive
            inspectionButtons[i].SetActive(false);
        }
    }
    
    public void Update()
    {
        inspectionMenuTitle.SetActive(startBool);
        changesWindow.SetActive(changesBool);
        projectWindow.SetActive(projectBool);
    }

    public void ButtonChanges() {
        spawnComponent = GameObject.Find("ImageTarget/RevEng_NoseCone_Fokker100(Clone)/default");     //Find GameObject within parent with script
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
        for (int i = 0; i < inspectionButtons.Length; i++) {        //Set all inspection buttons active
            inspectionButtons[i].SetActive(true);
        }
        startBool = true;
        projectBool = false;
    }

    public void ButtonList() {}                                  //Show Damages in List (Number, Type)

    public void ButtonBack() {
        SceneLoader.Load(SceneLoader.Scene.StartScreen);
    }
   
}
