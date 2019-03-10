using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    private float timeBetweenSpawn;
    public float startTimeBetweenSpawn;
    public GameObject[] obstaclePattern;

    // Start is called before the first frame update
    void Start()
    {
        timeBetweenSpawn = startTimeBetweenSpawn;
    }

    private void Update()
    {

        //chooses between a random pool to spawn

        if (timeBetweenSpawn <= 0)

        {
            int rand = Random.Range(0, obstaclePattern.Length);
            timeBetweenSpawn = startTimeBetweenSpawn;
            Pooler myPool = obstaclePattern[rand].GetComponent<Pooler>();
            myPool.GetPickup();


        }
        else
        {

            timeBetweenSpawn -= Time.deltaTime;


        }

   

    }
}
