using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObjectScenesManager : MonoBehaviour
{
    [SerializeField] GameObject start;
    [SerializeField] GameObject register;
    [SerializeField] GameObject inspection;
    [SerializeField] GameObject view;

    //Open start menu at start of script
    public void Awake() 
    {
        start.SetActive(true);
    }

    //Hide start menu and spawn register mode menu
    public void OpenRegisterMode() {
        start.SetActive(false);
        Instantiate(register);
    }

    //Hide start menu and spawn inspection mode menu
    public void OpenInspectionMode() {
        start.SetActive(false);
        Instantiate(inspection);
    }

    //Hide start menu and spawn view mode menu
    public void OpenViewMode() {
        start.SetActive(false);
        Instantiate(view);
    }
}
