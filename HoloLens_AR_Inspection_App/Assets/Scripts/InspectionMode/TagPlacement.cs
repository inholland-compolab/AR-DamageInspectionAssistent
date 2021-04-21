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
        parent = gameObject.transform.parent;

        //Set tag position
        vectorX = 0;
        vectorY = 0;
        vectorZ = 0;
        sumX = 0;
        sumY = 0;
        sumZ = 0;
        x = 0;
        
        foreach (Transform i in parent) {
            if (i.tag == "Coordinate") {
                x = x + 1;

                vectorX = i.position.x;
                vectorY = i.position.y;
                vectorZ = i.position.z;

                sumX = sumX + vectorX;
                sumY = sumY + vectorY;
                sumZ = sumZ + vectorZ;
            }
        }
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
