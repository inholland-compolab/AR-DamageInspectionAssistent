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
        component1.text = "...";
    }

    public void InspectionExplanation() {
        TextMeshProUGUI component = textMode.GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI component1 = textMode1.GetComponent<TextMeshProUGUI>();
        component.text = "Inspection Mode";
        component1.text = "...";
    }

    public void ViewExplanation() {
        TextMeshProUGUI component = textMode.GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI component1 = textMode1.GetComponent<TextMeshProUGUI>();
        component.text = "View Mode";
        component1.text = "...";
    }
}
