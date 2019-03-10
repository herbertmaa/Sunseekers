using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class CameraFollow : MonoBehaviour { 

    public Transform cameraTarget;

    public float stickSens;
    public float cameraTilt = 20;

    float lookX;
    float lookY;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        lookY = lookY % 360;
        lookX = Mathf.Clamp(lookX, 0, 120);

        lookY += Input.GetAxis("Mouse X");
        lookX += Input.GetAxis("Mouse Y");

        lookY += Input.GetAxis("CTRL X") * stickSens;
        lookX += -Input.GetAxis("CTRL Y") * stickSens;

        transform.position = Vector3.Lerp(transform.position, cameraTarget.position, 0.5f);

        transform.rotation = Quaternion.Euler(lookX - cameraTilt, lookY, 0);



    }
}
