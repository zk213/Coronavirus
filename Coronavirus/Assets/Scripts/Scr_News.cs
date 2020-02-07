using OfficeOpenXml;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Scr_News : MonoBehaviour
{
    public Text scrollNews1;
    public Text scrollNews2;

    Scr_Mode mode;
    NewsData newsData;

    float speed = 250;
    float iniPosx = 1111;
    float finPosx = -1123;
    float midPosx = -600;


    string TempNewsContent;

    [HideInInspector]
    public List<int> NewsList = new List<int>();
    [HideInInspector]
    public List<string> NewsListSlot = new List<string>();

    List<int> NewsListOver = new List<int>();

    bool activeNews1;
    bool activeNews2;


    void Awake()
    {
        mode = FindObjectOfType<Scr_Mode>();
        newsData = Resources.Load<NewsData>("ScriptableObject/News");

        activeNews1 = false;
        activeNews2 = false;

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

        /*
         //新闻板块的轮播
        if (scrollNews1.GetComponent<RectTransform>().anchoredPosition.x <= midPosx && !activeNews2)
        {
            activeNews2 = true;
        }
        if (scrollNews2.GetComponent<RectTransform>().anchoredPosition.x <= midPosx && !activeNews1)
        {
            activeNews1 = true;
        }
         */


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

    void LoadExcel(ExcelWorksheets workSheets, int SheetIndex, int NewsIndex)
    {
        ExcelWorksheet workSheet = workSheets[SheetIndex];//表1，即简体中文那张表
        int rowCount = workSheet.Dimension.End.Row;//统计表的列数
        bool hasContent = false;
        for (int row = 1; row <= rowCount; row++)
        {
            var text = workSheet.Cells[row, 1].Text ?? "Name Error";

            if (text == newsData.newsGroup[NewsIndex].content)
            {
                TempNewsContent = workSheet.Cells[row, 2].Text ?? "Content Error";
                hasContent = true;
            }
        }
        if (!hasContent)
        {
            TempNewsContent = newsData.newsGroup[NewsIndex].content;
        }
    }

    void PlayNews(int i)
    {
        //读取News.xls文件
        using (FileStream fs = new FileStream(Application.dataPath + "/Resources/Localization/News.xlsx", FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
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
