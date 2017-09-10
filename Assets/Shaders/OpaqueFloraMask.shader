Shader "AOW3/OpaqueFlora Mask (Legacy Shaders | Transparent | Cutout | Diffuse)" { // From Unity 5.2.1f1, see shader AOW3/MaskAlphaTest-Diffuse
	Properties {
		_Color ("Main Color", Color) = (1,1,1,1)
		_MainTex ("Main Texture (RGB)", 2D) = "white" {}
		_MaskTex ("Mask Texture (A)", 2D) = "white" {}
		_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
	}

	SubShader {
		Tags {
			"Queue"="Geometry-30" // "Queue"="AlphaTest"
			"IgnoreProjector"="True"
			"RenderType"="TransparentCutout"
		}
		LOD 200

		CGPROGRAM
			#pragma surface surf Lambert alphatest:_Cutoff

			sampler2D _MainTex;
			sampler2D _MaskTex;
			fixed4 _Color;

			struct Input {
				float2 uv_MainTex;
			};

			void surf (Input IN, inout SurfaceOutput o) {
				o.Albedo = tex2D(_MainTex, IN.uv_MainTex).rgb * _Color.rgb;
				o.Alpha = tex2D(_MaskTex, IN.uv_MainTex).r * _Color.a;
			}
		ENDCG
	}

	Fallback "AOW3/MaskAlphaTest-VertexLit (Legacy Shaders | Transparent | Cutout | VertexLit)"
}