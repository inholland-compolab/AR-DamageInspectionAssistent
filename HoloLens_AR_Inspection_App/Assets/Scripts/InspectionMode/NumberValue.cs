using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NumberValue : MonoBehaviour
{
    [SerializeField] GameObject numberDisplay;
    public string markingTag;
    string value;

    public void Update() 
    {
        markingTag = numberDisplay.GetComponent<TextMeshPro>().text;
        //LoadSaveManager loadSaveManager = GameObject.Find("InspectionManager").GetComponent<LoadSaveManager>();
        HierarchyManager hierarchyManager = GameObject.Find("InspectionManager").GetComponent<HierarchyManager>();
        CoordinatesInput coordinatesInput = GameObject.Find("ImageTarget/RevEng_NoseCone_Fokker100(Clone)/default").GetComponent<CoordinatesInput>();   //"ImageTarget/RevEng_NoseCone_Fokker100(Clone)/default"
        NewLoadSave newLoadSave = GameObject.Find("InspectionManager").GetComponent<NewLoadSave>();
        //loadSaveManager.GetMarkingTag(markingTag);
        hierarchyManager.GetMarkingTag(markingTag);
        coordinatesInput.GetMarkingTag(markingTag);
        newLoadSave.GetMarkingTag(markingTag);
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
}
