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

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
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
        if (Input.touchCount > 0 && verticalMove!=0 && horizontalMove!=0) {
            Vector3 rotDest = new Vector3(horizontalMove, 0, verticalMove);

            rb.AddForce(rotDest, ForceMode.Force);
            ship.gameObject.transform.rotation = Quaternion.RotateTowards(ship.gameObject.transform.rotation, Quaternion.LookRotation(rotDest), Time.fixedDeltaTime * 25f);

        }
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

    public void Affonda()
    {
        print("Hai perso!");
        Destroy(this.gameObject);
    }
}
