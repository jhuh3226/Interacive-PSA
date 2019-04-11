Shader "Custom/DecalModify" 
{
	Properties{
		_Color("Main Color", Color) = (1,1,1,1)
		_MainTex("Base (RGB)", 2D) = "white" {}
	_DecalTex("Decal (RGBA)", 2D) = "black" {}
	_DecalSecond("Decal (RGBA)", 2D) = "black" {}
	}

		SubShader{
		Tags{ "RenderType" = "Opaque" }
		LOD 250

		CGPROGRAM
#pragma surface surf Lambert

		sampler2D _MainTex;
	sampler2D _DecalTex;
	sampler2D _DecalSecond;
	fixed4 _Color;

	//communicate
	struct Input {
		float2 uv_MainTex;
		float2 uv_DecalTex;
		float2 uv_DecalSecond;
	};

	//o means, call SurfaceOutput as o
	void surf(Input IN, inout SurfaceOutput o) {
		fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
		half4 decal = tex2D(_DecalTex, IN.uv_DecalTex);
		half4 decal2 = tex2D(_DecalSecond, IN.uv_DecalSecond);
		float decalAlpha = decal2.a;

		//decal2.a = 0;
		c.rgb = lerp(c.rgb, (decal.rgb * decal2.rgb), (decal.a * decal2.a));
		c *= _Color;
		c *= _Color;
		o.Albedo = c.rgb;
		o.Alpha = c.a;
	}
	ENDCG
	}

		Fallback "Legacy Shaders/Diffuse"
}
