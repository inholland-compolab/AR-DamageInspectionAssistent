using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class TrackingManager : MonoBehaviour
{
    bool targetingBool;

    //Set ObjectTracker ON at start of script
    public void Awake() 
    {
        targetingBool = true;
        TrackerManager.Instance.GetTracker<ObjectTracker>().Start();
    }

    //Manage ON and OFF states of tracking function
    public void TrackingClick() {
        if (targetingBool == true) {
            TrackerManager.Instance.GetTracker<ObjectTracker>().Stop();
            targetingBool = false;
            return;
        }
        if (targetingBool == false) {
            TrackerManager.Instance.GetTracker<ObjectTracker>().Start();
            targetingBool = true;
            return;
        } 
    }

}
