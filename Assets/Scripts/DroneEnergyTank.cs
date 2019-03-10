using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class DroneEnergyTank : MonoBehaviour
{
    public Image energyBar;

    public int startingEnergy;
    public float drainRateModifier;
    public float baseDrainRate;

    public Transform chargeParticleParent;

    public float remainingEnergy;
    private float timeLastDrained;
    private bool onBase;
    public AudioSource lowEnergySource;
    // Start is called before the first frame update
    void Start()
    {
        remainingEnergy = startingEnergy;
        chargeParticleParent.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        //for debugging purposes
        if (Input.GetKeyDown(KeyCode.L))
        {
            remainingEnergy = 0;

        }
        if (!onBase)
        {
            StaticDrainEnergy();
        }
        else
        {

        }
//        energyBar.fillAmount = remainingEnergy / startingEnergy; null reference exception error
        if (remainingEnergy <= 0)
        {
            GetComponent<DroneController>().enabled = false;
        }
        if (remainingEnergy / (float) startingEnergy <= 0.3f)
        {
            if (!lowEnergySource.isPlaying)
            {
                lowEnergySource.Play();
            }
        }
        else
        {
            lowEnergySource.Stop();
        }
    }

    //Drains _drainRatePerSecond energy every second.
    private void StaticDrainEnergy()
    {
        if (Time.time > timeLastDrained + 1f)
        {
            remainingEnergy -= (baseDrainRate + drainRateModifier);
            timeLastDrained = Time.time;
        }


        if (remainingEnergy <= 0)
        {
            //Debug.Log("Main Base Destroyed");
        }
    }

    public void RegenEnergy(float energy)
    {
        remainingEnergy += energy;
        if (remainingEnergy > startingEnergy)
        {
            remainingEnergy = startingEnergy;
        }
    }

    private void OnCollisionStay(Collision other)
    {
        onBase = other.collider.transform.CompareTag("Home Base");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Home Base"))
        {
            chargeParticleParent.gameObject.SetActive(true);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.CompareTag("Home Base"))
        {
            chargeParticleParent.gameObject.SetActive(false);
        }
    }

    /** herbert's temporary drainenergy method() **/
    public void DrainEnergy(float height)
    {

        if (remainingEnergy > 0)
        {
            remainingEnergy -= (baseDrainRate * drainRateModifier *  height * 0.1f);
        }
        else
        {

        }

    }
}
