Shader "Zyf/Hex"{
	Properties{
		_MainTex("Texture", 2D) = "white" {}
		_Color("Color", Color) = (1,1,1,1)
	}

	SubShader{
		Tags{
			"Queue" = "Transparent"
			"PreviewType" = "Plane"
		}

		Stencil{
			Ref 1
			Comp NotEqual
		}

		Pass{
			Blend SrcAlpha OneMinusSrcAlpha
			ZWrite Off

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			//定义从每个顶点获取哪些数据
			struct appdata {
				//位置数据
				float4 vertex : POSITION;
				//UV
				float2 uv : TEXCOORD0;
				//颜色
				float4 color : COLOR;
			};

			//定义将哪些数据从顶点传到片元
			struct v2f {
				float4 vertex : SV_POSITION;
				//UV
				float2 uv : TEXCOORD0;
				float4 color : COLOR;
			};

			v2f vert(appdata v) {
				v2f o;
				//将顶点转换到屏幕上
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				o.color = v.color;
				return o;
			}

			sampler2D _MainTex;
			fixed4 _Color;

			//将像素转换成颜色
			float4 frag(v2f i) : SV_Target{
				return tex2D(_MainTex, i.uv) * _Color * i.color;
			}

			ENDCG
		}
	}
}