﻿using UnityEngine;
using UnityEngine.UI;

public class Scr_Upgrade : MonoBehaviour
{

    public GameObject UpGradePage;
    public GameObject MainPage;
    public GameObject PressPage1;
    public GameObject PressPage2;
    public GameObject PressPage3;

    public Text NationalCondition;
    public Text PolicyIssuance;
    public Text MedicalResearch;
    public Text Back;

    TimeMode TempTimeMode;

    Scr_Tech Tech;
    Scr_TimeControl Time;
    Scr_Event Events;
    Scr_Statistic Statistic;

    void OnEnable()
    {
        Tech = FindObjectOfType<Scr_Tech>();
        Time = FindObjectOfType<Scr_TimeControl>();
        Events = FindObjectOfType<Scr_Event>();
        Statistic = FindObjectOfType<Scr_Statistic>();
        UpGradePage.SetActive(false);
        PressPage1.SetActive(true);
        PressPage2.SetActive(false);
        PressPage3.SetActive(false);
    }


    // Update is called once per frame
    void Update()
    {
        if (Tech.hasLabel)
        {
            NationalCondition.text = Tech.Label[0];
            PolicyIssuance.text = Tech.Label[1];
            MedicalResearch.text = Tech.Label[2];
            Back.text = Tech.Label[3];
            Tech.hasLabel = false;
            UpGradePage.SetActive(false);
            PressPage1.SetActive(false);
            PressPage2.SetActive(false);
            PressPage3.SetActive(false);
        }
    }

    void Pause()
    {
        if (!Events.showEvent)
        {
            TempTimeMode = Time.timeMode;
            Time.Pause();
        }
    }

    void StopPause()
    {
        if (!Events.showEvent)
        {
            if (TempTimeMode == TimeMode.OneSpeed)
            {
                Time.OneSpeed();
            }
            if (TempTimeMode == TimeMode.FastSpeed)
            {
                Time.FastSpeed();
            }
        }
    }

    public void OpenUpGradePage1()
    {
        Pause();
        UpGradePage.SetActive(true);
        MainPage.SetActive(false);
        PressPage1.SetActive(true);
        Statistic.OpenStatiPage();
        Tech.TechIndex = -1;
        Tech.page = 1;
        Tech.TextUpdate = true;
    }
    public void OpenUpGradePage2()
    {
        Pause();
        UpGradePage.SetActive(true);
        MainPage.SetActive(false);
        PressPage2.SetActive(true);
        Statistic.OpenStatiPage();
        Tech.TechIndex = -1;
        Tech.page = 2;
        Tech.TextUpdate = true;
    }
    public void OpenUpGradePage3()
    {
        Pause();
        UpGradePage.SetActive(true);
        MainPage.SetActive(false);
        PressPage3.SetActive(true);
        Statistic.OpenStatiPage();
        Tech.TechIndex = -1;
        Tech.page = 3;
        Tech.TextUpdate = true;
    }

    public void CloseUpGradePage()
    {
        StopPause();
        UpGradePage.SetActive(false);
        MainPage.SetActive(true);
        PressPage1.SetActive(false);
        PressPage2.SetActive(false);
        PressPage3.SetActive(false);
        Statistic.CloseStatiPage();
    }

    public void Press1Effect()
    {
        Tech.TechIndex = -1;
        Tech.TextUpdate = true;
        Tech.page = 1;
        PressPage1.SetActive(true);
        PressPage2.SetActive(false);
        PressPage3.SetActive(false);
    }
    public void Press2Effect()
    {
        Tech.TechIndex = -1;
        Tech.TextUpdate = true;
        Tech.page = 2;
        PressPage1.SetActive(false);
        PressPage2.SetActive(true);
        PressPage3.SetActive(false);
    }
    public void Press3Effect()
    {
        Tech.TechIndex = -1;
        Tech.TextUpdate = true;
        Tech.page = 3;
        PressPage1.SetActive(false);
        PressPage2.SetActive(false);
        PressPage3.SetActive(true);
    }
}
