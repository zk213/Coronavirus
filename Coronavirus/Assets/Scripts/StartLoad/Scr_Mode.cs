﻿using System.IO;
using System.Xml;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameMode
{
    [EnumNameAttribute("普通模式")]
    Normal,

    [EnumNameAttribute("测试模式")]
    Test,

    [EnumNameAttribute("教学模式")]
    Tutorial
}
public class Scr_Mode : MonoBehaviour
{
    [EnumNameAttribute("游戏模式")]
    public GameMode gameMode;
    [InspectorShow("当前语言")]
    public string Language = "SimpleChinese";
    [HideInInspector]

    void Awake()
    {
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
            root.AppendChild(Day);

            XmlElement UnActiveEvent = xml.CreateElement("UnActiveEvent");
            root.AppendChild(UnActiveEvent);

            XmlElement ActiveEvent = xml.CreateElement("ActiveEvent");
            root.AppendChild(ActiveEvent);

            XmlElement ReadyEventt = xml.CreateElement("ReadyEvent");
            root.AppendChild(ReadyEventt);

            XmlElement FinishEvent = xml.CreateElement("FinishEvent");
            root.AppendChild(FinishEvent);

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

            xml.AppendChild(root);
            //最后保存文件
            xml.Save(path);
        }
    }

    public void Load()
    {
        string path = Application.persistentDataPath + "setting.set";
        if (!File.Exists(path))
        {
            XmlDocument xml = new XmlDocument();
            XmlDeclaration xmlDeclaration = xml.CreateXmlDeclaration("1.0", "UTF-8", null);//固定格式
            xml.AppendChild(xmlDeclaration);
            XmlElement root = xml.CreateElement("Set");

            XmlElement XLanguage = xml.CreateElement("Language");
            XLanguage.InnerText = Language;
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
        SceneManager.LoadScene("StartA");
    }

    public void NewStart()
    {
        string path = Application.persistentDataPath + "setting.set";
        if (!File.Exists(path))
        {
            XmlDocument xml = new XmlDocument();
            XmlDeclaration xmlDeclaration = xml.CreateXmlDeclaration("1.0", "UTF-8", null);//固定格式
            xml.AppendChild(xmlDeclaration);
            XmlElement root = xml.CreateElement("Set");

            XmlElement XLanguage = xml.CreateElement("Language");
            XLanguage.InnerText = Language;
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
        SceneManager.LoadScene("StartA");
    }
}