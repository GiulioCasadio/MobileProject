using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public ParticleSystem muzzle;
    public GameObject proiettile;
    public float fireSpeed;

    public void CannonFire()
    {
        muzzle.Play();
        GameObject p;
        p=Instantiate(proiettile, this.transform.position, Quaternion.identity);
        p.transform.GetComponent<Rigidbody>().AddForce((this.transform.right * -1) * fireSpeed * Time.deltaTime, ForceMode.Force);
    }
}
