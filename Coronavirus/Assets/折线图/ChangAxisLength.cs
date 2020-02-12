using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangAxisLength : MonoBehaviour {
  
    public RectTransform center;//中心点位置
    public RectTransform target;//图片做成的轴
    Vector2 initPos;
    Vector2 initSize;
    // Use this for initialization
    void Start()
    {
        initPos = target.anchoredPosition;
        initSize = target.sizeDelta;       
    }

    public bool isX = true;//是则改变横轴方向长度，否则改变竖直方向长度

    public void ChangAxis( float width = 1000, float height = 1000, bool isx = true)
    {
        isX = isx;
        if (isX)
        {
            WidthChage(width,height);
        }
        else
        {
            HeightChage(width, height);
        }
    }



    /// <summary>
    /// 修改X轴长度，必须先调用再画图
    /// </summary>
    void WidthChage(float width,float height)
    {
        target.anchoredPosition = new Vector2(center.anchoredPosition.x + width / 2, center.anchoredPosition.y + height / 2);
        target.sizeDelta = new Vector2(width, height);
        target.GetChild(0).GetComponent<RectTransform>().anchoredPosition = new Vector2(0, center.anchoredPosition.y - target.anchoredPosition.y);
        target.GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(width, 5);
    }

    /// <summary>
    /// 修改Y轴长度，必须先调用再画图
    /// </summary>
    void HeightChage(float width, float height)
    {
        target.anchoredPosition = new Vector2(center.anchoredPosition.x + width / 2, center.anchoredPosition.y + height / 2);
        target.sizeDelta = new Vector2(width, height);
        target.GetChild(0).GetComponent<RectTransform>().anchoredPosition = new Vector2(center.anchoredPosition.x - target.anchoredPosition.x, 0);
        target.GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(5, height);
    }
}
