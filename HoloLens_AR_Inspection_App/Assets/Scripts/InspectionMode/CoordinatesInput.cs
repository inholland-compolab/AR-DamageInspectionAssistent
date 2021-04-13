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

    void Start()
    {
        gazeCursor = GameObject.Find("MixedRealityPlayspace/DefaultCursor(Clone)");     //Find cursor GameObject within parent
    }

    void Update()
    {
        coordinates = GameObject.FindGameObjectsWithTag("Coordinate");          //Add all spawned objects ("Coordinate") to array
        line.SetupLine(coordinates);                                            //Send array to RenderLine script to make renderline
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
        //modelParent = GameObject.Find("ImageTarget/RevEng_NoseCone_Fokker100(Clone)");  //Find model for parent
        Instantiate(coordinatePrefab, cursorPosition, Quaternion.identity); //;modelParent.transform);                   //Spawn object as child of model
    }
}