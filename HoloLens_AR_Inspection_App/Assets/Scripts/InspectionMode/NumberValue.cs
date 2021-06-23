using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NumberValue : MonoBehaviour
{
    [SerializeField] GameObject numberDisplay;
    string modelName;
    public string markingTag; //(marking number)
    string value;

    //Get the marking tag each frame
    public void Update() 
    {
        //Get the number from the display within the Add Window
        markingTag = numberDisplay.GetComponent<TextMeshPro>().text;

        //Send variables to other scripts
        HierarchyManager hierarchyManager = GameObject.Find("InspectionManager").GetComponent<HierarchyManager>();
        hierarchyManager.GetMarkingTag(markingTag);
        NewLoadSave newLoadSave = GameObject.Find("InspectionManager").GetComponent<NewLoadSave>();
        newLoadSave.GetMarkingTag(markingTag);
        CoordinatesInput coordinatesInput = GameObject.Find("ImageTarget/"+modelName+"(Clone)/default").GetComponent<CoordinatesInput>();
        coordinatesInput.GetMarkingTag(markingTag);
    }

    //Add number to display
    public void Number() {
        value = gameObject.GetComponent<TextMeshPro>().text;
        numberDisplay.GetComponent<TextMeshPro>().text = markingTag + value;
    }

    //Remove last number from display
    public void Remove() {
        numberDisplay.GetComponent<TextMeshPro>().text = markingTag.Substring(0, markingTag.Length - 1);
    }

    //Start with a new empty display
    public void New() {
        numberDisplay.GetComponent<TextMeshPro>().text = "";
    }

    //////////////////////////////////////////////////////////////////////
    //Functions to receive variables from other scripts

    //Receive modelName variable
    public void GetModelName(string modelName) {
        this.modelName = modelName;
    }
}
