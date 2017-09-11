Shader "AOW3/Obsolete/BuildingSelect (vin3)" {
	Properties {
		_MainTex("Base (RGB) Gloss (A)", 2D) = "bump" {}
		_light("_light", Range(-1,1) ) = 0
		_light2("_light2", Range(-1,1) ) = 0
	}

	SubShader {
		Tags {
			"Queue"="Geometry"
			"IgnoreProjector"="False"
			"RenderType"="Opaque"
		}

		Cull Back
		ZWrite On
		ZTest LEqual
		ColorMask RGBA
		Fog {}

		CGPROGRAM
			#pragma surface surf BlinnPhongEditor dualforward fullforwardshadows noforwardadd halfasview
			#pragma target 2.0

			sampler2D _MainTex;
			float _light;
			float _light2;

			struct Input {
				float2 uv_MainTex;
			};

			struct EditorSurfaceOutput {
				half3 Albedo;
				half3 Normal;
				half3 Emission;
				half3 Gloss;
				half Specular;
				half Alpha;
			};

			inline half4 LightingBlinnPhongEditor_PrePass(EditorSurfaceOutput s, half4 light) {
				float4 normalize0 = normalize(float4(s.Albedo, 1.0));
				float4 add4 = _light.xxxx + normalize0;
				float4 add2 = _light2.xxxx + light;
				float4 multiply0 = add4 * add2;
				return multiply0;
			}

			inline half4 LightingBlinnPhongEditor(EditorSurfaceOutput s, half3 lightDir, half3 viewDir, half atten) {
				half3 h = normalize(lightDir + viewDir);

				half diff = max(0, dot(lightDir, s.Normal));

				float nh = max(0, dot(s.Normal, h));
				float spec = pow(nh, s.Specular * 128.0);

				half4 res;
				res.rgb = _LightColor0.rgb * diff;
				res.w = spec * Luminance(_LightColor0.rgb);
				res *= atten;

				return LightingBlinnPhongEditor_PrePass(s, res);
			}

			void surf(Input IN, inout EditorSurfaceOutput o) {
				o.Normal = float3(0.0,0.0,1.0);
				o.Emission = 0.0;
				o.Gloss = 0.0;
				o.Specular = 0.0;
				o.Albedo = tex2D(_MainTex, IN.uv_MainTex).rgb;
				o.Alpha = 1.0;
			}
		ENDCG
	}
	Fallback "Diffuse"
}