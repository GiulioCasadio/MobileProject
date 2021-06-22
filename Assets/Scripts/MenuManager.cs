using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{

    [Header("GameSelection")]
    private int currentShip = 0, currentMap = 0;
    public GameObject[] ships;
    public GameObject[] maps;
    public TextMeshProUGUI mapName;
    public TextMeshProUGUI shipName;
    public Image[] stats;
    public Image[] bonus;
    public int enemiesCount;

    [Header("UI")]
    public GameObject gameWinObject;
    public GameObject gameOverObject;
    public GameObject hud;
    public GameObject shipSelection;
    public GameObject comboTxt;
    public TextMeshProUGUI killsTxt;
    public TextMeshProUGUI timerTxt;
    public Image[] stars;
    public TextMeshProUGUI orologio;

    private float dmgBonus = 0, spdBonus = 0, hltBonus = 0, seconds = 0;
    private int kills = 0, starsEarned = 0, minutes;
    private bool isPaused = false;

    // Start is called before the first frame update
    void Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        SetMap(0);

        // RESETTA
        PlayerPrefs.SetInt("b0", 0);
        PlayerPrefs.SetInt("b1", 0);
        PlayerPrefs.SetInt("b2", 0);
    }

    private void Update()
    {
        if (orologio != null && !isPaused)
        {
            seconds += Time.deltaTime;
            orologio.text = minutes.ToString("00") + ":" + seconds.ToString("00");
            if (seconds >= 60)
            {
                minutes++;
                seconds = 0;
            }
        }
    }

    public void PlayLevel()
    {
        PlayerPrefs.SetInt("ship", currentShip);
        GetComponent<Animator>().SetTrigger("Load");
        StartCoroutine(LoadCoroutine());
    }

    public void MainMenu()
    {
        SceneManager.LoadSceneAsync("Scenes/FirstMenu");
    }

    public void LoadToMenu()
    {
        GetComponent<Animator>().SetTrigger("toMenuLoad");
        StartCoroutine(LoadMenuCoroutine());
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
        for (int i = 0; i < 3; i++)
        {
            if (i == currentShip)
                ships[i].SetActive(true);
            else
                ships[i].SetActive(false);
        }
        shipName.text = "Ship " + (currentShip + 1);
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
        shipName.text = "Ship " + (currentShip + 1);
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
        SetMap(currentMap);
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
        SetMap(currentMap);
    }

    public void SetMap(int i)
    {
        switch (PlayerPrefs.GetInt("b" + i.ToString()))
        {
            case 1:
                stars[0].gameObject.SetActive(true);
                stars[1].gameObject.SetActive(false);
                stars[2].gameObject.SetActive(false);
                break;
            case 2:
                stars[0].gameObject.SetActive(true);
                stars[1].gameObject.SetActive(true);
                stars[2].gameObject.SetActive(false);
                break;
            case 3:
                stars[0].gameObject.SetActive(true);
                stars[1].gameObject.SetActive(true);
                stars[2].gameObject.SetActive(true);
                break;
            default:
                stars[0].gameObject.SetActive(false);
                stars[1].gameObject.SetActive(false);
                stars[2].gameObject.SetActive(false);
                break;
        }
    }
    public void SetStats(int i)
    {
        switch (i)
        {
            case 0:
                stats[0].fillAmount = 0.3f;
                stats[1].fillAmount = 0.33f;
                stats[2].fillAmount = 0.3f;
                comboTxt.SetActive(true);
                break;
            case 1:
                stats[0].fillAmount = 0.3f;
                stats[1].fillAmount = 0.33f;
                stats[2].fillAmount = 0.5f;
                comboTxt.SetActive(false);
                break;
            default:
                stats[0].fillAmount = 0.6f;
                stats[1].fillAmount = 0.33f;
                stats[2].fillAmount = 0.3f;
                comboTxt.SetActive(false);
                break;
        }
        bonus[0].fillAmount = stats[0].fillAmount + hltBonus;
        bonus[1].fillAmount = stats[1].fillAmount + dmgBonus;
        bonus[2].fillAmount = stats[2].fillAmount + spdBonus;
    }

    public void ImproveBonus(int b)
    {
        switch (b)
        {
            case 0:
                hltBonus += 0.034f;
                break;
            case 1:
                dmgBonus += 0.34f;
                break;
            case 2:
                spdBonus += 0.1f;
                break;
        }
    }

    public void Pause()
    {
        isPaused = true;
        GetComponent<Animator>().SetTrigger("inPause");
        SetStats(PlayerPrefs.GetInt("ship"));
        ships[PlayerPrefs.GetInt("ship")].SetActive(true);
    }

    public void Resume()
    {
        isPaused = false;
        GetComponent<Animator>().SetTrigger("inGame");
    }

    public void Restart()
    {
        Scene scene = SceneManager.GetActiveScene();
        Debug.Log(scene.name);
        SceneManager.LoadScene(scene.name);
    }

    public void GameOver()
    {
        isPaused = true;
        hud.SetActive(false);
        shipSelection.SetActive(true);
        gameOverObject.SetActive(true);
        SetStats(PlayerPrefs.GetInt("ship"));
        ships[PlayerPrefs.GetInt("ship")].SetActive(true);
        GetComponent<Animator>().SetTrigger("isOver");
    }

    public void GameWin()
    {
        isPaused = true;
        killsTxt.text = "Kills: " + kills + "/" + enemiesCount;
        timerTxt.text = minutes.ToString("00") + ":" + seconds.ToString("00");

        if (kills == enemiesCount)
        {
            killsTxt.color = new Color(22f / 255f, 161f / 255f, 0f / 255f);
            starsEarned++;
        }

        if (minutes < 5)
        {
            timerTxt.color = new Color(22f / 255f, 161f / 255f, 0f / 255f);
            starsEarned++;
        }

        hud.SetActive(false);
        shipSelection.SetActive(true);
        gameWinObject.SetActive(true);
        SetStats(PlayerPrefs.GetInt("ship"));
        ships[PlayerPrefs.GetInt("ship")].SetActive(true);
        GetComponent<Animator>().SetTrigger("winning");

        switch (starsEarned)
        {
            case 1:
                stars[0].gameObject.SetActive(true);
                break;
            case 2:
                stars[0].gameObject.SetActive(true);
                stars[1].gameObject.SetActive(true);
                break;
        }

        // salvo il record
        int ppp = PlayerPrefs.GetInt("b" + currentMap.ToString());
        if (ppp<starsEarned+1)
            PlayerPrefs.SetInt("b"+ currentMap, (starsEarned+1));

    }

    public void SetIni()
    {
        SetStats(PlayerPrefs.GetInt("ship"));
    }

    public void AddKill() { kills++;  }

    IEnumerator LoadCoroutine()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadSceneAsync("Scenes/Level" + (currentMap + 1));
    }

    IEnumerator LoadMenuCoroutine()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadSceneAsync("Scenes/FirstMenu");
    }

    public bool GetStatusPause() { return isPaused; }
}
