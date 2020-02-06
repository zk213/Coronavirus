﻿using UnityEngine;
using UnityEngine.UI;


public enum TimeMode
{
    Pause, OneSpeed, FastSpeed
}

public class Scr_TimeControl : MonoBehaviour
{
    public Text GlobalTime;
    public Image pause;
    public Image onespeed;
    public Image fastspeed;
    public GameObject extendbutton;

    Scr_Event Event;

    float iniPosy = 9.1f;
    float finPosy = -64.9f;
    float yMoveSpeed = 300;


    public TimeMode timeMode = TimeMode.OneSpeed;//刚进游戏，状态是一倍速

    public int day = 1;//初始时间，对应2019.12.1
    public float OneSpeedSpawn = 1;//一倍速时的时间间隔
    public float FastSpeedSpawn = 0.5f;//快速时的时间间隔
    float daySpawn;//时间流逝间隔
    float TempDay = 0;
    int Year = 2019;
    int Month = 12;
    int Day = 1;
    string ShowFullTime;
    bool showbutton = false;
    bool showbuttonmovefinish = true;


    // Start is called before the first frame update
    void Start()
    {
        Event = FindObjectOfType<Scr_Event>();
        daySpawn = OneSpeedSpawn;
        ShowFullTime = "2019 12 01";
        extendbutton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (timeMode != TimeMode.Pause)
        {
            TempDay += Time.deltaTime;
        }
        if (timeMode == TimeMode.OneSpeed)
        {
            daySpawn = OneSpeedSpawn;
        }
        if (timeMode == TimeMode.FastSpeed)
        {
            daySpawn = FastSpeedSpawn;
        }

        if (TempDay >= daySpawn)
        {
            TempDay = 0;
            day++;
            ShowTime();

            Event.EventCheck();
        }
        GlobalTime.text = ShowFullTime;

        //这里是控制时间调速摁扭的出现与消失的动画
        if (!showbuttonmovefinish)
        {
            if (!showbutton)
            {
                extendbutton.GetComponent<RectTransform>().anchoredPosition += new Vector2(0, yMoveSpeed * Time.deltaTime);
                if (extendbutton.GetComponent<RectTransform>().anchoredPosition.y >= iniPosy)
                {
                    extendbutton.GetComponent<RectTransform>().anchoredPosition = new Vector2(extendbutton.GetComponent<RectTransform>().anchoredPosition.x, iniPosy);
                    extendbutton.SetActive(false);
                    showbuttonmovefinish = true;
                }
            }
            else
            {
                extendbutton.GetComponent<RectTransform>().anchoredPosition -= new Vector2(0, yMoveSpeed * Time.deltaTime);
                if (extendbutton.GetComponent<RectTransform>().anchoredPosition.y <= finPosy)
                {
                    extendbutton.GetComponent<RectTransform>().anchoredPosition = new Vector2(extendbutton.GetComponent<RectTransform>().anchoredPosition.x, finPosy);
                    showbuttonmovefinish = true;
                }
            }
        }
    }

    //计算标准格式的时间
    void ShowTime()
    {
        Day++;
        if (Day == 29 && Month == 2 && Year != 2020)
        {
            Day = 1;
            Month++;
        }
        if (Day == 30 && Month == 2)
        {
            Day = 1;
            Month++;
        }
        if (Day == 31 && (Month == 4 || Month == 6 || Month == 9 || Month == 11))
        {
            Day = 1;
            Month++;
        }
        if (Day == 32 && Month != 12)
        {
            Day = 1;
            Month++;
        }
        if (Day == 32 && Month == 12)
        {
            Day = 1;
            Month = 1;
            Year++;
        }
        ShowFullTime = Year.ToString() + " " + string.Format("{0:D2}", Month) + " " + string.Format("{0:D2}", Day);

    }

    //一下三个是条件控制时间速度的摁扭
    public void Pause()
    {
        if (timeMode == TimeMode.Pause) { return; }

        timeMode = TimeMode.Pause;
        string path = "UI/TimeFrame/Pause2";    //image路径
        Sprite sprite = Resources.Load(path, typeof(Sprite)) as Sprite;    //参数为资源路径和资源类型
        pause.sprite = sprite;

        path = "UI/TimeFrame/CSpeed1";
        sprite = Resources.Load(path, typeof(Sprite)) as Sprite;
        onespeed.sprite = sprite;

        path = "UI/TimeFrame/FSpeed1";
        sprite = Resources.Load(path, typeof(Sprite)) as Sprite;
        fastspeed.sprite = sprite;
    }
    public void OneSpeed()
    {
        if (timeMode == TimeMode.OneSpeed || Event.showEvent) { return; }

        timeMode = TimeMode.OneSpeed;
        string path = "UI/TimeFrame/CSpeed2";
        Sprite sprite = Resources.Load(path, typeof(Sprite)) as Sprite;
        onespeed.sprite = sprite;

        path = "UI/TimeFrame/Pause1";
        sprite = Resources.Load(path, typeof(Sprite)) as Sprite;
        pause.sprite = sprite;

        path = "UI/TimeFrame/FSpeed1";
        sprite = Resources.Load(path, typeof(Sprite)) as Sprite;
        fastspeed.sprite = sprite;
    }
    public void FastSpeed()
    {
        if (timeMode == TimeMode.FastSpeed || Event.showEvent) { return; }

        timeMode = TimeMode.FastSpeed;
        string path = "UI/TimeFrame/FSpeed2";
        Sprite sprite = Resources.Load(path, typeof(Sprite)) as Sprite;
        fastspeed.sprite = sprite;

        path = "UI/TimeFrame/CSpeed1";
        sprite = Resources.Load(path, typeof(Sprite)) as Sprite;
        onespeed.sprite = sprite;

        path = "UI/TimeFrame/Pause1";
        sprite = Resources.Load(path, typeof(Sprite)) as Sprite;
        pause.sprite = sprite;
    }

    //展现与隐藏控制时间的摁扭
    public void ShowButton()
    {
        if (!showbuttonmovefinish) { return; }

        if (showbutton)
        {
            showbutton = false;
            showbuttonmovefinish = false;
        }
        else
        {
            showbutton = true;
            showbuttonmovefinish = false;
            extendbutton.SetActive(true);

        }
    }
}
