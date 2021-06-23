using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChangesButtons : MonoBehaviour
{
    [SerializeField] GameObject addWindow;
    [SerializeField] GameObject changesWindow;

    GameObject[] coordinatesList;

    public void Awake() 
    {
        addWindow.SetActive(false);     //Don't show the Add window
    }

    //Activate Add window
    public void AddButton() {
        addWindow.SetActive(true);      //Show Add window and let user select damage number, type and colour
    }

    //Delete last placed coordinate
    public void UndoButton() {
        coordinatesList = GameObject.FindGameObjectsWithTag("Coordinate");  //Find coordinates
        GameObject.Destroy(coordinatesList[coordinatesList.Length-1]);      //Delete last coordinate
    }


    /////////////////////////////////////Button in Add Window////////////////////////////////////
    //When the number, type and color are selected, the Add window can close after confirmation
    
    //Disable Add window
    public void Select() {
        addWindow.SetActive(false);
    }
    
}
