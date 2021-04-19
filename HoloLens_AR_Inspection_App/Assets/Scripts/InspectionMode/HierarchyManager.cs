using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    string markingTag;
    string markingColour;

    Material selectedMaterial;
    string materialName;

    public void Update() 
    {
        parentPrefab = GameObject.Find("ImageTarget/RevEng_NoseCone_Fokker100(Clone)/Markings");    //"ImageTarget/RevEng_NoseCone_Fokker100(Clone)/Markings"
        Debug.Log("ColorName: "+markingColour);
    }

    public void MarkingTag() {
        //Get marking tag value
        marking = Instantiate(markingPrefab);
        marking.transform.SetParent(parentPrefab.transform, false);
        marking.name = markingTag;

        materialName = markingColour;
        MaterialSelection();
        marking.gameObject.GetComponent<Renderer>().material = selectedMaterial;

        GameObject lr = GameObject.Find("ImageTarget/RevEng_NoseCone_Fokker100(Clone)/Markings/"+markingTag); //"ImageTarget/RevEng_NoseCone_Fokker100(Clone)/Markings/"+markingTag+","+markingColour
        marking = Instantiate(lineRendererPrefab);
        marking.transform.SetParent(lr.transform, false);
    }

    public void MaterialSelection() {
        var material = Resources.Load("Colours/"+materialName);
        selectedMaterial = material as Material; 
    }

    //////////////////////////////////////////////////////////////////////

    public void GetMarkingTag(string markingTag) {
        this.markingTag = markingTag;
    }

    public void GetMarkingColour(string markingColour) {
        this.markingColour = markingColour;
    }
}
