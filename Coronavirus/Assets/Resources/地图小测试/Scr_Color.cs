using System.Collections.Generic;
using System.IO;
using System.Xml;
using UnityEngine;
using UnityEngine.UI;

public class Scr_Color : MonoBehaviour
{
    public Texture2D map;
    public Text provincesName;
    public Object prefab;
    public GameObject CCamera;
    float MoveSpeed;

    public float correctX = 1;//0.93f;
    public float correctY = 1;//0.93f;

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
        MoveSpeed = CCamera.GetComponent<Scr_Camera>().speed;
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
            }
        }

        for (int a = 0; a < colorNum; a++)
        {
            Provinces.Add((GameObject)Instantiate(prefab, transform.position, transform.rotation));
            Provinces[a].transform.SetParent(gameObject.transform);
            Provinces[a].transform.name = nameS[a];
            Provinces[a].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
            Provinces[a].GetComponent<Scr_Provinces>().provinceIndex = a;
            Provinces[a].GetComponent<Scr_Provinces>().provinceName = nameS[a];
            if (!Directory.Exists(Application.persistentDataPath + "/provinces"))
            {
                Directory.CreateDirectory(Application.persistentDataPath + "/provinces");
            }
            string path = Application.persistentDataPath + "/provinces/" + a.ToString() + ".png";
            if (!File.Exists(path))
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
                var bytes = col[a].EncodeToPNG();

                File.WriteAllBytes(path, bytes);
                Provinces[a].GetComponent<SpriteRenderer>().sprite = Sprite.Create(col[a], new Rect(0, 0, col[a].width, col[a].height), new Vector2(0, 0));
            }
            else
            {
                Texture2D colTexture = new Texture2D(w, h);
                FileStream files = new FileStream(path, FileMode.Open);
                //新建比特流对象
                byte[] imgByte = new byte[files.Length];
                //将文件写入对应比特流对象
                files.Read(imgByte, 0, imgByte.Length);
                //关闭文件
                files.Close();
                colTexture.LoadImage(imgByte);
                Provinces[a].GetComponent<SpriteRenderer>().sprite = Sprite.Create(colTexture, new Rect(0, 0, colTexture.width, colTexture.height), new Vector2(0, 0));
            }



        }

    }

    // Update is called once per frame
    void Update()
    {// + CCamera.transform.position.x * MoveSpeed * 20
        Debug.Log(Screen.width + "," + Screen.height);
        switch (((float)Screen.width / (float)Screen.height))
        {
            case (16f / 9f):
                correctX = 7.00386f * Mathf.Pow(10, -7) * Mathf.Pow(Screen.width, 2) - 0.00299f * Screen.width + 4.10058f;
                correctY = 2.19997f * Mathf.Pow(10, -6) * Mathf.Pow(Screen.height, 2) - 0.00530f * Screen.height + 4.10270f;
                break;
            case (5f / 4f):
                if (Screen.width == 800)
                {
                    correctX = 1.576f;
                    correctY = 1.585f;
                }
                if (Screen.width == 1280)
                {
                    correctX = 0.9883f;
                    correctY = 0.9894f;
                }
                break;
            case (4f / 3f):
                correctX = -8.27256f * Mathf.Pow(10, -4) * Screen.width + 2.14126f;
                correctY = -0.00110f * Screen.height + 2.14183f;
                break;
            case (16f / 10f):
                correctX = 4.25843f * Mathf.Pow(10, -7) * Mathf.Pow(Screen.width, 2) - 0.00202f * Screen.width + 3.14612f;
                correctY = 1.02948f * Mathf.Pow(10, -6) * Mathf.Pow(Screen.height, 2) - 0.00310f * Screen.height + 3.08079f;
                break;
            default:

                break;
        }




        Vector2 mouseP = new Vector2((int)((Input.mousePosition.x - Screen.width / 2) * correctX * Camera.main.orthographicSize / 5 + CCamera.transform.position.x * MoveSpeed * 20), (int)((Input.mousePosition.y - Screen.height / 2) * correctY * Camera.main.orthographicSize / 5 + CCamera.transform.position.y * MoveSpeed * 20));//获得鼠标的坐标
        if (Input.GetMouseButtonDown(0))
        {
            /*
             float corX = 591 / mouseP.x;
            float corY = 279 / mouseP.y;
            Debug.Log(string.Format("{0:F6}", corX) + "," + string.Format("{0:F6}", corY));
             */
            Debug.Log(mouseP);
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



            if (!hasHit)
            {
                provincesName.text = "";
                thisIndex = -1;
            }
        }
    }
}
