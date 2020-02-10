using UnityEngine;
using UnityEngine.UI;

public class Scr_Upgrade : MonoBehaviour
{
    [HideInInspector]
    public int PressNum;

    public GameObject UpGradePage;
    public GameObject MainPage;
    public GameObject PressPage1;
    public GameObject PressPage2;
    public GameObject PressPage3;
    public GameObject PressPage4;
    public Image Press1;
    public Image Press2;
    public Image Press3;
    public Image Press4;
    public Text PressText1;
    public Text PressText2;
    public Text PressText3;
    public Text PressText4;

    bool isUpdateButton;


    void OnEnable()
    {
        PressNum = 1;
        isUpdateButton = true;
        UpGradePage.SetActive(false);
        PressPage1.SetActive(true);
        PressPage2.SetActive(false);
        PressPage3.SetActive(false);
        PressPage4.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateButton();

    }

    public void OpenUpGradePage()
    {
        UpGradePage.SetActive(true);
        MainPage.SetActive(false);
        PressNum = 1;
        isUpdateButton = true;
    }

    public void CloseUpGradePage()
    {
        UpGradePage.SetActive(false);
        MainPage.SetActive(true);
    }

    public void Press1Effect()
    {
        PressNum = 1;
        isUpdateButton = true;
    }
    public void Press2Effect()
    {
        PressNum = 2;
        isUpdateButton = true;
    }
    public void Press3Effect()
    {
        PressNum = 3;
        isUpdateButton = true;
    }
    public void Press4Effect()
    {
        PressNum = 4;
        isUpdateButton = true;
    }
    void UpdateButton()
    {
        if (isUpdateButton)
        {
            switch (PressNum)
            {
                case 1:
                    Press1.sprite = Resources.Load("UI/Upgrade/UpGrade_Press1_2", typeof(Sprite)) as Sprite;
                    Press2.sprite = Resources.Load("UI/Upgrade/UpGrade_Press2_1", typeof(Sprite)) as Sprite;
                    Press3.sprite = Resources.Load("UI/Upgrade/UpGrade_Press2_1", typeof(Sprite)) as Sprite;
                    Press4.sprite = Resources.Load("UI/Upgrade/UpGrade_Press3_1", typeof(Sprite)) as Sprite;
                    PressPage1.SetActive(true);
                    PressPage2.SetActive(false);
                    PressPage3.SetActive(false);
                    PressPage4.SetActive(false);
                    break;
                case 2:
                    Press1.sprite = Resources.Load("UI/Upgrade/UpGrade_Press1_1", typeof(Sprite)) as Sprite;
                    Press2.sprite = Resources.Load("UI/Upgrade/UpGrade_Press2_2", typeof(Sprite)) as Sprite;
                    Press3.sprite = Resources.Load("UI/Upgrade/UpGrade_Press2_1", typeof(Sprite)) as Sprite;
                    Press4.sprite = Resources.Load("UI/Upgrade/UpGrade_Press3_1", typeof(Sprite)) as Sprite;
                    PressPage1.SetActive(false);
                    PressPage2.SetActive(true);
                    PressPage3.SetActive(false);
                    PressPage4.SetActive(false);
                    break;
                case 3:
                    Press1.sprite = Resources.Load("UI/Upgrade/UpGrade_Press1_1", typeof(Sprite)) as Sprite;
                    Press2.sprite = Resources.Load("UI/Upgrade/UpGrade_Press2_1", typeof(Sprite)) as Sprite;
                    Press3.sprite = Resources.Load("UI/Upgrade/UpGrade_Press2_2", typeof(Sprite)) as Sprite;
                    Press4.sprite = Resources.Load("UI/Upgrade/UpGrade_Press3_1", typeof(Sprite)) as Sprite;
                    PressPage1.SetActive(false);
                    PressPage2.SetActive(false);
                    PressPage3.SetActive(true);
                    PressPage4.SetActive(false);
                    break;
                case 4:
                    Press1.sprite = Resources.Load("UI/Upgrade/UpGrade_Press1_1", typeof(Sprite)) as Sprite;
                    Press2.sprite = Resources.Load("UI/Upgrade/UpGrade_Press2_1", typeof(Sprite)) as Sprite;
                    Press3.sprite = Resources.Load("UI/Upgrade/UpGrade_Press2_1", typeof(Sprite)) as Sprite;
                    Press4.sprite = Resources.Load("UI/Upgrade/UpGrade_Press3_2", typeof(Sprite)) as Sprite;
                    PressPage1.SetActive(false);
                    PressPage2.SetActive(false);
                    PressPage3.SetActive(false);
                    PressPage4.SetActive(true);
                    break;
                default:
                    PressNum = Mathf.Clamp(PressNum, 1, 4);
                    break;
            }
            isUpdateButton = false;
        }
    }
}
