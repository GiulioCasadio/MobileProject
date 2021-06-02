using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [Header("Game")]
    public Joystick joy;
    public GameObject ship;
    public float speed = 20f;
    public GameObject[] cannons;

    [Header("Hud")]
    public Button fireButtonBack;
    public Button fireButton;
    //public TextMeshProUGUI punteggio;

    [Header("GameOver")]
    //public TextMeshProUGUI nuovoPunteggio;
    //public TextMeshProUGUI record;

    [Header("Pause")]
    //public TextMeshProUGUI nuovoPunteggioPausa;
    //public TextMeshProUGUI recordPausa;

    //private float touchSpeed = 5;
    private Rigidbody rb;
    private float horizontalMove = 0f, verticalMove = 0f;
    private float maxSpeed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }

    // Update is called once per frame
    void Update()
    {
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
        if (Input.touchCount > 0) {
            Vector3 v = new Vector3(rb.position.x + horizontalMove * speed * Time.fixedDeltaTime, 0, rb.position.z + verticalMove * speed * Time.fixedDeltaTime);
            rb.AddForce(new Vector3(horizontalMove, 0, verticalMove), ForceMode.Acceleration);
            ship.gameObject.transform.rotation = Quaternion.LookRotation(new Vector3(horizontalMove, 0, verticalMove) * 0.1f);
        }
           
        //else
        //{
        //    rb.velocity = Vector3.zero;
        //    rb.angularVelocity = Vector3.zero;
        //    rb.Sleep();
        //    rb.AddForce(new Vector3(horizontalMove, 0, verticalMove), ForceMode.Force);
        //}
    }

    public void Pause()
    {
        Time.timeScale = 0;
        //gameIsPaused = true;

        //recordPausa.text = "Record: " + PlayerPrefs.GetInt("score", 0);
        //nuovoPunteggioPausa.text = "Punteggio: " + punteggioCounter;
    }

    public void Resume()
    {
        Time.timeScale = 1;
        //gameIsPaused = false;
    }

    public void GameOver()
    {
        //gameOver.SetActive(true);
        //hud.SetActive(false);
        //Cursor.lockState = CursorLockMode.Confined;
        //Time.timeScale = 0;
        //record.text = "Record: " + PlayerPrefs.GetInt("score", 0);
        //nuovoPunteggio.text = "Punteggio: " + punteggioCounter;
    }

    public void StartScene()
    {
        SceneManager.LoadScene("Scenes/FirstMenu");
    }

    //public void RetartScene()
    //{
    //    SceneManager.LoadScene("Scenes/MainScene");
    //}

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LateralFire()
    {
            fireButton.GetComponent<Animator>().SetTrigger("canCooldown");
            Color tmp = fireButtonBack.GetComponent<Image>().color;
            tmp.a = 0.3f;
            fireButtonBack.GetComponent<Image>().color = tmp;
            foreach (GameObject c in cannons)
            {
                c.GetComponent<Fire>().CannonFire();
            }
    }
}
