using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrusterHealth : MonoBehaviour
{

    public int thrusterID;
    public DroneHealth drone;
    public float collisionThreshold;
    public float damageFactor;

    public Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {

        //rb = GetComponentInParent<Rigidbody>();
        drone = GetComponentInParent<DroneHealth>();
        
    }

    // Update is called once per frame
    void Update()
    {

    }



    void OnCollisionEnter(Collision col)
    {

        float colForce = drone.rb.velocity.magnitude;

        print(colForce);

        if (colForce > collisionThreshold)
        {


            drone.ThrusterUpdate (-Mathf.RoundToInt(colForce * damageFactor), thrusterID);

            rb.AddExplosionForce(colForce * 10, col.GetContact(0).point, 10);

            print("Hit " + thrusterID + " " + colForce);

        }
        

    }
}
