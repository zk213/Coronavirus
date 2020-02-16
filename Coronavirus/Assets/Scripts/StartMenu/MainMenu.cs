using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void playGame()
    {
        SceneManager.LoadScene("Part1");//需要一个淡入淡出效果
    }
    public void quitGame()
    {
        Application.Quit();
    }
    public void playMenu()
    {
        SceneManager.LoadScene("StartMenu");
    }
}
