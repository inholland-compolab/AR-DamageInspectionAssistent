using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.Physics;
using Microsoft.MixedReality.Toolkit.Utilities;

public class CoordinatesInput : MonoBehaviour
{
    GameObject gazeCursor;              //Assign cursor gameobject
    Vector3 cursorPosition;             //Assign position variable

    public bool changesBool;            //Assign changes bool (altered by buttons in Inspection Mode)

    GameObject modelParent;

    [SerializeField] GameObject coordinatePrefab;                       //Set coordinate object
    [SerializeField] private GameObject[] coordinates;                  //Array of coordinates
    [SerializeField] private LineController line;                       //LineRenderer object
    GameObject spawnedCoordinate;

    string markingTag;
    int markingCount;


    void Start()
    {
        gazeCursor = GameObject.Find("MixedRealityPlayspace/DefaultCursor(Clone)");     //Find cursor GameObject within parent
    }

    void Update()
    {
        // coordinates = GameObject.FindGameObjectsWithTag("Coordinate");          //Add all spawned objects ("Coordinate") to array
        // line.SetupLine(coordinates);                                            //Send array to RenderLine script to make renderline

        // //foreach (Transform *coordinate* in *MarkingTag*.transform) {
        //     //if (*coordinate*.tag == "Coordinate") {
        //         //coordinates += *coordinate*;
        // //}

        modelParent = GameObject.Find("ImageTarget/RevEng_NoseCone_Fokker100(Clone)/Markings/"+markingTag);  //Find model for parent
        // Debug.Log("ImageTarget/RevEng_NoseCone_Fokker100(Clone)/Markings"+markingTag);
        // Debug.Log("markingTagParent: "+modelParent.name);
    }

    public void ChangesToggled(bool changesBool) {
        this.changesBool = changesBool;
    }

    public void SpawnPosition() {
        if (changesBool == true) {                                              //Only if changes are allowed
            cursorPosition = gazeCursor.transform.position;             //Get cursor position
            SpawnCoordinate();                                          //Perform spawn
        }
    }

    public void SpawnCoordinate() {
        spawnedCoordinate = Instantiate(coordinatePrefab, cursorPosition, Quaternion.identity);                   //Spawn object as child of model
        spawnedCoordinate.transform.SetParent(modelParent.transform);
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////

    public void GetMarkingTag(string markingTag) {
        this.markingTag = markingTag;
    }

    public void GetMarkingCount(int markingCount) {
        this.markingCount = markingCount;
    }
}