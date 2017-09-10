// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Unlit/Premultiplied Colored - Mask (AOW3)" // Основано на шейдере NGUI/Unlit/Premultiplied Colored
{
	Properties
	{
		_MainTex ("Main Texture (RGB)", 2D) = "black" {}
		_MaskTex ("Mask Texture (A)", 2D) = "white" {}
		_LayerMask ("Layer Mask", Vector) = (1, 0, 0, 0)
	}

	SubShader
	{
		Tags
		{
			"Queue" = "Transparent"
			"IgnoreProjector" = "True"
			"RenderType" = "Transparent"
			"DisableBatching" = "True"
		}

		Pass
		{
			Cull Off
			Lighting Off
			ZWrite Off
			AlphaTest Off
//			AlphaTest GEqual 0
			Fog { Mode Off }
			Offset -1, -1
//			ColorMask RGB
			Blend One OneMinusSrcAlpha

			CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				#include "UnityCG.cginc"

				sampler2D _MainTex;
				sampler2D _MaskTex;
				float4 _MaskTex_ST;
				fixed4 _LayerMask;

				struct appdata_t
				{
					float4 vertex : POSITION;
					half4 color : COLOR;
					float2 texcoord : TEXCOORD0;
				};

				struct v2f
				{
					float4 vertex : SV_POSITION;
					half4 color : COLOR;
					float2 texcoord : TEXCOORD0;
				};

				v2f vert (appdata_t v)
				{
					v2f o;
					o.vertex = UnityObjectToClipPos(v.vertex);
					o.color = v.color;
					o.texcoord = v.texcoord;
					return o;
				}

				half4 frag (v2f IN) : SV_Target
				{
					half4 col;
					col.rgb = tex2D(_MainTex, IN.texcoord).rgb;
					col.a = dot(tex2D(_MaskTex, TRANSFORM_TEX(IN.texcoord, _MaskTex)), _LayerMask);
					col *= IN.color;
					return col;
				}
			ENDCG
		}
	}
}