using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InspectionButtons : MonoBehaviour
{
    public void ButtonRegister() {
        SceneLoader.Load(SceneLoader.Scene.RegisterMode);
    }

    public void ButtonInspection() {
        SceneLoader.Load(SceneLoader.Scene.InspectionMode);
    }

    public void ButtonView() {
        SceneLoader.Load(SceneLoader.Scene.ViewMode);
    }

    public void ButtonInfo() {}

    public void ButtonStart() {
        SceneLoader.Load(SceneLoader.Scene.StartScreen);
    }
}
