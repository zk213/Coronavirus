using UnityEngine;
using UnityEngine.UI;

public class Scr_Start : MonoBehaviour
{
    public GameObject Scene0;
    public GameObject Scene1;
    public GameObject Scene2;
    public GameObject Scene3;
    public GameObject CG;
    public GameObject TextProcess;
    public GameObject TextInfor;

    Scr_Color Provinces;
    Scr_Event Events;
    Scr_Tech Tech;

    public int step = 0;

    bool LoadOver = false;
    int stepFull = 0;
    void Start()
    {
        Scene0.SetActive(true);
        Scene1.SetActive(false);
        Scene2.SetActive(false);
        Scene3.SetActive(false);
        string path = "EventPictures/CG/OP/" + ((int)Random.Range(1, 1)).ToString();
        CG.GetComponent<Image>().sprite = Resources.Load(path, typeof(Sprite)) as Sprite;
        CG.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width, Screen.height);

        Provinces = FindObjectOfType<Scr_Color>();
        Events = FindObjectOfType<Scr_Event>();
        Tech = FindObjectOfType<Scr_Tech>();
    }

    // Update is called once per frame
    void Update()
    {

        if (LoadOver)
        {
            Scene1.SetActive(true);
            Scene0.SetActive(false);
            gameObject.SetActive(false);
        }
        if (step == 0)
        {
            TextInfor.GetComponent<Text>().text = "正在加载基本信息";
            Provinces.Start1();
            stepFull = (int)Mathf.Ceil(Provinces.colorNum / 2) + 2;
        }
        if (step <= stepFull - 2 && step != 0)
        {
            TextInfor.GetComponent<Text>().text = "正在生成地图";
            Provinces.Start2();
        }
        if (step == stepFull - 1)
        {
            TextInfor.GetComponent<Text>().text = "正在加载事件";
            Events.Start1();
        }
        if (step == stepFull)
        {
            TextInfor.GetComponent<Text>().text = "正在加载升级项目";
            Tech.Start1();
        }
        if (step > stepFull)
        {
            TextInfor.GetComponent<Text>().text = "加载完成";
            LoadOver = true;
        }
        if (!LoadOver)
        {
            TextProcess.GetComponent<Text>().text = string.Format("{0:F0}", ((float)step / (float)stepFull) * 100) + "%";
            step += 1;

        }
        else
        {
            TextProcess.GetComponent<Text>().text = "100%";
        }


    }
}
