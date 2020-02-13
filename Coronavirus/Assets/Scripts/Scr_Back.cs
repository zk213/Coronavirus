using UnityEngine;
using UnityEngine.SceneManagement;

public class Scr_Back : MonoBehaviour
{
    public GameObject BackPage;
    public GameObject UpGradePage;
    public GameObject StatisticPage;

    Scr_Save save;
    Scr_TimeControl time;

    TimeMode TempTimeMode;
    bool OpenBack = false;
    void Awake()
    {
        OpenBack = false;
        BackPage.SetActive(false);
        save = FindObjectOfType<Scr_Save>();
        time = FindObjectOfType<Scr_TimeControl>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!OpenBack && !UpGradePage.activeInHierarchy && !StatisticPage.activeInHierarchy)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                OpenBackPage();
            }
        }
        else if (OpenBack && !UpGradePage.activeInHierarchy && !StatisticPage.activeInHierarchy)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                CloseBackPage();
            }
        }
    }

    public void CloseBackPage()
    {
        OpenBack = false;
        BackPage.SetActive(false);
        if (TempTimeMode == TimeMode.OneSpeed)
        {
            time.OneSpeed();
        }
        if (TempTimeMode == TimeMode.FastSpeed)
        {
            time.FastSpeed();
        }
    }

    public void ReturnStartScene()
    {
        save.SaveButton();
        SceneManager.LoadScene("Start");
    }

    public void OpenSettingPage()
    {
        Debug.Log("打开了设置界面");
    }

    void OpenBackPage()
    {
        OpenBack = true;
        BackPage.SetActive(true);
        TempTimeMode = time.timeMode;
        time.Pause();
    }
}
