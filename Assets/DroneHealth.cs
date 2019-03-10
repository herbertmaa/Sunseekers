using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.PostProcessing;

public class DroneHealth : MonoBehaviour
{

    public PostProcessVolume pp;

    public int maxPropellerHealth;
    public Image[] propellerUI;
    public int[] propellerHealth = new int[4];

    public int baseDamage;
    public float damageFactor;

    public float maxHealth;
    public float health;


    public float collisionThreshold;
    public float velocityThreshold;

    public Rigidbody rb;
    public ParticleSystem[] damageSystems;
    public ParticleSystem deathSystem;
    public AudioClip[] impactAudios;
    public AudioClip[] explosionAudios;
    public AudioClip lowHealthMusic;
    private AudioSource audioSource;
    public AudioSource bgSource;
    private float timeLastDamage = 0.0f;
    private float damageCoolDown;
    public bool exploded = false;
    // Start is called before the first frame update
    void Start()
    {
        deathSystem.gameObject.SetActive(false);
        audioSource = GetComponent<AudioSource>();
        damageCoolDown = 0.25f;
        rb = GetComponent<Rigidbody>();


        foreach (ParticleSystem p in damageSystems)
        {
            p.gameObject.SetActive(false);
        }

        for (int i = 0; i < propellerHealth.Length; i++) {

            propellerHealth[i] = maxPropellerHealth;

        }
        health = maxHealth;
    }


    private void Update()
    {
        if (health / maxHealth <= 0.40f)
        {
            bgSource.clip = lowHealthMusic;
            if (!bgSource.isPlaying)
            {
                bgSource.Play();
            }
            
            foreach(ParticleSystem p in damageSystems)
            {
                p.gameObject.SetActive(true);
            }
        }


        if (health <= 0)
        {
            deathSystem.gameObject.SetActive(true);
            int randomExplosion = Random.Range(0, explosionAudios.Length - 1);
            if (!audioSource.isPlaying && !exploded)
            {
                GetComponent<DroneController>().enabled = false;
                exploded = true;
                audioSource.PlayOneShot(explosionAudios[randomExplosion]);
            }
            Invoke("LoadLose", 3f);


            //pp.weight += 0.1f;
            


        }
        //for (int i = 0; i < propellerHealth.Length; i++)
        //{

        //    propellerUI [i].fillAmount = (float) propellerHealth[i] / maxPropellerHealth;

        //}

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (rb.velocity.magnitude > velocityThreshold && Time.time > timeLastDamage + damageCoolDown)
        {
            timeLastDamage = Time.time;
            health -= baseDamage + Mathf.Clamp((rb.velocity.magnitude), 0, 2f);
            int randomImpact = Random.Range(0, impactAudios.Length -1);
            audioSource.PlayOneShot(impactAudios[randomImpact]);
        }
    }


    /* void OnCollisionEnter(Collision col)
     {

         //if (col.relativeVelocity.magnitude > collisionThreshold)
         //print(col.relativeVelocity.magnitude);


         /*for (int i = 0; i < propellerHealth.Length; i++)
         {

             propellerHealth[i] -= Mathf.RoundToInt (col.relativeVelocity.magnitude / 5 * damageFactor);

             rb.AddExplosionForce(col.relativeVelocity.magnitude, col.GetContact(0).point, 10);


         }

     }*/


    public void ThrusterUpdate(int value, int id) {

        propellerHealth[id] += value;

        if (propellerHealth[id] > maxPropellerHealth) propellerHealth[id] = maxPropellerHealth;

        if (propellerHealth[id] < 0) print("DEAD");
    }

    private void LoadLose()
    {
        SceneManager.LoadScene("Game Lose");
    }


}
