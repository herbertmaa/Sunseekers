using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{

    private Vector3 startingPosition;
    public const float range = 100f;
    public AudioClip pickupFX;

    void OnTriggerEnter(Collider col)
    {



            //col.gameObject.GetComponent<PlayerController>().DealDamage(damage);
            //call PlayerController's GiveEnergy Method of some sort
            if(col.CompareTag("Player"))
                {
                    col.gameObject.GetComponentInParent<AudioSource>().PlayOneShot(pickupFX);
                    col.GetComponentInParent<DroneController>().GetEnergyPickedUp();
                    Despawn();
                }
     




    }




    public void Start()
    {

        //Vector3 position = new Vector3(Random.Range(-range, range), 0, Random.Range(-range, range));
        //startingPosition = position;
    }



    private void Despawn()
    {

        PoolMember pm = this.GetComponent<PoolMember>();

        if (pm != null)
        {
            pm.Despawn(gameObject);

        }
        else
        {

            Destroy(this.gameObject);
        }


    }




}
