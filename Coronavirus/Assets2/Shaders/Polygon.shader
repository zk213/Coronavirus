﻿// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader"Unlit/polygon"

{
	Properties
	{
		//定义基本属性，可以从编辑器里面进行设置的变量   
		// _MainTex ("Texture", 2D) = "white" {}   
	}

	CGINCLUDE
		//从应用程序传入顶点函数的数据结构定义  
		struct appdata
	{
		float4 vertex : POSITION;
		float2 uv : TEXCOORD0;
	};

	//从顶点函数传入片段函数的数据结构定义   

	struct v2f
	{
		float2 uv : TEXCOORD0;
		float4 vertex : SV_POSITION;
	};

	//定义贴图变量   
	sampler2D _MainTex;
	// float4 _MainTex_ST;   

	//定义与脚本进行通信的变量 
	vector Value[1000];
	int PointNum = 0;

	//计算两点间的距离的函数 
	float Dis(float4 v1, float4 v2)
	{
		return sqrt(pow((v1.x - v2.x), 2) + pow((v1.y - v2.y), 2));
	}

	//绘制线段 

	bool DrawLineSegment(float4 p1, float4 p2, float lineWidth, v2f i)

	{

		float4 center = float4((p1.x + p2.x) / 2, (p1.y + p2.y) / 2, 0, 0);

		//计算点到直线的距离   

		float d = abs((p2.y - p1.y) * i.vertex.x + (p1.x - p2.x) * i.vertex.y + p2.x * p1.y - p2.y * p1.x) / sqrt(pow(p2.y - p1.y, 2) + pow(p1.x - p2.x, 2));

		//小于或者等于线宽的一半时，属于直线范围   

		float lineLength = sqrt(pow(p1.x - p2.x, 2) + pow(p1.y - p2.y, 2));

		if (d <= lineWidth / 2 && Dis(i.vertex, center) < lineLength / 2)

		{

			return true;

		}

		return false;

	}



	//绘制多边形，这里限制了顶点数不超过6。可以自己根据需要更改。 

	bool pnpoly(int nvert, float4 vert[1000], float testx, float testy)

	{



		int i, j;

		bool c = false;

		float vertx[6];

		float verty[6];



		for (int n = 0; n < nvert; n++)

		{

			vertx[n] = vert[n].x;

			verty[n] = vert[n].y;

		}

		for (i = 0, j = nvert - 1; i < nvert; j = i++) {

			if (((verty[i] > testy) != (verty[j] > testy)) && (testx < (vertx[j] - vertx[i]) * (testy - verty[i]) / (verty[j] - verty[i]) + vertx[i]))

				c = !c;

		}

		return c;

	}



	v2f vert(appdata v)

	{

		v2f o;

		//将物体顶点从模型空间换到摄像机剪裁空间，也可采用简写方式——o.vertex = UnityObjectToClipPos(v.vertex);   

		o.vertex = UnityObjectToClipPos(v.vertex);

		//2D UV坐标变换,也可以采用简写方式——o.uv = TRANSFORM_TEX(v.uv, _MainTex);   

		//o.uv = v.uv.xy * _MainTex_ST.xy + _MainTex_ST.zw;   

		return o;

	}

	fixed4 frag(v2f i) : SV_Target

	{



		//绘制多边形顶点 

		for (int j = 0; j < PointNum; j++)

		{

			if (Dis(i.vertex, Value[j]) < 3)

			{

				return fixed4(1,0,0,0.5);

			}

		}

	//绘制多边形的边 

	for (int k = 0; k < PointNum; k++)

	{

		if (k == PointNum - 1)

		{

			if (DrawLineSegment(Value[k],Value[0],2,i))

			{

				return fixed4(1,1,0,0.5);

			}

		}

		else

		{

			if (DrawLineSegment(Value[k],Value[k + 1],2,i))

			{

				return fixed4(1,1,0,0.5);

			}

		}



	}

	//填充多边形内部 

	if (pnpoly(PointNum, Value,i.vertex.x ,i.vertex.y))

	{

		return fixed4(0,1,0,0.3);

	}

	return fixed4(0,0,0,0);

	//fixed4 col = tex2D(_MainTex, i.uv);  

	//return col;   

	}

		ENDCG



		SubShader

	{

		Tags{ "RenderType" = "Opaque" }

			LOD 100

			Pass

		{

			//选取Alpha混合方式   

			Blend  SrcAlpha OneMinusSrcAlpha

			//在CGPROGRAM代码块中写自己的处理过程   

			CGPROGRAM

			//定义顶点函数和片段函数的入口分别为vert和frag   

			#pragma vertex vert   

			#pragma fragment frag   

			//包含基本的文件，里面有一些宏定义和基本函数   

			#include "UnityCG.cginc"                



			ENDCG

		}

	}

}