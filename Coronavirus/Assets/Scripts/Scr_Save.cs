using System.Xml;     //xml操作 XML文件的命名空间
using UnityEngine;

public class Scr_Save : MonoBehaviour
{
    string id1;
    string id2;
    string name1;
    string name2;
    string year1;
    string year2;
    // Start is called before the first frame update
    void Start()
    {
        /*
         string path = Application.dataPath + "/data2.xml";
        if (!File.Exists(path))
        {
            //创建最上一层的节点。
            XmlDocument xml = new XmlDocument();
            //创建最上一层的节点。
            XmlElement root = xml.CreateElement("objects");
            //创建子节点
            XmlElement element = xml.CreateElement("messages");
            //设置节点的属性
            element.SetAttribute("id", "1");
            XmlElement elementChild1 = xml.CreateElement("contents");

            elementChild1.SetAttribute("name", "a");
            //设置节点内面的内容
            elementChild1.InnerText = "这就是你，你就是天狼";
            XmlElement elementChild2 = xml.CreateElement("mission");
            elementChild2.SetAttribute("map", "abc");
            elementChild2.InnerText = "去吧，少年，去实现你的梦想";
            //把节点一层一层的添加至xml中，注意他们之间的先后顺序，这是生成XML文件的顺序
            element.AppendChild(elementChild1);
            element.AppendChild(elementChild2);
            root.AppendChild(element);
            xml.AppendChild(root);
            //最后保存文件
            xml.Save(path);
        }
         */

        XmlDocument xml = new XmlDocument();

        xml.Load(Application.dataPath + "/data2.xml");
        //得到objects节点下的所有子节点
        XmlNodeList xmlNodeList = xml.SelectSingleNode("objects").ChildNodes;
        //遍历所有子节点
        foreach (XmlElement xl1 in xmlNodeList)
        {

            if (xl1.GetAttribute("id") == "1")
            {
                //继续遍历id为1的节点下的子节点
                foreach (XmlElement xl2 in xl1.ChildNodes)
                {
                    //放到一个textlist文本里
                    //textList.Add(xl2.GetAttribute("name") + ": " + xl2.InnerText);
                    //得到name为a的节点里的内容。放到TextList里
                    if (xl2.GetAttribute("name") == "a")
                    {
                        //Adialogue.Add(xl2.GetAttribute("name") + ": " + xl2.InnerText);
                        print("******************" + xl2.GetAttribute("name") + ": " + xl2.InnerText);
                    }
                    //得到name为b的节点里的内容。放到TextList里
                    else if (xl2.GetAttribute("map") == "abc")
                    {
                        //Bdialogue.Add(xl2.GetAttribute("name") + ": " + xl2.InnerText);
                        print("******************" + xl2.GetAttribute("name") + ": " + xl2.InnerText);
                    }
                }
            }
        }
        print(xml.OuterXml);
        /*
         string filePath = Application.dataPath + "/Resources/XML.xml";
        if (File.Exists(filePath))
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(filePath);
            XmlNodeList node = xmlDoc.SelectSingleNode("item").ChildNodes;
            //遍历节点
            foreach (XmlElement ele in node)
            {
                //item下面的节点
                Debug.Log(ele.Name);

                if (ele.Name == "item1")
                {
                    //first item1
                    foreach (XmlElement i1 in ele.ChildNodes)
                    {
                        Debug.Log(i1.Name);
                        if (i1.Name == "id")
                        {
                            id1 = i1.InnerText;
                        }
                        if (i1.Name == "name")
                        {
                            name1 = i1.InnerText;
                        }
                        if (i1.Name == "year")
                        {
                            year1 = i1.InnerText;
                        }
                    }
                }
                if (ele.Name == "item2")
                {
                    //first item1
                    foreach (XmlElement i2 in ele.ChildNodes)
                    {
                        Debug.Log(i2.Name);
                        if (i2.Name == "id")
                        {
                            id2 = i2.InnerText;
                        }
                        if (i2.Name == "name")
                        {
                            name2 = i2.InnerText;
                        }
                        if (i2.Name == "year")
                        {
                            year2 = i2.InnerText;
                        }
                    }
                }

            }
        }
        Debug.Log("id1:  " + id1);
        Debug.Log("name1:  " + name1);
        Debug.Log("year1:  " + year1);
        Debug.Log("id2:  " + id2);
        Debug.Log("name2:  " + name2);
        Debug.Log("year2:  " + year2);
         */

    }

    // Update is called once per frame
    void Update()
    {

    }
}
/*
        string path = Application.dataPath + "/Resources/Xml/Event.xml";
        if (!File.Exists(path))
        {
            XmlDocument xml = new XmlDocument();
            XmlDeclaration xmlDeclaration = xml.CreateXmlDeclaration("1.0", "UTF-8", null);//固定格式
            xml.AppendChild(xmlDeclaration);//添加版本号
            //创建根节点
            XmlElement root = xml.CreateElement("events");

            //创建子节点
            //List<XmlElement> event=new List<XmlElement>();
            for (int i = 0; i < eventData.eventGroup.Count; i++)
            {
                XmlElement element = xml.CreateElement("event");
                element.SetAttribute("id", i.ToString());
                //设置节点的属性
                XmlElement elementTitle = xml.CreateElement("title");
                elementTitle.InnerText = eventData.eventGroup[i].title;
                element.AppendChild(elementTitle);

                XmlElement elementDescribe = xml.CreateElement("describe");
                elementDescribe.InnerText = eventData.eventGroup[i].describe;
                element.AppendChild(elementDescribe);

                XmlElement elementPicture = xml.CreateElement("picture");
                elementPicture.InnerText = eventData.eventGroup[i].picture;
                element.AppendChild(elementPicture);

                if (eventData.eventGroup[i].priority != 0)
                {
                    XmlElement elementPriority = xml.CreateElement("priority");
                    elementPriority.InnerText = eventData.eventGroup[i].priority.ToString();
                    element.AppendChild(elementPriority);
                }

                if (eventData.eventGroup[i].probability != 100)
                {
                    XmlElement elementprobability = xml.CreateElement("probability");
                    elementprobability.InnerText = eventData.eventGroup[i].probability.ToString();
                    if (eventData.eventGroup[i].probabilityAdd != 0)
                    {
                        elementprobability.SetAttribute("Add", eventData.eventGroup[i].probabilityAdd.ToString());
                    }
                    element.AppendChild(elementprobability);
                }

                //添加事件的特质
                if (eventData.eventGroup[i].subEvent || eventData.eventGroup[i].isRepeat || eventData.eventGroup[i].isInvisible || eventData.eventGroup[i].isImportant)
                {
                    XmlElement elementTrait = xml.CreateElement("trait");
                    if (eventData.eventGroup[i].subEvent)
                    {
                        XmlElement elementsubEvent = xml.CreateElement("subEvent");
                        elementTrait.AppendChild(elementsubEvent);
                    }

                    if (eventData.eventGroup[i].isRepeat)
                    {
                        XmlElement elementisRepeat = xml.CreateElement("isRepeat");
                        elementTrait.AppendChild(elementisRepeat);
                    }

                    if (eventData.eventGroup[i].isInvisible)
                    {
                        XmlElement elementisInvisible = xml.CreateElement("isInvisible");
                        elementTrait.AppendChild(elementisInvisible);
                    }

                    if (eventData.eventGroup[i].isImportant)
                    {
                        XmlElement elementisImportant = xml.CreateElement("isImportant");
                        elementTrait.AppendChild(elementisImportant);
                    }

                    element.AppendChild(elementTrait);
                }
                //事件的触发条件
                XmlElement elementConditions = xml.CreateElement("conditions");
                elementConditions.SetAttribute("amout", "1");

                XmlElement elementCondition = xml.CreateElement("condition");
                elementCondition.SetAttribute("sigh", ">=");
                elementCondition.InnerText = "variable";

                elementConditions.AppendChild(elementCondition);
                element.AppendChild(elementConditions);

                //事件的效果
                XmlElement elementEffects = xml.CreateElement("effects");
                elementEffects.SetAttribute("amout", "1");

                XmlElement elementEffect = xml.CreateElement("effect");
                elementEffect.SetAttribute("sigh", "+");
                elementEffect.InnerText = "variable";

                elementEffects.AppendChild(elementEffect);
                element.AppendChild(elementEffects);

                root.AppendChild(element);
            }
            xml.AppendChild(root);
            //最后保存文件
            xml.Save(path);
        }
        */

/*
         string path = Application.dataPath + "/Resources/Xml/News.xml";
    if (!File.Exists(path))
    {
        XmlDocument xml = new XmlDocument();
        XmlDeclaration xmlDeclaration = xml.CreateXmlDeclaration("1.0", "UTF-8", null);//固定格式
        xml.AppendChild(xmlDeclaration);
        XmlElement root = xml.CreateElement("newsgroup");
        for (int i = 0; i < newsData.newsGroup.Count; i++)
        {
            XmlElement element = xml.CreateElement("news");
            element.SetAttribute("id", i.ToString());
            //设置节点的属性
            element.InnerText = newsData.newsGroup[i].content;
            root.AppendChild(element);
        }

        xml.AppendChild(root);
        //最后保存文件
        xml.Save(path);
    }
 */

/*
 string path = Application.dataPath + "/Resources/Xml/Tech.xml";
    if (!File.Exists(path))
    {
        XmlDocument xml = new XmlDocument();
        XmlDeclaration xmlDeclaration = xml.CreateXmlDeclaration("1.0", "UTF-8", null);//固定格式
        xml.AppendChild(xmlDeclaration);
        XmlElement root = xml.CreateElement("techs");
        for (int i = 0; i < TechData.technologyGroup.Count; i++)
        {
            XmlElement element = xml.CreateElement("tech");
            element.SetAttribute("id", i.ToString());
            XmlElement elementTitle = xml.CreateElement("title");
            elementTitle.InnerText = TechData.technologyGroup[i].title;
            element.AppendChild(elementTitle);

            XmlElement elementDescribe = xml.CreateElement("describe");
            elementDescribe.InnerText = TechData.technologyGroup[i].describe;
            element.AppendChild(elementDescribe);

            XmlElement elementPicture = xml.CreateElement("picture");
            elementPicture.InnerText = TechData.technologyGroup[i].picture;
            element.AppendChild(elementPicture);

            XmlElement elementEureka = xml.CreateElement("eureka");
            elementEureka.InnerText = TechData.technologyGroup[i].eureka;
            element.AppendChild(elementEureka);

            XmlElement elementType = xml.CreateElement("type");
            switch (TechData.technologyGroup[i].type)
            {
                case TypeTechnology.gover:
                    elementType.InnerText = "gover";
                    break;
                case TypeTechnology.medicine:
                    elementType.InnerText = "medicine";
                    break;
                case TypeTechnology.media:
                    elementType.InnerText = "media";
                    break;
            }

            if (TechData.technologyGroup[i].father != -1)
            {
                XmlElement elementFather = xml.CreateElement("father");
                elementFather.InnerText = TechData.technologyGroup[i].father.ToString();
                element.AppendChild(elementFather);
            }

            XmlElement elementCost = xml.CreateElement("cost");
            elementCost.InnerText = TechData.technologyGroup[i].cost.ToString();
            element.AppendChild(elementCost);

            XmlElement elementPosx = xml.CreateElement("posx");
            elementPosx.InnerText = TechData.technologyGroup[i].Posx.ToString();
            element.AppendChild(elementPosx);

            XmlElement elementPosy = xml.CreateElement("posy");
            elementPosy.InnerText = TechData.technologyGroup[i].Posy.ToString();
            element.AppendChild(elementPosy);

            root.AppendChild(element);
        }

        xml.AppendChild(root);
        //最后保存文件
        xml.Save(path);
    }
 */
