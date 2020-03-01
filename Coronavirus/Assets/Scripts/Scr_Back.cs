using UnityEngine;

public class Scr_Back : MonoBehaviour
{
    public GameObject BackPage;
    public GameObject UpGradePage;
    public GameObject StatisticPage;

    public GameObject Scene1;
    public GameObject Scene3;

    Scr_Save save;
    Scr_TimeControl time;
    Scr_Load load;
    Scr_Event Events;

    TimeMode TempTimeMode;
    bool OpenBack = false;
    void Awake()
    {
        OpenBack = false;
        BackPage.SetActive(false);
        save = FindObjectOfType<Scr_Save>();
        time = FindObjectOfType<Scr_TimeControl>();
        load = FindObjectOfType<Scr_Load>();
        Events = FindObjectOfType<Scr_Event>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!OpenBack && !UpGradePage.activeInHierarchy && !StatisticPage.activeInHierarchy)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (!Events.showEvent)
                {
                    OpenBackPage();
                }
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
        if (!Events.showEvent)
        {
            if (TempTimeMode == TimeMode.OneSpeed)
            {
                time.OneSpeed();
            }
            if (TempTimeMode == TimeMode.FastSpeed)
            {
                time.FastSpeed();
            }
        }
    }

    public void ReturnStartScene()
    {
        save.SaveButton();
        load.StartControl = false;
        Scene1.SetActive(true);
        Scene3.SetActive(false);
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
