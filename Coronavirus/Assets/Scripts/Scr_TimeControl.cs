using System;
using System.Xml;
using UnityEngine;
using UnityEngine.UI;


public enum TimeMode
{
    Pause,
    OneSpeed,
    FastSpeed
}

public class Scr_TimeControl : MonoBehaviour
{
    public void LocalSave()
    {
        XmlDocument xmlSave = new XmlDocument();
        xmlSave.Load(Application.persistentDataPath + "/save/Save.save");
        XmlElement xmlNodeS = xmlSave.DocumentElement;
        foreach (XmlNode elementsS in xmlNodeS)
        {
            if (elementsS == null)
                continue;
            if (elementsS.LocalName == "Day")
            {
                elementsS.InnerText = day.ToString();
            }
            if (elementsS.LocalName == "Time")
            {
                elementsS.InnerText = string.Format("{0:D2}:{1:D2}:{2:D2} " + "{3:D4}/{4:D2}/{5:D2}", DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second, DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            }

        }
        xmlSave.Save(Application.persistentDataPath + "/save/Save.save");
    }
    public TimeMode timeMode = TimeMode.OneSpeed;//刚进游戏，状态是一倍速

    public int day = 1;//初始时间，对应2019.12.1
    public float OneSpeedSpawn = 1;//一倍速时的时间间隔
    public float FastSpeedSpawn = 0.5f;//快速时的时间间隔

    public Text GlobalTime;
    public GameObject extendbutton;

    Scr_Event Event;
    Scr_Color provinces;
    Scr_Load LoadControl;

    float iniPosy = -5;
    float finPosy = -105;
    float yMoveSpeed = 300;


    float daySpawn;//时间流逝间隔
    float TempDay = 0;
    int Year = 2019;
    public int Month = 12;
    public int Day = 1;
    string ShowFullTime;
    bool showbutton = false;
    bool showbuttonmovefinish = true;

    bool isLoad = false;
    Color32 colorWhite2 = new Color32(255, 255, 255, 203);
    Color32 colorBlue1 = new Color32(0, 183, 255, 255);
    Color32 colorBlue2 = new Color32(0, 183, 255, 203);

    public void Start1()
    {
        Year = 2019;
        Month = 12;
        Day = 1;
        showbutton = false;
        showbuttonmovefinish = true;

        isLoad = false;
        XmlDocument SxmlDoc = new XmlDocument();
        SxmlDoc.Load(Application.persistentDataPath + "/setting.set");
        XmlElement SxmlNode = SxmlDoc.DocumentElement;
        foreach (XmlNode elements in SxmlNode)
        {
            if (elements == null)
                continue;
            if (elements.LocalName == "SMode")
            {
                if (elements.InnerText == "Load")
                {
                    isLoad = true;
                }
            }
        }

        if (isLoad)
        {
            XmlDocument xmlSave = new XmlDocument();
            xmlSave.Load(Application.persistentDataPath + "/save/Save.save");
            XmlElement xmlNodeS = xmlSave.DocumentElement;
            foreach (XmlNode elementsS in xmlNodeS)
            {
                if (elementsS == null)
                    continue;
                if (elementsS.LocalName == "Day")
                {
                    int.TryParse(elementsS.InnerText, out day);
                    for (int tempDay = 1; tempDay < day; tempDay++)
                    {
                        ShowTime();
                    }

                }
            }
        }
        timeMode = TimeMode.Pause;
        OneSpeed();
        GlobalTime.text = ShowFullTime;
    }


    // Start is called before the first frame update
    void Start()
    {


        Event = FindObjectOfType<Scr_Event>();
        provinces = FindObjectOfType<Scr_Color>();
        LoadControl = FindObjectOfType<Scr_Load>();
        daySpawn = OneSpeedSpawn;
        ShowFullTime = "2019 12 01";
        extendbutton.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if (!LoadControl.StartControl) { return; }
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
            provinces.ProvincesCheck();
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

        extendbutton.transform.Find("Pause").GetComponent<Image>().color = colorBlue2;
        extendbutton.transform.Find("Pause").transform.Find("PauseIcon").GetComponent<Image>().color = Color.white;
        extendbutton.transform.Find("OneSpeed").GetComponent<Image>().color = colorWhite2;
        extendbutton.transform.Find("OneSpeed").transform.Find("OneSpeedIcon").GetComponent<Image>().color = colorBlue1;
        extendbutton.transform.Find("FastSpeed").GetComponent<Image>().color = colorWhite2;
        extendbutton.transform.Find("FastSpeed").transform.Find("FastSpeedIcon").GetComponent<Image>().color = colorBlue1;
        /*
         string path = "UI/Button/TimeFrame/Pause2";    //image路径
        Sprite sprite = Resources.Load(path, typeof(Sprite)) as Sprite;    //参数为资源路径和资源类型
        pause.sprite = sprite;

        path = "UI/Button/TimeFrame/CSpeed1";
        sprite = Resources.Load(path, typeof(Sprite)) as Sprite;
        onespeed.sprite = sprite;

        path = "UI/Button/TimeFrame/FSpeed1";
        sprite = Resources.Load(path, typeof(Sprite)) as Sprite;
        fastspeed.sprite = sprite;
         */

    }
    public void OneSpeed()
    {
        if (timeMode == TimeMode.OneSpeed || Event.showEvent) { return; }

        timeMode = TimeMode.OneSpeed;

        extendbutton.transform.Find("Pause").GetComponent<Image>().color = colorWhite2;
        extendbutton.transform.Find("Pause").transform.Find("PauseIcon").GetComponent<Image>().color = colorBlue1;
        extendbutton.transform.Find("OneSpeed").GetComponent<Image>().color = colorBlue2;
        extendbutton.transform.Find("OneSpeed").transform.Find("OneSpeedIcon").GetComponent<Image>().color = Color.white;
        extendbutton.transform.Find("FastSpeed").GetComponent<Image>().color = colorWhite2;
        extendbutton.transform.Find("FastSpeed").transform.Find("FastSpeedIcon").GetComponent<Image>().color = colorBlue1;
        /*
         string path = "UI/Button/TimeFrame/CSpeed2";
        Sprite sprite = Resources.Load(path, typeof(Sprite)) as Sprite;
        onespeed.sprite = sprite;

        path = "UI/Button/TimeFrame/Pause1";
        sprite = Resources.Load(path, typeof(Sprite)) as Sprite;
        pause.sprite = sprite;

        path = "UI/Button/TimeFrame/FSpeed1";
        sprite = Resources.Load(path, typeof(Sprite)) as Sprite;
        fastspeed.sprite = sprite;
         */

    }
    public void FastSpeed()
    {
        if (timeMode == TimeMode.FastSpeed || Event.showEvent) { return; }

        timeMode = TimeMode.FastSpeed;

        extendbutton.transform.Find("Pause").GetComponent<Image>().color = colorWhite2;
        extendbutton.transform.Find("Pause").transform.Find("PauseIcon").GetComponent<Image>().color = colorBlue1;
        extendbutton.transform.Find("OneSpeed").GetComponent<Image>().color = colorWhite2;
        extendbutton.transform.Find("OneSpeed").transform.Find("OneSpeedIcon").GetComponent<Image>().color = colorBlue1;
        extendbutton.transform.Find("FastSpeed").GetComponent<Image>().color = colorBlue2;
        extendbutton.transform.Find("FastSpeed").transform.Find("FastSpeedIcon").GetComponent<Image>().color = Color.white;
        /*
         string path = "UI/Button/TimeFrame/FSpeed2";
        Sprite sprite = Resources.Load(path, typeof(Sprite)) as Sprite;
        fastspeed.sprite = sprite;

        path = "UI/Button/TimeFrame/CSpeed1";
        sprite = Resources.Load(path, typeof(Sprite)) as Sprite;
        onespeed.sprite = sprite;

        path = "UI/Button/TimeFrame/Pause1";
        sprite = Resources.Load(path, typeof(Sprite)) as Sprite;
        pause.sprite = sprite;
         */

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
