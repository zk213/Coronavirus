using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapDate : MonoBehaviour
{
    private int pointsNum;
    private int lastpointsNum = 0;
    private int times = 0;
    private bool Finish = true;
    private int pointstest = 0;
    private Vector2[] testPoints;
    private void Start()
    {
        testPoints = new Vector2[500];
    }
    private void Update()
    {
        if(GetComponent<PolygonCollider2D>() && Finish == true) //多边形碰撞体存在时执行
        {
            times = times + 1;
            Debug.Log("碰撞体" + times + "存在.");

            pointsNum = GetComponent<PolygonCollider2D>().points.Length; //获取顶点数
            Vector2[] points = new Vector2[pointsNum]; 
            points = GetComponent<PolygonCollider2D>().points; //存储顶点
            Destroy(gameObject.GetComponent<PolygonCollider2D>()); //销毁当前多边形碰撞体
            
            lastpointsNum = lastpointsNum + pointsNum;
            Debug.Log(lastpointsNum);

            for(int i = 0, j = pointstest; i < pointsNum; i++, j++)
            {
                testPoints[j] = points[i];
            }
            pointstest = pointstest + pointsNum;


        }
        else if(Finish == true)
        {
            Debug.Log("开始构件");
            for(int k = 0; k < lastpointsNum; k++)
            {
                Debug.Log(testPoints[k]);
            }
            Vector2[] lastpoints = new Vector2[lastpointsNum];
            for(int z = 0; z < lastpointsNum; z++)
            {
                lastpoints[z] = testPoints[z];
            }
            gameObject.AddComponent<PolygonCollider2D>();
            gameObject.GetComponent<PolygonCollider2D>().points = lastpoints;
            Finish = false;
        }
    }
}
