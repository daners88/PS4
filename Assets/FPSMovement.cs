using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSMovement : MonoBehaviour
{
    public float moveSpeed = 10.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");
        //vertical *= Time.deltaTime;
        //horizontal *= Time.deltaTime;
        GetComponent<Rigidbody>().AddForce(new Vector3(horizontal, 0, vertical) * moveSpeed);
        //transform.Translate(horizontal, 0, vertical);

    }
}
