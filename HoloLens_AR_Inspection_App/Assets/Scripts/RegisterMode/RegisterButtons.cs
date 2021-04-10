using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegisterButtons : MonoBehaviour
{
    public void ButtonBack() {
        SceneLoader.Load(SceneLoader.Scene.StartScreen);
    }
}
