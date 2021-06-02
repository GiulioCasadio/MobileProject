using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public ParticleSystem muzzle;

    public void CannonFire()
    {
        muzzle.Play();

        RaycastHit hit;
        if (Physics.Raycast(this.transform.position, this.transform.right, out hit))
        {
            if (hit.transform.name.Equals("EnemyShip"))
            {
                Debug.Log("Plof!");
            }
        }
    }
}
