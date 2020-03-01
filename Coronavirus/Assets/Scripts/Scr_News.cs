using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.UI;


public class Scr_News : MonoBehaviour
{
    public Text scrollNews1;
    public Text scrollNews2;


    float speed = 250;
    float iniPosx = 1320;
    float finPosx = -1300;
    float midPosx = -800;


    string TempNewsContent;

    [HideInInspector]
    public List<int> NewsList = new List<int>();
    [HideInInspector]
    public List<string> NewsListSlot = new List<string>();

    List<int> NewsListOver = new List<int>();

    bool activeNews1;
    bool activeNews2;

    List<string> XmlNews = new List<string>();

    List<string> Key = new List<string>();
    List<string> TextInfor = new List<string>();

    int Language = 1;

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
        activeNews1 = false;
        activeNews2 = false;

        Key = new List<string>();
        TextInfor = new List<string>();
        string LocalizationText = ((TextAsset)Resources.Load("Localization/" + Language.ToString() + "/News")).text;
        string[] LocalizationArray = LocalizationText.Split('\n');
        for (int l = 0; l < LocalizationArray.Length; l++)
        {
            string[] LocalArray = LocalizationArray[l].Split(':');
            if (LocalArray.Length == 2)
            {
                Key.Add(LocalArray[0]);

                TextInfor.Add(LocalArray[1]);
            }
        }
    }
    void Awake()
    {
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(Application.dataPath + "/Resources/Xml/News.xml");
        XmlElement xmlNode = xmlDoc.DocumentElement;
        foreach (XmlNode elements in xmlNode)
        {
            XmlElement element = elements as XmlElement;
            if (element == null)
                continue;
            if (element.LocalName == "news")
            {
                XmlNews.Add(element.InnerText);
            }
        }


    }

    void Update()
    {
        if (NewsList.Count > NewsListOver.Count)//如果有新闻没播放
        {
            for (int i = NewsListOver.Count; i < NewsList.Count; i++)
            {
                if (NewsList.Count - i >= 2)
                {
                    NewsListOver.Add(NewsList[i]);//过多的新闻直接隐藏
                }

                /*
                if (NewsList.Count - i == 2 && !activeNews1 && !activeNews2)//如果两个新闻位置都空闲//不知道为啥还是出Bug
                {
                    PlayNews(NewsList[i]);
                    activeNews1 = true;
                    scrollNews1.text = string.Format(TempNewsContent, NewsListSlot[i]);
                    NewsListOver.Add(NewsList[i]);
                    break;
                }
                */

                else if (NewsList.Count - i == 1)
                {
                    if (!activeNews1)
                    {
                        if (scrollNews2.GetComponent<RectTransform>().anchoredPosition.x <= midPosx || !activeNews2)
                        {
                            PlayNews(NewsList[i]);
                            activeNews1 = true;
                            scrollNews1.text = string.Format(TempNewsContent, NewsListSlot[i]);
                            NewsListOver.Add(NewsList[i]);

                        }
                        break;
                    }
                    else if (scrollNews1.GetComponent<RectTransform>().anchoredPosition.x <= midPosx && !activeNews2)
                    {

                        PlayNews(NewsList[i]);
                        activeNews2 = true;
                        scrollNews2.text = string.Format(TempNewsContent, NewsListSlot[i]);
                        NewsListOver.Add(NewsList[i]);
                        break;
                    }
                }
            }
        }


        //新闻板块的回调
        if (scrollNews1.GetComponent<RectTransform>().anchoredPosition.x <= finPosx)
        {
            scrollNews1.GetComponent<RectTransform>().anchoredPosition = new Vector2(iniPosx, scrollNews1.GetComponent<RectTransform>().anchoredPosition.y);
            activeNews1 = false;
        }
        if (scrollNews2.GetComponent<RectTransform>().anchoredPosition.x <= finPosx)
        {
            scrollNews2.GetComponent<RectTransform>().anchoredPosition = new Vector2(iniPosx, scrollNews1.GetComponent<RectTransform>().anchoredPosition.y);
            activeNews2 = false;
        }

        //新闻板块的移动
        if (activeNews1)
        {
            scrollNews1.GetComponent<RectTransform>().anchoredPosition -= new Vector2(speed * Time.deltaTime, 0);
        }
        if (activeNews2)
        {
            scrollNews2.GetComponent<RectTransform>().anchoredPosition -= new Vector2(speed * Time.deltaTime, 0);
        }



    }

    void PlayNews(int i)
    {
        TempNewsContent = XmlNews[i];
        for (int l = 0; l < Key.Count; l++)
        {

            if (Key[l] == XmlNews[i])
            {
                TempNewsContent = TextInfor[l]; ;
                break;
            }

        }
    }

    public void CheckNewsRecord()
    {
        for (int i = NewsListOver.Count - 5; i < NewsListOver.Count; i++)
        {
            if (i >= 0)
            {
                PlayNews(NewsListOver[i]);
                Debug.Log(string.Format(TempNewsContent, NewsListSlot[i]));

            }
        }

    }
}
