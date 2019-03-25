Shader "SkinnedDecals/Deferred Atlas Animated" {
	Properties {
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_BumpMap ("Normals", 2D) = "bump" {}
		_SmoothessMap("Smoothness map", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0

		//_Tile ("Tile", Float) = 0
		_AtlasWidth ("Atlas width", Float) = 4
		_AtlasHeight ("Atlas height", Float) = 4
	}
	SubShader {
		Tags { "RenderType"="Opaque" "Queue"="Geometry+5" }
		Fog { Mode Off }
		Offset -1, -1
		LOD 200

		Blend One OneMinusSrcAlpha
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard finalgbuffer:finalGBuffer exclude_path:forward noshadow noforwardadd

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;
		sampler2D _BumpMap;
		sampler2D _SmoothessMap;

		struct Input {
			float2 uv_MainTex;
			float4 color : COLOR;
		};

		half _Glossiness;
		half _Metallic;

		//float _Tile;
		float _AtlasWidth, _AtlasHeight;

		void surf (Input IN, inout SurfaceOutputStandard o) {

			// select tile from tileset
			float _Tile = floor((IN.color.a * 2) / (1.0 / 128.0));
			float2 uv = IN.uv_MainTex;
			float tileWidth = 1 / _AtlasWidth;
			float tileHeight = 1 / _AtlasHeight;
			float col = floor(_Tile / _AtlasWidth);
			float row = fmod(_Tile, _AtlasWidth);
			uv.x = row * tileWidth + uv.x * tileWidth;
			uv.y = col * -tileHeight + uv.y * tileHeight - tileHeight;

			// Albedo comes from a texture tinted by color
			fixed4 c = tex2D (_MainTex, uv);
			o.Albedo = c.rgb;
			o.Normal = UnpackNormal (tex2D (_BumpMap, uv));
			o.Metallic = _Metallic;
			fixed4 smoothness = tex2D (_SmoothessMap, uv);
			o.Smoothness = smoothness * _Glossiness;
			o.Alpha = c.a;
		}

		void finalGBuffer (Input IN, SurfaceOutputStandard o, inout half4 diffuse, inout half4 specSmoothness, inout half4 normal, inout half4 emission) {
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
