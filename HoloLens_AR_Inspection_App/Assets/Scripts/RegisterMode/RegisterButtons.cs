using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegisterButtons : MonoBehaviour
{
    //Function to go back to the start scene (Scenes were used for the HoloLens 1 version)
    //See SaveProject script for the HoloLens 2 function
    public void ButtonBack() {
        SceneLoader.Load(SceneLoader.Scene.StartScreen);
    }
}
