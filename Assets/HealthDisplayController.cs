using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplayController : MonoBehaviour
{
    GameObject TL, TR, BL, BR, drone;
    public Color dangerColor;
    public Sprite dangerGraphic;

    private void Awake()
    {
        TL = GameObject.Find("TL");
        TR = GameObject.Find("TR");
        BL = GameObject.Find("BL");
        BR = GameObject.Find("BR");
        drone = GameObject.Find("drone_model");
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DroneHealth healthObj = drone.GetComponent<DroneHealth>();
        float currentRatio = healthObj.health / healthObj.maxHealth;
        if (currentRatio < 1)
            if(currentRatio >= 0.75)
            {
                float value = (currentRatio - 0.75f) / 0.25f;
                setIndicatorAmount(TL, value);
            } else if (currentRatio >= 0.5)
            {
                float value = (currentRatio - 0.5f) / 0.25f;
                setIndicatorAmount(TL, 0);
                setIndicatorAmount(BL, value);
            } else if (currentRatio >= 0.25)
            {
                float value = (currentRatio - 0.25f) / 0.25f;
                setIndicatorAmount(TL, 0);
                setIndicatorAmount(BL, 0);
                setIndicatorAmount(BR, value);
            } else
            {
                float value = currentRatio / 0.25f;
                setIndicatorAmount(TL, 0);
                setIndicatorAmount(BL, 0);
                setIndicatorAmount(BR, 0);
                setIndicatorAmount(TR, value);
            }
        if (currentRatio < 0.20)
        {
            TL.GetComponent<Image>().color = dangerColor;
            BL.GetComponent<Image>().color = dangerColor;
            TR.GetComponent<Image>().color = dangerColor;
            BR.GetComponent<Image>().color = dangerColor;
            GetComponentInParent<SVGImage>().sprite = dangerGraphic;
        }
    }

    void setIndicatorAmount(GameObject target, float value)
    {
        target.GetComponent<Image>().fillAmount = value;
    }

}
