Shader "SkinnedDecals/Forward Rendering/Packed Parallax" {
	Properties {
		_MainTex ("Albedo (RGB) Alpha (A)", 2D) = "white" {}
		_Color ("Main Color (RGB) Alpha (A)", Color) = (1, 1, 1, 1)
		_ParallaxMap ("Packed Normals (R) Height (G) Normal UP (B) Smoothness (A) Normal RIGHT", 2D) = "bump" {}
		_Parallax ("Parallax Amount", Range(0.005, 0.08)) = 0.02
		_SpecularColor ("Specular Color", Color) = (1, 1, 1, 1)
		_Shininess ("Shininess", Range(0,1)) = 0.5
		_AlphaClip ("Alpha Clip", Range(0, 1))= 0.01
	}
	SubShader {
		Tags { "RenderType"="Opaque" "Queue"="Geometry+5" }
		Fog { Mode Off }
		Offset -1, -1
		LOD 200

		Blend One OneMinusSrcAlpha
		
		CGPROGRAM
		#pragma surface surf StandardSpecular noshadow alpha
		#pragma target 3.0
		#pragma shader_feature _PARALLAX_MAP

		sampler2D _MainTex;
		sampler2D _ParallaxMap;

		float4 _Color;
		float4 _SpecularColor;
		float _Parallax;
		half _Shininess;
		float _AlphaClip;

		struct Input {
			float2 uv_MainTex;
			float2 uv_Normal;
			float3 viewDir;
		};

		void surf (Input IN, inout SurfaceOutputStandardSpecular o) {

			// Parallax
			fixed2 uvs = IN.uv_MainTex;
			float2 offset = ParallaxOffset((tex2D(_ParallaxMap, IN.uv_MainTex).r, uvs), _Parallax, IN.viewDir);
			uvs += offset;

			// Albedo
			fixed4 c = tex2D(_MainTex, uvs);
			o.Albedo = c.rgb * _Color;

			// Packed Normals
			//R= Height, G= Normal map up, B= Smoothness, A= Normal map right
			fixed4 packedNormals = tex2D(_ParallaxMap, uvs);

			// Normal
			o.Normal = UnpackNormalDXT5nm(packedNormals);

			// Specularity & Smoothness
			o.Specular = _SpecularColor.rgb;
			o.Smoothness = packedNormals.b * _Shininess;

			// Alpha
			o.Alpha = c.a * _Color.a;
			clip(o.Alpha - _AlphaClip);
		}

		ENDCG
	}
	FallBack "Diffuse"
}
