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
        //Search prefabs and make array
        UnityEngine.Object[] prefabsInFolder = Resources.LoadAll("3DModels", typeof(GameObject));       //Get all prefabs of Resources folder into an array
        
        //read array
        for (int i = 0; i < prefabsInFolder.Length; i++) {          //Get all names of the prefabs in Resources folder
            prefabList.Add(prefabsInFolder[i].name);
        }
        
        //Make dropdown list
        dropdown = dropdownObject.GetComponent<TMP_Dropdown>();
        dropdown.ClearOptions();
        startList.Add("Choose");                                    //Instruction at first value of the list
        dropdown.AddOptions(startList);                             //Set first value of dropdown
        dropdown.AddOptions(prefabList);                            //Add possible choices
    }

    //Load and spawn prefab
    public void Spawn() {
        if (dropdownChange == true) {
            if (dropdown.value-1 >= 0) {                                //Prevent List position of -1
                selectionPrefab = prefabList[dropdown.value-1];         //Get name of prefab at corresponding dropdown value

                var prefab = Resources.Load(selectionPrefab);           //Load prefab, search for name within Resources folder
                selectedPrefab = prefab as GameObject;

                Instantiate(selectedPrefab, transform.position, transform.rotation);        //Spawn prefab
                dropdownChange = false;
            }
        }
    }

    //Get information of dropdown selection change
    public void DropdownChange() {
        dropdownChange = true;
    }
}
