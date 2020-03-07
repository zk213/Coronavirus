using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.UI;


public class Scr_Statistic : MonoBehaviour
{
    public GameObject MainPage;
    public Transform father;

    public Object prefab;


    int MaxShow = 12;//最大显示数量
    Scr_Color Provinces;

    public float LineWidth = 5;

    List<float> NewSuspected = new List<float>();
    List<float> NewInfected = new List<float>();
    List<float> AllSuspected = new List<float>();
    List<float> AllInfected = new List<float>();
    List<float> AllDead = new List<float>();
    List<float> AllCure = new List<float>();
    List<string> Date = new List<string>();
    List<GameObject> lines = new List<GameObject>();
    Color32 CNSuspected = new Color32(255, 204, 153, 255);
    Color32 CNInfected = new Color32(255, 102, 0, 255);
    Color32 CSuspected = new Color32(255, 192, 0, 255);
    Color32 CInfected = new Color32(247, 76, 49, 255);
    Color32 CDead = new Color32(255, 204, 153, 255);
    Color32 CCure = new Color32(64, 204, 204, 255);

    float MaxH = 176;
    float MaxW = 1185;
    float iniX = -570;
    float iniY = -108;

    int Language;
    string SAdd;
    string SAll;
    string SSaI;
    string SPeo;
    string SDaC;
    Scr_TimeControl Time;

    public void Start1()
    {
        XmlDocument SxmlDoc = new XmlDocument();
        SxmlDoc.Load(Application.persistentDataPath + "/setting.set");
        XmlElement SxmlNode = SxmlDoc.DocumentElement;
        foreach (XmlNode elements in SxmlNode)
        {
            if (elements == null)
                continue;
            if (elements.LocalName == "Language")
            {
                int.TryParse(elements.InnerText, out Language);

            }
        }
        string LocalizationText = ((TextAsset)Resources.Load("Localization/" + Language.ToString() + "/Chart")).text;
        string[] LocalizationArray = LocalizationText.Split('\n');
        for (int l = 0; l < LocalizationArray.Length; l++)
        {
            string[] LocalArray = LocalizationArray[l].Split(':');
            switch (LocalArray[0])
            {
                case "Add":
                    SAdd = LocalArray[1];
                    break;
                case "All":
                    SAll = LocalArray[1];
                    break;
                case "SaI":
                    SSaI = LocalArray[1];
                    break;
                case "Peo":
                    SPeo = LocalArray[1];
                    break;
                case "DaC":
                    SDaC = LocalArray[1];
                    break;
            }

        }
    }



    void Awake()
    {
        Provinces = FindObjectOfType<Scr_Color>();
        Time = FindObjectOfType<Scr_TimeControl>();
    }
    void Update()
    {
        if (Provinces.chartUpdate)
        {
            Date.Add(Time.Month.ToString() + "/" + Time.Day.ToString());
            Provinces.chartUpdate = false;
            if (AllSuspected.Count == 0)
            {
                NewSuspected.Add(Provinces.GlobalSuspectedPeople);
            }
            else
            {
                NewSuspected.Add(AllSuspected[AllSuspected.Count - 1]);
            }
            if (AllInfected.Count == 0)
            {
                NewInfected.Add(Provinces.GlobalPeople);
            }
            else
            {
                NewInfected.Add(AllInfected[AllInfected.Count - 1]);
            }
            AllSuspected.Add(Provinces.GlobalSuspectedPeople);
            AllInfected.Add(Provinces.GlobalPeople);
            AllDead.Add(Provinces.GlobalDeadPeople);
            AllCure.Add(Provinces.GlobalCurePeople);
        }
    }
    public void OpenStatiPage()
    {
        lines = new List<GameObject>();

        float TempMax = 0;
        float highNum = 0;
        int _14 = 0;
        if (NewSuspected.Count > MaxShow)
        {
            _14 = NewSuspected.Count - MaxShow;
        }
        for (int i = _14; i < NewSuspected.Count; i++)
        {
            if (NewSuspected[i] > TempMax)
            {
                TempMax = NewSuspected[i];
            }
            if (NewInfected[i] > TempMax)
            {
                TempMax = NewInfected[i];
            }
        }
        if (TempMax >= 0)//对整数部分进行判断
        {
            int digit = TempMax.ToString().Length;
            int modNum = 0;
            switch (digit)
            {
                case 1:
                    if (TempMax < 6)
                    {
                        highNum = 6;
                    }
                    else
                    {
                        highNum = 12;
                    }
                    break;
                case 2:
                    if (TempMax < 60)
                    {
                        highNum = 60;
                    }
                    else
                    {
                        highNum = 120;
                    }
                    break;

                default:
                    int.TryParse(TempMax.ToString().Substring(0, 1), out modNum);
                    highNum = 12 * Mathf.Pow(10, digit - 2) * (modNum + 1);
                    break;
            }
        }
        float perDis = MaxW / (NewSuspected.Count - _14 + 1);

        float perH = MaxH / highNum;
        //左端文字
        for (int a = 0; a < 7; a++)
        {
            father.Find("ChartBG1/LeftText/Text" + (a + 1).ToString()).GetComponent<Text>().text = (highNum / 6 * a).ToString();
        }
        father.Find("ChartBG1/TopText").GetComponent<Text>().text = SAdd + "<color=blue><size=35>" + SSaI + "</size></color>" + SPeo;//
        int LineNum = 0;
        for (int i = _14; i < NewSuspected.Count; i++)
        {
            lines.Add((GameObject)Instantiate(prefab, transform.position, transform.rotation));
            lines[LineNum].transform.SetParent(father.Find("ChartBG1"));
            lines[LineNum].transform.name = "Point" + i.ToString();
            lines[LineNum].GetComponent<RectTransform>().anchoredPosition = new Vector2(iniX + perDis * (i - _14 + 1), 0);
            lines[LineNum].transform.Find("Point").GetComponent<RectTransform>().anchoredPosition = new Vector2(0, iniY + perH * NewSuspected[i]);
            lines[LineNum].transform.Find("Point").GetComponent<Image>().color = CNSuspected;
            lines[LineNum].transform.Find("Line").GetComponent<Image>().color = CNSuspected;
            lines[LineNum].transform.Find("ButtonText").GetComponent<Text>().text = Date[i];
            if (i == NewSuspected.Count - 1)
            {
                lines[LineNum].transform.Find("Line").gameObject.SetActive(false);
            }
            if (i != _14)
            {
                Vector2 p1 = new Vector2(0, iniY + perH * NewSuspected[i - 1]);
                Vector2 p2 = new Vector2(perDis, iniY + perH * NewSuspected[i]);
                lines[LineNum - 1].transform.Find("Line").GetComponent<RectTransform>().anchoredPosition = (p1 + p2) / 2;
                lines[LineNum - 1].transform.Find("Line").GetComponent<RectTransform>().sizeDelta = new Vector2(LineWidth, Vector2.Distance(p1, p2));

                float angle = Mathf.Atan(Mathf.Abs((perH * NewSuspected[i] - perH * NewSuspected[i - 1]) / perDis)) * 180 / Mathf.PI;
                if (p2.y < p1.y)
                {
                    lines[LineNum - 1].transform.Find("Line").GetComponent<RectTransform>().Rotate(0, 0, 90 - angle);
                }
                else
                {
                    lines[LineNum - 1].transform.Find("Line").GetComponent<RectTransform>().Rotate(0, 0, 90 + angle);
                }
            }
            LineNum += 1;
        }

        for (int i = _14; i < NewInfected.Count; i++)
        {
            lines.Add((GameObject)Instantiate(prefab, transform.position, transform.rotation));
            lines[LineNum].transform.SetParent(father.Find("ChartBG1"));
            lines[LineNum].transform.name = "Point" + i.ToString();
            lines[LineNum].GetComponent<RectTransform>().anchoredPosition = new Vector2(iniX + perDis * (i - _14 + 1), 0);
            lines[LineNum].transform.Find("Point").GetComponent<RectTransform>().anchoredPosition = new Vector2(0, iniY + perH * NewInfected[i]);
            lines[LineNum].transform.Find("Point").GetComponent<Image>().color = CNInfected;
            lines[LineNum].transform.Find("Line").GetComponent<Image>().color = CNInfected;
            lines[LineNum].transform.Find("ButtonText").gameObject.SetActive(false);
            if (i == NewInfected.Count - 1)
            {
                lines[LineNum].transform.Find("Line").gameObject.SetActive(false);
            }
            if (i != _14)
            {
                Vector2 p1 = new Vector2(0, iniY + perH * NewInfected[i - 1]);
                Vector2 p2 = new Vector2(perDis, iniY + perH * NewInfected[i]);
                lines[LineNum - 1].transform.Find("Line").GetComponent<RectTransform>().anchoredPosition = (p1 + p2) / 2;
                lines[LineNum - 1].transform.Find("Line").GetComponent<RectTransform>().sizeDelta = new Vector2(LineWidth, Vector2.Distance(p1, p2));

                float angle = Mathf.Atan(Mathf.Abs((perH * NewInfected[i] - perH * NewInfected[i - 1]) / perDis)) * 180 / Mathf.PI;
                if (p2.y < p1.y)
                {
                    lines[LineNum - 1].transform.Find("Line").GetComponent<RectTransform>().Rotate(0, 0, 90 - angle);
                }
                else
                {
                    lines[LineNum - 1].transform.Find("Line").GetComponent<RectTransform>().Rotate(0, 0, 90 + angle);
                }
            }
            LineNum += 1;
        }





        //第二张图表
        TempMax = 0;
        highNum = 0;
        for (int i = _14; i < AllSuspected.Count; i++)
        {
            if (AllSuspected[i] > TempMax)
            {
                TempMax = AllSuspected[i];
            }
            if (AllInfected[i] > TempMax)
            {
                TempMax = AllInfected[i];
            }
        }
        if (TempMax >= 0)
        {
            int digit = TempMax.ToString().Length;
            int modNum = 0;
            switch (digit)
            {
                case 1:
                    if (TempMax < 6)
                    {
                        highNum = 6;
                    }
                    else
                    {
                        highNum = 12;
                    }
                    break;
                case 2:
                    if (TempMax < 60)
                    {
                        highNum = 60;
                    }
                    else
                    {
                        highNum = 120;
                    }
                    break;
                case 3:
                    int.TryParse(TempMax.ToString().Substring(0, 1), out modNum);
                    highNum = 120 * (modNum + 1);
                    break;
                case 4:
                    int.TryParse(TempMax.ToString().Substring(0, 1), out modNum);
                    highNum = 1200 * (modNum + 1);
                    break;
                case 5:
                    int.TryParse(TempMax.ToString().Substring(0, 1), out modNum);
                    highNum = 12000 * (modNum + 1);
                    break;
                case 6:
                    int.TryParse(TempMax.ToString().Substring(0, 1), out modNum);
                    highNum = 120000 * (modNum + 1);
                    break;
                case 7:
                    int.TryParse(TempMax.ToString().Substring(0, 1), out modNum);
                    highNum = 1200000 * (modNum + 1);
                    break;
                default:
                    break;
            }
        }
        perDis = MaxW / (AllSuspected.Count - _14 + 1);

        perH = MaxH / highNum;
        //左端文字
        for (int a = 0; a < 7; a++)
        {
            father.Find("ChartBG2/LeftText/Text" + (a + 1).ToString()).GetComponent<Text>().text = (highNum / 6 * a).ToString();
        }
        father.Find("ChartBG2/TopText").GetComponent<Text>().text = SAll + "<color=blue><size=35>" + SSaI + "</size></color>" + SPeo;//
        for (int i = _14; i < AllSuspected.Count; i++)
        {
            lines.Add((GameObject)Instantiate(prefab, transform.position, transform.rotation));
            lines[LineNum].transform.SetParent(father.Find("ChartBG2"));
            lines[LineNum].transform.name = "Point" + i.ToString();
            lines[LineNum].GetComponent<RectTransform>().anchoredPosition = new Vector2(iniX + perDis * (i - _14 + 1), 0);
            lines[LineNum].transform.Find("Point").GetComponent<RectTransform>().anchoredPosition = new Vector2(0, iniY + perH * AllSuspected[i]);
            lines[LineNum].transform.Find("Point").GetComponent<Image>().color = CSuspected;
            lines[LineNum].transform.Find("Line").GetComponent<Image>().color = CSuspected;
            lines[LineNum].transform.Find("ButtonText").GetComponent<Text>().text = Date[i];
            if (i == AllSuspected.Count - 1)
            {
                lines[LineNum].transform.Find("Line").gameObject.SetActive(false);
            }
            if (i != _14)
            {
                Vector2 p1 = new Vector2(0, iniY + perH * AllSuspected[i - 1]);
                Vector2 p2 = new Vector2(perDis, iniY + perH * AllSuspected[i]);
                lines[LineNum - 1].transform.Find("Line").GetComponent<RectTransform>().anchoredPosition = (p1 + p2) / 2;
                lines[LineNum - 1].transform.Find("Line").GetComponent<RectTransform>().sizeDelta = new Vector2(LineWidth, Vector2.Distance(p1, p2));

                float angle = Mathf.Atan(Mathf.Abs((perH * AllSuspected[i] - perH * AllSuspected[i - 1]) / perDis)) * 180 / Mathf.PI;
                if (p2.y < p1.y)
                {
                    lines[LineNum - 1].transform.Find("Line").GetComponent<RectTransform>().Rotate(0, 0, 90 - angle);
                }
                else
                {
                    lines[LineNum - 1].transform.Find("Line").GetComponent<RectTransform>().Rotate(0, 0, 90 + angle);
                }
            }
            LineNum += 1;
        }

        for (int i = _14; i < AllInfected.Count; i++)
        {
            lines.Add((GameObject)Instantiate(prefab, transform.position, transform.rotation));
            lines[LineNum].transform.SetParent(father.Find("ChartBG2"));
            lines[LineNum].transform.name = "Point" + i.ToString();
            lines[LineNum].GetComponent<RectTransform>().anchoredPosition = new Vector2(iniX + perDis * (i - _14 + 1), 0);
            lines[LineNum].transform.Find("Point").GetComponent<RectTransform>().anchoredPosition = new Vector2(0, iniY + perH * AllInfected[i]);
            lines[LineNum].transform.Find("Point").GetComponent<Image>().color = CInfected;
            lines[LineNum].transform.Find("Line").GetComponent<Image>().color = CInfected;
            lines[LineNum].transform.Find("ButtonText").gameObject.SetActive(false);
            if (i == AllInfected.Count - 1)
            {
                lines[LineNum].transform.Find("Line").gameObject.SetActive(false);
            }
            if (i != _14)
            {
                Vector2 p1 = new Vector2(0, iniY + perH * AllInfected[i - 1]);
                Vector2 p2 = new Vector2(perDis, iniY + perH * AllInfected[i]);
                lines[LineNum - 1].transform.Find("Line").GetComponent<RectTransform>().anchoredPosition = (p1 + p2) / 2;
                lines[LineNum - 1].transform.Find("Line").GetComponent<RectTransform>().sizeDelta = new Vector2(LineWidth, Vector2.Distance(p1, p2));

                float angle = Mathf.Atan(Mathf.Abs((perH * AllInfected[i] - perH * AllInfected[i - 1]) / perDis)) * 180 / Mathf.PI;
                if (p2.y < p1.y)
                {
                    lines[LineNum - 1].transform.Find("Line").GetComponent<RectTransform>().Rotate(0, 0, 90 - angle);
                }
                else
                {
                    lines[LineNum - 1].transform.Find("Line").GetComponent<RectTransform>().Rotate(0, 0, 90 + angle);
                }
            }
            LineNum += 1;
        }

        //第三张图表
        TempMax = 0;
        highNum = 0;
        for (int i = _14; i < AllDead.Count; i++)
        {
            if (AllDead[i] > TempMax)
            {
                TempMax = AllDead[i];
            }
            if (AllCure[i] > TempMax)
            {
                TempMax = AllCure[i];
            }
        }
        if (TempMax >= 0)
        {
            int digit = TempMax.ToString().Length;
            int modNum = 0;
            switch (digit)
            {
                case 1:
                    if (TempMax < 6)
                    {
                        highNum = 6;
                    }
                    else
                    {
                        highNum = 12;
                    }
                    break;
                case 2:
                    if (TempMax < 60)
                    {
                        highNum = 60;
                    }
                    else
                    {
                        highNum = 120;
                    }
                    break;
                case 3:
                    int.TryParse(TempMax.ToString().Substring(0, 1), out modNum);
                    highNum = 120 * (modNum + 1);
                    break;
                case 4:
                    int.TryParse(TempMax.ToString().Substring(0, 1), out modNum);
                    highNum = 1200 * (modNum + 1);
                    break;
                case 5:
                    int.TryParse(TempMax.ToString().Substring(0, 1), out modNum);
                    highNum = 12000 * (modNum + 1);
                    break;
                case 6:
                    int.TryParse(TempMax.ToString().Substring(0, 1), out modNum);
                    highNum = 120000 * (modNum + 1);
                    break;
                case 7:
                    int.TryParse(TempMax.ToString().Substring(0, 1), out modNum);
                    highNum = 1200000 * (modNum + 1);
                    break;
                default:
                    break;
            }
        }
        perDis = MaxW / (AllDead.Count - _14 + 1);

        perH = MaxH / highNum;
        //左端文字
        for (int a = 0; a < 7; a++)
        {
            father.Find("ChartBG3/LeftText/Text" + (a + 1).ToString()).GetComponent<Text>().text = (highNum / 6 * a).ToString();
        }
        father.Find("ChartBG3/TopText").GetComponent<Text>().text = SAll + "<color=blue><size=35>" + SDaC + "</size></color>" + SPeo;//
        for (int i = _14; i < AllDead.Count; i++)
        {
            lines.Add((GameObject)Instantiate(prefab, transform.position, transform.rotation));
            lines[LineNum].transform.SetParent(father.Find("ChartBG3"));
            lines[LineNum].transform.name = "Point" + i.ToString();
            lines[LineNum].GetComponent<RectTransform>().anchoredPosition = new Vector2(iniX + perDis * (i - _14 + 1), 0);
            lines[LineNum].transform.Find("Point").GetComponent<RectTransform>().anchoredPosition = new Vector2(0, iniY + perH * AllDead[i]);
            lines[LineNum].transform.Find("Point").GetComponent<Image>().color = CDead;
            lines[LineNum].transform.Find("Line").GetComponent<Image>().color = CDead;
            lines[LineNum].transform.Find("ButtonText").gameObject.SetActive(false);
            if (i == AllDead.Count - 1)
            {
                lines[LineNum].transform.Find("Line").gameObject.SetActive(false);
            }
            if (i != _14)
            {
                Vector2 p1 = new Vector2(0, iniY + perH * AllDead[i - 1]);
                Vector2 p2 = new Vector2(perDis, iniY + perH * AllDead[i]);
                lines[LineNum - 1].transform.Find("Line").GetComponent<RectTransform>().anchoredPosition = (p1 + p2) / 2;
                lines[LineNum - 1].transform.Find("Line").GetComponent<RectTransform>().sizeDelta = new Vector2(LineWidth, Vector2.Distance(p1, p2));

                float angle = Mathf.Atan(Mathf.Abs((perH * AllDead[i] - perH * AllDead[i - 1]) / perDis)) * 180 / Mathf.PI;
                if (p2.y < p1.y)
                {
                    lines[LineNum - 1].transform.Find("Line").GetComponent<RectTransform>().Rotate(0, 0, 90 - angle);
                }
                else
                {
                    lines[LineNum - 1].transform.Find("Line").GetComponent<RectTransform>().Rotate(0, 0, 90 + angle);
                }
            }
            LineNum += 1;
        }

        for (int i = _14; i < AllCure.Count; i++)
        {
            lines.Add((GameObject)Instantiate(prefab, transform.position, transform.rotation));
            lines[LineNum].transform.SetParent(father.Find("ChartBG3"));
            lines[LineNum].transform.name = "Point" + i.ToString();
            lines[LineNum].GetComponent<RectTransform>().anchoredPosition = new Vector2(iniX + perDis * (i - _14 + 1), 0);
            lines[LineNum].transform.Find("Point").GetComponent<RectTransform>().anchoredPosition = new Vector2(0, iniY + perH * AllCure[i]);
            lines[LineNum].transform.Find("Point").GetComponent<Image>().color = CCure;
            lines[LineNum].transform.Find("Line").GetComponent<Image>().color = CCure;
            lines[LineNum].transform.Find("ButtonText").GetComponent<Text>().text = Date[i];
            if (i == AllCure.Count - 1)
            {
                lines[LineNum].transform.Find("Line").gameObject.SetActive(false);
            }
            if (i != _14)
            {
                Vector2 p1 = new Vector2(0, iniY + perH * AllCure[i - 1]);
                Vector2 p2 = new Vector2(perDis, iniY + perH * AllCure[i]);
                lines[LineNum - 1].transform.Find("Line").GetComponent<RectTransform>().anchoredPosition = (p1 + p2) / 2;
                lines[LineNum - 1].transform.Find("Line").GetComponent<RectTransform>().sizeDelta = new Vector2(LineWidth, Vector2.Distance(p1, p2));

                float angle = Mathf.Atan(Mathf.Abs((perH * AllCure[i] - perH * AllCure[i - 1]) / perDis)) * 180 / Mathf.PI;
                if (p2.y < p1.y)
                {
                    lines[LineNum - 1].transform.Find("Line").GetComponent<RectTransform>().Rotate(0, 0, 90 - angle);
                }
                else
                {
                    lines[LineNum - 1].transform.Find("Line").GetComponent<RectTransform>().Rotate(0, 0, 90 + angle);
                }
            }
            LineNum += 1;
        }
    }

    public void CloseStatiPage()
    {
        for (int i = 0; i < lines.Count; i++)
        {
            Destroy(lines[i], 0);

        }

    }
}
