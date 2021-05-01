using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Joystick joy;
    public float speed = 20f;
    private float touchSpeed = 5;

    private Rigidbody rb;
    private float horizontalMove = 0f, verticalMove =0f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }

    // Update is called once per frame
    void Update()
    {
        if (joy.Horizontal >= .2f || joy.Horizontal <= .2f)
            horizontalMove = speed * joy.Horizontal;
        else
            horizontalMove = 0;

        if (joy.Vertical >= .2f || joy.Vertical <= .2f)
            verticalMove = speed * joy.Vertical;
        else
            verticalMove = 0;
    }

    private void FixedUpdate()
    {
        rb.MovePosition(new Vector3(rb.position.x + horizontalMove * speed * Time.fixedDeltaTime, 0, rb.position.z + verticalMove * speed * Time.fixedDeltaTime));
    }
}
