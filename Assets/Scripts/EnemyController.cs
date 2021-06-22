using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed;
    public float distanceToFire;
    public float rotationSpeed;
    public GameObject[] cannons;
    private GameObject target;
    private bool isChasing = false;
    private Rigidbody rb;
    public float timeToFire = 5f;
    private float recharge;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        recharge = timeToFire;
    }

    private void Update()
    {
        if (!GameObject.Find("CanvasUI").GetComponent<MenuManager>().GetStatusPause())
        {
            if (isChasing && Vector3.Distance(this.transform.position, target.transform.position) > distanceToFire)
            {
                Vector3 targetDir = (this.transform.position - target.transform.position).normalized;

                rb.AddForce(targetDir * -speed * Time.deltaTime, ForceMode.Force);
                this.gameObject.transform.rotation = Quaternion.RotateTowards(this.gameObject.transform.rotation, Quaternion.LookRotation(-targetDir), Time.fixedDeltaTime * rotationSpeed);
            }
            else if (isChasing)
            {
                Vector3 targetDir = Quaternion.Euler(0, -90, 0) * (this.transform.position - target.transform.position).normalized;
                if (Quaternion.LookRotation(-targetDir) == this.gameObject.transform.rotation || Quaternion.LookRotation(targetDir) == this.gameObject.transform.rotation)
                {
                    Ray ray;
                    RaycastHit hit;
                    foreach (GameObject c in cannons)
                    {
                        ray = new Ray(c.transform.position, -c.transform.right);

                        if (Physics.Raycast(ray, out hit, 30) &&
                            hit.collider.name == "ScafoPersonaggio" &&
                            recharge <= 0 &&
                            gameObject.GetComponentInChildren<ShipManager>().shipLife > 0)
                        {
                            c.transform.GetComponent<Fire>().CannonFire();
                            recharge = timeToFire;
                        }
                    }

                }
                else
                {
                    if (Quaternion.Angle(this.gameObject.transform.rotation, Quaternion.LookRotation(targetDir)) < Quaternion.Angle(this.gameObject.transform.rotation, Quaternion.LookRotation(-targetDir)))
                        this.gameObject.transform.rotation = Quaternion.RotateTowards(this.gameObject.transform.rotation, Quaternion.LookRotation(targetDir), Time.deltaTime * rotationSpeed);
                    else
                        this.gameObject.transform.rotation = Quaternion.RotateTowards(this.gameObject.transform.rotation, Quaternion.LookRotation(-targetDir), Time.deltaTime * rotationSpeed);
                }
            }
            recharge -= Time.deltaTime;
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
