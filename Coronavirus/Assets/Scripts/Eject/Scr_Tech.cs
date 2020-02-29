using OfficeOpenXml;
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
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
        }
    }

    public void LocalSave()
    {
        string SaveString = "";
        for (int i = 0; i < Tech.Count; i++)
        {
            if (!Tech[i].GetComponent<Scr_TechButton>().isLock)
            {
                if (SaveString.Length == 0)
                {
                    SaveString = i.ToString();
                }
                else
                {
                    SaveString += "," + i.ToString();
                }
            }
        }
        XmlDocument xmlSave = new XmlDocument();
        xmlSave.Load(Application.persistentDataPath + "/save/Save.save");
        XmlElement xmlNodeS = xmlSave.DocumentElement;
        foreach (XmlNode elementsS in xmlNodeS)
        {
            if (elementsS == null)
                continue;
            if (elementsS.LocalName == "Tech")
            {
                elementsS.InnerText = SaveString;
            }
        }
        xmlSave.Save(Application.persistentDataPath + "/save/Save.save");
    }

    //public Text TechText;
    public RectTransform parent;
    //public RectTransform parent2;
    public Object prefab;

    public List<GameObject> Tech = new List<GameObject>();
    public List<string> Label = new List<string>();


    public int TechIndex;
    public int page = 2;
    public bool TextUpdate;
    public bool canUpgrade;
    public bool hasLabel;

    List<int> finishTech = new List<int>();

    Scr_Num Value;

    string TechTitle;
    string TechDescribe;

    int count = 0;

    int Language = 1;
    bool isLoad = false;

    Color32 ButtonCol = new Color32(175, 219, 240, 255);
    Color32 ButtonCol2 = Color.grey;

    public void Start1()
    {
        //对科技摁扭的初始化
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(Application.dataPath + "/Resources/Xml/Tech.xml");
        XmlElement xmlNode = xmlDoc.DocumentElement;
        Color32 baseColor;
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
                        baseColor = new Color32(102, 202, 255, 255);
                        break;
                    case "medicine":
                        page = 3;
                        baseColor = new Color32(140, 255, 171, 255);
                        break;
                    default:
                        page = 2;
                        baseColor = new Color32(102, 202, 255, 255);
                        break;
                }
                Tech[i].transform.SetParent(parent.transform.Find("UpGradePage" + page.ToString()), false);
                Tech[i].transform.Find("TechBase").GetComponent<Image>().color = baseColor;
                if (File.Exists(Application.dataPath + "/Resources/" + element.SelectSingleNode("picture").InnerText + ".png"))
                {
                    //Tech[i].transform.Find("TechIcon").GetComponent<Image>().sprite = Resources.Load(TechData.technologyGroup[i].picture, typeof(Sprite)) as Sprite;
                    Tech[i].transform.Find("TechIcon").GetComponent<Image>().sprite = Resources.Load(element.SelectSingleNode("picture").InnerText, typeof(Sprite)) as Sprite;
                }
                else
                {
                    Tech[i].transform.Find("TechIcon").GetComponent<Image>().sprite = Resources.Load("TechnologyPictures/TechError", typeof(Sprite)) as Sprite;
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
                Tech[i].transform.name = element.SelectSingleNode("title").InnerText;
            }
        }
    }
    public void Start2()
    {
        Language = 1;
        isLoad = false;
        finishTech = new List<int>();
        TechIndex = -1;
        TextUpdate = true;
        canUpgrade = false;
        XmlDocument SxmlDoc = new XmlDocument();
        SxmlDoc.Load(Application.persistentDataPath + "/setting.set");
        XmlElement SxmlNode = SxmlDoc.DocumentElement;
        foreach (XmlNode elements in SxmlNode)
        {
            if (elements == null)
                continue;
            if (elements.LocalName == "SMode")
            {
                if (elements.InnerText == "Load")
                {
                    isLoad = true;
                }
            }
            if (elements.LocalName == "Language")
            {
                int.TryParse(elements.InnerText, out Language);
            }
        }
        if (isLoad)
        {
            XmlDocument xmlSave = new XmlDocument();
            xmlSave.Load(Application.persistentDataPath + "/save/Save.save");
            XmlElement xmlNodeS = xmlSave.DocumentElement;
            foreach (XmlNode elementsS in xmlNodeS)
            {
                if (elementsS == null)
                    continue;
                if (elementsS.LocalName == "Tech")
                {
                    if (elementsS.InnerText != "")
                    {
                        string[] finishAry = elementsS.InnerText.Split(',');
                        for (int a = 0; a < finishAry.Length; a++)
                        {
                            int.TryParse(finishAry[a], out int b);
                            finishTech.Add(b);
                        }
                    }
                }
            }
        }
        for (int i = 0; i < Tech.Count; i++)
        {
            if (finishTech.Contains(i))
            {
                Tech[i].GetComponent<Scr_TechButton>().isLock = false;
            }
        }
        using (FileStream fs = new FileStream(Application.dataPath + "/Resources/Localization/Button.xlsx", FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
        {
            using (ExcelPackage excel = new ExcelPackage(fs))
            {
                ExcelWorksheets workSheets = excel.Workbook.Worksheets;
                ExcelWorksheet workSheet = workSheets[Language];//表1，即简体中文那张表
                int rowCount = workSheet.Dimension.End.Row;//统计表的列数

                for (int row = 1; row <= rowCount; row++)
                {
                    var text = workSheet.Cells[row, 1].Text ?? "Name Error";

                    if (text == "NationalCondition")
                    {
                        Label[0] = workSheet.Cells[row, 2].Text ?? "Label Error";
                    }
                    if (text == "PolicyIssuance")
                    {
                        Label[1] = workSheet.Cells[row, 2].Text ?? "Label Error";
                    }
                    if (text == "MedicalResearch")
                    {
                        Label[2] = workSheet.Cells[row, 2].Text ?? "Label Error";
                    }
                    if (text == "Back")
                    {
                        Label[3] = workSheet.Cells[row, 2].Text ?? "Label Error";
                    }
                }
            }
        }
        hasLabel = true;
    }

    void Awake()
    {


        Value = FindObjectOfType<Scr_Num>();
        TechIndex = -1;
        TextUpdate = true;
        canUpgrade = false;
        for (int i = 0; i < 4; i++)
        {
            Label.Add("");
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
                parent.transform.Find("UpGradePage" + page.ToString()).transform.Find("UpGradePage" + page.ToString() + "Ground").transform.Find("Button").GetComponent<Image>().color = ButtonCol;
            }
            else
            {
                parent.transform.Find("UpGradePage" + page.ToString()).transform.Find("UpGradePage" + page.ToString() + "Ground").transform.Find("Button").GetComponent<Image>().color = ButtonCol2;
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
                LoadExcel(workSheets, Language, i);
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
