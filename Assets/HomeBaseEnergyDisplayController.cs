using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HomeBaseEnergyDisplayController : MonoBehaviour
{
    GameObject homebase;
    HomeBase energyObj;

    private void Awake()
    {
        homebase = GameObject.Find("Charging Station");
        energyObj = homebase.GetComponent<HomeBase>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float startingEnergy = energyObj.startingEnergy;
        float currentEnergy = energyObj.remainingEnergy;
        GetComponent<Image>().fillAmount = currentEnergy / startingEnergy;
    }
}
