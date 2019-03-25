Shader "SkinnedDecals/Specular" {
	Properties {
		_MainTex ("Albedo (RGB) Alpha (A)", 2D) = "white" {}
		_Color ("Main Color (RGB) Alpha (A)", Color) = (1, 1, 1, 1)
		_BumpMap ("Normals", 2D) = "bump" {}
		_SpecularMap("Specular map", 2D) = "white" {}
		_SpecularColor ("Specular Color", Color) = (1, 1, 1, 1)
		_Shininess ("Shininess", Range(0,1)) = 0.5

	}
	SubShader {
		Tags { "RenderType"="Opaque" "Queue"="Geometry+5" }
		Fog { Mode Off }
		Offset -1, -1
		LOD 200

		Blend One OneMinusSrcAlpha
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf StandardSpecular finalgbuffer:finalGBuffer exclude_path:forward noshadow noforwardadd

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;
		sampler2D _BumpMap;
		sampler2D _SpecularMap;

		float4 _Color;
		float4 _SpecularColor;
		half _Shininess;

		struct Input {
			float2 uv_MainTex;
			float2 uv_BumpMap;
			float2 uv_SpecularMap;
		};

		void surf (Input IN, inout SurfaceOutputStandardSpecular o) {
			// Albedo
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex);
			o.Albedo = c.rgb * _Color.rgb;

			// Normals
			o.Normal = UnpackNormal (tex2D (_BumpMap, IN.uv_BumpMap));

			// Specularity & Smoothness
			fixed4 specMap = tex2D(_SpecularMap, IN.uv_SpecularMap);
			o.Specular = specMap.rgb  * _SpecularColor.rgb;
			o.Smoothness = specMap.a * _Shininess;

			// Alpha
			o.Alpha = c.a * _Color.a;
		}

		void finalGBuffer (Input IN, SurfaceOutputStandardSpecular o, inout half4 diffuse, inout half4 specSmoothness, inout half4 normal, inout half4 emission) {
			diffuse *= o.Alpha;
			specSmoothness.xyz *= o.Alpha;
			specSmoothness.w *= o.Alpha;
			normal *= o.Alpha;
			emission *= o.Alpha;
		}

		ENDCG
	}
	FallBack "Diffuse"
}
