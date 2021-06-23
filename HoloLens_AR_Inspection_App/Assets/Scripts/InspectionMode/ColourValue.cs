using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourValue : MonoBehaviour
{
    [SerializeField] GameObject greenButton;
    [SerializeField] GameObject pinkButton;
    [SerializeField] GameObject orangeButton;
    string markingColour;
    Microsoft.MixedReality.Toolkit.UI.Interactable buttonStatePink;
    Microsoft.MixedReality.Toolkit.UI.Interactable buttonStateGreen;
    Microsoft.MixedReality.Toolkit.UI.Interactable buttonStateOrange;

    public void Awake() 
    {
        //Set variables for interactable buttons at start of script
        buttonStateGreen = greenButton.GetComponent<Microsoft.MixedReality.Toolkit.UI.Interactable>();
        buttonStatePink = pinkButton.GetComponent<Microsoft.MixedReality.Toolkit.UI.Interactable>();
        buttonStateOrange = orangeButton.GetComponent<Microsoft.MixedReality.Toolkit.UI.Interactable>();
    }

    public void Update() 
    {
        //Send current colour to HierarchyManager script
        HierarchyManager hierarchyManager = GameObject.Find("InspectionManager").GetComponent<HierarchyManager>();
        hierarchyManager.GetMarkingColour(markingColour);
    }

    //When green is selected, deactivate other colour buttons
    public void Green() {
        markingColour = "Green";
        buttonStatePink.IsToggled = false;
        buttonStateOrange.IsToggled = false;
    }

    //When pink is selected, deactivate other colour buttons
    public void Pink() {
        markingColour = "Pink";
        buttonStateGreen.IsToggled = false;
        buttonStateOrange.IsToggled = false;
    }

    //When orange is selected, deactivate other colour buttons
    public void Orange() {
        markingColour = "Orange";
        buttonStatePink.IsToggled = false;
        buttonStateGreen.IsToggled = false;
    }
}
