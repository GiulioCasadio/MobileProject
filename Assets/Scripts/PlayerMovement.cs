using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [Header("Game")]
    public Joystick joy;
    public float speed;
    public float rotationSpeed;
    public GameObject ship;
    public GameObject[] cannons;
    public GameObject[] differentShips;

    [Header("Hud")]
    public Button fireButtonBack;
    public Button fireButton;
    public HealthBar healthBar;
    public GameObject canvas;

    
    [FMODUnity.EventRef]
    public string EventCollect;
    private FMOD.Studio.EventInstance collect;

    private Rigidbody rb;
    private float horizontalMove = 0f, verticalMove = 0f;
    private bool chiave;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        GameObject newShip = Instantiate(differentShips[PlayerPrefs.GetInt("ship")]);
        newShip.gameObject.transform.parent = ship.transform;
        newShip.gameObject.transform.localPosition = new Vector3(0, 0, 0);
        newShip.transform.GetComponent<ShipManager>().healthBar = healthBar;
        if (PlayerPrefs.GetInt("ship") == 1)
            speed += 1;
        chiave = false;
        PlayerPrefs.SetInt("combo", 0);

        collect = FMODUnity.RuntimeManager.CreateInstance(EventCollect);
    }

    // Update is called once per frame
    void Update()
    {
        if (ship.GetComponentInChildren<ShipManager>().shipLife == 0)
            canvas.GetComponent<MenuManager>().GameOver();

        if (joy.Horizontal >= .2f || joy.Horizontal <= .2f)
            horizontalMove = speed * joy.Horizontal;
        else
            horizontalMove = 0;

        if (joy.Vertical >= .2f || joy.Vertical <= .2f)
            verticalMove = speed * joy.Vertical;
        else
            verticalMove = 0;
    }

    private void FixedUpdate()
    {        
        if (Input.touchCount > 0 && verticalMove!=0 && horizontalMove!=0) {
            Vector3 rotDest = new Vector3(horizontalMove, 0, verticalMove);

            rb.AddForce(rotDest, ForceMode.Force);
            ship.gameObject.transform.rotation = Quaternion.RotateTowards(ship.gameObject.transform.rotation, Quaternion.LookRotation(rotDest), Time.deltaTime * rotationSpeed);

        }
    }

    public void LateralFire()
    {
        canvas.GetComponent<Animator>().SetTrigger("canCooldown");
        Color tmp = fireButtonBack.GetComponent<Image>().color;
        tmp.a = 0.3f;
        fireButtonBack.GetComponent<Image>().color = tmp;
        foreach (GameObject c in cannons)
        {
            c.GetComponent<Fire>().CannonFire();
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.name.StartsWith("Forziere"))
        {
            if (chiave)
            {
                collect.start();
                canvas.GetComponent<MenuManager>().GameWin();
                Destroy(other.gameObject);
            }
        }
        else if (other.name.StartsWith("Key"))
        {
            collect.start();
            chiave = true;
            Destroy(other.gameObject);
        }
        else if (other.name.StartsWith("heart"))
        {
            collect.start();
            canvas.GetComponent<MenuManager>().ImproveBonus(0);
            float lifeMax = GameObject.Find("Player").GetComponentInChildren<ShipManager>().healthBar.GetMaxHealth();
            if (lifeMax < 31)
            {
                GameObject.Find("Player").GetComponentInChildren<ShipManager>().healthBar.SetMaxHealth((int)++lifeMax);
                int life = ++GameObject.Find("Player").GetComponentInChildren<ShipManager>().shipLife;
                GameObject.Find("Player").GetComponentInChildren<ShipManager>().healthBar.SetHealth(life);
            }
            Destroy(other.gameObject.transform.parent.gameObject);
        }
        else if (other.name.StartsWith("skull"))
        {
            collect.start();
            canvas.GetComponent<MenuManager>().ImproveBonus(1);
            int combo = PlayerPrefs.GetInt("combo") < 2 ? PlayerPrefs.GetInt("combo") + 1 : PlayerPrefs.GetInt("combo");
            PlayerPrefs.SetInt("combo", combo);
            Destroy(other.gameObject.transform.parent.gameObject);
        }
        else if (other.name.StartsWith("timone"))
        {
            collect.start();
            canvas.GetComponent<MenuManager>().ImproveBonus(2);
            if (speed < 6.5f) 
                speed+=0.5f;
            Destroy(other.gameObject.transform.parent.gameObject);
        }
            
    }
}
