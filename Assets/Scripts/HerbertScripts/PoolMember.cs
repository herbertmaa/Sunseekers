using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolMember : MonoBehaviour
{

    public Pooler myPool;
    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {

    }



    /// <summary>
    /// Despawn the specified gameobject back into its pool.
    /// </summary>
    public void Despawn(GameObject obj)
    {
        PoolMember pm = obj.GetComponent<PoolMember>();
        if (pm == null)
        {
            Destroy(obj); // if the object does not belong to any pool then we should delete it
        }
        else
        {
            pm.myPool.PushPickup(obj);
            Invoke("Spawn", 5.0f);
        }
    }


    public void Spawn()
    {
        myPool.GetPickup();
    }
}
