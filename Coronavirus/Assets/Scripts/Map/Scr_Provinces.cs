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
    public int Population = 0;
    public int Medicine = 0;
    //public float
    public int TotalPeople = 0;
    public List<int> People = new List<int>();

    SpriteRenderer spr;

    float t = 0;
    bool tAdd = false;

    int IncubationTurn0 = 10;
    int IncubationTurn1 = 60;
    int IncubationTurn2 = 10;

    int r0 = 3;


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

        if (TotalPeople != 0)
        {
            int InfectionProbability = 0;
            switch (IPTransport)
            {
                case 0:
                    InfectionProbability = 0;
                    break;
                case 1:
                    InfectionProbability = 5;
                    break;
                case 2:
                    InfectionProbability = 10;
                    break;
                case 3:
                    InfectionProbability = 15;
                    break;
                case 4:
                    InfectionProbability = 20;
                    break;
                case 5:
                    InfectionProbability = 25;
                    break;
                default:
                    InfectionProbability = 0;
                    break;
            }

            int InfectionPeople = 0;
            for (int i = 4; i >= 0; i--)
            {
                if (i != 0)
                {
                    InfectionPeople += People[i] * r0 * Random.Range(0, InfectionProbability) / 100;
                    int turn0 = People[i] * Random.Range(0, IncubationTurn0) / 100;
                    if (i != 4)
                    {
                        int turn1 = People[i] * Random.Range(0, IncubationTurn1) / 100;
                        if (i != 3)
                        {
                            int turn2 = People[i] * Random.Range(0, IncubationTurn2) / 100;
                            People[i + 2] += turn2;
                            People[i] -= turn2;
                        }
                        People[i + 1] += turn1;
                        People[i] -= turn1;
                    }
                    People[i - 1] += turn0;
                    People[i] -= turn0;
                }
                else
                {
                    InfectionPeople += People[i] * r0 * Random.Range(0, InfectionProbability) / 50;
                    int turn1 = People[i] * Random.Range(0, IncubationTurn1) / 100;
                    int turn2 = People[i] * Random.Range(0, IncubationTurn2) / 100;
                    People[i + 2] += turn2;
                    People[i] -= turn2;
                    People[i + 1] += turn1;
                    People[i] -= turn1;
                }


            }
            People[0] += InfectionPeople;


        }

        if (provinceIndex != 18)
        {
            int MoveProbability = 0;
            switch (PPTransport)
            {
                case 0:
                    MoveProbability = 0;
                    break;
                case 1:
                    MoveProbability = 2;
                    break;
                case 2:
                    MoveProbability = 3;
                    break;
                case 3:
                    MoveProbability = 4;
                    break;
                case 4:
                    MoveProbability = 5;
                    break;
                case 5:
                    MoveProbability = 6;
                    break;
                default:
                    MoveProbability = 0;
                    break;
            }
            if (provinces.HuBeiPeople > 10)
            {
                int HuBeiPeople = Mathf.Clamp(provinces.HuBeiPeople, 0, 100);
                People[0] += HuBeiPeople * r0 * Random.Range(0, MoveProbability) / 100;
            }
        }
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
        if (provinceIndex == 18)
        {
            int HuBeiPeople = 0;
            for (int i = 0; i < 5; i++)
            {
                HuBeiPeople += People[i];
            }
            provinces.HuBeiPeople = HuBeiPeople;
        }
    }
}
