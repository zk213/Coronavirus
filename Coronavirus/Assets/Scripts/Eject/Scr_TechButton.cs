using UnityEngine;
using UnityEngine.UI;

public class Scr_TechButton : MonoBehaviour
{
    Scr_Tech Tech;
    Scr_Num Value;
    public int LocalIndex;
    public int father;
    public int cost;
    public bool isLock;


    float t = 0;
    bool tAdd = false;
    Color32 colorEmpty = new Color32(255, 255, 255, 0);
    Color colorGrey = new Color(0.5f, 0.5f, 0.5f, 0.5f);

    void Awake()
    {
        Tech = FindObjectOfType<Scr_Tech>();
        Value = FindObjectOfType<Scr_Num>();
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

                    gameObject.transform.Find("TechIcon").transform.Find("TechShadow").GetComponent<Image>().color = Color.Lerp(colorEmpty, colorGrey, t);
                    if (Tech.TechIndex == LocalIndex && !Tech.canUpgrade)
                    {
                        Tech.canUpgrade = true;
                    }
                }
                else
                {
                    gameObject.transform.Find("TechIcon").transform.Find("TechShadow").GetComponent<Image>().color = colorGrey;
                }
            }
            else if (cost <= Value.InfluenceVal)
            {
                gameObject.transform.Find("TechIcon").transform.Find("TechShadow").GetComponent<Image>().color = Color.Lerp(colorEmpty, colorGrey, t);
                if (Tech.TechIndex == LocalIndex && !Tech.canUpgrade)
                {
                    Tech.canUpgrade = true;
                }
            }
            else
            {
                gameObject.transform.Find("TechIcon").transform.Find("TechShadow").GetComponent<Image>().color = colorGrey;
            }
        }
        else
        {
            gameObject.transform.Find("TechIcon").transform.Find("TechShadow").GetComponent<Image>().color = colorEmpty;
        }
    }

    public void TechButtonClick()
    {
        Tech.TechIndex = LocalIndex;
        Tech.TextUpdate = true;
        Tech.canUpgrade = false;
    }
}
