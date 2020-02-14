using UnityEngine;

public class Scr_Color : MonoBehaviour
{
    public Texture2D map;

    int colorNum = 3;

    Color32 m_red = new Color32(237, 28, 36, 255);
    Color32 m_green = new Color32(34, 177, 76, 255);
    Color32 m_blue = new Color32(0, 162, 232, 255);

    Color32[] textureCol;
    int w = 0;
    int h = 0;


    void Start()
    {
        textureCol = map.GetPixels32();
        w = map.width;
        h = map.height;
        for (int a = 0; a < colorNum; a++)
        {
            Texture2D col = new Texture2D(w, h);
            Color32[] Col = new Color32[w * h];
            string name = "";
            Color32 tempColor = new Color32(255, 255, 255, 255);
            switch (a)
            {
                case 0:
                    name = "Red";
                    tempColor = m_red;
                    break;
                case 1:
                    name = "Blue";
                    tempColor = m_blue;
                    break;
                case 2:
                    name = "Green";
                    tempColor = m_green;
                    break;
            }
            for (int i = 0; i < textureCol.Length; i++)
            {
                if (textureCol[i].Equals(tempColor))
                {
                    Col[i] = Color.white;
                }

            }
            col.SetPixels32(Col);
            col.Apply();

            gameObject.transform.Find(name).GetComponent<SpriteRenderer>().sprite = Sprite.Create(col, new Rect(0, 0, col.width, col.height), new Vector2(0, 0));
        }

    }

    // Update is called once per frame
    void Update()
    {

        Vector2 mouseP = new Vector2((int)(Input.mousePosition.x - Screen.width / 2), (int)(Input.mousePosition.y - Screen.height / 2));//获得鼠标的坐标
        if (Input.GetMouseButtonDown(0) && (mouseP.x >= 0 && mouseP.x < w) && (mouseP.y >= 0 && mouseP.y < h))
        {
            Debug.Log(mouseP);

            Color32[] textureCol = map.GetPixels32(); ;
            for (int a = 0; a < colorNum; a++)
            {
                string name = "";
                Color32 tempColor = new Color32(255, 255, 255, 255); ;
                switch (a)
                {
                    case 0:
                        name = "Red";
                        tempColor = m_red;
                        break;
                    case 1:
                        name = "Blue";
                        tempColor = m_blue;
                        break;
                    case 2:
                        name = "Green";
                        tempColor = m_green;
                        break;
                }

                if (textureCol[(int)(mouseP.x + mouseP.y * w)].Equals(tempColor))
                {
                    Debug.Log(name);
                }
            }
        }
    }
}
