using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FindWayHome : MonoBehaviour
{
    GameObject drone, baseObject;


    private void Awake()
    {
        drone = GameObject.Find("drone_model");
        baseObject = GameObject.Find("Charging Station");
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    //float currentTime = 0f;

    // Update is called once per frame
    void Update()
    {
        Vector3 homeVector = baseObject.transform.position;
        Vector3 droneVector = drone.transform.position;
        Vector3 pointBack = homeVector - droneVector;
        Vector3 forwardVector = drone.transform.forward * -1;

        pointBack.y = forwardVector.y;

        //currentTime += Time.deltaTime;
        //if (currentTime > 2f)
        //{
        //    Debug.DrawRay(drone.transform.position, pointBack, Color.blue, 2f);
        //    Debug.DrawRay(drone.transform.position, forwardVector, Color.red, 2f);
        //    currentTime = 0f;
        //}


        float angle = Vector3.SignedAngle(forwardVector, pointBack, Vector3.up);
        //Debug.Log("ANGLE : " + angle);


        GetComponent<RectTransform>().localRotation = Quaternion.Euler(0, 0, angle);


        float distance = pointBack.magnitude;
        const int minDistance = 10;
        if (distance <= minDistance)
        {
            Color color = GetComponent<Image>().color;
            color.a = distance / minDistance;
            GetComponent<Image>().color = color;
        }
        else
        {
            Color color = GetComponent<Image>().color;
            color.a = 1;
            GetComponent<Image>().color = color;
        }
    }
}
