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
    public float speed = 20f;

    [Header("Hud")]
    //public TextMeshProUGUI punteggio;

    [Header("GameOver")]
    //public TextMeshProUGUI nuovoPunteggio;
    //public TextMeshProUGUI record;

    [Header("Pause")]
    //public TextMeshProUGUI nuovoPunteggioPausa;
    //public TextMeshProUGUI recordPausa;

    private float touchSpeed = 5;
    private Rigidbody rb;
    private float horizontalMove = 0f, verticalMove = 0f;

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
        rb.MovePosition(new Vector3(rb.position.x + horizontalMove * speed * Time.fixedDeltaTime, 0, rb.position.z + verticalMove * speed * Time.fixedDeltaTime));
    }

    public void Pause()
    {
        //pause.SetActive(true);
        //hud.SetActive(false);
        //Cursor.lockState = CursorLockMode.Confined;
        //Time.timeScale = 0;
        //gameIsPaused = true;

        //recordPausa.text = "Record: " + PlayerPrefs.GetInt("score", 0);
        //nuovoPunteggioPausa.text = "Punteggio: " + punteggioCounter;
    }

    public void Resume()
    {
        //pause.SetActive(false);
        //hud.SetActive(true);
        //Cursor.lockState = CursorLockMode.Locked;
        //Time.timeScale = 1;
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

}
