using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourValue : MonoBehaviour
{
    [SerializeField] GameObject greenButton;
    [SerializeField] GameObject pinkButton;
    [SerializeField] GameObject orangeButton;
    string markingColour;
    Component[] components;
    Microsoft.MixedReality.Toolkit.UI.Interactable buttonStatePink;
    Microsoft.MixedReality.Toolkit.UI.Interactable buttonStateGreen;
    Microsoft.MixedReality.Toolkit.UI.Interactable buttonStateOrange;

    public void Awake() 
    {
        buttonStateGreen = greenButton.GetComponent<Microsoft.MixedReality.Toolkit.UI.Interactable>();
        buttonStatePink = pinkButton.GetComponent<Microsoft.MixedReality.Toolkit.UI.Interactable>();
        buttonStateOrange = orangeButton.GetComponent<Microsoft.MixedReality.Toolkit.UI.Interactable>();
    }

    public void Update() 
    {
        HierarchyManager hierarchyManager = GameObject.Find("InspectionManager").GetComponent<HierarchyManager>();
        CoordinatesInput coordinatesInput = GameObject.Find("ImageTarget/RevEng_NoseCone_Fokker100(Clone)/default").GetComponent<CoordinatesInput>();   //"ImageTarget/RevEng_NoseCone_Fokker100(Clone)/default"
        hierarchyManager.GetMarkingColour(markingColour);
        coordinatesInput.GetMarkingColour(markingColour);
        Debug.Log(markingColour);
    }
    public void Green() {
        markingColour = "Green";
        buttonStatePink.IsToggled = false;
        buttonStateOrange.IsToggled = false;
    }

    public void Pink() {
        markingColour = "Pink";
        buttonStateGreen.IsToggled = false;
        buttonStateOrange.IsToggled = false;
    }

    public void Orange() {
        markingColour = "Orange";
        buttonStatePink.IsToggled = false;
        buttonStateGreen.IsToggled = false;
    }
}
