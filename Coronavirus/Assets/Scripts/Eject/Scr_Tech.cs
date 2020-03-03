using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.UI;

public class Scr_Tech : MonoBehaviour
{
    void TechEffect()
    {
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(((TextAsset)Resources.Load("Xml/Tech")).text);
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
                            case "event":
                                int delayDay = 0;
                                int.TryParse(effect.InnerText, out int eventIndex);
                                if (effect.HasAttribute("delay"))
                                {
                                    int.TryParse(effect.Attributes["delay"].Value, out delayDay);
                                }
                                if (Events.UnActiveEvent.Contains(eventIndex))
                                {
                                    if (delayDay > 0)
                                    {

                                        Events.DelayEvent.Add(eventIndex);
                                        Events.DelayDay.Add(delayDay);
                                        Events.UnActiveEvent.Remove(eventIndex);

                                    }
                                    else
                                    {
                                        Events.ActiveEvent.Add(eventIndex);
                                        Events.UnActiveEvent.Remove(eventIndex);
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

    List<string> Key = new List<string>();
    List<string> TextInfor = new List<string>();

    Scr_Num Value;
    Scr_Event Events;

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
        xmlDoc.LoadXml(((TextAsset)Resources.Load("Xml/Tech")).text);
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
                Tech[i].transform.Find("TechIcon").GetComponent<Image>().sprite = Resources.Load(element.SelectSingleNode("picture").InnerText, typeof(Sprite)) as Sprite;
                //Tech[i].transform.Find("TechIcon").GetComponent<Image>().sprite = Resources.Load("TechnologyPictures/TechError", typeof(Sprite)) as Sprite;
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

        string BLocalizationText = ((TextAsset)Resources.Load("Localization/" + Language.ToString() + "/Button")).text;
        string[] BLocalizationArray = BLocalizationText.Split('\n');
        for (int l = 0; l < BLocalizationArray.Length; l++)
        {
            string[] LocalArray = BLocalizationArray[l].Split(':');
            switch (LocalArray[0])
            {
                case "NationalCondition":
                    Label[0] = LocalArray[1];
                    break;
                case "PolicyIssuance":
                    Label[1] = LocalArray[1];
                    break;
                case "MedicalResearch":
                    Label[2] = LocalArray[1];
                    break;
                case "Back":
                    Label[3] = LocalArray[1];
                    break;
                default:
                    break;
            }
        }
        hasLabel = true;

        Key = new List<string>();
        TextInfor = new List<string>();
        string LocalizationText = ((TextAsset)Resources.Load("Localization/" + Language.ToString() + "/Tech")).text;
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


        Value = FindObjectOfType<Scr_Num>();
        Events = FindObjectOfType<Scr_Event>();
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
                if (TechIndex != -1)
                {
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.LoadXml(((TextAsset)Resources.Load("Xml/Tech")).text);
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
                                TechTitle = element.SelectSingleNode("title").InnerText;
                                TechDescribe = element.SelectSingleNode("describe").InnerText;
                            }
                        }
                    }
                    for (int l = 0; l < Key.Count; l++)
                    {
                        if (Key[l] == TechTitle)
                        {
                            TechTitle = TextInfor[l]; ;

                        }
                        if (Key[l] == TechDescribe)
                        {
                            TechDescribe = TextInfor[l]; ;

                        }
                    }

                }
                else
                {
                    for (int l = 0; l < Key.Count; l++)
                    {
                        if (Key[l] == "Introduce")
                        {
                            TechTitle = TextInfor[l]; ;

                        }
                        if (Key[l] == "Introduce_Describe")
                        {
                            TechDescribe = TextInfor[l]; ;
                        }
                    }
                }
            }
            else
            {
                TechIndex = -1;
            }
            if (page != 1)
            {
                parent.transform.Find("UpGradePage" + page.ToString()).transform.Find("UpGradePage" + page.ToString() + "Ground").transform.Find("Describe").GetComponent<Text>().text = "<b>" + TechTitle + "</b>" + "\n\n" + "<size=40>" + TechDescribe + "</size>";
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
            xmlDoc.LoadXml(((TextAsset)Resources.Load("Xml/Tech")).text);
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
