using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TypeValue : MonoBehaviour
{
    [SerializeField] GameObject I_Button;
    [SerializeField] GameObject II_Button;
    [SerializeField] GameObject III_Button;
    [SerializeField] GameObject IV_Button;
    string markingType;
    Component[] components;
    Microsoft.MixedReality.Toolkit.UI.Interactable buttonState_I;
    Microsoft.MixedReality.Toolkit.UI.Interactable buttonState_II;
    Microsoft.MixedReality.Toolkit.UI.Interactable buttonState_III;
    Microsoft.MixedReality.Toolkit.UI.Interactable buttonState_IV;

    public void Awake() 
    {
        buttonState_I = I_Button.GetComponent<Microsoft.MixedReality.Toolkit.UI.Interactable>();
        buttonState_II = II_Button.GetComponent<Microsoft.MixedReality.Toolkit.UI.Interactable>();
        buttonState_III = III_Button.GetComponent<Microsoft.MixedReality.Toolkit.UI.Interactable>();
        buttonState_IV = IV_Button.GetComponent<Microsoft.MixedReality.Toolkit.UI.Interactable>();
    }

    public void Update() 
    {
        HierarchyManager hierarchyManager = GameObject.Find("InspectionManager").GetComponent<HierarchyManager>();
        hierarchyManager.GetMarkingType(markingType);
    }

    public void Type_I() {
        markingType = "I";
        buttonState_II.IsToggled = false;
        buttonState_III.IsToggled = false;
        buttonState_IV.IsToggled = false;
    }

    public void Type_II() {
        markingType = "II";
        buttonState_I.IsToggled = false;
        buttonState_III.IsToggled = false;
        buttonState_IV.IsToggled = false;
    }

    public void Type_III() {
        markingType = "III";
        buttonState_I.IsToggled = false;
        buttonState_II.IsToggled = false;
        buttonState_IV.IsToggled = false;
    }

    public void Type_IV() {
        markingType = "IV";
        buttonState_I.IsToggled = false;
        buttonState_II.IsToggled = false;
        buttonState_III.IsToggled = false;
    }
}
