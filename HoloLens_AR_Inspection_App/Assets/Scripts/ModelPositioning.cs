using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;

public class ModelPositioning : MonoBehaviour
{
    [SerializeField] GameObject sliderUp;
    float upValue;
    [SerializeField] GameObject sliderRight;
    float rightValue;
    [SerializeField] GameObject sliderForward;
    float forwardValue;

    [SerializeField] GameObject sliderRotUp;
    float upRotValue;
    [SerializeField] GameObject sliderRotRight;
    float rightRotValue;
    [SerializeField] GameObject sliderRotForward;
    float forwardRotValue;

    GameObject imageTarget;
    
    bool sliderUpBool = false;
    bool sliderRightBool = false;
    bool sliderForwardBool = false;
    bool sliderUpRotBool = false;
    bool sliderRightRotBool = false;
    bool sliderForwardRotBool = false;

    Vector3[] startPosition;
    Vector3[] startRotation;

    public float speed = 0.1f;

    public List<Vector3> positionList = new List<Vector3>();
    public List<Quaternion> rotationList = new List<Quaternion>();

    public void Awake() 
    {
        imageTarget = GameObject.Find("ImageTarget");

        positionList.Clear();
        rotationList.Clear();

        foreach(Transform i in imageTarget.transform) {
            positionList.Add(i.position);
            rotationList.Add(i.rotation);
        }

        Debug.Log(positionList.ToString());
    }

    public void Update() 
    {
        foreach(Transform i in imageTarget.transform) {
            //Translate
            if (sliderUpBool == true) {
                upValue = sliderUp.GetComponent<PinchSlider>().SliderValue;
                i.Translate(Vector3.up * (upValue-0.5f) * speed * Time.deltaTime, imageTarget.transform);
            }
            if (sliderRightBool == true) {
                rightValue = sliderRight.GetComponent<PinchSlider>().SliderValue;
                i.Translate(-1 * Vector3.left * (rightValue-0.5f) * speed * Time.deltaTime, imageTarget.transform);
            }
            if (sliderForwardBool == true) {
                forwardValue = sliderForward.GetComponent<PinchSlider>().SliderValue;
                i.Translate(Vector3.forward * (forwardValue-0.5f) * speed * Time.deltaTime, imageTarget.transform);
            }

            //Rotate
            if (sliderUpRotBool == true) {
                upRotValue = sliderRotUp.GetComponent<PinchSlider>().SliderValue;
                i.Rotate(0, speed * (upRotValue-0.5f), 0, Space.World);       //transform.RotateAround(target.transform.position, Vector3.up, 20 * Time.deltaTime);
            }
            if (sliderRightRotBool == true) {
                rightRotValue = sliderRotRight.GetComponent<PinchSlider>().SliderValue;
                i.Rotate(speed * (rightRotValue-0.5f), 0, 0, Space.World);
            }
            if (sliderForwardRotBool == true) {
                forwardRotValue = sliderRotForward.GetComponent<PinchSlider>().SliderValue;
                i.Rotate(0, 0, speed * (forwardRotValue-0.5f), Space.World);
            }
        }
    }

    public void SliderUp() {
        sliderUpBool = true;
    }

    public void SliderRight() {
        sliderRightBool = true;
    }

    public void SliderForward() {
        sliderForwardBool = true;
    }

    public void SliderRotUp() {
        sliderUpRotBool = true;
    }

    public void SliderRotRight() {
        sliderRightRotBool = true;
    }

    public void SliderRotForward() {
        sliderForwardRotBool = true;
    }

    public void Cancel() {
        sliderUpBool = false;
        sliderRightBool = false;
        sliderForwardBool = false;
        sliderUp.GetComponent<PinchSlider>().SliderValue = 0.5f;
        sliderRight.GetComponent<PinchSlider>().SliderValue = 0.5f;
        sliderForward.GetComponent<PinchSlider>().SliderValue = 0.5f;

        sliderUpRotBool = false;
        sliderRightRotBool = false;
        sliderForwardRotBool = false;
        sliderRotUp.GetComponent<PinchSlider>().SliderValue = 0.5f;
        sliderRotRight.GetComponent<PinchSlider>().SliderValue = 0.5f;
        sliderRotForward.GetComponent<PinchSlider>().SliderValue = 0.5f;
    }

    public void Reset() {
        int x = -1;
        foreach(Transform i in imageTarget.transform) {
            x = x + 1;
            i.position = positionList[x];
            i.rotation = rotationList[x];
        }
    }
}
