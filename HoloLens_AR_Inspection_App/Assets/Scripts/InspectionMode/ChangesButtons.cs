using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChangesButtons : MonoBehaviour
{
    [SerializeField] GameObject changesMenu;
    [SerializeField] GameObject addWindow;

    GameObject[] coordinatesList;

    string markingTag;

    public void Awake() 
    {
        addWindow.SetActive(false);
    }

    public void AddButton() {
        addWindow.SetActive(true);      //Let user select damage number and colour
        //Allow Tap to spawn coordinates
    }

    public void UndoButton() {
        coordinatesList = GameObject.FindGameObjectsWithTag("Coordinate");
        GameObject.Destroy(coordinatesList[coordinatesList.Length-1]);
    }

    public void SaveButton() {
        //Save number in Json object
        //Create gameobject number as child of "Markings"
        //Save coulour in Json Object
        //Move coordinates to "*number*" as childs
        //Save coordinates from "*number*" gameobject in json array
    }

    


    /////////////////////////////////////Add Window////////////////////////////////////
    public void Select() {
        addWindow.SetActive(false);
    }
    
}
