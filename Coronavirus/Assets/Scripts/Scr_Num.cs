using System.Xml;
using UnityEngine;
using UnityEngine.UI;


public class Scr_Num : MonoBehaviour
{
    public void LocalSave()
    {
        XmlDocument xmlSave = new XmlDocument();
        xmlSave.Load(Application.persistentDataPath + "/save/Save.save");
        XmlElement xmlNodeS = xmlSave.DocumentElement;
        foreach (XmlNode elementsS in xmlNodeS)
        {
            if (elementsS == null)
                continue;
            if (elementsS.LocalName == "Influence")
            {
                elementsS.InnerText = InfluenceVal.ToString();
            }
            if (elementsS.LocalName == "Cohesion")
            {
                elementsS.InnerText = CohesionVal.ToString();
            }
            if (elementsS.LocalName == "Vaccine")
            {
                elementsS.InnerText = VaccineProcess.ToString();
            }
        }
        xmlSave.Save(Application.persistentDataPath + "/save/Save.save");
    }


    [Header("基本数值")]
    [InspectorShow("影响力")]
    public float InfluenceVal;
    [InspectorShow("凝聚力")]
    public float CohesionVal;

    [Header("疫苗进程")]
    [Range(0, 100)]
    public float VaccineProcess;
    public Text InfluenceNum;
    public Text CohesionNum;
    public Text ProcessText;
    public Image FullProcess;

    GameMode gamemode = GameMode.Normal;
    bool isLoad = false;

    void Awake()
    {
        XmlDocument SxmlDoc = new XmlDocument();
        SxmlDoc.Load(Application.persistentDataPath + "setting.set");
        XmlElement SxmlNode = SxmlDoc.DocumentElement;
        foreach (XmlNode elements in SxmlNode)
        {
            if (elements == null)
                continue;
            if (elements.LocalName == "Mode")
            {
                switch (elements.InnerText)
                {
                    case "Normal":
                        gamemode = GameMode.Normal;
                        break;
                    case "Test":
                        gamemode = GameMode.Test;
                        break;
                    case "Tutorial":
                        gamemode = GameMode.Tutorial;
                        break;
                }
            }
            if (elements.LocalName == "SMode")
            {
                if (elements.InnerText == "Load")
                {
                    isLoad = true;
                }
            }
        }

        VaccineProcess = 0;
        if (isLoad)
        {
            XmlDocument xmlSave = new XmlDocument();
            xmlSave.Load(Application.persistentDataPath + "/save/Save.save");
            XmlElement xmlNodeS = xmlSave.DocumentElement;
            foreach (XmlNode elementsS in xmlNodeS)
            {
                if (elementsS == null)
                    continue;
                if (elementsS.LocalName == "Influence")
                {
                    InfluenceVal = float.Parse(elementsS.InnerText);
                }
                if (elementsS.LocalName == "Cohesion")
                {
                    CohesionVal = float.Parse(elementsS.InnerText);
                }
                if (elementsS.LocalName == "Vaccine")
                {
                    VaccineProcess = float.Parse(elementsS.InnerText);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (gamemode == GameMode.Normal)
        {
            if (InfluenceVal <= 999 && InfluenceVal >= -99)
            {
                InfluenceNum.text = string.Format("{0:F0}", InfluenceVal);
            }
            else if (InfluenceVal > 999)
            {
                InfluenceNum.text = "∞";
            }
            else
            {
                InfluenceNum.text = "-∞";
            }
            if (CohesionVal <= 999 && CohesionVal >= -99)
            {
                CohesionNum.text = string.Format("{0:F0}", CohesionVal);
            }
            else if (CohesionVal > 999)
            {
                CohesionNum.text = "∞";
            }
            else
            {
                CohesionNum.text = "-∞";
            }


        }
        else
        {
            InfluenceNum.text = "∞";
            CohesionNum.text = "∞";
        }

        VaccineProcess = Mathf.Clamp(VaccineProcess, 0, 100);
        FullProcess.fillAmount = VaccineProcess / 100;
        ProcessText.text = string.Format("{0:F0}", VaccineProcess) + "%";
    }
}
