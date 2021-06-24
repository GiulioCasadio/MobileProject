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

    [Header("Bonus")]
    public GameObject[] bonus;

    [FMODUnity.EventRef]
    public string EventOnFire;


    private Animator anim;
    private bool hasGenereated=false;
    private FMOD.Studio.EventInstance playerFire;

    private void Start()
    {
        healthBar.SetMaxHealth(shipLife);
        healthBar.SetHealth(shipLife);
        maxHealth = shipLife;
        anim=GetComponent<Animator>();
        playerFire = FMODUnity.RuntimeManager.CreateInstance(EventOnFire);
    }

    private void Update()
    {
        if (!isOnFire && shipLife<=maxHealth/2)
        {
            isOnFire = true;
            playerFire.start();
            foreach (ParticleSystem p in fire){
                p.Play();
            }
        }    
        else if(isOnFire && shipLife > maxHealth / 2)
        {
            isOnFire = false;
            playerFire.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            foreach (ParticleSystem p in fire)
            {
                p.Stop();
            }
        }
    }

    

    private void OnTriggerStay(Collider other)  //OnTriggerEnter
    {
        if (other.name.Equals("Proiettile(Clone)"))
        {
            GameObject e;
            e = Instantiate(explosion, other.transform.position, Quaternion.identity);
            e.GetComponent<ParticleSystem>().Play();
            Destroy(other.gameObject);
            if (!gameObject.name.StartsWith("Player"))
            {
                shipLife -= PlayerPrefs.GetInt("combo");
            }
            shipLife--;
            healthBar.SetHealth(shipLife);
            if (shipLife <= 0)
            {
                if (!gameObject.name.StartsWith("Player"))
                {
                    int life=++GameObject.Find("Player").GetComponentInChildren<ShipManager>().shipLife;
                    GameObject.Find("Player").GetComponentInChildren<ShipManager>().healthBar.SetHealth(life);
                    if (!hasGenereated)
                    {
                        GameObject.Find("CanvasUI").GetComponent<MenuManager>().AddKill();
                        RandomBonusGen();
                    }
                }
                playerFire.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                anim.Play("Affondamento");
            }
        }
    }

    public void Affonda()
    {
        Destroy(this.gameObject.transform.parent.gameObject);
    }

    public void RandomBonusGen()
    {
        hasGenereated = true;
        if (Random.Range(1, 3) == 1)
        {
            if (Random.Range(1, 3) == 1)
            {
                if (Random.Range(1, 4) == 1)
                {
                    Vector3 vec = this.transform.position;
                    GameObject b = Instantiate(bonus[2], vec, Quaternion.identity);
                    b.transform.parent = null;
                }
                else
                {
                    Vector3 vec = this.transform.position;
                    GameObject b = Instantiate(bonus[1], vec, Quaternion.identity);
                    b.transform.parent = null;
                }
            }
            else
            {
                Vector3 vec = this.transform.position;
                GameObject b = Instantiate(bonus[0], vec, Quaternion.identity);
                b.transform.parent = null;
            }
        }
    }
}
