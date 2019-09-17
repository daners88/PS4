using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    Vector2 mouseLook;
    Vector2 smoother;
    public float sensitivity = 5.0f;
    public float smoothing = 2.0f;
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = this.transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mouseMovement = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        mouseMovement = Vector2.Scale(mouseMovement, new Vector2(sensitivity * smoothing, sensitivity * smoothing));

        smoother.x = Mathf.Lerp(smoother.x, mouseMovement.x, 1.0f / smoothing);
        smoother.y = Mathf.Lerp(smoother.y, mouseMovement.y, 1.0f / smoothing);
        mouseLook += smoother;

        transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right);
        player.transform.localRotation = Quaternion.AngleAxis(mouseLook.x, player.transform.up);
    }
}
