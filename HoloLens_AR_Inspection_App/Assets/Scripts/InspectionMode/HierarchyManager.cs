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

    public void Update() 
    {
        parentPrefab = GameObject.Find("ImageTarget/RevEng_NoseCone_Fokker100(Clone)/Markings");
    }

    public void MarkingTag() {
        //Get marking tag value
        marking = Instantiate(markingPrefab);
        marking.transform.SetParent(parentPrefab.transform, false);
        marking.name = markingTag;

        GameObject lr = GameObject.Find("ImageTarget/RevEng_NoseCone_Fokker100(Clone)/Markings/"+markingTag);
        marking = Instantiate(lineRendererPrefab);
        marking.transform.SetParent(lr.transform, false);
    }

    // public void PopulateMarking() {
    //     coordinates = GameObject.FindGameObjectsWithTag("Coordinate");
    //     if (coordinates != null) {                                       //Not begin before array is created
    //         for (int i = 0; i < coordinates.Length; i++) {
    //             coordinates[i].transform.SetParent(marking.transform, false);
    //             coordinates[i].transform.tag = ("Coordinate");
                
    //             CoordinatesInput coordinatesInput = GameObject.Find("InspectionManager").GetComponent<CoordinatesInput>();
    //         }
    //     }
    //     else {return;}
    // }

    //////////////////////////////////////////////////////////////////////

    public void GetMarkingTag(string markingTag) {
        this.markingTag = markingTag;
    }
}
