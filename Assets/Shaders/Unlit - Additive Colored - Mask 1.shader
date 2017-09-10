// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Hidden/Unlit/Additive Colored - Mask (AOW3) 1"  // �������� �� ������� NGUI/Unlit/Additive Colored
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

		LOD 100
		Cull Off
		Lighting Off
		ZWrite Off
		Fog { Mode Off }
//		ColorMask RGB
		AlphaTest Greater .01
		Blend One One

		Pass
		{
			CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				#include "UnityCG.cginc"

				sampler2D _MainTex;
				sampler2D _MaskTex;
				float4 _MainTex_ST;
				float4 _MaskTex_ST;
				fixed4 _LayerMask;
				float4 _ClipRange0 = float4(0.0, 0.0, 1.0, 1.0);
				float4 _ClipArgs0 = float4(1000.0, 1000.0, 0.0, 1.0);

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
					float2 worldPos : TEXCOORD1;
				};

				v2f vert (appdata_t v)
				{
					v2f o;
					o.vertex = UnityObjectToClipPos(v.vertex);
					o.color = v.color;
					o.texcoord = v.texcoord;
					o.worldPos = v.vertex.xy * _ClipRange0.zw + _ClipRange0.xy;
					return o;
				}

				half4 frag (v2f IN) : SV_Target
				{
					// Softness factor
					float2 factor = (float2(1.0, 1.0) - abs(IN.worldPos)) * _ClipArgs0.xy;

					// Sample the texture
					half4 col = tex2D(_MainTex, TRANSFORM_TEX(IN.texcoord, _MainTex));
					col.a = dot(tex2D(_MaskTex, TRANSFORM_TEX(IN.texcoord, _MaskTex)), _LayerMask);
					col *= IN.color;
					float fade = clamp(min(factor.x, factor.y), 0.0, 1.0);
					col.a *= fade;
					col.rgb = lerp(half3(0.0, 0.0, 0.0), col.rgb, fade);
					return col;
				}
			ENDCG
		}
	}
	Fallback "Unlit/Additive Colored - Mask (AOW3)"
}