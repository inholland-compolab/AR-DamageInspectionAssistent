using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HierarchyManager : MonoBehaviour
{
    //If marking tag inserted
        //Create child named "Tag_*number*" of Markings parent
    //If saved
        //Move all created coordinates as child to Tag_*number* parent

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
        parentPrefab = GameObject.Find("ImageTarget/"+modelName+"(Clone)/Markings");    //"ImageTarget/RevEng_NoseCone_Fokker100(Clone)/Markings"
    }

    public void MarkingTag() {
        //Get marking tag value
        marking = Instantiate(markingPrefab);
        marking.transform.SetParent(parentPrefab.transform, false);
        marking.name = markingTag;

        materialName = markingColour;
        MaterialSelection();
        marking.gameObject.GetComponent<Renderer>().material = selectedMaterial;

        GameObject lr = GameObject.Find("ImageTarget/"+modelName+"(Clone)/Markings/"+markingTag);   //"ImageTarget/RevEng_NoseCone_Fokker100(Clone)/Markings/"+markingTag
        marking = Instantiate(lineRendererPrefab);
        marking.transform.SetParent(lr.transform, false);

        tagObject = Instantiate(tagPrefab);
        tagObject.transform.SetParent(lr.transform, false);

        typeName = markingType;
        GameObject typeObject = GameObject.Find("ImageTarget/"+modelName+"(Clone)/Markings/"+markingTag+"/HoloTag(Clone)/DescriptionWindow/TypeAnswer");    //"ImageTarget/RevEng_NoseCone_Fokker100(Clone)/Markings/"+markingTag+"/HoloTag(Clone)/DescriptionWindow/TypeAnswer"
        typeObject.GetComponent<TextMeshPro>().text = typeName;
    }

    public void MaterialSelection() {
        var material = Resources.Load("Colours/"+materialName);
        selectedMaterial = material as Material; 
    }

    //////////////////////////////////////////////////////////////////////

    public void GetModelName(string modelName) {
        this.modelName = modelName;
    }
    
    public void GetMarkingTag(string markingTag) {
        this.markingTag = markingTag;
    }

    public void GetMarkingColour(string markingColour) {
        this.markingColour = markingColour;
    }

    public void GetMarkingType(string markingType) {
        this.markingType = markingType;
    }
}
