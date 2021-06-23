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

    //Set variable for interactable buttons at start of script
    public void Awake() 
    {
        buttonState_I = I_Button.GetComponent<Microsoft.MixedReality.Toolkit.UI.Interactable>();
        buttonState_II = II_Button.GetComponent<Microsoft.MixedReality.Toolkit.UI.Interactable>();
        buttonState_III = III_Button.GetComponent<Microsoft.MixedReality.Toolkit.UI.Interactable>();
        buttonState_IV = IV_Button.GetComponent<Microsoft.MixedReality.Toolkit.UI.Interactable>();
    }

    //Send variable to other script each frame
    public void Update() 
    {
        HierarchyManager hierarchyManager = GameObject.Find("InspectionManager").GetComponent<HierarchyManager>();
        hierarchyManager.GetMarkingType(markingType);
    }

    //Function for Type_I selection
    public void Type_I() {
        //Set marking type
        markingType = "I";

        //Untoggle other buttons
        buttonState_II.IsToggled = false;
        buttonState_III.IsToggled = false;
        buttonState_IV.IsToggled = false;
    }

    //Function for Type_II selection
    public void Type_II() {
        //Set marking type
        markingType = "II";

        //Untoggle other buttons
        buttonState_I.IsToggled = false;
        buttonState_III.IsToggled = false;
        buttonState_IV.IsToggled = false;
    }

    //Function for Type_III selection
    public void Type_III() {
        //Set marking type
        markingType = "III";
        
        //Untoggle other buttons
        buttonState_I.IsToggled = false;
        buttonState_II.IsToggled = false;
        buttonState_IV.IsToggled = false;
    }

    //Function for Type_IV selection
    public void Type_IV() {
        //Set marking type
        markingType = "IV";
        
        //Untoggle other buttons
        buttonState_I.IsToggled = false;
        buttonState_II.IsToggled = false;
        buttonState_III.IsToggled = false;
    }
}
