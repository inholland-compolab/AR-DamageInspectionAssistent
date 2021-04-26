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
        addWindow.SetActive(false);
    }

    public void AddButton() {
        addWindow.SetActive(true);      //Let user select damage number and colour
    }

    public void UndoButton() {
        coordinatesList = GameObject.FindGameObjectsWithTag("Coordinate");
        GameObject.Destroy(coordinatesList[coordinatesList.Length-1]);
    }

    /////////////////////////////////////Add Window////////////////////////////////////
    public void Select() {
        addWindow.SetActive(false);
    }
    
}
