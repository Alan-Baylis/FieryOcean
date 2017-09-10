// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "AOW3/Obsolete/Particles" { // Аналог шейдера "Mobile/Transparent/Vertex Color" взятого из Unity 4 Standard Assets
	Properties {
		_Color ("Main Color", Color) = (1,1,1,1)
		_MainTex ("Base (RGB) Trans (A)", 2D) = "white" {}
	}

	Category {
		Tags {
			"Queue"="Transparent"
			"IgnoreProjector"="True"
			"RenderType"="Transparent"
		}

		ZWrite Off
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
					fixed4 _Color;

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
						return tex2D(_MainTex, IN.texcoord) * IN.color * _Color * 2;
					}
				ENDCG
			}
		}
	}
}