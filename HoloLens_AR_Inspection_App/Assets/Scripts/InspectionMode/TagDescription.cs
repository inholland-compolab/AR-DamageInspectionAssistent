using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TagDescription : MonoBehaviour
{
    [SerializeField] GameObject descriptionWindow;
    bool clickBool = false;

    public void Update() 
    {
        descriptionWindow.SetActive(clickBool);
    }

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
