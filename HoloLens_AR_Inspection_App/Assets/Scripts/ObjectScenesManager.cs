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

    public void Awake() 
    {
        start.SetActive(true);
    }

    // public void OpenStartScreen() {
    //     GameObject imageTarget = GameObject.Find("ImageTarget");
    //     foreach (Transform i in imageTarget.transform) {
    //         Destroy(i.gameObject);
    //     }
    // }

    public void OpenRegisterMode() {
        start.SetActive(false);
        Instantiate(register);
    }

    public void OpenInspectionMode() {
        start.SetActive(false);
        Instantiate(inspection);
    }

    public void OpenViewMode() {
        start.SetActive(false);
        Instantiate(view);
    }
}
