// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Hidden/Unlit/Obsolete/Premultiplied Colored - Mask (AOW3) (AlphaClip)" // Основано на шейдере NGUI/Unlit/Premultiplied Colored (AlphaClip)
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
					o.worldPos = TRANSFORM_TEX(v.vertex.xy, _MainTex);
					return o;
				}

				half4 frag (v2f IN) : SV_Target
				{
					// Sample the texture
					half4 col = tex2D(_MainTex, TRANSFORM_TEX(IN.texcoord, _MainTex));
					col.a = dot(tex2D(_MaskTex, TRANSFORM_TEX(IN.texcoord, _MaskTex)), _LayerMask);
					col *= IN.color;

					float2 factor = abs(IN.worldPos);
					float val = 1.0 - max(factor.x, factor.y);

					// Option 1: 'if' statement
					if (val < 0.0)
						return half4(0.0, 0.0, 0.0, 0.0);

					// Option 2: no 'if' statement -- may be faster on some devices
					col.a *= ceil(clamp(val, 0.0, 1.0));
					return col;
				}
			ENDCG
		}
	}
}