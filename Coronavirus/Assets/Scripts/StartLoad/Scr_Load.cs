using UnityEngine;

public class Scr_Load : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Scene2;
    public GameObject Scene3;

    Scr_TimeControl Time;
    Scr_Color Provinces;
    Scr_Event Events;
    Scr_Num Num;
    Scr_Tech Tech;
    Scr_News News;

    public bool StartControl = false;
    void Start()
    {
        Time = FindObjectOfType<Scr_TimeControl>();
        Provinces = FindObjectOfType<Scr_Color>();
        Events = FindObjectOfType<Scr_Event>();
        Num = FindObjectOfType<Scr_Num>();
        Tech = FindObjectOfType<Scr_Tech>();
        News = FindObjectOfType<Scr_News>();
    }

    public void LoadScene()
    {
        Scene3.SetActive(true);
        Scene2.SetActive(false);
        Time.Start1();
        Num.Start1();
        Provinces.Start3();
        Events.Start2();
        Tech.Start2();
        News.Start1();
        StartControl = true;
    }
}
