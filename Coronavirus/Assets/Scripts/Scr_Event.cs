﻿using OfficeOpenXml;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;


public class Scr_Event : MonoBehaviour
{
    GameData gameData;
    Scr_TimeControl time;
    Scr_Num value;
    Scr_Mode mode;

    public GameObject EventGroup;
    public Text Title;
    public Text Describe;
    public Image Picture;

    [HideInInspector]
    public bool showEvent;

    TimeMode TempTimeMode;


    List<int> UnActiveEvent = new List<int>();//未激活事件，未激活事件就像事件2那样，需要有前一个事件的完成才会激活，在游戏运行时，游戏不会检测未激活事件的触发条件
    List<int> ActiveEvent = new List<int>();//激活事件，已激活事件就是母事件（没有前置需求的事件）和已经激活的子事件，游戏会检测他们的条件
    List<int> ReadyEvent = new List<int>();//就绪事件，就绪事件就是所有条件都满足的事件，他会每回合过一次概率，如果概率过了就会触发事件，如果没有继续等待下一回合
    List<int> HappenEvent = new List<int>();//正在发生的事件,将发生的事件按照一定顺序排序，并依次发生
    List<int> FinishEvent = new List<int>();//已发生事件,无论如何也不会再检测了，即使触发条件都成立
    List<float> dynamicProbability = new List<float>();//事件的发生概率


    void Awake()
    {
        gameData = Resources.Load<GameData>("Event");
        time = FindObjectOfType<Scr_TimeControl>();
        value = FindObjectOfType<Scr_Num>();
        mode = FindObjectOfType<Scr_Mode>();
        EventGroup.SetActive(false);

        //对事件的类别进行初始化，分出未激活的子事件与普通事件,并初始化事件发生的概率
        FinishEvent.Add(0);//把事件模板进去，让他不会发生
        for (int i = 0; i < gameData.eventGroup.Count; i++)
        {
            dynamicProbability.Add(gameData.eventGroup[i].probability);//初始化事件发生的概率

            if (!FinishEvent.Contains(i))//首先不检测已经结束的事件
            {
                if (gameData.eventGroup[i].subEvent)//如果这个事件是子事件
                {
                    UnActiveEvent.Add(i);
                }
                else
                {
                    ActiveEvent.Add(i);
                }
            }
        }
    }

    //以下是事件触发机制
    public void EventCheck()//在Scr_TimeControl里的Update里，这样就可以游戏事件里每增加一天才会检查一次，不至于每帧都查
    {

        for (int i = 0; i < gameData.eventGroup.Count; i++)
        {

            if (!FinishEvent.Contains(i) && !UnActiveEvent.Contains(i))//首先不检测已经结束的事件和未激活事件
            {
                switch (i)
                {
                    case 1:
                        //设条件是第五天就会触发
                        EventCondition(time.day == 5, i);
                        break;
                    case 2:
                        //设条件是影响力达到5
                        EventCondition(value.InfluenceVal == 5, i);
                        break;
                    default:
                        break;
                }
            }
        }
    }

    void LateUpdate()//事件的发生
    {
        if (HappenEvent.Count == 1)
        {
            HappeningEvent(0);
        }
        if (HappenEvent.Count > 1)
        {
            for (int i = 0; i < HappenEvent.Count; i++)
            {
                //把要发生的事件排序，先比较优先级，如果优先级相同再比较序号，优先级高的先发生，序号小的先发生
                //以此发生事件
            }
        }


    }

    public void CloseEvent()
    {

        showEvent = false;

        //以下是恢复游戏速度
        if (TempTimeMode == TimeMode.OneSpeed)
        {
            time.OneSpeed();
        }
        if (TempTimeMode == TimeMode.FastSpeed)
        {
            time.FastSpeed();
        }

        EventGroup.SetActive(false);

    }

    //事件检测符合条件的机制
    void EventCondition(bool isMeetingConditions, int i)
    {
        if (isMeetingConditions)//看是否满足条件
        {
            if (ActiveEvent.Contains(i))//如果这个事件是激活事件
            {
                ActiveEvent.Remove(i);//从激活事件的列表中移除该事件
                ReadyEvent.Add(i);//再放在就绪事件列表里
            }
            if (ReadyEvent.Contains(i))//如果这个事件是就绪事件
            {
                if (Random.Range(0, 100) <= dynamicProbability[i])//过一次概率
                {
                    ReadyEvent.Remove(i);//从就绪事件的列表中移除该事件
                    HappenEvent.Add(i);//放在正在发生事件列表里
                }
                else
                {
                    dynamicProbability[i] += gameData.eventGroup[i].probabilityAdd;//概率增加
                    dynamicProbability[i] = Mathf.Clamp(dynamicProbability[i], 0, 100);//限制概率的范围
                }
            }
        }
        else
        {
            //如果条件不符合，则从就绪列表返回到之前的激活列表里
            ReadyEvent.Remove(i);
            ActiveEvent.Add(i);
        }
    }

    //读取Localization中的事件excel文件
    void LoadExcel(ExcelWorksheets workSheets, int SheetIndex, int EventIndex)
    {
        ExcelWorksheet workSheet = workSheets[SheetIndex];//表1，即简体中文那张表
        int rowCount = workSheet.Dimension.End.Row;//统计表的列数
        bool hasTitle = false;
        bool hasDescribe = false;
        for (int row = 1; row <= rowCount; row++)
        {
            var text = workSheet.Cells[row, 1].Text ?? "Name Error";

            if (text == gameData.eventGroup[HappenEvent[EventIndex]].title)
            {
                Title.text = workSheet.Cells[row, 2].Text ?? "Title Error";
                hasTitle = true;
            }
            if (text == gameData.eventGroup[HappenEvent[EventIndex]].describe)
            {
                Describe.text = workSheet.Cells[row, 2].Text ?? "Describe Error";
                hasDescribe = true;
            }
        }
        if (!hasTitle)
        {
            Title.text = gameData.eventGroup[HappenEvent[EventIndex]].title;
        }
        if (!hasDescribe)
        {
            Describe.text = gameData.eventGroup[HappenEvent[EventIndex]].describe;
        }
    }

    void HappeningEvent(int i)
    {
        TempTimeMode = time.timeMode;
        showEvent = true;
        time.Pause();//发生事件时游戏暂停

        //读取Event.xls文件
        using (FileStream fs = new FileStream(Application.dataPath + "/Resources/Localization/Event.xlsx", FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
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
        Picture.sprite = Resources.Load(gameData.eventGroup[HappenEvent[i]].picture, typeof(Sprite)) as Sprite;

        //执行事件的效果

        switch (HappenEvent[i])
        {
            case 1:
                value.InfluenceVal += 5;//事件1的效果是影响力+5
                break;
            case 2:
                value.InfluenceVal += 5;//事件2的效果是影响力+5
                break;
            default:
                break;
        }

        FinishEvent.Add(HappenEvent[i]);
        HappenEvent.Remove(HappenEvent[i]);
        EventGroup.SetActive(true);
    }
}

/*
                if (ActiveEvent.Contains(i))//如果这个事件是激活事件
                {
                    //在这里对事件进行判断看他是否符合条件
                    //在这里就拟合新年事件
                    if (i == 1 && time.day == 5)//事件2的效果是影响力+5
                    {
                        ActiveEvent.Remove(i);//从激活事件的列表中移除该事件
                        ReadyEvent.Add(i);//再放在就绪事件列表里
                    }
                }
                if (ReadyEvent.Contains(i))//如果这个事件是就绪事件
                {
                    //这里还需要进行检测，看他是否符合条件
                    if (true)
                    {
                        if (Random.Range(0, 100) <= dynamicProbability[i])//过一次概率
                        {
                            ReadyEvent.Remove(i);//从就绪事件的列表中移除该事件
                            HappenEvent.Add(i);//放在正在发生事件列表里
                        }
                        else
                        {
                            dynamicProbability[i] += gameData.eventGroup[i].probabilityAdd;//概率增加
                            dynamicProbability[i] = Mathf.Clamp(dynamicProbability[i], 0, 100);//限制概率的范围
                        }
                    }
                    else
                    {
                        //如果条件不符合，则从就绪列表返回到之前的激活列表里
                        ReadyEvent.Remove(i);
                        ActiveEvent.Add(i);
                    }
                }
                */
