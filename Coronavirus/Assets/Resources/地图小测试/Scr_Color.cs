using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.UI;

public class Scr_Color : MonoBehaviour
{
    public Texture2D map;
    public Text provincesName;
    public Object prefab;

    public int thisIndex;

    int colorNum = 3;

    List<Color32> colorList = new List<Color32>();
    List<GameObject> Provinces = new List<GameObject>();

    Color32[] textureCol;
    List<Texture2D> col = new List<Texture2D>();
    List<string> nameS = new List<string>();
    int w = 0;
    int h = 0;


    void Start()
    {
        provincesName.text = "";
        textureCol = map.GetPixels32();
        w = map.width;
        h = map.height;
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(Application.dataPath + "/Resources/Xml/Provinces.xml");
        XmlElement xmlNode = xmlDoc.DocumentElement;
        foreach (XmlNode elements in xmlNode)
        {
            if (elements == null)
                continue;
            if (elements.LocalName == "provincesAmount")
            {
                int.TryParse(elements.InnerText, out colorNum);
            }
            if (elements.LocalName == "province")
            {
                int.TryParse(elements.Attributes["r"].Value, out int r);
                int.TryParse(elements.Attributes["g"].Value, out int g);
                int.TryParse(elements.Attributes["b"].Value, out int b);
                colorList.Add(new Color32((byte)r, (byte)g, (byte)b, 255));
                nameS.Add(elements.InnerText);
                //Debug.Log(r + "+" + g + "+" + b);
            }
        }
        /*
         Debug.Log(textureCol[1]);
        Debug.Log(colorList[0]);
        Debug.Log(textureCol[1].Equals(colorList[0]));
         */

        for (int a = 0; a < colorNum; a++)
        {
            col.Add(new Texture2D(w, h));
            Color32[] Col = new Color32[w * h];
            for (int i = 0; i < textureCol.Length; i++)
            {
                if (textureCol[i].Equals(colorList[a]))
                {
                    Col[i] = Color.white;
                }

            }
            col[a].SetPixels32(Col);
            col[a].Apply();
            Provinces.Add((GameObject)Instantiate(prefab, transform.position, transform.rotation));
            Provinces[a].transform.SetParent(gameObject.transform);
            Provinces[a].transform.name = nameS[a];
            Provinces[a].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
            Provinces[a].GetComponent<Scr_Provinces>().provinceIndex = a;
            Provinces[a].GetComponent<Scr_Provinces>().provinceName = nameS[a];

            Provinces[a].GetComponent<SpriteRenderer>().sprite = Sprite.Create(col[a], new Rect(0, 0, col[a].width, col[a].height), new Vector2(0, 0));

        }
    }

    // Update is called once per frame
    void Update()
    {

        Vector2 mouseP = new Vector2((int)((Input.mousePosition.x - Screen.width / 2) * 0.93), (int)((Input.mousePosition.y - Screen.height / 2) * 0.93));//获得鼠标的坐标
        if (Input.GetMouseButtonDown(0))
        {
            bool hasHit = false;

            if ((mouseP.x >= 0 && mouseP.x < w) && (mouseP.y >= 0 && mouseP.y < h))
            {
                Color32[] textureCol = map.GetPixels32(); ;
                for (int a = 0; a < colorNum; a++)
                {


                    if (textureCol[(int)(mouseP.x + mouseP.y * w)].Equals(colorList[a]))
                    {
                        hasHit = true;
                        provincesName.text = nameS[a];
                        thisIndex = a;
                        Provinces[a].GetComponent<Scr_Provinces>().isMe = true;
                    }
                }
            }

            //Debug.Log(mouseP);


            if (!hasHit)
            {
                provincesName.text = "";
                thisIndex = -1;
            }
        }
    }
}
