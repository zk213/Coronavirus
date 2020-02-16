using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject PauseMenuUI;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Awake()
    {
        //DontDestroyOnLoad(gameObject);

    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {

            if (GameIsPaused)
            {
                Resume();
            }
            else
            {

                Pause();
            }
        }
    }


    public void Resume()
    {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;


    }
    public void Pause()
    {

        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;


    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);

    }

    public void QuitGame()
    {

        Application.Quit();

    }

    public void PlayGame()
    {
        SceneManager.LoadScene(1);



    }
}
