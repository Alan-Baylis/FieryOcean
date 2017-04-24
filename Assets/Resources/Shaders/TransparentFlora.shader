Shader "AOW3/TransparentFlora (Legacy Shaders | Transparent | Cutout | Diffuse)" { // From Unity 5.2.1f1
	Properties {
		_Color ("Main Color", Color) = (1,1,1,1)
		_MainTex ("Base (RGB) Trans (A)", 2D) = "white" {}
		_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
	}

	SubShader {
		Tags {
			"Queue"="Geometry-90" // "Queue"="AlphaTest"
			"IgnoreProjector"="True"
			"RenderType"="TransparentCutout"
			"ForceNoShadowCasting" = "True"
		}
		LOD 200

		ZWrite Off
//		ZTest Always
		Offset 0, -1

		CGPROGRAM
			#pragma surface surf Lambert alphatest:_Cutoff

			sampler2D _MainTex;
			fixed4 _Color;

			struct Input {
				float2 uv_MainTex;
			};

			void surf (Input IN, inout SurfaceOutput o) {
				fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
				o.Albedo = c.rgb;
				o.Alpha = c.a;
			}
		ENDCG
	}

	Fallback "Legacy Shaders/Transparent/Cutout/VertexLit"
}
