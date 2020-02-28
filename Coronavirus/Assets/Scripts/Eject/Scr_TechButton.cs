using UnityEngine;
using UnityEngine.UI;

public class Scr_TechButton : MonoBehaviour
{
    Scr_Tech Tech;
    Scr_Num Value;
    Image Base;
    public int LocalIndex;
    public int father;
    public int cost;
    public bool isLock;

    float t = 0;
    bool tAdd = false;

    void Awake()
    {
        Tech = FindObjectOfType<Scr_Tech>();
        Value = FindObjectOfType<Scr_Num>();
        Base = GetComponent<Image>();
        isLock = true;
    }

    // Update is called once per frame
    void Update()
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

        if (isLock)
        {
            if (father != -1)//如果他不是起点技术
            {


                if (cost <= Value.InfluenceVal && !Tech.Tech[father].GetComponent<Scr_TechButton>().isLock)
                {
                    Base.color = Color.Lerp(Color.gray, Color.white, t);
                    if (Tech.TechIndex == LocalIndex && !Tech.canUpgrade)
                    {
                        Tech.canUpgrade = true;
                    }
                }
                else
                {
                    Base.color = Color.grey;
                }
            }
            else if (cost <= Value.InfluenceVal)
            {
                Base.color = Color.Lerp(Color.gray, Color.white, t);
                if (Tech.TechIndex == LocalIndex && !Tech.canUpgrade)
                {
                    Tech.canUpgrade = true;
                }
            }
            else
            {
                Base.color = Color.grey;
            }
        }
        else
        {
            Base.color = Color.white;
        }
    }

    public void TechButtonClick()
    {
        Tech.TechIndex = LocalIndex;
        Tech.TextUpdate = true;
        Tech.canUpgrade = false;
    }
}
