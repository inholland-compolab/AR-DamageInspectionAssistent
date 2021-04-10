using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneLoader
{
    
    public enum Scene {
        StartScreen,
        RegisterMode,
        InspectionMode,
        ViewMode,
    }

    public static void Load(Scene scene) {
        SceneManager.LoadScene(scene.ToString());
    }

}
