using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnDroneCanvasController : MonoBehaviour
{
    GameObject drone;

    private void Awake()
    {
        drone = GameObject.Find("drone_model");
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 droneRotation = drone.transform.rotation.eulerAngles;
        GetComponent<Transform>().rotation = Quaternion.Euler(0, droneRotation.y, droneRotation.z);
    }
}
