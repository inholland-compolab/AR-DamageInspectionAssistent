using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TagPlacement : MonoBehaviour
{
    [SerializeField] GameObject tagNumber;
    Transform parent;
    float vectorX;
    float vectorY;
    float vectorZ;

    float sumX;
    float sumY;
    float sumZ;

    int x;

    
    public void Update()
    {
        //Set parent gameobject
        parent = gameObject.transform.parent;

        //Set initial tag position
        vectorX = 0;
        vectorY = 0;
        vectorZ = 0;
        sumX = 0;
        sumY = 0;
        sumZ = 0;
        x = 0;
        
        //Search all coordinates within a marking
        foreach (Transform i in parent) {
            if (i.tag == "Coordinate") {
                x = x + 1;

                //Get position
                vectorX = i.position.x;
                vectorY = i.position.y;
                vectorZ = i.position.z;
                
                //Add position value to previous found values to get the SUM of the positions
                sumX = sumX + vectorX;
                sumY = sumY + vectorY;
                sumZ = sumZ + vectorZ;
            }
        }
        
        //Set position of marking tag (mean position of all coordinates: SUM / amount of coordinates)
        //Multipling with 1.1, 0.9 or other values will affect its placement
        if (x > 0) {
            //gameObject.transform.position = new Vector3 ( (sumX / x)*1.05f , (sumY / x)*0.95f , (sumZ / x)*0.95f );
            //gameObject.transform.position = new Vector3 ( (sumX / x)*1.1f , (sumY / x)*1.1f , (sumZ / x)*1.1f );
            gameObject.transform.position = new Vector3 ( (sumX / x), (sumY / x), (sumZ / x) );
        }

        //Rotate tag to camera
        gameObject.transform.rotation = Quaternion.LookRotation(gameObject.transform.position - Camera.main.transform.position);

        //Set tag number
        tagNumber.GetComponent<TextMeshPro>().text = parent.name;
    }
}
