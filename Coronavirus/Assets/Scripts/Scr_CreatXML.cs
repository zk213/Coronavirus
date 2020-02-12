using System.IO;
using System.Xml;
using UnityEngine;

public class Scr_CreatXML : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //创建Xml
        XmlDocument xmlDoc = new XmlDocument();
        XmlDeclaration xmlDeclaration = xmlDoc.CreateXmlDeclaration("1.0", "UTF-8", null);
        xmlDoc.AppendChild(xmlDeclaration);

        //在节点中写入数据
        XmlElement root = xmlDoc.CreateElement("XmlRoot");
        xmlDoc.AppendChild(root);
        XmlElement group = xmlDoc.CreateElement("Group");
        group.SetAttribute("Name1", "AAA");
        group.SetAttribute("Name2", "BBB");
        root.AppendChild(group);

        //读取节点并输出XML字符串
        using (StringWriter stringWriter = new StringWriter())
        {
            using (XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter))
            {
                xmlDoc.WriteTo(xmlTextWriter);
                xmlTextWriter.Flush();
                Debug.Log(stringWriter.ToString());
            }
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
