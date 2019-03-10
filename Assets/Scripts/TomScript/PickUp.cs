using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    Rigidbody rg;
    bool triggered = false;
    Collider mineral;
    List<Component> mineralHolder;
    float lastCollided = 0;

    private void Awake()
    {
        rg = (Rigidbody)GetComponent("Rigidbody");
        mineralHolder = new List<Component>(); 
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Horizontal") || Input.GetButton("Vertical") || Input.GetButton("Forward"))
        {
            float translateX = Input.GetAxis("Horizontal");
            float translateY = Input.GetAxis("Vertical");
            float translateZ = Input.GetAxis("Forward");
            rg.AddForce(translateX, translateY, translateZ);
        }
        if (triggered)
        {
            if (Time.time - lastCollided > 2)
            {
                mineralHolder.Add(mineral);
                mineral.gameObject.SetActive(false);
                OnTriggerExit(null);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        lastCollided = Time.time;
        mineral = other;
        triggered = true;
    }

    private void OnTriggerExit(Collider other)
    {
        lastCollided = 0;
        mineral = null;
        triggered = false;
    }

    public int getMineralCount() => mineralHolder.Count;

    public Component getMineralWeight(int index) => mineralHolder[index];
}
