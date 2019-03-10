using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeBase : MonoBehaviour
{
    public int startingEnergy;
    public int droneChargeRate;
    public float drainRatePerSecond;

    public int energyDroppedOffMultiplier = 30;
    public float remainingEnergy;
    private float timeLastDrained;

    private Rigidbody rb;
    private AudioSource lowEnergySource;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        lowEnergySource = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        remainingEnergy = startingEnergy;
    }

    // Update is called once per frame
    void Update()
    {
        DrainEnergy();

        //for testing purposes

        if(Input.GetKeyDown("b"))
        {
            remainingEnergy = 0;
        }

        if (remainingEnergy <= 0)
        {
            SceneManager.LoadScene("Game Lose");
        }

        if (remainingEnergy / (float)startingEnergy <= 0.3f)
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

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && other != null)
        {

            other.GetComponentInParent<DroneEnergyTank>().RegenEnergy(droneChargeRate * Time.deltaTime);

            DroneHealth droneHealth = other.GetComponentInParent<DroneHealth>();

            for (int i = 0; i < droneHealth.propellerHealth.Length; i++)
            {

                droneHealth.ThrusterUpdate(droneChargeRate, i);

            }



            HealBase(other.GetComponentInParent<DroneController>().DropOffEnergy());

        }
    }

    //Drains _drainRatePerSecond energy every second.
    private void DrainEnergy()
    {
        remainingEnergy -= drainRatePerSecond * Time.deltaTime;


        if (remainingEnergy <= 0)
        {

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


    public void HealBase(int energy)
    {
        if (remainingEnergy + energy * energyDroppedOffMultiplier > startingEnergy)
        {

            remainingEnergy = startingEnergy;
        }
        else
        {
            remainingEnergy += energy * energyDroppedOffMultiplier;

        }

    }
}
