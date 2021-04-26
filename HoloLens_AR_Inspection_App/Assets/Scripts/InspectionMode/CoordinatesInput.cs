using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.Physics;
using Microsoft.MixedReality.Toolkit.Utilities;
using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.Input;

public class CoordinatesInput : MonoBehaviour
{
    GameObject gazeCursor;              //Assign cursor gameobject
    Vector3 cursorPosition;             //Assign position variable
    Vector3 cameraPosition;

    public bool changesBool;            //Assign changes bool (altered by buttons in Inspection Mode)

    GameObject modelParent;

    [SerializeField] GameObject coordinatePrefab;                       //Set coordinate object
    GameObject spawnedCoordinate;

    string markingTag;
    string modelName;

    RaycastHit hit;

    Vector3 startPoint;
    Vector3 endPoint;
    GameObject hitObject;


    void Start()
    {
        gazeCursor = GameObject.Find("MixedRealityPlayspace/DefaultCursor(Clone)");     //Find cursor GameObject within parent
    }

    void Update()
    {
        modelParent = GameObject.Find("ImageTarget/"+modelName+"(Clone)/Markings/"+markingTag);  //Find model for parent      "ImageTarget/RevEng_NoseCone_Fokker100(Clone)/Markings/"+markingTag

        

        //cameraPosition = GameObject.FindWithTag("MainCamera").transform.position;
        //cursorPosition = (gazeCursor.transform.position) - cameraPosition;
        //Physics.Raycast(cameraPosition, cursorPosition, out hit, Mathf.Infinity);
            
    }

    public void SpawnPosition() {
        if (changesBool == true) {                                      //Only if changes are allowed
        foreach(var source in MixedRealityToolkit.InputSystem.DetectedInputSources)
        {
            // Ignore anything that is not a hand because we want articulated hands
            if (source.SourceType == Microsoft.MixedReality.Toolkit.Input.InputSourceType.Hand)
            {
                foreach (var p in source.Pointers)
                {
                    if (p is IMixedRealityNearPointer)
                    {
                        // Ignore near pointers, we only want the rays
                        continue;
                    }
                    if (p.Result != null)
                    {
                        startPoint = p.Position;
                        endPoint = p.Result.Details.Point;
                        hitObject = p.Result.Details.Object;
                    }
                }
            }
        }
            SpawnCoordinate();                                          //Perform spawn
        }
    }

    public void SpawnCoordinate() {
        spawnedCoordinate = Instantiate(coordinatePrefab, endPoint, Quaternion.identity);         //Spawn object as child of model
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