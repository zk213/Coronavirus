using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProvinceClickEffectI : MonoBehaviour
{
	private string provinceName;
	private Dictionary<string, string> provinceNameCN;

	public Vector2[] points;
	public int pointsNum;
	private int times = 0;

	public InputField countInput;
	public GameObject drawGo;
	public Material mat;
	public MeshFilter meshFilter;
	public MeshRenderer meshRenderer;
	int count;

	private void Start()
	{
		provinceNameCN = new Dictionary<string, string>();
		provinceNameCN.Add("Hebei", "河北");
		provinceNameCN.Add("Hunan", "湖南");
		provinceNameCN.Add("Heilongjiang", "黑龙江");
		provinceNameCN.Add("Jilin", "吉林");
		provinceNameCN.Add("Hainan", "海南");
		provinceNameCN.Add("Guangdong", "广东");
		provinceNameCN.Add("Guangxi", "广西");
		provinceNameCN.Add("Guizhou", "贵州");
		provinceNameCN.Add("Yunnan", "云南");
		provinceNameCN.Add("Sichuan", "四川");
		provinceNameCN.Add("Chongqing", "重庆");
		provinceNameCN.Add("Hubei", "湖北");
		provinceNameCN.Add("Henan", "河南");
		provinceNameCN.Add("Jiangxi", "江西");
		provinceNameCN.Add("Fujian", "福建");
		provinceNameCN.Add("Zhejiang", "浙江");
		provinceNameCN.Add("Jiangsu", "江苏");
		provinceNameCN.Add("Shanghai", "上海");
		provinceNameCN.Add("Anhui", "安徽");
		provinceNameCN.Add("Shandong", "山东");
		provinceNameCN.Add("InnerMongolia", "内蒙古");
		provinceNameCN.Add("Hongkong", "香港");
		provinceNameCN.Add("Macao", "澳门");
		provinceNameCN.Add("Xizang", "西藏");
		provinceNameCN.Add("Qinghai", "青海");
		provinceNameCN.Add("Xinjiang", "新疆");
		provinceNameCN.Add("Gansu", "甘肃");
		provinceNameCN.Add("Liaoning", "辽宁");
		provinceNameCN.Add("Beijing", "北京");
		provinceNameCN.Add("Tianjin", "天津");
		provinceNameCN.Add("Taiwan", "台湾");
		provinceNameCN.Add("Shaanxi", "陕西");
		provinceNameCN.Add("Ningxia", "宁夏");
		provinceNameCN.Add("Shanxi", "山西");
	}

	//点击省份后触发
	private void OnMouseDown()
	{
		provinceName = gameObject.name;
		if(provinceNameCN.ContainsKey(provinceName))
		{
			provinceName = provinceNameCN[provinceName];
		}
		times = times + 1;
		Debug.Log(provinceName +　"治愈第" + times + "例患者。");
		//获取该省份坐标点
		points = GetComponent<PolygonCollider2D>().points;
		count = points.Length;
		DrawProvince(points, pointsNum);
	}

	private void Update()
	{
		//鼠标滚轮缩放地图
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (Camera.main.fieldOfView <= 100)
                Camera.main.fieldOfView += 2;
            if (Camera.main.orthographicSize <= 20)
                Camera.main.orthographicSize += 0.5F;
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (Camera.main.fieldOfView > 2)
                Camera.main.fieldOfView -= 2;
            if (Camera.main.orthographicSize >= 1)
                Camera.main.orthographicSize -= 0.5F;
        }
    }

	//图形绘制方法
	private void DrawProvince(Vector2[] points, int count)
	{
		Mesh mesh = PrepareMesh();
		Vector3[] vertices = new Vector3[count + 1];
		for (int i = 0; i < count; i++)
		{
			vertices[i] = points[i];
		}
		mesh.vertices = vertices;

		int[] triangles = new int[count * 3];
		for (int i = 0; i < triangles.Length; i += 3)
		{
			int first = 0;
			int second = i / 3 + 1;
			int third = second + 1;
			if (third > count)
			{
				third = 1;
			}
			triangles[i] = first;
			triangles[i + 1] = second;
			triangles[i + 2] = third;
		}
		mesh.triangles = triangles;
	}

	//Mesh构造
	Mesh PrepareMesh()
	{
		drawGo = new GameObject("DrawWithMesh");
		drawGo.transform.SetParent(transform);
		drawGo.transform.localPosition = Vector3.zero;
		drawGo.transform.localScale = Vector3.one;

		meshRenderer = drawGo.AddComponent<MeshRenderer>();
		meshRenderer.material = mat;

		meshFilter = drawGo.AddComponent<MeshFilter>();
		Mesh mesh = meshFilter.mesh;
		mesh.Clear();

		return mesh;
	}

}
