using UnityEngine;

public class Scr_Provinces : MonoBehaviour
{
    Scr_Color provinces;
    public string provinceName;
    public int provinceIndex;
    public bool isMe = false;
    public Color32 myColor = Color.white;

    SpriteRenderer spr;

    float t = 0;
    bool tAdd = false;

    void Awake()
    {
        provinces = FindObjectOfType<Scr_Color>();
        spr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (isMe)
        {
            if (tAdd)
            {
                t += Time.deltaTime;
                if (t >= 1)
                {
                    t = 1;
                    tAdd = !tAdd;
                }
            }
            else
            {
                t -= Time.deltaTime;
                if (t <= 0)
                {
                    t = 0;
                    tAdd = !tAdd;
                }
            }
            if (provinces.thisIndex == provinceIndex)
            {
                spr.color = Color.Lerp(Color.gray, myColor, t);
            }
            else
            {
                spr.color = myColor;
                isMe = false;
            }
        }
    }
}
