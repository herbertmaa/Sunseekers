using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skyFollow : MonoBehaviour
{

    public Transform cam;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        Vector3 newPos = new Vector3(cam.position.x, cam.position.y - 500, cam.position.z);

        transform.position = cam.position;

    }
}
