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
    GameObject spawnedCoordinate;

    string markingTag;
    string modelName;


    void Start()
    {
        gazeCursor = GameObject.Find("MixedRealityPlayspace/DefaultCursor(Clone)");     //Find cursor GameObject within parent
    }

    void Update()
    {
        modelParent = GameObject.Find("ImageTarget/"+modelName+"(Clone)/Markings/"+markingTag);  //Find model for parent      "ImageTarget/RevEng_NoseCone_Fokker100(Clone)/Markings/"+markingTag
    }

    public void SpawnPosition() {
        if (changesBool == true) {                                      //Only if changes are allowed
            cursorPosition = gazeCursor.transform.position;             //Get cursor position
            SpawnCoordinate();                                          //Perform spawn
        }
    }

    public void SpawnCoordinate() {
        spawnedCoordinate = Instantiate(coordinatePrefab, cursorPosition, Quaternion.identity);         //Spawn object as child of model
        spawnedCoordinate.transform.SetParent(modelParent.transform);
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////
    public void ChangesToggled(bool changesBool) {
        this.changesBool = changesBool;
    }
    
    public void GetMarkingTag(string markingTag) {
        this.markingTag = markingTag;
    }

    public void GetModelName(string modelName) {
        this.modelName = modelName;
    }
}