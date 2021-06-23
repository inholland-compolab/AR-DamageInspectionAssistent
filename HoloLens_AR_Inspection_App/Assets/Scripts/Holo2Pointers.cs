using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.Input;
using UnityEngine;

public class Holo2Pointers : MonoBehaviour
{
    //This script is used as example for the placement of coordinates!!!
    //For the HoloLens 2: the pointers from your hand are used to create rays and detect a hit.


    // Update is called once per frame
    void Update()
    {
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
                        var startPoint = p.Position;
                        var endPoint = p.Result.Details.Point;
                        var hitObject = p.Result.Details.Object;
                        if (hitObject)
                        {
                            var sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                            sphere.transform.localScale = Vector3.one * 0.01f;
                            sphere.transform.position = endPoint;
                        }
                        Debug.Log("start: "+startPoint);
                        Debug.Log("end: "+endPoint);
                        Debug.Log("hit: "+hitObject);
                    }

                }
            }
        }
    }
}
