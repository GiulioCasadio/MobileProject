using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private float speed=70f;
    private float distanceToFire = 40f;
    private float rotationSpeed = 5f;
    private GameObject target;
    private bool isChasing = false;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();    
    }

    private void Update()
    {
        if (isChasing && Vector3.Distance(this.transform.position, target.transform.position) > distanceToFire)
        {
            Vector3 targetDir = (this.transform.position - target.transform.position).normalized;

            rb.AddForce(targetDir * -speed * Time.deltaTime, ForceMode.Force);
            this.gameObject.transform.rotation = Quaternion.RotateTowards(this.gameObject.transform.rotation, Quaternion.LookRotation(targetDir*-1), Time.fixedDeltaTime * rotationSpeed);
        }
        else if (isChasing)
        {
            Vector3 targetDir = Quaternion.Euler(0, -90, 0)*(this.transform.position - target.transform.position).normalized;
            this.gameObject.transform.rotation = Quaternion.RotateTowards(this.gameObject.transform.rotation, Quaternion.LookRotation(targetDir * -1), Time.fixedDeltaTime * rotationSpeed);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name.Equals("ScafoPersonaggio"))
        {
            isChasing = true;
            target = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name.Equals("ScafoPersonaggio"))
        {
            isChasing = false;
        }
    }

}
