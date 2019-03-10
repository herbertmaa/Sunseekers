using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DroneController : MonoBehaviour
{

    public LayerMask ground;
    public Transform landingGear;
    public bool landed;


    Rigidbody rb;

    public Transform camera;

    public float pitchFactor = 15;
    public float droneHeight;
    public float bestHeight = 10;


    public Transform[] propellers;
    public ParticleSystem[] thrusterSystems;

    public float fallThrust;
    public float verticalThrust;
    public float lateralThrust;
    public float heightLimit = 5.5f;
    private int energyCollected = 0;

    public AudioSource thrusterSource;

    DroneHealth health;


    Vector3 joystickInput = new Vector3();

    private DroneEnergyTank energyTank;

    void Awake()
    {
        health = GetComponent<DroneHealth>();
        energyTank = GetComponent<DroneEnergyTank>();
    }

    // Start is called before the first frame update
    void Start()
    {

        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        UpdateThrusterLength();
    }

    // Update is called once per frame
    void FixedUpdate()
    {


        landed = Physics.CheckSphere(landingGear.position, 0.15f, ground);

        //Tracking Horizontal Input
        joystickInput = new Vector3(-Input.GetAxis("Horizontal"), 0 , -Input.GetAxis("Vertical"));

        //print(droneHeight);

        Quaternion newRotation = Quaternion.Euler(joystickInput.z * pitchFactor, camera.eulerAngles.y, -joystickInput.x * pitchFactor);

        // Quaternion camRot = Quaternion.Euler(0, camera.eulerAngles.y, 0);

        if (!landed) rb.rotation = Quaternion.Lerp(transform.rotation, newRotation, 0.1f);


        fall(); 

        if(this.GetComponentInParent<DroneEnergyTank>().remainingEnergy > 0)
        {
            Debug.Log("remaining energy" + this.GetComponentInParent<DroneEnergyTank>().remainingEnergy);
            thrust();
        }

        PlayThrustSound();

        
    }

    void PlayThrustSound() {
        if (Input.GetAxisRaw("Thrust") > 0 && !thrusterSource.isPlaying)
        {
            thrusterSource.Play();
        }

        if (Input.GetAxisRaw("Thrust") == 0)
        {
            thrusterSource.Stop();
        }
    }

    /* HMA - Added a burner for when the player thrusts the drone up, 
     * energy is reduced (this game mechanic limits how high the player can go on our terrain */

    void UpdateThrusterLength()
    {
        
        foreach (ParticleSystem p in thrusterSystems)
        {
            if (Input.GetAxisRaw("Thrust") > 0)
            {
                p.startSpeed = 1.4f;
            }
            else
            {
                p.startSpeed = Mathf.Clamp(joystickInput.magnitude, 0f, 1.4f);
            }
            
        }
    }


    void fall()
    {

        for (int i = 0; i < propellers.Length; i++)
        {


            RaycastHit hit;

            Physics.Raycast(new Ray(propellers[i].position, Vector3.down), out hit, ground);

            droneHeight = hit.distance;



            rb.AddForceAtPosition(transform.up * fallThrust * Mathf.Abs(Input.GetAxisRaw("Thrust")), propellers[i].position);
            // rb.AddForceAtPosition (transform.up * droneHeight / bestHeight * Mathf.Abs(Input.GetAxisRaw("Thrust")), propellers[i].position);
            //propellerUI[i].fillAmount = droneHeight / bestHeight;



        }

    } 

    void thrust()
    {
        Vector3 joyInputToWorld = transform.TransformDirection(joystickInput);
        rb.AddForce(joyInputToWorld * lateralThrust);

        if(Input.GetAxisRaw("Thrust") > 0.0f)
        {
            for (int i = 0; i < propellers.Length; i++)
            {


                RaycastHit hit;

                Physics.Raycast(new Ray(propellers[i].position, Vector3.down), out hit, ground);

                droneHeight = hit.distance;



                rb.AddForceAtPosition(transform.up * verticalThrust * Mathf.Abs(Input.GetAxisRaw("Thrust")) / propellers.Length, propellers[i].position);
                // rb.AddForceAtPosition (transform.up * droneHeight / bestHeight * Mathf.Abs(Input.GetAxisRaw("Thrust")), propellers[i].position);
                //propellerUI[i].fillAmount = droneHeight / bestHeight;

            }
            // this will need to be updated so it drains more when the player is at a higher altitude

            if (transform.position.y > heightLimit)
            {
                energyTank.DrainEnergy(energyTank.baseDrainRate);
            }

        }


    } //added by herbert

    private void OnCollisionEnter(Collision collision)
    {
        if(!(collision.gameObject.CompareTag("Pickup") || collision.gameObject.CompareTag("Home Base")))
            // we only have two objects that can be interacted with so far the pickup object and the home base object
        {

            DamageDrone();

        }
    }

    public void DamageDrone()
    {


            
    
    }



    public void GetEnergyPickedUp()

    {

        energyCollected++;
    }


    public int DropOffEnergy()
    {
        int totalEnergy = 0;

        if(energyCollected > 0)
        {

            totalEnergy = energyCollected;
            energyCollected = 0;


        }

        return totalEnergy;
    
    }





}





    [System.Serializable]
    public class Propeller
    {

        public int hp;



    }


