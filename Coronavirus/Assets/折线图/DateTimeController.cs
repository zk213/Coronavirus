using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


/// <summary>
/// 横轴的处理
/// </summary>
public class DateTimeController : MonoBehaviour {


    float length;
     Vector2 anchoredPosition;
    Vector2 sizeDelta;
    [SerializeField] RectTransform center;//坐标轴起点
    [SerializeField] RectTransform lineImageTime;
    [SerializeField] RectTransform lineImage;
    bool XBig;//x轴向是长边
   

 


    public static int timespace=0;//每天间隔长度数值
    int acitiveTimespace ;//显示时间间隔（每五天显示一次日期）
                          //绘制近七天的坐标轴
    public void SetCount1()
    {
        sizeDelta = GetComponent<RectTransform>().sizeDelta;
        length =  sizeDelta.x;//获取横轴长边长度
     
        //每段长度       
        int temp1 = (int)length / 8;
        timespace = temp1;
        for (int i = 0; i < 7; i++)
        {          
                GameObject count = Instantiate(lineImageTime.gameObject, transform);
                count.SetActive(true);
                count.GetComponent<RectTransform>().sizeDelta =new Vector2(10,20);
                count.GetComponent<RectTransform>().anchoredPosition =
                    new Vector2(-sizeDelta.x/2 + temp1 * (i), 5);

                //count.GetComponentInChildren<Text>().text = 
                //    (DateTime.Now.AddDays(-6+i).Month>9?
                //    DateTime.Now.AddDays(-6+i).Month.ToString(): 
                //    ("0"+DateTime.Now.AddDays(-6+i).Month.ToString()))
                //    +"-"+ (DateTime.Now.AddDays(-6+i).Day > 9 ?
                //    DateTime.Now.AddDays(-6+i).Day.ToString() : 
                //    ("0" + DateTime.Now.AddDays(-6+i).Day.ToString()));

            count.GetComponentInChildren<Text>().text = DateTime.Now.AddDays(-6 + i).Year.ToString()+
                   (DateTime.Now.AddDays(-6 + i).Month > 9 ?
                   DateTime.Now.AddDays(-6 + i).Month.ToString() :
                   ("0" + DateTime.Now.AddDays(-6 + i).Month.ToString()))
                    + (DateTime.Now.AddDays(-6 + i).Day > 9 ?
                   DateTime.Now.AddDays(-6 + i).Day.ToString() :
                   ("0" + DateTime.Now.AddDays(-6 + i).Day.ToString()));

        }
    }

    //绘制近30天的坐标轴
    public void SetCount2()
    {     
        sizeDelta = GetComponent<RectTransform>().sizeDelta;
        length = sizeDelta.x ;//获取长边长度      
        //每段长度       5*7+1  每五天显示一次日期
        int temp1 = (int)length / 36;
        timespace = temp1;
        //是否要生成完整的30天 隔五天显示一次
        //for (int i = 0; i < 31; i+=5)
        //{
            
        //        GameObject count = Instantiate(lineImageTime.gameObject, transform);
        //        count.SetActive(true);
        //        count.GetComponent<RectTransform>().sizeDelta = new Vector2(5, 20);
        //        count.GetComponent<RectTransform>().anchoredPosition =
        //            new Vector2(-sizeDelta.x / 2 + temp1 * (i),  5);

        //        count.GetComponentInChildren<Text>().text =
        //            (DateTime.Now.AddDays(-30 + i).Month > 9 ?
        //            DateTime.Now.AddDays(-30 + i).Month.ToString() :
        //            ("0" + DateTime.Now.AddDays(-30 + i).Month.ToString()))
        //            + "-" + (DateTime.Now.AddDays(-30 + i).Day > 9 ?
        //            DateTime.Now.AddDays(-30 + i).Day.ToString() :
        //            ("0" + DateTime.Now.AddDays(-30 + i).Day.ToString()));
         
        //}

        //是否要生成完整的30天 每天显示一次
        for (int i = 0; i < 31; i += 1)
        {

            GameObject count = Instantiate(lineImageTime.gameObject, transform);
            count.SetActive(true);
            count.GetComponent<RectTransform>().sizeDelta = new Vector2(5, 20);
            count.GetComponent<RectTransform>().anchoredPosition =
                new Vector2(-sizeDelta.x / 2 + temp1 * (i), 5);

            //count.GetComponentInChildren<Text>().text =
            //    (DateTime.Now.AddDays(-30 + i).Month > 9 ?
            //    DateTime.Now.AddDays(-30 + i).Month.ToString() :
            //    ("0" + DateTime.Now.AddDays(-30 + i).Month.ToString()))
            //    + "-" + (DateTime.Now.AddDays(-30 + i).Day > 9 ?
            //    DateTime.Now.AddDays(-30 + i).Day.ToString() :
            //    ("0" + DateTime.Now.AddDays(-30 + i).Day.ToString()));

            count.GetComponentInChildren<Text>().text = DateTime.Now.AddDays(-30 + i).Year.ToString()+
              (DateTime.Now.AddDays(-30 + i).Month > 9 ?
              DateTime.Now.AddDays(-30 + i).Month.ToString() :
              ("0" + DateTime.Now.AddDays(-30 + i).Month.ToString()))
               + (DateTime.Now.AddDays(-30 + i).Day > 9 ?
              DateTime.Now.AddDays(-30 + i).Day.ToString() :
              ("0" + DateTime.Now.AddDays(-30 + i).Day.ToString()));

        }
    }


    //绘制近半年就是6个月的坐标轴
    public void SetCount3()
    {
     
        sizeDelta = GetComponent<RectTransform>().sizeDelta;
        length = sizeDelta.x ;//获取长边长度
     
        //每段长度       7*30+1  6个月
        int temp1 = (int)length / 211;
        timespace = temp1;

        ////是否要生成完整的180天 每隔30天显示一次
        //for (int i = 0; i < 181; i+=30)
        //{
          
        //        GameObject count = Instantiate(lineImageTime.gameObject, transform);
        //        count.SetActive(true);
        //        count.GetComponent<RectTransform>().sizeDelta = new Vector2(10, 20);
        //        count.GetComponent<RectTransform>().anchoredPosition =
        //            new Vector2(-sizeDelta.x / 2 + temp1 * (i),  5);

        //        count.GetComponentInChildren<Text>().text =
        //            (DateTime.Now.AddDays(-180 + i).Month > 9 ?
        //            DateTime.Now.AddDays(-180 + i).Month.ToString() :
        //            ("0" + DateTime.Now.AddDays(-180 + i).Month.ToString()))
        //            + "-" + (DateTime.Now.AddDays(-180 + i).Day > 9 ?
        //            DateTime.Now.AddDays(-180 + i).Day.ToString() :
        //            ("0" + DateTime.Now.AddDays(-180 + i).Day.ToString()));                  
        // }

        //是否要生成完整的180天 每天显示一次
        for (int i = 0; i < 181; i += 1)
        {

            GameObject count = Instantiate(lineImageTime.gameObject, transform);
            count.SetActive(true);
            count.GetComponent<RectTransform>().sizeDelta = new Vector2(10, 20);
            count.GetComponent<RectTransform>().anchoredPosition =
                new Vector2(-sizeDelta.x / 2 + temp1 * (i), 5);

            //count.GetComponentInChildren<Text>().text =
            //    (DateTime.Now.AddDays(-180 + i).Month > 9 ?
            //    DateTime.Now.AddDays(-180 + i).Month.ToString() :
            //    ("0" + DateTime.Now.AddDays(-180 + i).Month.ToString()))
            //    + "-" + (DateTime.Now.AddDays(-180 + i).Day > 9 ?
            //    DateTime.Now.AddDays(-180 + i).Day.ToString() :
            //    ("0" + DateTime.Now.AddDays(-180 + i).Day.ToString()));

            count.GetComponentInChildren<Text>().text = DateTime.Now.AddDays(-180 + i).Year.ToString()+
               (DateTime.Now.AddDays(-180 + i).Month > 9 ?
               DateTime.Now.AddDays(-180 + i).Month.ToString() :
               ("0" + DateTime.Now.AddDays(-180 + i).Month.ToString()))
                + (DateTime.Now.AddDays(-180 + i).Day > 9 ?
               DateTime.Now.AddDays(-180 + i).Day.ToString() :
               ("0" + DateTime.Now.AddDays(-180 + i).Day.ToString()));
        }
    }

  

    /*
     *   0105    0104   0103   0102   0101  1231  1230 
     *   10      20     30     40     50     60    70
     * 
     * 
     * **/

    /// <summary>
    /// 绘制七天的折线图
    /// </summary>
  public  void DrawBrokenLineChartClick1(DateTime dateTime)
  {
        acitiveTimespace = 1;
        //倒数天数
        List<int> steps = new List<int>();
        for (int i = 0; i < 7; i++)
        {         
            steps.Add(UnityEngine.Random.Range(10,91));
        }
        steps.Reverse();//反转数组
        Vector2 a, b;
        DrawLine drawLine = GetComponent<DrawLine>();
        TimeSpan timeSpan = DateTime.Now.Subtract(dateTime);
        for (int i = 1; i < 7; i++)
        {                    
            if (timeSpan.Days < 7)
            {
                if (i >= 7 - timeSpan.Days)
                {
                     a = new Vector2(-sizeDelta.x / 2 + timespace * (i - 1), steps[i - 1] * 1.0f / 10 * StepCountController.stepspace);
                     b = new Vector2(-sizeDelta.x / 2 + timespace * (i ), steps[i] * 1.0f / 10 * StepCountController.stepspace);
                    drawLine.DrawStraightLine(a, b, lineImage, center, transform,true,8);
                }
            }
            else
            {
                 a =  new Vector2(-sizeDelta.x / 2 + timespace * (i - 1 ), steps[i - 1] * 1.0f / 10 * StepCountController.stepspace);
                 b =  new Vector2(-sizeDelta.x / 2 + timespace * (i ), steps[i] * 1.0f / 10 * StepCountController.stepspace);
                drawLine.DrawStraightLine(a, b, lineImage, center, transform,true,8);
            }
          
        }
    }

    /// <summary>
    /// 绘制30天的折线图
    /// </summary>
    /// <param name="dayCounts"></param>
    public void DrawBrokenLineChartClick2(DateTime dateTime)
    {
        acitiveTimespace = 5;
        //倒数天数
        List<int> steps = new List<int>();
        for (int i = 0; i < 31; i++)
        {
            steps.Add(UnityEngine.Random.Range(10, 91));
        }
        steps.Reverse();//反转数组
        Vector2 a, b;
        DrawLine drawLine = GetComponent<DrawLine>();
        TimeSpan timeSpan = DateTime.Now.Subtract(dateTime);
        for (int i = 1; i < 31; i++)
        {                    
            if (timeSpan.Days<30)
            {
                if (i>=31-timeSpan.Days)
                {
                     a = new Vector2(-sizeDelta.x / 2 + timespace * (i - 1 ), steps[i - 1] * 1.0f / 10 * StepCountController.stepspace);
                     b =  new Vector2(-sizeDelta.x / 2 + timespace * (i ), steps[i] * 1.0f / 10 * StepCountController.stepspace);
                    drawLine.DrawStraightLine(a, b, lineImage, center, transform,true,8);
                }
            }
            else
            {
                 a =new Vector2(-sizeDelta.x / 2 + timespace * (i - 1 ), steps[i - 1] * 1.0f / 10 * StepCountController.stepspace);
                 b = new Vector2(-sizeDelta.x / 2 + timespace * (i), steps[i] * 1.0f / 10 * StepCountController.stepspace);
                drawLine.DrawStraightLine(a, b, lineImage, center, transform,true,8);
            }
           
        }
    }

    /// <summary>
    /// 绘制180天的折线图
    /// </summary>
    /// <param name="dayCounts"></param>
    public void DrawBrokenLineChartClick3(DateTime dateTime)
    {
        acitiveTimespace = 30;
        //倒数天数
        List<int> steps = new List<int>();
        for (int i = 0; i < 181; i++)
        {
            steps.Add(UnityEngine.Random.Range(10, 91));
        }
        steps.Reverse();//反转数组
        Vector2 a, b;
        DrawLine drawLine = GetComponent<DrawLine>();
        //此处需要日期         
        TimeSpan timeSpan = DateTime.Now.Subtract(dateTime);
        for (int i = 1; i < 181; i++)
        {          
            if (timeSpan.Days<180)
            {
                if (i >=181- timeSpan.Days)
                {
                     a = new Vector2(-sizeDelta.x / 2 + timespace * (i - 1 ), steps[i - 1] * 1.0f / 10 * StepCountController.stepspace);
                     b = new Vector2(-sizeDelta.x / 2 + timespace * (i ), steps[i] * 1.0f / 10 * StepCountController.stepspace);
                    drawLine.DrawStraightLine(a, b, lineImage, center, transform, false, 8);
                }
            }         
            else
            {
                 a = new Vector2(-sizeDelta.x / 2 + timespace * (i - 1 ), steps[i - 1] * 1.0f / 10 * StepCountController.stepspace);
                 b =new Vector2(-sizeDelta.x / 2 + timespace * (i), steps[i] * 1.0f / 10 * StepCountController.stepspace);
                drawLine.DrawStraightLine(a, b, lineImage, center, transform, false, 8);
            }
          
        }
    }


   
    [ContextMenu("嗷嗷嗷")]
    void Test()
    {
        DateTime dateTime = new DateTime(2018, 10, 10);
        TimeSpan timeSpan = DateTime.Now.Subtract(dateTime);
        Debug.Log(timeSpan.Days);
        Debug.Log(timeSpan.TotalDays);
     

    }


}
