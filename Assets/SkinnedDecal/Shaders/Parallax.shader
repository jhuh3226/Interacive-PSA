Shader "SkinnedDecals/Parallax" {
	Properties {
		_MainTex ("Albedo (RGB) Alpha (A)", 2D) = "white" {}
		_Color ("Main Color (RGB) Alpha (A)", Color) = (1, 1, 1, 1)
		_BumpMap ("Normals", 2D) = "bump" {}
		_SpecularMap("Specular map", 2D) = "white" {}
		_SpecularColor ("Specular Color", Color) = (1, 1, 1, 1)
		_Shininess ("Shininess", Range(0,1)) = 0.5
		_ParallaxMap ("Height", 2D) = "gray" {}
		_Parallax ("Parallax Amount", Range(0.005, 0.08)) = 0.02

	}
	SubShader {
		Tags { "RenderType"="Opaque" "Queue"="Geometry+5" }
		Fog { Mode Off }
		Offset -1, -1
		LOD 600

		Blend One OneMinusSrcAlpha
		
		CGPROGRAM
		#pragma surface surf StandardSpecular finalgbuffer:finalGBuffer exclude_path:forward noshadow noforwardadd nolightmap
		#pragma target 3.0

		sampler2D _MainTex;
		sampler2D _BumpMap;
		sampler2D _SpecularMap;
		sampler2D _ParallaxMap;

		float4 _Color;
		float4 _SpecularColor;

		half _Shininess;
		float _Parallax;

		struct Input {
			float2 uv_MainTex;
			float3 viewDir;
		};

		void surf (Input IN, inout SurfaceOutputStandardSpecular o) {

			// Parallax
			fixed2 uvs= IN.uv_MainTex;
			fixed4 height = tex2D(_ParallaxMap, IN.uv_MainTex);			
			float2 parallax = ParallaxOffset(height, _Parallax, IN.viewDir);
			uvs += parallax;

			// Albedo comes from a texture tinted by color
			fixed4 c = tex2D (_MainTex, uvs);
			o.Albedo = c.rgb * _Color.rgb;

			// Normals
			o.Normal = UnpackNormal (tex2D (_BumpMap, uvs));

			// Specularity & Smoothness
			fixed4 specMap = tex2D(_SpecularMap, uvs);
			o.Specular = specMap.rgb * _SpecularColor.rgb;
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
