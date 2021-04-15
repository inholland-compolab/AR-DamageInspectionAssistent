using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineController : MonoBehaviour
{
    private LineRenderer lr;    //Line GameObject
    private GameObject[] coordinates;    //Array of coordinates
    private List<string> coordinatesCount = new List<string>();
    private List<string> otherCoordinates = new List<string>();

    // Awake is called when scene is loaded
    private void Awake() 
    {
        GameObject currentGameObject = this.gameObject;
        lr = currentGameObject.GetComponent<LineRenderer>();
    }
    
    // Update is called once per frame
    void Update()
    {
        coordinates = GameObject.FindGameObjectsWithTag("Coordinate");          //Add all spawned objects ("Coordinate") to array
        coordinatesCount.Clear();
        foreach (Transform i in lr.transform.parent.transform) {
            coordinatesCount.Add(i.name);
        }

        if (coordinatesCount.Count > 0) {
        lr.positionCount = coordinatesCount.Count - 1;
        }

        if (coordinates != null) {                                       //Not begin before array is created
            for (int i = 0; i < coordinates.Length; i++) {
                if (coordinates[i].transform.parent.name == lr.transform.parent.name) {
                    lr.SetPosition((i-otherCoordinates.Count), coordinates[i].transform.position);    //For all array elements: Draw line
                }
                if (coordinates[i].transform.parent.name != lr.transform.parent.name) {
                    otherCoordinates.Add(coordinates[i].transform.name);
                }
            }
            //Debug.Log("not used: "+otherCoordinates.Count);
            otherCoordinates.Clear();
        }
        else {return;}
    }

    // public void SetupLine(GameObject[] coordinates) {
    //     lr.positionCount = coordinates.Length;
    //     this.coordinates = coordinates;
    // }
}
