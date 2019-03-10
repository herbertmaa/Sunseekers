using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{

    public float fallSpeed = 8.0f;
    public float spinSpeed = 250.0f;
    public Vector3 asteroidAngle;


    // Start is called before the first frame update
    void Start()
    {
        asteroidAngle = Vector3.down + Random.insideUnitSphere/5;


    }

    // Update is called once per frame
     void Update() {

        transform.Translate(asteroidAngle * fallSpeed * Time.deltaTime, Space.World);
        transform.Rotate(Vector3.forward, spinSpeed * Time.deltaTime);
 
     }


    private void OnTriggerEnter(Collider other)
    {
        Destroy(this.gameObject);
    }



}
