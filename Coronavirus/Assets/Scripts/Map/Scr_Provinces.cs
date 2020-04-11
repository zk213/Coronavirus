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

    public int Material = 0;
    public int Personnel = 0;
    public int Bed = 0;
    public int Death = 0;
    public int Cure = 0;
    public int Heavy = 0;
    public int Suspected = 0;

    //public float
    public int TotalPeople = 0;
    public int showTotalPeople = 0;
    public int fakeSuspected = 0;
    public int showSuspected = 0;
    public List<int> People = new List<int>();
    public List<float> DIPTransport = new List<float>();
    public List<float> DPopulation = new List<float>();
    public List<float> DPPTransport = new List<float>();
    public List<float> DMedicine = new List<float>();

    SpriteRenderer spr;
    //PolygonCollider2D pc2;

    float t = 0;
    bool tAdd = false;

    public List<float> IncubationTurn = new List<float>();
    public List<float> MildTurn = new List<float>();
    public List<float> HeavyTurn = new List<float>();
    public List<float> CureTurn = new List<float>();

    public float r0 = 4;


    void Awake()
    {
        provinces = FindObjectOfType<Scr_Color>();
        spr = GetComponent<SpriteRenderer>();
        //pc2 = GetComponent<PolygonCollider2D>();
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
            float InfectionProbability = DIPTransport[IPTransport];
            InfectionProbability *= DPopulation[Population];
            float MedicineTurn = DMedicine[Medicine] + 1;


            float InfectionPeople = 0;
            //潜伏患者
            int turn0 = 0;
            int turn1 = 0;
            for (int i = 4; i >= 0; i--)
            {
                if (i != 0)
                {
                    int tempTurn0 = turn0;
                    InfectionPeople += (People[i] * r0 / 5.0f * InfectionProbability / 100);
                    turn0 = (int)(People[i] * IncubationTurn[0] / 100);
                    turn1 = (int)(People[i] * IncubationTurn[1] / 100);
                    if (i != 4)
                    {

                        if (i != 3)
                        {
                            int turn2 = (int)(People[i] * IncubationTurn[2] / 100);
                            People[i + 2] += turn2;
                            People[i] -= turn2;
                        }
                        else
                        {
                            turn1 += (int)(People[i] * IncubationTurn[2] / 100);
                        }
                        People[i + 1] += turn1;
                    }
                    else
                    {
                        turn1 += (int)(People[i] * IncubationTurn[2] / 100);
                        int turnB = (int)(turn1 * provinces.goToDoc / 100);
                        People[5] += turn1 - turnB;
                        People[10] += turnB;
                    }
                    People[i] += tempTurn0;
                }
                else
                {
                    InfectionPeople += (People[i] * r0 / 5.0f * InfectionProbability / 50);
                    turn1 = (int)(People[i] * IncubationTurn[1] / 100);
                    int turn2 = (int)(People[i] * IncubationTurn[2] / 100);
                    People[i + 2] += turn2;
                    People[i] -= turn2;
                    People[i + 1] += turn1;
                }
                People[i] -= (turn1 + turn0);
                if (turn1 == 0 && People[i] != 0)
                {
                    //此处待定
                    //People[0] += 1;
                    People[i] -= 1;
                    People[i + 1] += 1;
                }
            }
            //疑似未确诊
            turn0 = 0;
            turn1 = 0;
            for (int i = 9; i >= 5; i--)
            {
                int turnA = (int)(People[i] * provinces.goToDoc / 100);
                fakeSuspected += (int)(People[i] * r0 / 5);
                People[i] -= turnA;
                People[i + 5] += turnA;
                if (turnA > 0)
                    if (i != 5)
                    {
                        int tempTurn0 = turn0;
                        InfectionPeople += (People[i] * r0 / 5.0f * InfectionProbability / 100 * provinces.SuspectedInfected);
                        turn0 = (int)(People[i] * MildTurn[0] / 100);
                        turn1 = (int)(People[i] * MildTurn[1] / 100);
                        if (i != 9)
                        {

                            if (i != 8)
                            {
                                int turn2 = (int)(People[i] * MildTurn[2] / 100);
                                People[i + 2] += turn2;
                                People[i] -= turn2;
                            }
                            else
                            {
                                turn1 += (int)(People[i] * MildTurn[2] / 100);
                            }
                            People[i + 1] += turn1;
                        }
                        else
                        {
                            turn1 += (int)(People[i] * MildTurn[2] / 100);
                            int turnB = (int)(turn1 * provinces.goToDoc / 100);
                            People[15] += turn1 - turnB;
                            People[20] += turnB;
                        }
                        People[i] -= turn1;
                        People[i] -= turn0;
                        People[i] += tempTurn0;
                    }
                    else
                    {
                        InfectionPeople += (People[i] * r0 / 5.0f * InfectionProbability / 100 * provinces.SuspectedInfected);
                        turn1 = (int)(People[i] * MildTurn[1] / 100);
                        int turn2 = (int)(People[i] * MildTurn[2] / 100);
                        People[i + 2] += turn2;
                        People[i] -= turn2;
                        People[i + 1] += turn1;
                        People[i] -= turn1;
                    }
                if (turn1 == 0 && People[i] != 0)
                {
                    //此处待定
                    //People[0] += 1;
                    if (i != 9)
                    {
                        People[i] -= 1;
                        People[i + 1] += 1;
                    }
                    else
                    {
                        People[i] -= 1;
                        People[21] += 1;
                    }
                }
            }

            //轻症确诊
            turn0 = 0;
            turn1 = 0;
            for (int i = 14; i >= 10; i--)
            {
                fakeSuspected += (int)(People[i] * r0 / 10);
                if (i != 10)
                {
                    int tempTurn0 = turn0;
                    InfectionPeople += (People[i] * r0 / 5.0f * InfectionProbability / 100 * provinces.QuarantineInfected);
                    turn0 = (int)(People[i] * MildTurn[0] / 100);
                    turn1 = (int)(People[i] * MildTurn[1] / 100);
                    if (i != 14)
                    {

                        if (i != 13)
                        {
                            int turn2 = (int)(People[i] * MildTurn[2] / 100);
                            People[i + 2] += turn2;
                            People[i] -= turn2;
                        }
                        else
                        {
                            turn1 += (int)(People[i] * MildTurn[2] / 100);
                        }
                        People[i + 1] += turn1;
                    }
                    else
                    {
                        turn1 += (int)(People[i] * MildTurn[2] / 100);
                        int turnB = (int)(turn1 * Mathf.Clamp((CureTurn[1] + 5 * MedicineTurn) / 100, 0, 1));
                        People[20] += turn1 - turnB;
                        People[25] += turnB;
                    }
                    People[i] -= turn1;
                    People[i] -= turn0;
                    People[i] += tempTurn0;
                }
                else
                {
                    InfectionPeople += (People[i] * r0 / 5.0f * InfectionProbability / 100 * provinces.QuarantineInfected);
                    turn1 = (int)(People[i] * MildTurn[1] / 100);
                    int turn2 = (int)(People[i] * MildTurn[2] / 100);
                    People[i + 2] += turn2;
                    People[i] -= turn2;
                    People[i + 1] += turn1;
                    People[i] -= turn1;
                }
                if (turn1 == 0 && People[i] != 0)
                {
                    //此处待定
                    //People[0] += 1;
                    if (i != 15)
                    {
                        People[i] -= 1;
                        People[i + 1] += 1;
                    }
                    else
                    {
                        People[i] -= 1;
                        People[25] += 1;
                    }
                }
            }


            //疑似重症
            turn0 = 0;
            turn1 = 0;
            for (int i = 19; i >= 15; i--)
            {
                fakeSuspected += (int)(People[i] * r0 / 10);
                int turnA = (int)(People[i] * provinces.goToDoc / 100);
                People[i] -= turnA;
                People[i + 5] += turnA;
                if (i != 15)
                {
                    int tempTurn0 = turn0;
                    InfectionPeople += (People[i] * r0 / 5.0f * InfectionProbability / 100 * provinces.SuspectedInfected);
                    turn0 = (int)(People[i] * Mathf.Clamp(HeavyTurn[0] / 100 * MedicineTurn, 0, 1));
                    turn1 = (int)(People[i] * HeavyTurn[1] / 100);
                    if (i != 19)
                    {

                        if (i != 18)
                        {
                            int turn2 = (int)(People[i] * HeavyTurn[2] / 100);
                            People[i + 2] += turn2;
                            People[i] -= turn2;
                        }
                        else
                        {
                            turn1 += (int)(People[i] * HeavyTurn[2] / 100);
                        }
                        People[i + 1] += turn1;
                    }
                    else
                    {
                        turn1 += (int)(People[i] * HeavyTurn[2] / 100);
                        Death += turn1;
                    }
                    People[i] -= turn1;
                    People[i] -= turn0;
                    People[i] += tempTurn0;
                }
                else
                {
                    InfectionPeople += (People[i] * r0 / 5.0f * InfectionProbability / 100 * provinces.SuspectedInfected);
                    turn1 = (int)(People[i] * HeavyTurn[1] / 100);
                    int turn2 = (int)(People[i] * HeavyTurn[2] / 100);
                    turn0 = (int)(People[i] * Mathf.Clamp(HeavyTurn[0] / 100 * MedicineTurn, 0, 1));
                    People[i + 2] += turn2;
                    People[i] -= turn2;
                    People[i + 1] += turn1;
                    People[i] -= turn1;
                    People[i] -= turn0;
                    People[25] += turn0;
                }
                if (turn1 == 0 && People[i] != 0)
                {
                    //此处待定
                    //People[0] += 1;

                    People[i] -= 1;
                    People[i + 5] += 1;


                }
            }

            //重症确诊
            turn0 = 0;
            turn1 = 0;
            for (int i = 24; i >= 20; i--)
            {
                fakeSuspected += (int)(People[i] * r0 / 20);
                if (i != 20)
                {
                    int tempTurn0 = turn0;
                    InfectionPeople += (People[i] * r0 / 5.0f * InfectionProbability / 100 * provinces.QuarantineInfected);
                    turn0 = (int)(People[i] * Mathf.Clamp(HeavyTurn[0] / 100 * MedicineTurn, 0, 1));
                    turn1 = (int)(People[i] * HeavyTurn[1] / 100);
                    if (i != 24)
                    {

                        if (i != 23)
                        {
                            int turn2 = (int)(People[i] * HeavyTurn[2] / 100);
                            People[i + 2] += turn2;
                            People[i] -= turn2;
                        }
                        else
                        {
                            turn1 += (int)(People[i] * HeavyTurn[2] / 100);
                        }
                        People[i + 1] += turn1;
                    }
                    else
                    {
                        turn1 += (int)(People[i] * HeavyTurn[2] / 100);
                        Death += turn1;
                    }
                    People[i] -= turn1;
                    People[i] -= turn0;
                    People[i] += tempTurn0;
                }
                else
                {
                    InfectionPeople += (People[i] * r0 / 5.0f * InfectionProbability / 100 * provinces.QuarantineInfected);
                    turn1 = (int)(People[i] * HeavyTurn[1] / 100);
                    int turn2 = (int)(People[i] * HeavyTurn[2] / 100);
                    turn0 = (int)(People[i] * Mathf.Clamp(HeavyTurn[0] / 100 * MedicineTurn, 0, 1));
                    People[i + 2] += turn2;
                    People[i] -= turn2;
                    People[i + 1] += turn1;
                    People[i] -= turn1;
                    People[i] -= turn0;
                    People[25] += turn0;
                }
                if (turn1 == 0 && People[i] != 0)
                {
                    //此处待定
                    //People[0] += 1;

                    People[i] -= 1;
                    People[26] += 1;

                }
            }

            //治愈1阶段
            turn0 = 0;
            turn1 = 0;
            for (int i = 29; i >= 25; i--)
            {

                if (i != 25)
                {
                    int tempTurn0 = turn0;
                    InfectionPeople += (People[i] * r0 / 5.0f * InfectionProbability / 100 * provinces.QuarantineInfected);
                    turn0 = (int)(People[i] * CureTurn[0] / 100);
                    turn1 = (int)(People[i] * CureTurn[1] / 100);
                    if (i != 29)
                    {

                        if (i != 28)
                        {
                            int turn2 = (int)(People[i] * CureTurn[2] / 100);
                            People[i + 2] += turn2;
                            People[i] -= turn2;
                        }
                        else
                        {
                            turn1 += (int)(People[i] * CureTurn[2] / 100);
                        }
                        People[i + 1] += turn1;
                    }
                    else
                    {
                        turn1 += (int)(People[i] * CureTurn[2] / 100);
                        People[30] += turn1;
                    }
                    People[i] -= turn1;
                    People[i] -= turn0;
                    People[i] += tempTurn0;
                }
                else
                {
                    InfectionPeople += (People[i] * r0 / 5.0f * InfectionProbability / 100 * provinces.QuarantineInfected);
                    turn1 = (int)(People[i] * CureTurn[1] / 100);
                    int turn2 = (int)(People[i] * CureTurn[2] / 100);
                    turn0 = (int)(People[i] * Mathf.Clamp(HeavyTurn[0] / 100 / MedicineTurn, 0, 1));
                    People[i + 2] += turn2;
                    People[i] -= turn2;
                    People[i + 1] += turn1;
                    People[i] -= turn1;
                    People[i] -= turn0;
                    People[20] += turn0;
                }
                if (turn1 == 0 && People[i] != 0)
                {
                    //此处待定
                    //People[0] += 1;

                    People[i] -= 1;
                    People[i + 1] += 1;

                }
            }

            //治愈2阶段
            turn0 = 0;
            turn1 = 0;
            for (int i = 34; i >= 30; i--)
            {
                if (i != 30)
                {
                    int tempTurn0 = turn0;
                    InfectionPeople += (People[i] * r0 / 5.0f * InfectionProbability / 100 * provinces.QuarantineInfected);
                    turn0 = (int)(People[i] * CureTurn[0] / 100);
                    turn1 = (int)(People[i] * CureTurn[1] / 100);
                    if (i != 34)
                    {

                        if (i != 33)
                        {
                            int turn2 = (int)(People[i] * CureTurn[2] / 100);
                            People[i + 2] += turn2;
                            People[i] -= turn2;
                        }
                        else
                        {
                            turn1 += (int)(People[i] * CureTurn[2] / 100);
                        }
                        People[i + 1] += turn1;
                    }
                    else
                    {
                        turn1 += (int)(People[i] * CureTurn[2] / 100);
                        Cure += turn1;
                    }
                    People[i] -= turn1;
                    People[i] -= turn0;
                    People[i] += tempTurn0;
                }
                else
                {
                    InfectionPeople += (People[i] * r0 / 5.0f * InfectionProbability / 100 * provinces.QuarantineInfected);
                    turn1 = (int)(People[i] * CureTurn[1] / 100);
                    int turn2 = (int)(People[i] * CureTurn[2] / 100);
                    People[i + 2] += turn2;
                    People[i] -= turn2;
                    People[i + 1] += turn1;
                    People[i] -= turn1;
                }
                if (turn1 == 0 && People[i] != 0)
                {
                    //此处待定
                    //People[0] += 1;
                    if (i != 34)
                    {
                        People[i] -= 1;
                        People[i + 1] += 1;
                    }
                    else
                    {
                        People[i] -= 1;
                        Cure += 1;
                    }
                }
            }

            People[0] += (int)InfectionPeople;

        }

        if (provinceIndex != 18)
        {
            float MoveProbability = DPPTransport[PPTransport];

            if (provinces.HuBeiPeople > 10)
            {
                int HuBeiPeople = Mathf.Clamp(provinces.HuBeiPeople, 0, (int)(20 * Random.Range(0, MoveProbability) / 20.0f));
                People[0] += (int)(HuBeiPeople * r0 / 5.0f);
            }
        }
        TotalPeople = 0;
        Suspected = 0;
        Heavy = 0;
        showTotalPeople = 0;
        for (int i = 0; i < People.Count; i++)
        {
            TotalPeople += People[i];

            if (i >= 10 && (i < 15 || i >= 20))
            {
                showTotalPeople += People[i];
            }


            if ((i >= 5 && i < 10) || (i >= 15 && i < 20))
            {
                Suspected += People[i];
            }
            if (i >= 20 && i < 25)
            {
                Heavy += People[i];
            }
        }
        showSuspected = Suspected + fakeSuspected;
        int digit = showTotalPeople.ToString().Length;
        switch (digit)
        {
            case 1:
                if (showTotalPeople == 0)
                {
                    myColor = Color.white;
                    if (showSuspected != 0)
                    {
                        myColor = new Color32(252, 235, 207, 255);
                    }
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
                if (showTotalPeople < 500)
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
            for (int i = 0; i < 10; i++)
            {
                HuBeiPeople += People[i];
            }
            provinces.HuBeiPeople = HuBeiPeople;
        }
    }
    public void PeopleCheck()
    {
        TotalPeople = 0;
        showTotalPeople = 0;
        for (int i = 0; i < People.Count; i++)
        {
            TotalPeople += People[i];

            if (i >= 10 && (i < 15 || i >= 20))
            {
                showTotalPeople += People[i];
            }


            if ((i >= 5 && i < 10) || (i >= 15 && i < 20))
            {
                Suspected += People[i];
            }
            if (i >= 20 && i < 25)
            {
                Heavy += People[i];
            }
        }
        TotalPeople += (Death + Cure);
        showSuspected = Suspected + fakeSuspected;
        int digit = showTotalPeople.ToString().Length;
        switch (digit)
        {
            case 1:
                if (showTotalPeople == 0)
                {
                    myColor = Color.white;
                    if (showSuspected != 0)
                    {
                        myColor = new Color32(252, 235, 207, 255);
                    }
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
                if (showTotalPeople < 500)
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
