using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    //private int bestScore;

    //public TextMeshProUGUI best;

    // Start is called before the first frame update
    void Start()
    {
        //bestScore = PlayerPrefs.GetInt("score", 0);
        //best.text = "Record: " + bestScore;
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void PlayLevel()
    {
        SceneManager.LoadScene("Scenes/Level1");
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
}
