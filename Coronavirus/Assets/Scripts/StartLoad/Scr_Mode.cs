﻿using System.IO;
using System.Xml;
using UnityEngine;
using UnityEngine.UI;

public enum GameMode
{
    Normal,
    Test,
    Tutorial
}
public class Scr_Mode : MonoBehaviour
{
    public GameMode gameMode;
    public int Language = 1;

    public GameObject Scene1;
    public GameObject Scene2;
    public GameObject Scene3;
    public GameObject ContinueB;
    Scr_Load LoadS;
    bool hasSave = false;

    void Awake()
    {
        /*
         Scene1.SetActive(true);
        Scene2.SetActive(false);
        Scene3.SetActive(false);
         */
        LoadS = FindObjectOfType<Scr_Load>();
        gameMode = GameMode.Normal;
        //DontDestroyOnLoad(gameObject);
        if (!Directory.Exists(Application.persistentDataPath + "/save"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/save");
        }
        string path = Application.persistentDataPath + "/save/Save.save";
        if (!File.Exists(path))
        {

            XmlDocument xml = new XmlDocument();
            XmlDeclaration xmlDeclaration = xml.CreateXmlDeclaration("1.0", "UTF-8", null);//固定格式
            xml.AppendChild(xmlDeclaration);
            XmlElement root = xml.CreateElement("Save");
            root.SetAttribute("id", "1");

            XmlElement Day = xml.CreateElement("Day");
            Day.InnerText = "0";
            root.AppendChild(Day);

            XmlElement UnActiveEvent = xml.CreateElement("UnActiveEvent");
            root.AppendChild(UnActiveEvent);

            XmlElement ActiveEvent = xml.CreateElement("ActiveEvent");
            root.AppendChild(ActiveEvent);

            XmlElement ReadyEventt = xml.CreateElement("ReadyEvent");
            root.AppendChild(ReadyEventt);

            XmlElement FinishEvent = xml.CreateElement("FinishEvent");
            root.AppendChild(FinishEvent);

            XmlElement DelayEvent = xml.CreateElement("DelayEvent");
            root.AppendChild(DelayEvent);

            XmlElement DelayDay = xml.CreateElement("DelayDay");
            root.AppendChild(DelayDay);

            XmlElement Tech = xml.CreateElement("Tech");
            root.AppendChild(Tech);

            XmlElement TechEureka = xml.CreateElement("TechEureka");
            root.AppendChild(TechEureka);

            XmlElement Influence = xml.CreateElement("Influence");
            root.AppendChild(Influence);

            XmlElement Cohesion = xml.CreateElement("Cohesion");
            root.AppendChild(Cohesion);

            XmlElement Vaccine = xml.CreateElement("Vaccine");
            root.AppendChild(Vaccine);

            XmlElement PPTransport = xml.CreateElement("PPTransport");
            root.AppendChild(PPTransport);

            XmlElement IPTransport = xml.CreateElement("IPTransport");
            root.AppendChild(IPTransport);

            XmlElement Population = xml.CreateElement("Population");
            root.AppendChild(Population);

            XmlElement Medicine = xml.CreateElement("Medicine");
            root.AppendChild(Medicine);

            XmlElement Material = xml.CreateElement("Material");
            root.AppendChild(Material);

            XmlElement Personnel = xml.CreateElement("Personnel");
            root.AppendChild(Personnel);

            XmlElement Bed = xml.CreateElement("Bed");
            root.AppendChild(Bed);

            XmlElement Death = xml.CreateElement("Death");
            root.AppendChild(Death);

            XmlElement Cure = xml.CreateElement("Cure");
            root.AppendChild(Cure);

            for (int a = 1; a <= 35; a++)
            {
                XmlElement People = xml.CreateElement("People" + a.ToString());
                root.AppendChild(People);
            }

            xml.AppendChild(root);
            //最后保存文件
            xml.Save(path);
        }
        else
        {
            XmlDocument xmlSave = new XmlDocument();
            xmlSave.Load(Application.persistentDataPath + "/save/Save.save");
            XmlElement xmlNodeS = xmlSave.DocumentElement;
            foreach (XmlNode elementsS in xmlNodeS)
            {
                if (elementsS == null)
                    continue;
                if (elementsS.LocalName == "Day")
                {
                    int.TryParse(elementsS.InnerText, out int day);
                    if (day > 1)
                    {
                        hasSave = true;
                    }
                }
            }
        }
        if (hasSave)
        {
            ContinueB.GetComponent<Image>().color = Color.white;
        }
        else
        {
            ContinueB.GetComponent<Image>().color = new Color32(255, 255, 255, 125);
        }
    }


    public void Load()
    {
        if (!hasSave) { return; }
        string path = Application.persistentDataPath + "/setting.set";
        if (!File.Exists(path))
        {
            XmlDocument xml = new XmlDocument();
            XmlDeclaration xmlDeclaration = xml.CreateXmlDeclaration("1.0", "UTF-8", null);//固定格式
            xml.AppendChild(xmlDeclaration);
            XmlElement root = xml.CreateElement("Set");

            XmlElement XLanguage = xml.CreateElement("Language");
            XLanguage.InnerText = Language.ToString();
            root.AppendChild(XLanguage);

            XmlElement XMode = xml.CreateElement("Mode");
            XMode.InnerText = gameMode.ToString();
            root.AppendChild(XMode);

            XmlElement XSMode = xml.CreateElement("SMode");
            XSMode.InnerText = "Load";
            root.AppendChild(XSMode);

            xml.AppendChild(root);
            //最后保存文件
            xml.Save(path);
        }
        else
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(path);
            XmlElement xmlNode = xmlDoc.DocumentElement;
            foreach (XmlNode elements in xmlNode)
            {
                if (elements == null)
                    continue;
                if (elements.LocalName == "SMode")
                {
                    elements.InnerText = "Load";
                }
            }
            xmlDoc.Save(path);
        }
        Scene2.SetActive(true);
        Scene1.SetActive(false);
        //SceneManager.LoadScene("StartA");
        LoadS.isLoad = true;
    }

    public void NewStart()
    {
        string path = Application.persistentDataPath + "/setting.set";
        if (!File.Exists(path))
        {
            XmlDocument xml = new XmlDocument();
            XmlDeclaration xmlDeclaration = xml.CreateXmlDeclaration("1.0", "UTF-8", null);//固定格式
            xml.AppendChild(xmlDeclaration);
            XmlElement root = xml.CreateElement("Set");

            XmlElement XLanguage = xml.CreateElement("Language");
            XLanguage.InnerText = Language.ToString();
            root.AppendChild(XLanguage);

            XmlElement XMode = xml.CreateElement("Mode");
            XMode.InnerText = gameMode.ToString();
            root.AppendChild(XMode);

            XmlElement XSMode = xml.CreateElement("SMode");
            XSMode.InnerText = "New";
            root.AppendChild(XSMode);

            xml.AppendChild(root);
            //最后保存文件
            xml.Save(path);
        }
        else
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(path);
            XmlElement xmlNode = xmlDoc.DocumentElement;
            foreach (XmlNode elements in xmlNode)
            {
                if (elements == null)
                    continue;
                if (elements.LocalName == "SMode")
                {
                    elements.InnerText = "New";
                }
            }
            xmlDoc.Save(path);
        }
        Scene2.SetActive(true);
        Scene1.SetActive(false);
    }
}
