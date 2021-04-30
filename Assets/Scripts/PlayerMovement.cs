using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 20f;
    private float touchSpeed = 5;

    public Joystick joy;

    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }

    // Update is called once per frame
    void Update()
    {
        touchSpeed = joy.Horizontal * speed;

        /*Vector3 gravity = new Vector3();

        if (Input.GetKeyDown(KeyCode.Escape))   // Chiusura app
            Application.Quit();

        if (Input.touchCount > 0)   // Uso touch
        {
            gravity.x = gravity.x = gravity.x = 0;
            Touch touch = Input.GetTouch(0);

            if (touch.position.x < Screen.width / 2)
                gravity.x = touchSpeed;
            else
                gravity.x = -touchSpeed;
            if (touch.position.y < Screen.height / 2)
                gravity.z = touchSpeed;
            else
                gravity.z = -touchSpeed;
        }*/
    }
}
