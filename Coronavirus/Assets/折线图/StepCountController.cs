
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 生成竖轴分段
/// </summary>
public class StepCountController : MonoBehaviour
{
    float length;
    Vector2 sizeDelta;
    public RectTransform lineStepPrefab;
    public static int stepspace = 0;

    private void Start()
    {
        
    }
    private void Update()
    {
            
    }

    // 分段法  根据span来分段数
    /// <summary>
    /// 
    /// </summary>
    /// <param name="span">段数</param>
    /// <param name="steps">每段代表多少</param>
    public  void SetStepCount(int span,int steps=10)
    {
        sizeDelta = GetComponent<RectTransform>().sizeDelta;
        length = sizeDelta.y;//获取长边长度

        //每段长度       
        int temp1 = (int)length / (span+1);
        stepspace = temp1;
        for (int i = 0; i < span; i++)
        {
                GameObject count = Instantiate(lineStepPrefab.gameObject, transform);
                count.SetActive(true);
                count.GetComponent<RectTransform>().sizeDelta = new Vector2(20, 10);
                count.GetComponent<RectTransform>().anchoredPosition =
                    new Vector2(5,  -sizeDelta.y/2+ temp1 * (i + 1));

                count.GetComponentInChildren<Text>().text =
                  ((i+1)* steps).ToString();//一步代表10
        }
    }

   
}
