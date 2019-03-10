using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FindEnergy : MonoBehaviour
{
    GameObject drone, trackerUI;
    GameObject[] myPickups;
    List<GameObject> myActivePickups;
    Color color;
   

    bool ready = true;
    float lastActivate = 0;

    private void Awake()
    {
        drone = GameObject.Find("drone_model");
        trackerUI = GameObject.Find("Tracker");
    }
    // Start is called before the first frame update
    void Start()
    {
        myActivePickups = new List<GameObject>();
        GetActivePickups();
        color = GetComponent<Image>().color;
        color.a = 0;
        GetComponent<Image>().color = color;
    }

    // Update is called once per frame
    void Update()
    {
        color = GetComponent<Image>().color;
        const float cooldown = 9;
        const float duration = 5;
        float time = Time.time;
        if(time - lastActivate > cooldown)
        {
            setReady();
        }
        float input = Input.GetAxis("Ping");
        if (ready && input > 0)
        {
            myActivePickups.Clear();
            GetActivePickups();
            color.a = 1;
            GetComponent<Image>().color = color;
            lastActivate = time;
            setNotReady();
        }
        if(color.a > 0)
        {
            if (time - lastActivate < duration)
                color.a = 1 - (time - lastActivate) / duration;
            else
                color.a = 0;
            GetComponent<Image>().color = color;
        }
        updateArrow();
    }

    public List<GameObject> GetActivePickups()
    {

        myPickups = GameObject.FindGameObjectsWithTag("Pickup");

        for (int i = 0; myPickups.Length > i; i++)
        {

            myPickups[i].GetComponent<Pickup>();
            if (myPickups[i].activeSelf && myPickups[i].GetComponent<Pickup>().isActiveAndEnabled)
            {
                myActivePickups.Add(myPickups[i]);
            }

        }
        if (myActivePickups.Count > 0)
        {
      
            return myActivePickups;

        }
        else
        {

            return null;
        }

    }

    /*
     * Returns the closest pickup location 
     * 
     * */
    public GameObject GetClosestPickUp(List<GameObject> pickups)
    {
        GameObject myClosest = null;
        float minDist = 3000.0f;
        Vector3 currentPos = transform.position;
        for (int i = 0; pickups.Count > i; i++)
        {
            if(pickups[i] == null)
            {
                myActivePickups.Clear();
                GetActivePickups();
                return GetClosestPickUp(myActivePickups);
            }
            float dist = Vector3.Distance(pickups[i].transform.position, currentPos);
            if (dist < minDist)
            {
                myClosest = pickups[i];
                minDist = dist;
            }
        }
        return myClosest;
    }

    void updateArrow()
    {
        if (myActivePickups != null && GetComponent<Image>().color.a > 0)
        {
            GameObject nextPoint = GetClosestPickUp(myActivePickups);
            if (nextPoint == null)
            {
                return;
            }
            Vector3 destVector = nextPoint.transform.position;
            Vector3 droneVector = drone.transform.position;
            Vector3 pointBack = destVector - droneVector;
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

            GetComponent<Image>().color = nextPoint.GetComponent<Light>().color;
            //this changes the color of the pointer to the light source (make sure the light source matches the color of the energy/

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


    void setNotReady()
    {
        ready = false;
        SVGImage icon = trackerUI.GetComponentInChildren<SVGImage>();
        Color iconColor = new Color(50/255f, 70/255f, 70/255f, 105/255f);
        icon.color = iconColor;
        Image iconBackground = trackerUI.GetComponentInChildren<Image>();
        Color backgroundColor = new Color(0, 0, 0, 130/255f);
        iconBackground.color = backgroundColor;
        Text text = trackerUI.GetComponentInChildren<Text>();
        Color textColor = text.color;
        textColor.a = 0.2f;
        text.color = textColor;
    }

    void setReady()
    {
        ready = true;
        SVGImage icon = trackerUI.GetComponentInChildren<SVGImage>();
        Color iconColor = new Color(29 / 255f, 223 / 255f, 231 / 255f, 1);
        icon.color = iconColor;
        Image iconBackground = trackerUI.GetComponentInChildren<Image>();
        Color backgroundColor = new Color(64/255f, 73/255f, 103/255f, 70 / 255f);
        iconBackground.color = backgroundColor;
        Text text = trackerUI.GetComponentInChildren<Text>();
        Color textColor = text.color;
        textColor.a = 1;
        text.color = textColor;
    }
}
