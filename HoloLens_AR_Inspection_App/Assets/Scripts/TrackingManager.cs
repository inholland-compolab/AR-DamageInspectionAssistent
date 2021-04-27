using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class TrackingManager : MonoBehaviour
{
    bool targetingBool = true;

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
