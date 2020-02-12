using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 挂载在parent上
/// </summary>
public class DrawLine : MonoBehaviour
{
    //public Image lineImage;//直线素材  给个红颜色用于识别
    //public Vector2 rectA;//指的是rectTransform.anchoredPosition；直线起点
    //public Vector2 rectB;//直线终点
    public RectTransform point;//辅助显示起点终点位置的小圆形，实际是一张Image，圆形图片给个黄色用于识别
    public RectTransform parent;//直线的父物体
    // Use this for initialization
    void Start()
    {    
        //Debug.Log(GetComponent<RectTransform>().anchoredPosition);
      
    }     

    //划线功能
    public  void DrawStraightLine(Vector2 a, Vector2 b, RectTransform prefab, RectTransform point, Transform parent,bool pointActive=true,int wide=5)
    {
        if (a != b)
        {

            GameObject point1 = Instantiate(point.gameObject, parent);
            GameObject point2 = Instantiate(point.gameObject, parent);
            point1.SetActive(pointActive);
            point2.SetActive(pointActive);
            point1.GetComponent<RectTransform>().anchoredPosition = a;
            point2.GetComponent<RectTransform>().anchoredPosition = b;

            


            float distance = Vector2.Distance(a, b);//计算起点终点两点距离
            float angle = Vector2.SignedAngle(a - b, Vector2.left);//求夹角  计算起点终点的向量和 Vector2.left的夹角

            GameObject go = Instantiate(prefab.gameObject, parent);//克隆预设进行划线
            go.gameObject.SetActive(true);
            go.GetComponent<Image>().color = Color.blue;
            go.GetComponent<RectTransform>().anchoredPosition = (a + b) / 2;
            go.GetComponent<RectTransform>().sizeDelta = new Vector2(distance, wide);
            go.transform.localRotation = Quaternion.AngleAxis(-angle, Vector3.forward);
            //Debug.Log("distance：" + distance + "  angle:" + angle + "  imagePos:" + go.GetComponent<RectTransform>().anchoredPosition);
        }

    }

  
}
