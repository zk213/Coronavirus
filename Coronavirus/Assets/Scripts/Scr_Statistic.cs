using UnityEngine;

public class Scr_Statistic : MonoBehaviour
{
    public GameObject StaticPage;
    public GameObject MainPage;

    void Awake()
    {
        StaticPage.SetActive(false);


    }

    public void OpenStatiPage()
    {
        StaticPage.SetActive(true);
        MainPage.SetActive(false);
    }

    public void CloseStatiPage()
    {
        StaticPage.SetActive(false);
        MainPage.SetActive(true);
    }

}
