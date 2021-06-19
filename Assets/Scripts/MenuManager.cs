using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [Header("GameSelection")]
    private int currenntShip=0, currentMap=0;
    public GameObject[] ships, maps;
    public TextMeshProUGUI mapName, shipName;

    //private int bestScore;

    // Start is called before the first frame update
    void Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        Time.timeScale = 1; // Mi assicuro di aver ripreso la corretta esecuazione 
        //bestScore = PlayerPrefs.GetInt("score", 0);
        //best.text = "Record: " + bestScore;
    }

    public void PlayLevel()
    {
        SceneManager.LoadScene("Scenes/Level1");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Scenes/FirstMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void GoToSettings()
    {
        GetComponent<Animator>().SetBool("menuToSet", true);
    }

    public void GoToMenu()
    {
        GetComponent<Animator>().SetBool("setToMenu", true);
    }

    public void NextShip()
    {
        currenntShip = currenntShip >= 2 ? 0 : ++currenntShip;
        for(int i=0; i<3; i++)
        {
            if (i == currenntShip)
                ships[i].SetActive(true);
            else
                ships[i].SetActive(false);
        }
        shipName.text = "Ship " + (currenntShip+1);
    }

    public void PrevShip()
    {
        currenntShip = currenntShip <= 0 ? 2 : --currenntShip;
        for (int i = 0; i < 3; i++)
        {
            if (i == currenntShip)
                ships[i].SetActive(true);
            else
                ships[i].SetActive(false);
        }
        shipName.text= "Ship " + (currenntShip + 1);
    }

    public void NextMap()
    {
        currentMap = currentMap >= 2 ? 0 : ++currentMap;
        for (int i = 0; i < 3; i++)
        {
            if (i == currentMap)
                maps[i].SetActive(true);
            else
                maps[i].SetActive(false);
        }
        mapName.text = "Level " + (currentMap + 1);
    }

    public void PrevMap()
    {
        currentMap = currentMap <= 0 ? 2 : --currentMap;
        for (int i = 0; i < 3; i++)
        {
            if (i == currentMap)
                maps[i].SetActive(true);
            else
                maps[i].SetActive(false);
        }
        mapName.text = "Level " + (currentMap + 1);
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

    public void Restart()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

}
