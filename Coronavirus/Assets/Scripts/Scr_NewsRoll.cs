using UnityEngine;
using UnityEngine.UI;

public class Scr_NewsRoll : MonoBehaviour
{
    //设置ScrollRect变量
    ScrollRect rect;
    public float speed = 0.8f;

    void Start()
    {
        //获取 ScrollRect变量
        rect = this.GetComponent<ScrollRect>();
    }

    void Update()

    {
        //在Update函数中调用ScrollValue函数
        ScrollValue();
    }

    private void ScrollValue()
    {
        //当对应值超过1，重新开始从 0 开始
        if (rect.horizontalNormalizedPosition > 1.0f)
        {
            //rect.horizontalNormalizedPosition = 0;
        }
        //逐渐递增 ScrollRect 水平方向上的值
        rect.horizontalNormalizedPosition = rect.horizontalNormalizedPosition + speed * Time.deltaTime;
    }
}