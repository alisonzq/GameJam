using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class lockPicking : MonoBehaviour
{

    float sliderWidth = 20.0f; //how wide the slider is
    float checkLocation; //the location of the check location
    public float checkAreaWidth = 10.0f; //how wide the check area is
    public GameObject prefab;
    private Slider slider;
    Boolean isPlaying = true;


    // Start is called before the first frame update
    void Start()
    {
        isPlaying = true;
        slider = transform.GetChild(0).GetComponent<Slider>();
        sliderWidth = slider.transform.GetComponent<RectTransform>().rect.width /2;
        checkAreaWidth = (prefab.transform.GetComponent<RectTransform>().rect.width / 2);


        setPosition();

    }

    // Update is called once per frame
    void Update()
    {
        slider.value = Mathf.PingPong(Time.time, slider.maxValue); //bounces the slider

        if (Input.GetKeyDown(KeyCode.Space) && isPlaying) {
            checkPosition();
        }
    }

    // set the check position
    private void setPosition() 
    {
        

        checkLocation = Random.Range(-sliderWidth, sliderWidth); // get random point based on slider
        var position = new Vector3(checkLocation, (prefab.transform.GetComponent<RectTransform>().rect.height/2), 0); // create Vector3 based on random pos
        prefab = Instantiate(prefab, position, Quaternion.identity); // create arrow at pos
        prefab.transform.SetParent(this.transform); //set canvas as parent
        prefab.transform.SetLocalPositionAndRotation(position, Quaternion.identity); //set pos to local 

    }


    //check position reaction
    private void checkPosition()
    {
        float sliderPos = ((slider.value-0.5f) * sliderWidth * 2);
        Debug.Log((checkLocation - checkAreaWidth) + " < slider.value : " + sliderPos + " < " + (checkLocation + checkAreaWidth));
        if ((sliderPos > (checkLocation - checkAreaWidth)) && (sliderPos < (checkLocation + checkAreaWidth))) {
            Debug.Log("Sucess!!!!!!!!!!!");
            isPlaying = false;
        }

    }

}
