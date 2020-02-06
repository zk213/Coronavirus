using UnityEngine;
using UnityEngine.UI;

public class Scr_News : MonoBehaviour
{
    //ScrollRect rect;
    public Text scrollNews1;
    public Text scrollNews2;

    Scr_Event Event;
    NewsData newsData;

    public float speed = 250;
    public float iniPosx = 1111;
    public float finPosx = -1123;
    public float midPosx = -870;

    bool activeNews1;
    bool activeNews2;
    void Awake()
    {
        Event = FindObjectOfType<Scr_Event>();
        newsData = Resources.Load<NewsData>("ScriptableObject/News");

        activeNews1 = false;
        activeNews2 = false;
        //rect = this.GetComponent<ScrollRect>();

    }

    void Update()

    {
        //新闻板块的回调
        if (scrollNews1.GetComponent<RectTransform>().anchoredPosition.x <= finPosx)
        {
            scrollNews1.GetComponent<RectTransform>().anchoredPosition = new Vector2(iniPosx, scrollNews1.GetComponent<RectTransform>().anchoredPosition.y);
            activeNews1 = false;
        }
        if (scrollNews2.GetComponent<RectTransform>().anchoredPosition.x <= finPosx)
        {
            scrollNews2.GetComponent<RectTransform>().anchoredPosition = new Vector2(iniPosx, scrollNews1.GetComponent<RectTransform>().anchoredPosition.y);
            activeNews2 = false;
        }

        //新闻板块的轮播
        if (scrollNews1.GetComponent<RectTransform>().anchoredPosition.x <= midPosx && !activeNews2)
        {
            activeNews2 = true;
        }
        if (scrollNews2.GetComponent<RectTransform>().anchoredPosition.x <= midPosx && !activeNews1)
        {
            activeNews1 = true;
        }

        //新闻板块的移动
        if (activeNews1)
        {
            scrollNews1.GetComponent<RectTransform>().anchoredPosition -= new Vector2(speed * Time.deltaTime, 0);
        }
        if (activeNews2)
        {
            scrollNews2.GetComponent<RectTransform>().anchoredPosition -= new Vector2(speed * Time.deltaTime, 0);
        }

        //rect.horizontalNormalizedPosition += speed * Time.deltaTime;

    }
}
