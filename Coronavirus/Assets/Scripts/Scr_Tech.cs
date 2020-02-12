﻿using OfficeOpenXml;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using UnityEngine;
using UnityEngine.UI;

public class Scr_Tech : MonoBehaviour
{
    void TechEffect()
    {
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(Application.dataPath + "/Resources/Xml/Tech.xml");
        XmlElement xmlNode = xmlDoc.DocumentElement;
        foreach (XmlNode elements in xmlNode)
        {
            XmlElement element = elements as XmlElement;
            if (element == null)
                continue;
            if (element.LocalName == "tech")
            {
                if (element.Attributes["id"].Value == TechIndex.ToString())
                {
                    XmlElement effects = element.SelectSingleNode("effects") as XmlElement;
                    int.TryParse(effects.Attributes["amount"].Value, out int amount);
                    for (int a = 0; a < amount; a++)
                    {
                        var effect = effects.ChildNodes[a] as XmlElement;
                        switch (effect.Attributes["type"].Value)
                        {
                            case "message":
                                Debug.Log(effect.InnerText);
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
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



    Scr_Mode mode;
    Scr_Num Value;

    string TechTitle;
    string TechDescribe;

    int count = 0;

    void Awake()
    {
        mode = FindObjectOfType<Scr_Mode>();
        Value = FindObjectOfType<Scr_Num>();
        TechIndex = -1;
        TextUpdate = true;
        canUpgrade = false;
        //对科技摁扭的初始化
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(Application.dataPath + "/Resources/Xml/Tech.xml");
        XmlElement xmlNode = xmlDoc.DocumentElement;
        foreach (XmlNode elements in xmlNode)
        {
            XmlElement element = elements as XmlElement;
            if (element == null)
                continue;
            if (element.LocalName == "techAmount")
            {
                int.TryParse(element.InnerText, out count);
            }
            if (element.LocalName == "tech")
            {
                int.TryParse(element.Attributes["id"].Value, out int i);
                Tech.Add((GameObject)Instantiate(prefab, transform.position, transform.rotation));

                switch (element.SelectSingleNode("type").InnerText)
                {
                    case "gover":
                        page = 2;
                        break;
                    case "medicine":
                        page = 3;
                        break;
                    case "media":
                        page = 4;
                        break;
                    default:
                        page = 2;
                        break;
                }
                Tech[i].transform.SetParent(parent.transform.Find("UpGradePage" + page.ToString()), false);
                if (File.Exists(Application.dataPath + "/Resources/" + element.SelectSingleNode("picture").InnerText + ".png"))
                {
                    //Tech[i].transform.Find("TechIcon").GetComponent<Image>().sprite = Resources.Load(TechData.technologyGroup[i].picture, typeof(Sprite)) as Sprite;
                    Tech[i].GetComponent<Image>().sprite = Resources.Load(element.SelectSingleNode("picture").InnerText, typeof(Sprite)) as Sprite;
                }
                else
                {
                    Tech[i].GetComponent<Image>().sprite = Resources.Load("TechnologyPictures/1231", typeof(Sprite)) as Sprite;
                }
                int.TryParse(element.SelectSingleNode("posx").InnerText, out int posx);
                int.TryParse(element.SelectSingleNode("posy").InnerText, out int posy);
                Tech[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(posx, posy);
                Tech[i].GetComponent<Scr_TechButton>().LocalIndex = i;
                var fatherNode = element.GetElementsByTagName("father");
                if (fatherNode.Count != 0)
                {
                    var father = fatherNode[0] as XmlElement;
                    int.TryParse(father.InnerText, out int fatherIndex);
                    Tech[i].GetComponent<Scr_TechButton>().father = fatherIndex;
                }
                else
                {
                    Tech[i].GetComponent<Scr_TechButton>().father = -1;
                }
                int.TryParse(element.SelectSingleNode("cost").InnerText, out int cost);
                Tech[i].GetComponent<Scr_TechButton>().cost = cost;


            }
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
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(Application.dataPath + "/Resources/Xml/Tech.xml");
            XmlElement xmlNode = xmlDoc.DocumentElement;
            foreach (XmlNode elements in xmlNode)
            {
                XmlElement element = elements as XmlElement;
                if (element == null)
                    continue;
                if (element.LocalName == "tech")
                {
                    if (element.Attributes["id"].Value == TechIndex.ToString())
                    {
                        bool hasTitle = false;
                        bool hasDescribe = false;
                        for (int row = 1; row <= rowCount; row++)
                        {
                            var text = workSheet.Cells[row, 1].Text ?? "Name Error";

                            if (text == element.SelectSingleNode("title").InnerText)
                            {
                                TechTitle = workSheet.Cells[row, 2].Text ?? "Title Error";
                                hasTitle = true;
                            }
                            if (text == element.SelectSingleNode("describe").InnerText)
                            {
                                TechDescribe = workSheet.Cells[row, 2].Text ?? "Describe Error";
                                hasDescribe = true;
                            }
                        }
                        if (!hasTitle)
                        {
                            TechTitle = element.SelectSingleNode("title").InnerText;
                        }
                        if (!hasDescribe)
                        {
                            TechDescribe = element.SelectSingleNode("describe").InnerText;
                        }
                    }
                }
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
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(Application.dataPath + "/Resources/Xml/Tech.xml");
            XmlElement xmlNode = xmlDoc.DocumentElement;
            foreach (XmlNode elements in xmlNode)
            {
                XmlElement element = elements as XmlElement;
                if (element == null)
                    continue;
                if (element.LocalName == "tech")
                {
                    if (element.Attributes["id"].Value == TechIndex.ToString())
                    {
                        int.TryParse(element.SelectSingleNode("cost").InnerText, out int cost);
                        Value.InfluenceVal -= cost;
                    }
                }
            }
            Tech[TechIndex].GetComponent<Scr_TechButton>().isLock = false;
            canUpgrade = false;
            TechEffect();
        }
    }
}
