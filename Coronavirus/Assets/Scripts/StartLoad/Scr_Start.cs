using UnityEngine;
using UnityEngine.UI;

public class Scr_Start : MonoBehaviour
{

    public GameObject SceneT;
    public GameObject Scene0;
    public GameObject Scene1;
    public GameObject Scene2;
    public GameObject Scene3;
    public GameObject CG;
    public GameObject BlackG;
    public GameObject TextProcess;
    public GameObject TextInfor;

    Scr_Color Provinces;
    Scr_Event Events;
    Scr_Tech Tech;

    public int step = 0;

    bool LoadOver = false;
    bool LoadStart = false;
    int stepFull = 0;
    int t = 0;
    bool alphaMinus = false;
    float changeSpeed = 0.1f;
    int alpha = 255;
    void Start()
    {
        SceneT.SetActive(true);
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


        if (!LoadStart)
        {
            t++;
            TextInfor.GetComponent<Text>().text = "";
            TextProcess.GetComponent<Text>().text = "";

            if (alpha > 0)
            {
                BlackG.GetComponent<Image>().color = new Color32(0, 0, 0, (byte)alpha);
                alpha -= (int)(t * changeSpeed);
            }
            else
            {
                LoadStart = true;
                alpha = 0;
                t = 0;
            }
            return;
        }
        if (LoadOver)
        {
            t++;
            if (!alphaMinus)
            {
                if (alpha < 255)
                {
                    BlackG.GetComponent<Image>().color = new Color32(0, 0, 0, (byte)alpha);
                    alpha += (int)(t * changeSpeed);
                    return;
                }
                else
                {
                    alpha = 255;
                    alphaMinus = true;
                    Scene1.SetActive(true);
                    CG.SetActive(false);
                }
            }

            if (alpha > 0)
            {
                BlackG.GetComponent<Image>().color = new Color32(0, 0, 0, (byte)alpha);
                alpha -= (int)(t * changeSpeed);
            }
            else
            {
                Scene0.SetActive(false);
                gameObject.SetActive(false);
            }
            return;
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

        if (step > stepFull + 5)
        {
            LoadOver = true;

        }
        if (!LoadOver)
        {
            TextProcess.GetComponent<Text>().text = string.Format("{0:F0}", ((float)step / (float)stepFull) * 100) + "%";
            step += 1;

        }
        if (step > stepFull)
        {
            TextInfor.GetComponent<Text>().text = "加载完成";
            TextProcess.GetComponent<Text>().text = "100%";
        }
    }
}
