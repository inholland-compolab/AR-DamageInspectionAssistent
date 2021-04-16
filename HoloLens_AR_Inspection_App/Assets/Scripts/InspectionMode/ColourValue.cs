using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourValue : MonoBehaviour
{
    [SerializeField] GameObject greenButton;
    [SerializeField] GameObject pinkButton;
    [SerializeField] GameObject orangeButton;
    string colourSelect;
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
        Debug.Log(colourSelect);
    }
    public void Green() {
        colourSelect = "Green";
        buttonStatePink.IsToggled = false;
        buttonStateOrange.IsToggled = false;
    }

    public void Pink() {
        colourSelect = "Pink";
        buttonStateGreen.IsToggled = false;
        buttonStateOrange.IsToggled = false;
    }

    public void Orange() {
        colourSelect = "Orange";
        buttonStatePink.IsToggled = false;
        buttonStateGreen.IsToggled = false;
    }
}
