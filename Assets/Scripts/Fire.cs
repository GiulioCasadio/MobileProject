using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public ParticleSystem muzzle;
    public GameObject proiettile;
    public float fireSpeed;

    [FMODUnity.EventRef]
    public string strevent;
    private FMOD.Studio.EventInstance playerShot;

    private void Start()
    {
        playerShot = FMODUnity.RuntimeManager.CreateInstance(strevent);
    }

    public void CannonFire()
    {
        muzzle.Play();
        playerShot.start();
        //FMODUnity.RuntimeManager.PlayOneShot(Event);
        GameObject p;
        p=Instantiate(proiettile, this.transform.position, Quaternion.identity);
        p.transform.GetComponent<Rigidbody>().AddForce((this.transform.right * -1) * fireSpeed * Time.deltaTime, ForceMode.VelocityChange);
    }
}
