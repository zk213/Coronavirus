using UnityEngine;
using UnityEngine.UI;

public class Scr_Load : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Scene2;
    public GameObject Scene3;
    public GameObject BlackG;

    Scr_TimeControl Time;
    Scr_Color Provinces;
    Scr_Event Events;
    Scr_Num Num;
    Scr_Tech Tech;
    Scr_News News;

    public bool StartControl = false;
    public bool isLoad = false;
    int t = -1;
    float changeSpeed = 0.1f;
    int alpha = 255;
    void Start()
    {
        Time = FindObjectOfType<Scr_TimeControl>();
        Provinces = FindObjectOfType<Scr_Color>();
        Events = FindObjectOfType<Scr_Event>();
        Num = FindObjectOfType<Scr_Num>();
        Tech = FindObjectOfType<Scr_Tech>();
        News = FindObjectOfType<Scr_News>();
        BlackG.SetActive(false);
    }

    public void TurnButton()
    {
        t = 0;
        LoadScene();
    }

    void LoadScene()
    {
        Scene3.SetActive(true);
        Scene2.SetActive(false);
        BlackG.SetActive(true);
        Time.Start1();
        Num.Start1();
        Provinces.Start3();
        Events.Start2();
        Tech.Start2();
        News.Start1();
    }
    void Update()
    {
        if (isLoad)
        {
            isLoad = false;
            LoadScene();
            t = 0;
        }
        if (t >= 0)
        {
            t++;
            if (alpha > 0)
            {
                BlackG.GetComponent<Image>().color = new Color32(0, 0, 0, (byte)alpha);
                alpha -= (int)(t * changeSpeed);
            }
            else
            {
                StartControl = true;
                alpha = 255;
                t = -1;
                BlackG.SetActive(false);
            }
        }
    }
}
