using OfficeOpenXml;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Scr_Tech : MonoBehaviour
{
    void TechEffect()
    {
        switch (TechIndex)
        {
            case 0:
                Debug.Log("政策1升级");
                break;
            case 1:
                Debug.Log("政策2升级");
                break;
            case 2:
                Debug.Log("医疗1升级");
                break;
            case 3:
                Debug.Log("医疗2升级");
                break;
            case 4:
                Debug.Log("媒体1升级");
                break;
            case 5:
                Debug.Log("媒体2升级");
                break;
            default:
                break;
        }
    }

    public Text TechText;
    public RectTransform parent;
    //public RectTransform parent2;
    public Object prefab;

    public List<GameObject> Tech = new List<GameObject>();

    public int TechIndex;
    public int page = 2;
    public bool TextUpdate;
    public bool canUpgrade;



    TechnologyData TechData;
    Scr_Mode mode;
    Scr_Num Value;

    string TechTitle;
    string TechDescribe;



    void Awake()
    {
        mode = FindObjectOfType<Scr_Mode>();
        Value = FindObjectOfType<Scr_Num>();
        TechData = Resources.Load<TechnologyData>("ScriptableObject/Technology");
        TechIndex = -1;
        TextUpdate = true;
        canUpgrade = false;
        //对科技摁扭的初始化
        for (int i = 0; i < TechData.technologyGroup.Count; i++)
        {
            Tech.Add((GameObject)Instantiate(prefab, transform.position, transform.rotation));

            switch (TechData.technologyGroup[i].type)
            {
                case TypeTechnology.gover:
                    page = 2;
                    break;
                case TypeTechnology.medicine:
                    page = 3;
                    break;
                case TypeTechnology.media:
                    page = 4;
                    break;
                default:
                    page = 2;
                    break;
            }
            Tech[i].transform.SetParent(parent.transform.Find("UpGradePage" + page.ToString()), false);
            if (File.Exists(Application.dataPath + "/Resources/" + TechData.technologyGroup[i].picture + ".png"))
            {
                //Tech[i].transform.Find("TechIcon").GetComponent<Image>().sprite = Resources.Load(TechData.technologyGroup[i].picture, typeof(Sprite)) as Sprite;
                Tech[i].GetComponent<Image>().sprite = Resources.Load(TechData.technologyGroup[i].picture, typeof(Sprite)) as Sprite;
            }
            else
            {
                Tech[i].GetComponent<Image>().sprite = Resources.Load("TechnologyPictures/1231", typeof(Sprite)) as Sprite;
            }
            Tech[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(TechData.technologyGroup[i].Posx, TechData.technologyGroup[i].Posy);
            Tech[i].GetComponent<Scr_TechButton>().LocalIndex = i;
            Tech[i].GetComponent<Scr_TechButton>().father = TechData.technologyGroup[i].father;
            Tech[i].GetComponent<Scr_TechButton>().cost = TechData.technologyGroup[i].cost;

        }

    }

    // Update is called once per frame
    void Update()
    {
        if (TextUpdate)
        {
            if (TechIndex >= -1)
            {
                LoadText(TechIndex);
            }
            else
            {
                TechIndex = -1;
            }
            if (page != 1)
            {
                parent.transform.Find("UpGradePage" + page.ToString()).transform.Find("UpGradePage" + page.ToString() + "Ground").transform.Find("Describe").GetComponent<Text>().text = TechTitle + "\n\n" + TechDescribe;
            }
        }
        if (page != 1)
        {
            if (canUpgrade)
            {
                parent.transform.Find("UpGradePage" + page.ToString()).transform.Find("UpGradePage" + page.ToString() + "Ground").transform.Find("Button").GetComponent<Image>().color = Color.white;
            }
            else
            {
                parent.transform.Find("UpGradePage" + page.ToString()).transform.Find("UpGradePage" + page.ToString() + "Ground").transform.Find("Button").GetComponent<Image>().color = Color.grey;
            }
        }

    }

    void LoadExcel(ExcelWorksheets workSheets, int SheetIndex, int TechnologyIndex)
    {
        ExcelWorksheet workSheet = workSheets[SheetIndex];//表1，即简体中文那张表
        int rowCount = workSheet.Dimension.End.Row;//统计表的列数
        if (TechIndex != -1)
        {
            bool hasTitle = false;
            bool hasDescribe = false;
            for (int row = 1; row <= rowCount; row++)
            {
                var text = workSheet.Cells[row, 1].Text ?? "Name Error";

                if (text == TechData.technologyGroup[TechnologyIndex].title)
                {
                    TechTitle = workSheet.Cells[row, 2].Text ?? "Title Error";
                    hasTitle = true;
                }
                if (text == TechData.technologyGroup[TechnologyIndex].describe)
                {
                    TechDescribe = workSheet.Cells[row, 2].Text ?? "Describe Error";
                    hasDescribe = true;
                }
            }
            if (!hasTitle)
            {
                TechTitle = TechData.technologyGroup[TechnologyIndex].title;
            }
            if (!hasDescribe)
            {
                TechDescribe = TechData.technologyGroup[TechnologyIndex].describe;
            }
        }
        else
        {
            bool hasTitle = false;
            bool hasDescribe = false;
            for (int row = 1; row <= rowCount; row++)
            {
                var text = workSheet.Cells[row, 1].Text ?? "Name Error";

                if (text == "Introduce")
                {
                    TechTitle = workSheet.Cells[row, 2].Text ?? "Title Error";
                    hasTitle = true;
                }
                if (text == "Introduce_Describe")
                {
                    TechDescribe = workSheet.Cells[row, 2].Text ?? "Describe Error";
                    hasDescribe = true;
                }
            }
            if (!hasTitle)
            {
                TechTitle = "Title Error";
            }
            if (!hasDescribe)
            {
                TechDescribe = "Describe Error";
            }
        }
    }

    void LoadText(int i)
    {
        using (FileStream fs = new FileStream(Application.dataPath + "/Resources/Localization/Technology.xlsx", FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
        {
            using (ExcelPackage excel = new ExcelPackage(fs))
            {
                ExcelWorksheets workSheets = excel.Workbook.Worksheets;
                switch (mode.Language)
                {
                    case "SimpleChinese":
                        LoadExcel(workSheets, 1, i);
                        break;
                    default:
                        LoadExcel(workSheets, 1, i);
                        break;
                }
            }
        }
    }

    public void CloseDescribe()
    {
        TextUpdate = true;
        TechIndex = -1;
        canUpgrade = false;
    }

    public void UpGradeButton()
    {
        if (canUpgrade)
        {
            Value.InfluenceVal -= TechData.technologyGroup[TechIndex].cost;
            Tech[TechIndex].GetComponent<Scr_TechButton>().isLock = false;
            canUpgrade = false;
            TechEffect();
        }
    }
}
