using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnDroneCanvasController2 : MonoBehaviour
{
    GameObject drone;

    // Start is called before the first frame update
    void Start()
    {
        drone = GameObject.Find("drone_model");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 droneRotation = drone.transform.rotation.eulerAngles;
        GetComponent<Transform>().rotation = Quaternion.Euler(0, droneRotation.y, 0);
    }
}
