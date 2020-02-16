using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Scr_Statistic : MonoBehaviour
{
    public GameObject StaticPage;
    public GameObject MainPage;
    public Transform father;

    public Object prefab;

    public float LineWidth = 5;

    List<float> points = new List<float>();
    List<GameObject> lines = new List<GameObject>();

    float MaxH = 190;
    float MaxW = 590;
    float iniX = -280;
    float iniY = -113;



    void Awake()
    {
        StaticPage.SetActive(false);

        points.Add(1);
        points.Add(3);
        points.Add(7);
        points.Add(8);
        points.Add(2);
        points.Add(5);
        points.Add(3);
    }

    public void OpenStatiPage()
    {
        lines = new List<GameObject>();
        StaticPage.SetActive(true);
        MainPage.SetActive(false);

        float TempMax = 0;
        float highNum = 0;
        for (int i = 0; i < points.Count; i++)
        {
            if (points[i] > TempMax)
            {
                TempMax = points[i];
            }
        }
        if (TempMax > 1)//对整数部分进行判断
        {
            int digit = TempMax.ToString().Length;
            int modNum;
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
                    int.TryParse(TempMax.ToString().Substring(0), out modNum);
                    highNum = 120 * modNum;
                    break;
                case 4:
                    int.TryParse(TempMax.ToString().Substring(0), out modNum);
                    highNum = 1200 * modNum;
                    break;
                case 5:
                    int.TryParse(TempMax.ToString().Substring(0), out modNum);
                    highNum = 12000 * modNum;
                    break;
                case 6:
                    int.TryParse(TempMax.ToString().Substring(0), out modNum);
                    highNum = 120000 * modNum;
                    break;
                case 7:
                    int.TryParse(TempMax.ToString().Substring(0), out modNum);
                    highNum = 1200000 * modNum;
                    break;
                default:
                    break;
            }
        }
        float perDis = MaxW / (points.Count + 1);

        float perH = MaxH / highNum;
        //左端文字
        for (int i = 0; i < 7; i++)
        {
            father.Find("LeftText").transform.Find("Text" + (i + 1).ToString()).GetComponent<Text>().text = (highNum / 6 * i).ToString();
        }
        father.Find("TopText").GetComponent<Text>().text = "<color=blue><size=35>折线</size></color>统计图";//
        for (int i = 0; i < points.Count; i++)
        {
            lines.Add((GameObject)Instantiate(prefab, transform.position, transform.rotation));
            lines[i].transform.SetParent(father);
            lines[i].transform.name = "Point" + i.ToString();
            lines[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(iniX + perDis * (i + 1), 0);
            lines[i].transform.Find("Point").GetComponent<RectTransform>().anchoredPosition = new Vector2(0, iniY + perH * points[i]);
            lines[i].transform.Find("ButtonText").GetComponent<Text>().text = i.ToString();
            if (i == points.Count - 1)
            {
                lines[i].transform.Find("Line").gameObject.SetActive(false);
            }
            if (i != 0)
            {
                Vector2 p1 = new Vector2(0, iniY + perH * points[i - 1]);
                Vector2 p2 = new Vector2(perDis, iniY + perH * points[i]);
                lines[i - 1].transform.Find("Line").GetComponent<RectTransform>().anchoredPosition = (p1 + p2) / 2;
                lines[i - 1].transform.Find("Line").GetComponent<RectTransform>().sizeDelta = new Vector2(LineWidth, Vector2.Distance(p1, p2));

                float angle = Mathf.Atan(Mathf.Abs((perH * points[i] - perH * points[i - 1]) / perDis)) * 180 / Mathf.PI;
                if (p2.y < p1.y)
                {
                    lines[i - 1].transform.Find("Line").GetComponent<RectTransform>().Rotate(0, 0, 90 - angle);
                }
                else
                {
                    lines[i - 1].transform.Find("Line").GetComponent<RectTransform>().Rotate(0, 0, 90 + angle);
                }
            }

        }
    }

    public void CloseStatiPage()
    {
        StaticPage.SetActive(false);
        MainPage.SetActive(true);
        for (int i = 0; i < points.Count; i++)
        {
            Destroy(lines[i], 0);

        }

    }
}
