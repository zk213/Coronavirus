using UnityEngine;
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



    Scr_Tech Tech;

    void OnEnable()
    {
        Tech = FindObjectOfType<Scr_Tech>();
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
            PressPage1.SetActive(false);
            PressPage2.SetActive(false);
            PressPage3.SetActive(false);
        }
    }

    public void OpenUpGradePage1()
    {
        UpGradePage.SetActive(true);
        MainPage.SetActive(false);
        PressPage1.SetActive(true);
        Tech.TechIndex = -1;
        Tech.page = 1;
        Tech.TextUpdate = true;
    }
    public void OpenUpGradePage2()
    {
        UpGradePage.SetActive(true);
        MainPage.SetActive(false);
        PressPage2.SetActive(true);
        Tech.TechIndex = -1;
        Tech.page = 2;
        Tech.TextUpdate = true;
    }
    public void OpenUpGradePage3()
    {
        UpGradePage.SetActive(true);
        MainPage.SetActive(false);
        PressPage3.SetActive(true);
        Tech.TechIndex = -1;
        Tech.page = 3;
        Tech.TextUpdate = true;
    }

    public void CloseUpGradePage()
    {
        UpGradePage.SetActive(false);
        MainPage.SetActive(true);
        PressPage1.SetActive(false);
        PressPage2.SetActive(false);
        PressPage3.SetActive(false);
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
