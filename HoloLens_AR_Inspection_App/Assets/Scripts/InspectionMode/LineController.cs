using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineController : MonoBehaviour
{
    private LineRenderer lr;    //Line GameObject
    private GameObject[] coordinates;    //Array of coordinates

    // Awake is called when scene is loaded
    private void Awake() 
    {
        lr = GetComponent<LineRenderer>();
    }
    
    // Update is called once per frame
    void Update()
    {   
        if (coordinates != null) {                                       //Not begin before array is created
            for (int i = 0; i < coordinates.Length; i++) {
                lr.SetPosition(i, coordinates[i].transform.position);    //For all array elements: Draw line
            }
        }
        else {return;}
    }

    public void SetupLine(GameObject[] coordinates) {
        lr.positionCount = coordinates.Length;
        this.coordinates = coordinates;
    }
}
