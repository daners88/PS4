using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController _controller;
    public float moveSpeed = 10.0f;
    Transform playerPos;
    private float rotateDegrees = 90.0f;
    Vector3 move = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion thing = transform.localRotation;
        if(thing.eulerAngles.y == 0.0)
        {
            move = new Vector3(0, 0, Input.GetAxis("Vertical"));
        }
        else if(thing.eulerAngles.y == 90.0)
        {
            move = new Vector3(Input.GetAxis("Vertical"), 0, 0);
        }
        else if(thing.eulerAngles.y == 270.0)
        {
            move = new Vector3(-Input.GetAxis("Vertical"), 0, 0);
        }
        else if (thing.eulerAngles.y == 180.0)
        {
            move = new Vector3(0, 0, -Input.GetAxis("Vertical"));
        }
        move *= moveSpeed;
        _controller.Move(move * Time.deltaTime);
        if (Input.GetKeyUp(KeyCode.A))
        {
            Rotate(-rotateDegrees);
        }
        else if(Input.GetKeyUp(KeyCode.D))
        {
            Rotate(rotateDegrees);
        }
       
        
    }

    void Rotate(float degrees)
    {
        var startRotation = transform.rotation;
        var startPosition = transform.position;
        transform.RotateAround(transform.position, Vector3.up, degrees);
        var endRotation = transform.rotation;
        var endPosition = transform.position;
        transform.rotation = startRotation;
        transform.position = startPosition;
        transform.rotation = endRotation;
        transform.position = endPosition;
    }
}
