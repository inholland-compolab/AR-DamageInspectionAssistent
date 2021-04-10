using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InfoWindow : MonoBehaviour
{
    [SerializeField]
    GameObject textMode;
    [SerializeField]
    GameObject textMode1;

    


    public void RegisterExplanation() {
        TextMeshProUGUI component = textMode.GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI component1 = textMode1.GetComponent<TextMeshProUGUI>();
        component.text = "Register Mode";
        component1.text = "Within this mode, it is possible to make a new project and connect the corresponding nose cone model. This provides the Image Tracking function to place the right nose cone model at the target.";
    }

    public void InspectionExplanation() {
        TextMeshProUGUI component = textMode.GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI component1 = textMode1.GetComponent<TextMeshProUGUI>();
        component.text = "Inspection Mode";
        component1.text = "Within this mode, inspection markings are drawn upon the nose cone. Also additional damage information is linked to the markings.";
    }

    public void ViewExplanation() {
        TextMeshProUGUI component = textMode.GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI component1 = textMode1.GetComponent<TextMeshProUGUI>();
        component.text = "View Mode";
        component1.text = "Within this mode, the made inspection markings can be viewed in AR. Adjustments can only be made in Inspection Mode.";
    }
}
