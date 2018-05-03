using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuUI : MonoBehaviour {

    public Text score_text;
    public GameObject main_panel;
    public GameObject help_panel;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        if(SceneManager.GetActiveScene() == SceneManager.GetSceneByName("WinScreen"))
        {
            score_text.text = Stats.Player_Score.ToString();
            Invoke("ReturnToMenu", 4.0f);
        }

    }
    public void StartGame()
    {
        SceneManager.LoadScene("LevelOne");
    }

    public void toggleHelp()
    {
        if(help_panel.activeInHierarchy)
        {
            help_panel.SetActive(false);
            main_panel.SetActive(true);
        }
        else
        {
            main_panel.SetActive(false);
            help_panel.SetActive(true);
        }
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("TitleScreen");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
