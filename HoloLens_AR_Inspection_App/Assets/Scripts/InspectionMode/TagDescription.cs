using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TagDescription : MonoBehaviour
{
    [SerializeField] GameObject descriptionWindow;
    bool clickBool = false;

    //Check each frame whether the marking description window needs to be active or not
    public void Update() 
    {
        descriptionWindow.SetActive(clickBool);
    }

    //Function to open/close description window
    public void ClickTag() {
        if (clickBool == false) {
            clickBool = true;
            return;
        }
        if (clickBool == true) {
            clickBool = false;
            return;
        }
    }
}
