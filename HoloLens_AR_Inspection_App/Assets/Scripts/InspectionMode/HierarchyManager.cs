using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HierarchyManager : MonoBehaviour
{
    GameObject parentPrefab;
    [SerializeField] GameObject markingPrefab;
    [SerializeField] GameObject lineRendererPrefab;
    GameObject marking;
    GameObject[] coordinates;
    [SerializeField] GameObject tagPrefab;
    GameObject tagObject;

    string modelName;

    string markingTag;
    string markingColour;
    string markingType;

    Material selectedMaterial;
    string materialName;
    
    string typeName;

    public void Update() 
    {
        //Search for model and set model as parentPrefab
        parentPrefab = GameObject.Find("ImageTarget/"+modelName+"(Clone)/Markings");
    }

    //Function to create the marking
    public void MarkingTag() {
        //Get marking tag value
        marking = Instantiate(markingPrefab);
        marking.transform.SetParent(parentPrefab.transform, false);
        marking.name = markingTag;

        //Set marking colour
        materialName = markingColour;
        MaterialSelection();
        marking.gameObject.GetComponent<Renderer>().material = selectedMaterial;

        //Place LineRenderer in marking gameobject
        GameObject lr = GameObject.Find("ImageTarget/"+modelName+"(Clone)/Markings/"+markingTag);
        marking = Instantiate(lineRendererPrefab);
        marking.transform.SetParent(lr.transform, false);

        //Add tag gameobject to marking
        tagObject = Instantiate(tagPrefab);
        tagObject.transform.SetParent(lr.transform, false);

        //Add damage type info to the tag's information window
        typeName = markingType;
        GameObject typeObject = GameObject.Find("ImageTarget/"+modelName+"(Clone)/Markings/"+markingTag+"/HoloTag(Clone)/DescriptionWindow/TypeAnswer");
        typeObject.GetComponent<TextMeshPro>().text = typeName;
    }

    //Function to get the colour material from the Resources/Colours/ folder
    public void MaterialSelection() {
        var material = Resources.Load("Colours/"+materialName);
        selectedMaterial = material as Material; 
    }

    //////////////////////////////////////////////////////////////////////
    //Functions to receive variables from other scripts

    //Receive modelName variable
    public void GetModelName(string modelName) {
        this.modelName = modelName;
    }
    
    //Receive markingTag variable
    public void GetMarkingTag(string markingTag) {
        this.markingTag = markingTag;
    }

    //Receive markingColour variable
    public void GetMarkingColour(string markingColour) {
        this.markingColour = markingColour;
    }

    //Receive markingType variable
    public void GetMarkingType(string markingType) {
        this.markingType = markingType;
    }
}
