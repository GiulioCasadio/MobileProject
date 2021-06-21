using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [Header("GameSelection")]
    private int currentShip=0, currentMap=0;
    public GameObject[] ships, maps;
    public TextMeshProUGUI mapName, shipName;
    public Image[] stats;

    [Header("UI")]
    public GameObject gameOverObject;
    public GameObject hud;
    public GameObject shipSelection;

    // Start is called before the first frame update
    void Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        Time.timeScale = 1; // Mi assicuro di aver ripreso la corretta esecuazione 
    }

    public void PlayLevel()
    {
        PlayerPrefs.SetInt("ship", currentShip);
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
        currentShip = currentShip >= 2 ? 0 : ++currentShip;
        for(int i=0; i<3; i++)
        {
            if (i == currentShip)
                ships[i].SetActive(true);
            else
                ships[i].SetActive(false);
        }
        shipName.text = "Ship " + (currentShip+1);
        SetStats(currentShip);
    }


    public void PrevShip()
    {
        currentShip = currentShip <= 0 ? 2 : --currentShip;
        for (int i = 0; i < 3; i++)
        {
            if (i == currentShip)
                ships[i].SetActive(true);
            else
                ships[i].SetActive(false);
        }
        shipName.text= "Ship " + (currentShip + 1);
        SetStats(currentShip);
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

    public void SetStats(int i)
    {
        switch (i)
        {
            case 0:
                stats[0].fillAmount = 0.3f;
                stats[1].fillAmount = 0.6f;
                stats[2].fillAmount = 0.3f;
                break;
            case 1:
                stats[0].fillAmount = 0.3f;
                stats[1].fillAmount = 0.3f;
                stats[2].fillAmount = 0.6f;
                break;
            default:
                stats[0].fillAmount = 0.6f;
                stats[1].fillAmount = 0.3f;
                stats[2].fillAmount = 0.3f;
                break;
        }
    }
    public void Pause()
    {
        shipSelection.SetActive(true);
        Time.timeScale = 0;
        SetStats(PlayerPrefs.GetInt("ship"));
        ships[PlayerPrefs.GetInt("ship")].SetActive(true);
    }

    public void Resume()
    {
        Time.timeScale = 1;
    }

    public void Restart()
    {
        Scene scene = SceneManager.GetActiveScene();
        Debug.Log(scene.name);
        SceneManager.LoadScene(scene.name);
    }

    public void GameOver()
    {
        shipSelection.SetActive(true);
        hud.SetActive(false);
        gameOverObject.SetActive(true);
        SetStats(PlayerPrefs.GetInt("ship"));
        ships[PlayerPrefs.GetInt("ship")].SetActive(true);
        GetComponent<Animator>().SetTrigger("isOver");
    }


}
