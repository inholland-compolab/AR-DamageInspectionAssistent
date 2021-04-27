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

    public void Awake() 
    {
        imageTarget = GameObject.Find("ImageTarget");
    }

    public void Update() 
    {
        foreach(Transform i in imageTarget.transform) {
            //Translate
            if (sliderUpBool == true) {
                upValue = sliderUp.GetComponent<PinchSlider>().SliderValue;
                i.Translate(Vector3.up * (upValue-0.5f) * Time.deltaTime);
            }
            if (sliderRightBool == true) {
                rightValue = sliderRight.GetComponent<PinchSlider>().SliderValue;
                i.Translate(-1 * Vector3.left * (rightValue-0.5f) * Time.deltaTime);
            }
            if (sliderForwardBool == true) {
                forwardValue = sliderForward.GetComponent<PinchSlider>().SliderValue;
                i.Translate(Vector3.forward * (forwardValue-0.5f) * Time.deltaTime);
            }

            //Rotate
            if (sliderUpRotBool == true) {
                upRotValue = sliderUp.GetComponent<PinchSlider>().SliderValue;
                i.Rotate(Vector3.up * (upRotValue-0.5f) * Time.deltaTime);       //transform.RotateAround(target.transform.position, Vector3.up, 20 * Time.deltaTime);
            }
            if (sliderRightRotBool == true) {
                rightRotValue = sliderRight.GetComponent<PinchSlider>().SliderValue;
                i.Rotate(-1 * Vector3.left * (rightRotValue-0.5f) * Time.deltaTime);
            }
            if (sliderForwardRotBool == true) {
                forwardRotValue = sliderForward.GetComponent<PinchSlider>().SliderValue;
                i.Rotate(Vector3.forward * (forwardRotValue-0.5f) * Time.deltaTime);
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
}
