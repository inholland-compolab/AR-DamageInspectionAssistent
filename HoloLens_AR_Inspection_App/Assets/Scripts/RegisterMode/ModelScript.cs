using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using TMPro;

public class ModelScript : MonoBehaviour
{
    GameObject selectedPrefab;
    string selectionPrefab;

    bool dropdownChange = false;

    UnityEngine.Object[] prefabsInFolder;

    [SerializeField] GameObject dropdownObject;
    TMP_Dropdown dropdown;
    
    public List<string> startList = new List<string>();
    public List<string> prefabList = new List<string>();


    public void Awake()
    {   
        //Make an array from all prefabs within the Resources folder
        UnityEngine.Object[] prefabsInFolder = Resources.LoadAll("3DModels", typeof(GameObject));
        
        //Read array to get all names of the 3Dmodels in Resources folder
        for (int i = 0; i < prefabsInFolder.Length; i++) {
            prefabList.Add(prefabsInFolder[i].name);
        }
        
        //Make dropdown list
        dropdown = dropdownObject.GetComponent<TMP_Dropdown>();
        dropdown.ClearOptions();
        startList.Add("Choose");                //Instruction at first value of the list
        dropdown.AddOptions(startList);         //Set first value of dropdown
        dropdown.AddOptions(prefabList);        //Add possible choices from prefab list
    }
}
