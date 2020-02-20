using System.Collections.Generic;
using UnityEngine;

public class Scr_Provinces : MonoBehaviour
{
    Scr_Color provinces;
    public string provinceName;
    public int provinceIndex;
    public bool isMe = false;
    public Color32 myColor = Color.white;

    public int PPTransport = 0;
    public int IPTransport = 0;
    //public float
    public int TotalPeople = 0;
    public List<int> People = new List<int>();

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
                Color32 grayColor = new Color32(myColor.r, myColor.g, myColor.b, 140);
                spr.color = Color.Lerp(grayColor, myColor, t);
            }
            else
            {
                spr.color = myColor;
                isMe = false;
            }
        }
    }

    public void PeopleTurn()
    {
        int tempPeople = 0;

        tempPeople += (People[1] * Random.Range(0, 50) / 100);
        int turn1 = (People[0] * Random.Range(0, 80) / 100);
        People[1] += turn1;
        People[0] -= turn1;
        tempPeople += (People[0] * Random.Range(0, 50) / 100);
        People[0] += tempPeople;
        TotalPeople = 0;
        for (int i = 0; i < People.Count; i++)
        {
            TotalPeople += People[i];
        }
        int digit = TotalPeople.ToString().Length;
        switch (digit)
        {
            case 1:
                if (TotalPeople == 0)
                {
                    myColor = Color.white;
                }
                else
                {
                    myColor = new Color32(252, 235, 207, 255);
                }
                break;
            case 2:
                myColor = new Color32(245, 158, 131, 255);
                break;
            case 3:
                if (TotalPeople < 500)
                {
                    myColor = new Color32(228, 90, 79, 255);
                }
                else
                {
                    myColor = new Color32(203, 42, 47, 255);
                }
                break;
            case 4:
                myColor = new Color32(129, 28, 36, 255);
                break;
            default:
                myColor = new Color32(79, 9, 13, 255);
                break;
        }
        spr.color = myColor;
    }
}
