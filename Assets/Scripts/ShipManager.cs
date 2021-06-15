using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipManager : MonoBehaviour
{
    [Header("Health")]
    public int shipLife;
    public HealthBar healthBar;
    private int maxHealth;

    [Header("Particles")]
    public GameObject explosion;
    public ParticleSystem[] fire;
    private bool isOnFire=false;

    private void Start()
    {
        healthBar.SetHealth(shipLife);
        maxHealth = shipLife;
    }

    private void Update()
    {
        if (!isOnFire && shipLife<=maxHealth/2)
        {
            isOnFire = true;
            foreach(ParticleSystem p in fire){
                p.Play();
            }
        }    
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name.Equals("Proiettile(Clone)"))
        {
            GameObject e;
            e = Instantiate(explosion, other.transform.position, Quaternion.identity);
            e.GetComponent<ParticleSystem>().Play();
            Destroy(other.gameObject);
            shipLife--;
            healthBar.SetHealth(shipLife);
            if (shipLife == 0)
            {
                this.GetComponent<Animator>().SetTrigger("affonda");
            }
        }
    }

    public void Affonda()
    {
        Destroy(this.gameObject.transform.parent.gameObject);
    }
}
