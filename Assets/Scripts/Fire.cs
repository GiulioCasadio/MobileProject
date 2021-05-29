using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public ParticleSystem muzzle;

    public void CannonFire()
    {
        muzzle.Play();
    }
}
