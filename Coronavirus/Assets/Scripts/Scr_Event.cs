using System;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.UI;


public class Scr_Event : MonoBehaviour
{
    void EventConditionControl(int i)
    {
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(((TextAsset)Resources.Load("Xml/Event")).text);
        XmlElement xmlNode = xmlDoc.DocumentElement;
        foreach (XmlNode elements in xmlNode)
        {
            XmlElement element = elements as XmlElement;
            if (element == null)
                continue;
            if (element.LocalName == "event")
            {
                if (element.Attributes["id"].Value == i.ToString())
                {
                    XmlElement conditions = element.SelectSingleNode("conditions") as XmlElement;
                    int.TryParse(conditions.Attributes["amount"].Value, out int amount);
                    int amountNow = 0;
                    for (int a = 0; a < amount; a++)
                    {
                        var condition = conditions.ChildNodes[a] as XmlElement;
                        float value = float.Parse(condition.Attributes["value"].Value);
                        switch (condition.InnerText)
                        {
                            case "day":
                                if (InnerCondition(condition.Attributes["sign"].Value, time.day, value))
                                {
                                    amountNow += 1;
                                }
                                break;
                            case "Influence":
                                if (InnerCondition(condition.Attributes["sign"].Value, num.InfluenceVal, value))
                                {
                                    amountNow += 1;
                                }
                                break;
                            default:
                                break;
                        }

                    }
                    //
                    EventCondition(amountNow >= amount, i);
                }
            }
        }
    }
    bool InnerCondition(string InnerText, float variable, float InnerValue)
    {
        switch (InnerText)
        {
            case "ge":
                if (variable >= InnerValue)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            case "ee":
                if (variable == InnerValue)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            case "le":
                if (variable <= InnerValue)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            case "ne":
                if (variable != InnerValue)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            default:
                return false;
        }
    }
    void EventEffectControl(int i)
    {
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(((TextAsset)Resources.Load("Xml/Event")).text);
        XmlElement xmlNode = xmlDoc.DocumentElement;
        foreach (XmlNode elements in xmlNode)
        {
            XmlElement element = elements as XmlElement;
            if (element == null)
                continue;
            if (element.LocalName == "event")
            {
                if (element.Attributes["id"].Value == i.ToString())
                {
                    XmlElement effects = element.SelectSingleNode("effects") as XmlElement;
                    int.TryParse(effects.Attributes["amount"].Value, out int amount);
                    for (int a = 0; a < amount; a++)
                    {
                        var effect = effects.ChildNodes[a] as XmlElement;
                        switch (effect.Attributes["type"].Value)
                        {
                            case "value":
                                float value = float.Parse(effect.Attributes["value"].Value);
                                switch (effect.InnerText)
                                {
                                    case "Influence":
                                        num.InfluenceVal = InnerEffectValue(effect.Attributes["sign"].Value, num.InfluenceVal, value);
                                        break;
                                    default:
                                        break;
                                }
                                break;
                            case "news":
                                int.TryParse(effect.Attributes["index"].Value, out int newsIndex);
                                if (effect.HasAttribute("slot"))
                                {
                                    switch (effect.Attributes["slot"].Value)
                                    {
                                        case "Influence":
                                            AddNews(newsIndex, num.InfluenceVal.ToString());
                                            break;
                                        default:
                                            AddNews(newsIndex, "");
                                            break;
                                    }
                                }
                                else
                                {
                                    AddNews(newsIndex, "");
                                }
                                break;
                            case "event":
                                int delayDay = 0;
                                int.TryParse(effect.Attributes["index"].Value, out int eventIndex);
                                if (effect.HasAttribute("delay"))
                                {
                                    int.TryParse(effect.Attributes["delay"].Value, out delayDay);
                                }
                                if (UnActiveEvent.Contains(eventIndex))
                                {
                                    if (delayDay > 0)
                                    {
                                        DelayEvent.Add(eventIndex);
                                        DelayDay.Add(delayDay);
                                        UnActiveEvent.Remove(eventIndex);

                                    }
                                    else
                                    {
                                        ActiveEvent.Add(eventIndex);
                                        UnActiveEvent.Remove(eventIndex);
                                    }
                                }
                                break;
                            default:
                                break;
                        }
                    }

                }
            }
        }
    }

    float InnerEffectValue(string InnerText, float variable, float InnerValue)
    {
        switch (InnerText)
        {
            case "+":
                variable += InnerValue;
                return variable;
            case "-":
                variable -= InnerValue;
                return variable;
            default:
                return variable;
        }
    }

    public void LocalSave()
    {
        string SaveString1 = "";
        for (int i = 0; i < UnActiveEvent.Count; i++)
        {
            if (SaveString1.Length == 0)
            {
                SaveString1 = UnActiveEvent[i].ToString();
            }
            else
            {
                SaveString1 += "," + UnActiveEvent[i].ToString();
            }
        }
        string SaveString2 = "";
        for (int i = 0; i < ActiveEvent.Count; i++)
        {
            if (SaveString2.Length == 0)
            {
                SaveString2 = ActiveEvent[i].ToString();
            }
            else
            {
                SaveString2 += "," + ActiveEvent[i].ToString();
            }
        }
        string SaveString3 = "";
        for (int i = 0; i < ReadyEvent.Count; i++)
        {
            if (SaveString3.Length == 0)
            {
                SaveString3 = ReadyEvent[i].ToString();
            }
            else
            {
                SaveString3 += "," + ReadyEvent[i].ToString();
            }
        }
        string SaveString4 = "";
        for (int i = 0; i < FinishEvent.Count; i++)
        {
            if (SaveString4.Length == 0)
            {
                SaveString4 = FinishEvent[i].ToString();
            }
            else
            {
                SaveString4 += "," + FinishEvent[i].ToString();
            }
        }
        string SaveString5 = "";
        for (int i = 0; i < DelayEvent.Count; i++)
        {
            if (SaveString5.Length == 0)
            {
                SaveString5 = DelayEvent[i].ToString();
            }
            else
            {
                SaveString5 += "," + DelayEvent[i].ToString();
            }
        }
        string SaveString6 = "";
        for (int i = 0; i < DelayDay.Count; i++)
        {
            if (SaveString6.Length == 0)
            {
                SaveString6 = DelayDay[i].ToString();
            }
            else
            {
                SaveString6 += "," + DelayDay[i].ToString();
            }
        }
        XmlDocument xmlSave = new XmlDocument();
        xmlSave.Load(Application.persistentDataPath + "/save/Save.save");
        XmlElement xmlNodeS = xmlSave.DocumentElement;
        foreach (XmlNode elementsS in xmlNodeS)
        {
            if (elementsS == null)
                continue;
            switch (elementsS.LocalName)
            {
                case "UnActiveEvent":
                    elementsS.InnerText = SaveString1;
                    break;
                case "ActiveEvent":
                    elementsS.InnerText = SaveString2;
                    break;
                case "ReadyEvent":
                    elementsS.InnerText = SaveString3;
                    break;
                case "FinishEvent":
                    elementsS.InnerText = SaveString4;
                    break;
                case "DelayEvent":
                    elementsS.InnerText = SaveString5;
                    break;
                case "DelayDay":
                    elementsS.InnerText = SaveString6;
                    break;
            }
        }
        xmlSave.Save(Application.persistentDataPath + "/save/Save.save");
    }


    Scr_TimeControl time;
    Scr_Num num;
    Scr_News news;
    Scr_Load LoadControl;

    public GameObject EventGroup;
    public GameObject CG;
    public Text Title;
    public Text Describe;
    public Image Picture;
    public Text CGTitle;
    public Text CGDescribe;
    public Image CGPicture;

    [HideInInspector]
    public bool showEvent;

    TimeMode TempTimeMode;


    public List<int> UnActiveEvent = new List<int>();//未激活事件，未激活事件就像事件2那样，需要有前一个事件的完成才会激活，在游戏运行时，游戏不会检测未激活事件的触发条件
    public List<int> ActiveEvent = new List<int>();//激活事件，已激活事件就是母事件（没有前置需求的事件）和已经激活的子事件，游戏会检测他们的条件
    List<int> ReadyEvent = new List<int>();//就绪事件，就绪事件就是所有条件都满足的事件，他会每回合过一次概率，如果概率过了就会触发事件，如果没有继续等待下一回合
    List<int> HappenEvent = new List<int>();//正在发生的事件,将发生的事件按照一定顺序排序，并依次发生
    List<int> FinishEvent = new List<int>();//已发生事件,无论如何也不会再检测了，即使触发条件都成立
    List<int> DelayEvent = new List<int>();//延迟事件
    List<int> DelayDay = new List<int>();//延迟天数

    List<float> dynamicProbability = new List<float>();//事件的发生概率
    List<float> addProbability = new List<float>();//事件增加的概率
    List<int> isaddProbability = new List<int>();//有增加的概率的事件

    List<int> priorityList = new List<int>();//储存每个事件的优先级
    List<int> isInvisibleList = new List<int>();//看事件是否是隐藏
    List<int> isRepeatList = new List<int>();//看事件是否是重复
    List<float> staticProbability = new List<float>();//事件的发生概率
    List<int> isImportantList = new List<int>();//看事件是否是重要

    int Language = 1;
    bool isLoad = false;

    int count;

    //记录事件列表，便于初始化
    List<int> RUnActiveEvent = new List<int>();//未激活事件，未激活事件就像事件2那样，需要有前一个事件的完成才会激活，在游戏运行时，游戏不会检测未激活事件的触发条件
    List<int> RActiveEvent = new List<int>();//激活事件，已激活事件就是母事件（没有前置需求的事件）和已经激活的子事件，游戏会检测他们的条件
    List<int> RReadyEvent = new List<int>();//就绪事件，就绪事件就是所有条件都满足的事件，他会每回合过一次概率，如果概率过了就会触发事件，如果没有继续等待下一回合
    List<int> RFinishEvent = new List<int>();//已发生事件,无论如何也不会再检测了，即使触发条件都成立
    List<float> RdynamicProbability = new List<float>();//事件的发生概率
    List<float> RaddProbability = new List<float>();//事件增加的概率
    List<int> RisaddProbability = new List<int>();//有增加的概率的事件
                                                  //

    List<string> Key = new List<string>();
    List<string> TextInfor = new List<string>();


    public void Start1()
    {
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(((TextAsset)Resources.Load("Xml/Event")).text);
        XmlElement xmlNode = xmlDoc.DocumentElement;//SelectSingleNode("events").ChildNodes;
        foreach (XmlNode elements in xmlNode)
        {
            //首先要帮憨憨u3d过滤掉注释部分
            XmlElement element = elements as XmlElement;
            if (element == null)
                continue;
            if (element.LocalName == "eventAmount")
            {
                int.TryParse(element.InnerText, out count);
            }
            if (element.LocalName == "event")
            {
                int.TryParse(element.Attributes["id"].Value, out int i);

                //初始化事件发生的概率
                var probability = element.GetElementsByTagName("probability");
                //如果未填写概率，即默认100
                if (probability.Count == 0)
                {
                    dynamicProbability.Add(100);
                }
                else
                {
                    int.TryParse(probability[0].InnerText, out int TempProbability);
                    var probabilityA = probability[0] as XmlElement;
                    dynamicProbability.Add(TempProbability);
                    if (!FinishEvent.Contains(i))//不检测已经结束的事件
                    {
                        //对增加概率的初始化
                        if (probabilityA.HasAttribute("Add"))
                        {
                            isaddProbability.Add(i);

                            addProbability.Add(Convert.ToSingle(probabilityA.Attributes["Add"].Value));
                        }
                    }
                }
                //检测事件特质

                var trait = element.GetElementsByTagName("trait");
                if (trait.Count != 0)
                {
                    var straitE = trait[0] as XmlElement;

                    //是否是子事件
                    if (straitE.GetElementsByTagName("subEvent").Count == 1)
                    {
                        UnActiveEvent.Add(i);
                    }
                    else
                    {
                        ActiveEvent.Add(i);
                    }

                    //是否是隐藏事件
                    if (straitE.GetElementsByTagName("isInvisible").Count == 1)
                    {
                        isInvisibleList.Add(i);
                    }
                    //是否是重复事件
                    if (straitE.GetElementsByTagName("isRepeat").Count == 1)
                    {
                        isRepeatList.Add(i);
                        staticProbability.Add(dynamicProbability[i]);
                    }
                    //是否是重要事件
                    if (straitE.GetElementsByTagName("isImportant").Count == 1)
                    {
                        isImportantList.Add(i);

                    }
                }
                else
                {
                    if (!isLoad)
                    {
                        ActiveEvent.Add(i);
                    }
                }

                //初始化优先级
                var priority = element.GetElementsByTagName("priority");
                //未填写，默认是0
                if (priority.Count == 0)
                {
                    priorityList.Add(0);
                }
                else
                {
                    var priorityE = priority[0] as XmlElement;
                    int.TryParse(priorityE.InnerText, out int priorityI);
                    priorityList.Add(priorityI);
                }

            }
        }
        //储存一下
        RUnActiveEvent = UnActiveEvent;
        RActiveEvent = ActiveEvent;
        RReadyEvent = ReadyEvent;
        RFinishEvent = FinishEvent;
        RdynamicProbability = dynamicProbability;
        RaddProbability = addProbability;
        RisaddProbability = isaddProbability;
    }


    public void Start2()
    {
        bool isLoad = false;

        UnActiveEvent = RUnActiveEvent;
        ActiveEvent = RActiveEvent;
        ReadyEvent = RReadyEvent;
        FinishEvent = RFinishEvent;
        dynamicProbability = RdynamicProbability;
        addProbability = RaddProbability;
        isaddProbability = RisaddProbability;

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
        DelayEvent = new List<int>();
        DelayDay = new List<int>();

        Key = new List<string>();
        TextInfor = new List<string>();
        string LocalizationText = ((TextAsset)Resources.Load("Localization/" + Language.ToString() + "/Events")).text;
        string[] LocalizationArray = LocalizationText.Split('\n');
        for (int l = 0; l < LocalizationArray.Length; l++)
        {
            string[] LocalArray = LocalizationArray[l].Split(':');
            Key.Add(LocalArray[0]);
            TextInfor.Add(LocalArray[1]);
        }



        //对事件的类别进行初始化，分出未激活的子事件与普通事件,并初始化事件发生的概率
        if (isLoad)
        {
            UnActiveEvent = new List<int>();
            ActiveEvent = new List<int>();
            FinishEvent = new List<int>();
            ReadyEvent = new List<int>();

            XmlDocument xmlSave = new XmlDocument();
            xmlSave.Load(Application.persistentDataPath + "/save/Save.save");
            XmlElement xmlNodeS = xmlSave.DocumentElement;
            foreach (XmlNode elementsS in xmlNodeS)
            {
                if (elementsS == null)
                    continue;
                switch (elementsS.LocalName)
                {
                    case "FinishEvent":
                        if (elementsS.InnerText != "")
                        {
                            string[] finishAry = elementsS.InnerText.Split(',');
                            for (int a = 0; a < finishAry.Length; a++)
                            {
                                int.TryParse(finishAry[a], out int b);
                                FinishEvent.Add(b);
                            }
                        }
                        break;
                    case "UnActiveEvent":
                        if (elementsS.InnerText != "")
                        {
                            string[] finishAry = elementsS.InnerText.Split(',');
                            for (int a = 0; a < finishAry.Length; a++)
                            {
                                int.TryParse(finishAry[a], out int b);
                                UnActiveEvent.Add(b);
                            }
                        }
                        break;
                    case "ActiveEvent":
                        if (elementsS.InnerText != "")
                        {
                            string[] finishAry = elementsS.InnerText.Split(',');
                            for (int a = 0; a < finishAry.Length; a++)
                            {
                                int.TryParse(finishAry[a], out int b);
                                ActiveEvent.Add(b);
                            }
                        }
                        break;
                    case "ReadyEvent":
                        if (elementsS.InnerText != "")
                        {
                            string[] finishAry = elementsS.InnerText.Split(',');
                            for (int a = 0; a < finishAry.Length; a++)
                            {
                                int.TryParse(finishAry[a], out int b);
                                ReadyEvent.Add(b);
                            }
                        }
                        break;
                    case "DelayEvent":
                        if (elementsS.InnerText != "")
                        {
                            string[] finishAry = elementsS.InnerText.Split(',');
                            for (int a = 0; a < finishAry.Length; a++)
                            {
                                int.TryParse(finishAry[a], out int b);
                                DelayEvent.Add(b);
                            }
                        }
                        break;
                    case "DelayDay":
                        if (elementsS.InnerText != "")
                        {
                            string[] finishAry = elementsS.InnerText.Split(',');
                            for (int a = 0; a < finishAry.Length; a++)
                            {
                                int.TryParse(finishAry[a], out int b);
                                DelayDay.Add(b);
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
        }
        else
        {
            FinishEvent.Add(0);//把事件模板进去，让他不会发生
        }
    }
    void Awake()
    {
        time = FindObjectOfType<Scr_TimeControl>();
        num = FindObjectOfType<Scr_Num>();
        news = FindObjectOfType<Scr_News>();
        LoadControl = FindObjectOfType<Scr_Load>();
        EventGroup.SetActive(false);
        CG.SetActive(false);

    }

    //以下是事件触发机制
    public void EventCheck()//在Scr_TimeControl里的Update里，这样就可以游戏事件里每增加一天才会检查一次，不至于每帧都查
    {
        if (true)//测试时候先不显示事件
        {
            if (DelayEvent.Count > 0)
            {
                for (int i = 0; i < DelayDay.Count; i++)
                {
                    if (DelayDay[i] > 0)
                    {
                        DelayDay[i] -= 1;
                    }
                    else
                    {
                        DelayDay.RemoveAt(i);
                        ActiveEvent.Add(DelayEvent[i]);
                        DelayEvent.RemoveAt(i);
                    }
                }
            }
            for (int i = 0; i <= count; i++)
            {
                //Debug.Log(ActiveEvent.Count);

                if (!FinishEvent.Contains(i) && !UnActiveEvent.Contains(i))//首先不检测已经结束的事件和未激活事件
                {
                    EventConditionControl(i);
                }

            }

        }

    }

    void LateUpdate()//事件的发生
    {
        if (!LoadControl.StartControl) { return; }
        if (showEvent) { return; }
        if (HappenEvent.Count == 1)
        {
            HappeningEvent(0);
        }
        if (HappenEvent.Count > 1)
        {
            int tempEvent;
            for (int i = 0; i < HappenEvent.Count; i++)
            {
                //把要发生的事件排序，先比较优先级，如果优先级相同再比较序号，优先级高的先发生，序号小的先发生
                //以此发生事件
                for (int j = 0; j < HappenEvent.Count - i - 1; j++)
                {
                    if (priorityList[HappenEvent[i]] < priorityList[HappenEvent[i + 1]])
                    {
                        tempEvent = HappenEvent[j];
                        HappenEvent[j] = HappenEvent[j + 1];
                        HappenEvent[j + 1] = tempEvent;
                    }
                }
            }
            HappeningEvent(0);
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
        CG.SetActive(false);

    }

    //事件检测符合条件的机制
    void EventCondition(bool isMeetingConditions, int i)
    {
        if (isMeetingConditions)//看是否满足条件
        {
            /*
             for (int a = 0; a < ActiveEvent.Count; a++)//如果这个事件是激活事件
            {
                if (ActiveEvent[a] == i)
                {
                    ReadyEvent.Add(i);//再放在就绪事件列表里
                    ActiveEvent.RemoveAt(a);//从激活事件的列表中移除该事件
                    Debug.Log(ActiveEvent.Count);
                }
            }
             */


            if (ActiveEvent.Contains(i))//如果这个事件是激活事件
            {
                ReadyEvent.Add(i);//再放在就绪事件列表里
                ActiveEvent.Remove(i);//从激活事件的列表中移除该事件
            }

            if (ReadyEvent.Contains(i))//如果这个事件是就绪事件
            {
                if (UnityEngine.Random.Range(0, 100) <= dynamicProbability[i])//过一次概率
                {
                    HappenEvent.Add(i);//放在正在发生事件列表里
                    ReadyEvent.Remove(i);//从就绪事件的列表中移除该事件
                }
                else
                {
                    if (isaddProbability.Contains(i))
                    {
                        dynamicProbability[i] += addProbability[isaddProbability.FindIndex(a => a == i)];//概率增加
                        dynamicProbability[i] = Mathf.Clamp(dynamicProbability[i], 0, 100);//限制概率的范围
                    }

                }
            }
        }
        else
        {
            //如果条件不符合，则从就绪列表返回到之前的激活列表里
            if (ReadyEvent.Contains(i))
            {
                ActiveEvent.Add(i);
                ReadyEvent.Remove(i);
            }
        }
    }

    //读取Localization中的事件excel文件


    void HappeningEvent(int i)
    {
        if (!isInvisibleList.Contains(HappenEvent[i]))
        {
            TempTimeMode = time.timeMode;
            showEvent = true;
            time.Pause();//发生事件时游戏暂停




            //读取Event.xls文件

            string path = "";
            string xmlTitle = "Title Missing";
            string xmlDescribe = "Describe Missing";
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(((TextAsset)Resources.Load("Xml/Event")).text);
            XmlElement xmlNode = xmlDoc.DocumentElement;//SelectSingleNode("events").ChildNodes;
            foreach (XmlNode elements in xmlNode)
            {
                XmlElement element = elements as XmlElement;
                if (element == null)
                    continue;
                if (element.LocalName == "event")
                {
                    if (element.Attributes["id"].Value == HappenEvent[i].ToString())
                    {
                        xmlTitle = element.SelectSingleNode("title").InnerText;
                        xmlDescribe = element.SelectSingleNode("describe").InnerText;
                        path = element.SelectSingleNode("picture").InnerText;
                    }
                }
            }

            bool hasTitle = false;
            bool hasDescribe = false;
            for (int l = 0; l < Key.Count; l++)
            {
                if (Key[l] == xmlTitle)
                {
                    if (isImportantList.Contains(HappenEvent[i]))
                    {
                        CGTitle.text = TextInfor[l];
                    }
                    else
                    {
                        Title.text = TextInfor[l];
                    }
                    hasTitle = true;
                }
                if (Key[l] == xmlDescribe)
                {
                    if (isImportantList.Contains(HappenEvent[i]))
                    {
                        CGDescribe.text = TextInfor[l];
                    }
                    else
                    {
                        Describe.text = TextInfor[l];
                    }
                    hasDescribe = true;
                    Debug.Log(TextInfor[l]);
                }
                Debug.Log(Key[l]);
                Debug.Log(xmlDescribe);
            }
            if (!hasTitle)
            {
                if (isImportantList.Contains(HappenEvent[i]))
                {
                    CGTitle.text = xmlTitle;
                }
                else
                {
                    Title.text = xmlTitle;
                }
            }
            if (!hasDescribe)
            {
                if (isImportantList.Contains(HappenEvent[i]))
                {
                    CGDescribe.text = xmlDescribe;
                }
                else
                {
                    Describe.text = xmlDescribe;
                }

            }


            if (path != "*")// File.Exists(Application.dataPath + "/Resources/" + path + ".png")
            {


                if (isImportantList.Contains(HappenEvent[i]))
                {
                    CGPicture.sprite = Resources.Load(path, typeof(Sprite)) as Sprite;

                }
                else
                {
                    Picture.sprite = Resources.Load(path, typeof(Sprite)) as Sprite;
                }
            }
            else
            {
                if (isImportantList.Contains(HappenEvent[i]))
                {
                    CGPicture.sprite = Resources.Load("EventPictures/EventPic", typeof(Sprite)) as Sprite;

                }
                else
                {
                    Picture.sprite = Resources.Load("EventPictures/EventPic", typeof(Sprite)) as Sprite;
                }

            }
            if (isImportantList.Contains(HappenEvent[i]))
            {
                CG.SetActive(true);
                isImportantList.Contains(HappenEvent[i]);
            }
            else
            {
                EventGroup.SetActive(true);
            }
        }


        //执行事件的效果
        EventEffectControl(HappenEvent[i]);

        if (!isRepeatList.Contains(HappenEvent[i]))
        {
            FinishEvent.Add(HappenEvent[i]);
            HappenEvent.Remove(HappenEvent[i]);

        }
        else
        {
            ReadyEvent.Add(HappenEvent[i]);
            dynamicProbability[HappenEvent[i]] = staticProbability[isRepeatList.FindIndex(a => a == HappenEvent[i])];//概率重置
            HappenEvent.Remove(HappenEvent[i]);
        }


    }

    void AddNews(int NewsIndex, string NewsSlot)
    {
        news.NewsList.Add(NewsIndex);
        news.NewsListSlot.Add(NewsSlot);
    }
}
