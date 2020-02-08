using UnityEngine;

public class Scr_Upgrade : MonoBehaviour
{
    public GameObject UpGradePage;
    public GameObject MainPage;

    void Awake()
    {
        UpGradePage.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OpenUpGradePage()
    {
        UpGradePage.SetActive(true);
        MainPage.SetActive(false);
    }

    public void CloseUpGradePage()
    {
        UpGradePage.SetActive(false);
        MainPage.SetActive(true);
    }
}
