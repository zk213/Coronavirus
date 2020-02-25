using System.IO;
using System.Xml;
using UnityEngine;

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

    public GameObject Scene1;
    public GameObject Scene2;
    public GameObject Scene3;
    [HideInInspector]


    void Awake()
    {
        /*
         Scene1.SetActive(true);
        Scene2.SetActive(false);
        Scene3.SetActive(false);
         */

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

            XmlElement PPTransport = xml.CreateElement("Vaccine");
            root.AppendChild(PPTransport);

            XmlElement IPTransport = xml.CreateElement("Vaccine");
            root.AppendChild(IPTransport);

            XmlElement Population = xml.CreateElement("Vaccine");
            root.AppendChild(Population);

            XmlElement Medicine = xml.CreateElement("Vaccine");
            root.AppendChild(Medicine);

            XmlElement Material = xml.CreateElement("Vaccine");
            root.AppendChild(Material);

            XmlElement Personnel = xml.CreateElement("Vaccine");
            root.AppendChild(Personnel);

            XmlElement Bed = xml.CreateElement("Vaccine");
            root.AppendChild(Bed);

            XmlElement Death = xml.CreateElement("Vaccine");
            root.AppendChild(Death);

            XmlElement Cure = xml.CreateElement("Vaccine");
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
        Scene2.SetActive(true);
        Scene1.SetActive(false);
        //SceneManager.LoadScene("StartA");
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
        Scene2.SetActive(true);
        Scene1.SetActive(false);
    }
}
