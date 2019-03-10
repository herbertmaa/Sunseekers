using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyIndicatorController : MonoBehaviour
{
    GameObject indicator;
    DroneEnergyTank energyTank;
    int startingEnergy;
    float remainingEnergy;

    private void Awake()
    {
        energyTank = GetComponent<DroneEnergyTank>();
        indicator = GameObject.Find("EnergyBar");
    }

    // Start is called before the first frame update
    void Start()
    {
        startingEnergy = energyTank.startingEnergy;
    }

    // Update is called once per frame
    void Update()
    {
        remainingEnergy = energyTank.remainingEnergy;
        indicator.GetComponent<Image>().fillAmount = remainingEnergy / startingEnergy;
    }
}
