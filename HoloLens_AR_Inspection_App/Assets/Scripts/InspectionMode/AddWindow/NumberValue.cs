using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NumberValue : MonoBehaviour
{
    [SerializeField] GameObject numberDisplay;
    public string current;
    string value;

    string colour;
    string colour_;

    public void Update() 
    {
        current = numberDisplay.GetComponent<TextMeshPro>().text;
    }

    public void Number() {
        value = gameObject.GetComponent<TextMeshPro>().text;
        numberDisplay.GetComponent<TextMeshPro>().text = current + value;
    }

    public void Remove() {
        numberDisplay.GetComponent<TextMeshPro>().text = current.Substring(0, current.Length - 1);
    }
}
