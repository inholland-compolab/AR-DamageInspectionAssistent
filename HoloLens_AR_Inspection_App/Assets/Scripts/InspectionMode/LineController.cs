using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineController : MonoBehaviour
{
    private LineRenderer lr;    //Line GameObject
    private GameObject[] coordinates;    //Array of coordinates
    private List<string> coordinatesCount = new List<string>();
    private List<string> otherCoordinates = new List<string>();

    Material material;
    Material selectedMaterial;

    //Set variable to LineRenderer component at start of script 
    private void Awake() 
    {
        GameObject currentGameObject = this.gameObject;
        lr = currentGameObject.GetComponent<LineRenderer>();
    }
    
    //The LineRenderer searches for coordinates to create a line every frame
    void Update()
    {
        //Add all spawned objects ("Coordinate") to array
        coordinates = GameObject.FindGameObjectsWithTag("Coordinate");
        coordinatesCount.Clear();
        foreach (Transform i in lr.transform.parent.transform) {
            coordinatesCount.Add(i.name);
        }

        if (coordinatesCount.Count > 0) {
        lr.positionCount = coordinatesCount.Count - 2;
        }

        if (coordinates != null) {  //Don't start before array is created
            for (int i = 0; i < coordinates.Length; i++) {
                if (coordinates[i].transform.parent.name == lr.transform.parent.name) {
                    lr.SetPosition((i-otherCoordinates.Count), coordinates[i].transform.position);    //For all array elements: Draw line
                }
                if (coordinates[i].transform.parent.name != lr.transform.parent.name) {
                    otherCoordinates.Add(coordinates[i].transform.name);
                }
            }
            otherCoordinates.Clear();
        }
        else {return;}

        selectedMaterial = lr.transform.parent.GetComponent<Renderer>().material;
        lr.material = selectedMaterial;
    }
}
