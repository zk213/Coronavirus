using OfficeOpenXml;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Scr_Tech : MonoBehaviour
{
    public Text TechText;
    public RectTransform parent;
    public Object prefab;

    public List<GameObject> Tech = new List<GameObject>();

    public int TechIndex;
    public bool TextUpdate;



    TechnologyData TechData;
    Scr_Mode mode;

    string TechTitle;
    string TechDescribe;


    void Awake()
    {
        mode = FindObjectOfType<Scr_Mode>();
        TechData = Resources.Load<TechnologyData>("ScriptableObject/Technology");
        TechIndex = -1;
        TextUpdate = false;
        //对科技摁扭的初始化
        for (int i = 0; i < TechData.technologyGroup.Count; i++)
        {
            Tech.Add((GameObject)Instantiate(prefab, transform.position, transform.rotation));
            Tech[i].transform.SetParent(parent, false);
            if (File.Exists(Application.dataPath + "/Resources/" + TechData.technologyGroup[i].picture + ".png"))
            {
                Tech[i].transform.Find("TechIcon").GetComponent<Image>().sprite = Resources.Load(TechData.technologyGroup[i].picture, typeof(Sprite)) as Sprite;
            }
            else
            {
                Tech[i].transform.Find("TechIcon").GetComponent<Image>().sprite = Resources.Load("TechnologyPictures/1231", typeof(Sprite)) as Sprite;
            }
            Tech[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(TechData.technologyGroup[i].Posx, TechData.technologyGroup[i].Posy);
            Tech[i].GetComponent<Scr_TechButton>().LocalIndex = i;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (TextUpdate)
        {
            LoadText(TechIndex);
            TechText.text = TechTitle + "\n\n" + TechDescribe;
        }
    }

    void LoadExcel(ExcelWorksheets workSheets, int SheetIndex, int NewsIndex)
    {
        ExcelWorksheet workSheet = workSheets[SheetIndex];//表1，即简体中文那张表
        int rowCount = workSheet.Dimension.End.Row;//统计表的列数
        bool hasTitle = false;
        bool hasDescribe = false;
        for (int row = 1; row <= rowCount; row++)
        {
            var text = workSheet.Cells[row, 1].Text ?? "Name Error";

            if (text == TechData.technologyGroup[NewsIndex].title)
            {
                TechTitle = workSheet.Cells[row, 2].Text ?? "Title Error";
                hasTitle = true;
            }
            if (text == TechData.technologyGroup[NewsIndex].describe)
            {
                TechDescribe = workSheet.Cells[row, 2].Text ?? "Describe Error";
                hasDescribe = true;
            }
        }
        if (!hasTitle)
        {
            TechTitle = TechData.technologyGroup[NewsIndex].title;
        }
        if (!hasDescribe)
        {
            TechDescribe = TechData.technologyGroup[NewsIndex].describe;
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

}
