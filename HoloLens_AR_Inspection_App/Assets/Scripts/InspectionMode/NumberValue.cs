using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NumberValue : MonoBehaviour
{
    [SerializeField] GameObject numberDisplay;
    string modelName;
    public string markingTag;
    string value;

    public void Update() 
    {
        markingTag = numberDisplay.GetComponent<TextMeshPro>().text;
        HierarchyManager hierarchyManager = GameObject.Find("InspectionManager").GetComponent<HierarchyManager>();
        NewLoadSave newLoadSave = GameObject.Find("InspectionManager").GetComponent<NewLoadSave>();
        hierarchyManager.GetMarkingTag(markingTag);
        newLoadSave.GetMarkingTag(markingTag);
        CoordinatesInput coordinatesInput = GameObject.Find("ImageTarget/"+modelName+"(Clone)/default").GetComponent<CoordinatesInput>();
        
        coordinatesInput.GetMarkingTag(markingTag);
    }

    public void Number() {
        value = gameObject.GetComponent<TextMeshPro>().text;
        numberDisplay.GetComponent<TextMeshPro>().text = markingTag + value;
    }

    public void Remove() {
        numberDisplay.GetComponent<TextMeshPro>().text = markingTag.Substring(0, markingTag.Length - 1);
    }

    public void New() {
        numberDisplay.GetComponent<TextMeshPro>().text = "";
    }

    //////////////////////////////////////////////////////////////////////
    public void GetModelName(string modelName) {
        this.modelName = modelName;
    }
}
