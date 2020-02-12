using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MoveChart : MonoBehaviour, IDragHandler, IPointerDownHandler
{
    public RectTransform canvas;//所在canvas

    [SerializeField] private RectTransform curRecTran;
    private void Start()
    {
        //curRecTran = transform.GetComponent<RectTransform>();
    }


    Vector3 offet;
    /// <summary>
    /// 记录初始手的位置以及地图中心的向量差
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerDown(PointerEventData eventData)
    {
        if (RectTransformUtility.RectangleContainsScreenPoint(canvas, eventData.position))
        {
            Vector3 mouseDown;
            //屏幕坐标转世界坐标
            if (RectTransformUtility.ScreenPointToWorldPointInRectangle(curRecTran, eventData.position,
    eventData.pressEventCamera, out mouseDown))
            {
                offet = curRecTran.position - mouseDown;
            }
        }      
    }
    /// <summary>
    /// 移动
    /// </summary>
    /// <param name="eventData"></param>
    public void OnDrag(PointerEventData eventData)
    {

        if (RectTransformUtility.RectangleContainsScreenPoint(canvas, eventData.position))
        {
            Vector3 globalMousePos;
            if (RectTransformUtility.ScreenPointToWorldPointInRectangle(curRecTran, eventData.position,
    eventData.pressEventCamera, out globalMousePos))

            {
                curRecTran.position = new Vector3(globalMousePos.x + offet.x, curRecTran.position.y, curRecTran.position.z);
            }
        }

    }
}
