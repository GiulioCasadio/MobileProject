using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipManager : MonoBehaviour
{
    public int shipLife;
    public HealthBar healthBar;

    private void Start()
    {
        healthBar.SetHealth(shipLife);    
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name.Equals("Proiettile(Clone)"))
        {
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
