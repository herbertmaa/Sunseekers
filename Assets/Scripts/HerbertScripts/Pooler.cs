using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pooler : MonoBehaviour
{


    private Stack<GameObject> m_FreeInstances;
    public GameObject myObstacle;
    public int stackSize;
    //public float spawnRadius = 200.0f;



    public void Start()
    {

        m_FreeInstances = new Stack<GameObject>(stackSize);

        for (int i = 0; stackSize > i; i++)
        {

            //center looks prettier

            //Vector3 randomLocation = UnityEngine.Random.insideUnitSphere*spawnRadius;

            //randomLocation.y = transform.position.y;

            GameObject obj = Instantiate(myObstacle,transform.position, Quaternion.identity);

            obj.SetActive(false);
            obj.GetComponent<Pickup>().enabled = false;
            m_FreeInstances.Push(obj);

            // Add a PoolMember component so we know what pool
            // we belong to.
            obj.AddComponent<PoolMember>().myPool = this;

        }


    }


    public GameObject GetPickup()
    {

        GameObject myPickup = null;

        if (m_FreeInstances.Count > 0)
        {
            myPickup = m_FreeInstances.Pop();
            myPickup.SetActive(true);
            myPickup.GetComponent<Pickup>().enabled = true;


        }
        return myPickup;

    }


    public void PushPickup(GameObject obj)

    {
        obj.transform.SetParent(null);
        obj.SetActive(false);
        obj.GetComponent<Pickup>().enabled = false;
        m_FreeInstances.Push(obj);


    }






}
