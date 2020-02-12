using UnityEngine;

public class Scr_Statistic : MonoBehaviour
{
    public GameObject StaticPage;
    public GameObject Line;
    public GameObject MainPage;

    int TempPointA;
    int TempPointB;

    void Awake()
    {
        StaticPage.SetActive(false);
        Line.SetActive(false);


    }

    public void OpenStatiPage()
    {
        StaticPage.SetActive(true);
        Line.SetActive(true);
        MainPage.SetActive(false);
    }

    public void CloseStatiPage()
    {
        StaticPage.SetActive(false);
        MainPage.SetActive(true);
    }

}
