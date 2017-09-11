// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "AOW3/Bullet - Mask (Mobile | Transparent | Vertex Color)" { // Аналог шейдера "Mobile/Transparent/Vertex Color" взятого из Unity 4 Standard Assets
	Properties {
		_MainTex ("Main Texture (RGB)", 2D) = "white" {}
		_MaskTex ("Mask Texture (A)", 2D) = "white" {}
		_LayerMask ("Layer Mask", Vector) = (1, 0, 0, 0)
	}

	Category {
		Tags {
			"Queue"="Transparent" // Geometry-10
			"IgnoreProjector"="True"
			"RenderType"="Transparent"
		}
//		ZTest Always
		ZWrite Off
		Offset 0, -2000
		Alphatest Greater 0
		Blend SrcAlpha OneMinusSrcAlpha

		SubShader {
			Pass {
				ColorMaterial AmbientAndDiffuse
				Fog { Mode Off }
				Lighting Off
				SeparateSpecular On

				CGPROGRAM
					#pragma vertex vert
					#pragma fragment frag
					#include "UnityCG.cginc"

					sampler2D _MainTex;
					sampler2D _MaskTex;
					float4 _MainTex_ST;
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
						float4 vertex : POSITION;
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

					half4 frag (v2f IN) : COLOR
					{
						fixed4 col;
						col.rgb = tex2D(_MainTex, TRANSFORM_TEX(IN.texcoord, _MainTex)).rgb;
						col.a = dot(tex2D(_MaskTex, TRANSFORM_TEX(IN.texcoord, _MaskTex)), _LayerMask);
						col *= IN.color * 2;
						return col;
					}
				ENDCG
			}
		}
	}
}