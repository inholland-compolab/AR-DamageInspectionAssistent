using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class TrackingManager : MonoBehaviour
{
    bool targetingBool;

    public void Awake() 
    {
        targetingBool = true;
        TrackerManager.Instance.GetTracker<ObjectTracker>().Start();
    }

    // public void EnableTracking() {
    //     targetingBool = true;
    //     TrackerManager.Instance.GetTracker<ObjectTracker>().Start();
    // }

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
