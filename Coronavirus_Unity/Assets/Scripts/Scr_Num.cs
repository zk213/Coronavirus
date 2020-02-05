using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scr_Num : MonoBehaviour
{
    public Text InfluenceNum;
    public Text CohesionNum;

    public float InfluenceVal;
    public float CohesionVal;

    Scr_Mode Mode;

    void Awake()
    {
        Mode = FindObjectOfType<Scr_Mode>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Mode.gameMode == Scr_Mode.GameMode.Normal)
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
    }
}
