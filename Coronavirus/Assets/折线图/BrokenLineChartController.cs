using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrokenLineChartController : MonoBehaviour {
    [SerializeField] Button button1;//对应7天折线图
    [SerializeField] Button button2;//对应30天折线图
    [SerializeField] Button button3;//对应180天折线图
    [SerializeField] DateTimeController timeLine7;
    [SerializeField] DateTimeController timeLine30;
    [SerializeField] DateTimeController timeLine180;
    [SerializeField] StepCountController stepCountController;
    [SerializeField] ChangAxisLength tl7;//横轴一
    [SerializeField] ChangAxisLength tl30;//横轴二
    [SerializeField] ChangAxisLength tl180;//横轴三
    public float height = 1000;//竖向初始长度
    public float width = 1000;//横向初始长度
    // Use this for initialization
    void Start () {

        //初始化横竖轴位置以及大小
        tl7.ChangAxis(width,height);
        tl30.ChangAxis(width*4, height);
        tl180.ChangAxis(width*25, height);


        stepCountController.SetStepCount(9);//画竖轴分段
        DrawBrokenLines();//画折线图


        button1.onClick.AddListener(OpentimeLine7);
        button2.onClick.AddListener(OpentimeLine30);
        button3.onClick.AddListener(OpentimeLine180);
    }

    void DrawBrokenLines()
    {
        timeLine7.SetCount1();
        timeLine7.DrawBrokenLineChartClick1(new System.DateTime(2018,12,1));
        timeLine30.SetCount2();
        timeLine30.DrawBrokenLineChartClick2(new System.DateTime(2018, 12, 1));
        timeLine180.SetCount3();
        timeLine180.DrawBrokenLineChartClick3(new System.DateTime(2017, 12, 1));
    }
	
    void OpentimeLine7()
    {
        tl7.gameObject.SetActive(true);
        tl30.gameObject.SetActive(false);
        tl180.gameObject.SetActive(false);
     
    }
    void OpentimeLine30()
    {
        tl7.gameObject.SetActive(false);
        tl30.gameObject.SetActive(true);
        tl180.gameObject.SetActive(false);

    }
    void OpentimeLine180()
    {
        tl7.gameObject.SetActive(false);
        tl30.gameObject.SetActive(false);
        tl180.gameObject.SetActive(true);
    }

}
