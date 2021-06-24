using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineController : MonoBehaviour
{
    private LineRenderer lr;                                        //Line gameobject
    private GameObject[] coordinates;                               //Array of coordinates
    private List<string> coordinatesCount = new List<string>();     //Create list
    private List<string> otherCoordinates = new List<string>();     //Create list

    Material material;              //Create material variable
    Material selectedMaterial;      //Create material variable

    //Set lr variable to LineRenderer component at start of script
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
        
        //Count amount of gameobject within the marking gameobject
        foreach (Transform i in lr.transform.parent.transform) {
            coordinatesCount.Add(i.name);
        }

        //Amount of coordinates = amount of gameobjects - LineRenderer and MarkingTag
        if (coordinatesCount.Count > 0) {
        lr.positionCount = coordinatesCount.Count - 2;
        }

        if (coordinates != null) {          //Don't start before coordinates are found
        //Search for all coordinates
            for (int i = 0; i < coordinates.Length; i++) {
                //Only select coordinates that are located in the same marking gameobject as the LineRenderer
                if (coordinates[i].transform.parent.name == lr.transform.parent.name) {
                    lr.SetPosition((i-otherCoordinates.Count), coordinates[i].transform.position);      //Draw line
                }

                //Don't use coordinates outside the LineRenderer's marking gameobject
                if (coordinates[i].transform.parent.name != lr.transform.parent.name) {
                    otherCoordinates.Add(coordinates[i].transform.name);
                }
            }
            otherCoordinates.Clear();
        }
        else {
            return;
        }

        //Set line material to loaded colour
        selectedMaterial = lr.transform.parent.GetComponent<Renderer>().material;
        lr.material = selectedMaterial;
    }
}
