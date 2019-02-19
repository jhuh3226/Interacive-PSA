Shader "Custom/MultipleTexture2" {
	Properties
	{
		_Color("Main Color", Color) = (1,1,1,1)
		_MainTex("Base (RGB) Trans (A)", 2D) = "white" {}
	_SecondTex("Base (RGB) Trans (A)", 2D) = "white" {}
	_ThirdTex("Base (RGB) Trans (A)", 2D) = "white" {}
	_FourthTex("Base (RGB) Trans (A)", 2D) = "white" {}
	}

		SubShader
	{
		Tags{ "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent" }
		LOD 200

		//-----------------------------------------------------
		//Main
		CGPROGRAM
#pragma surface surf Lambert alpha:fade

		sampler2D _MainTex;
	fixed4 _Color;

	struct Input
	{
		float2 uv_MainTex;
	};

	void surf(Input IN, inout SurfaceOutput o)
	{
		fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
		o.Albedo = c.rgb;
		o.Alpha = c.a;
	}
	ENDCG

		//-----------------------------------------------------
		//Second
		CGPROGRAM
#pragma surface surf Lambert alpha:fade

		sampler2D _SecondTex;
	fixed4 _Color;

	struct Input
	{
		float2 uv_SecondTex;
	};

	void surf(Input IN, inout SurfaceOutput o)
	{
		//fixed4 means color + alpha
		//in.uv ~ contains coordinate of the current pixel
		fixed4 c = tex2D(_SecondTex, IN.uv_SecondTex) * _Color;
		o.Albedo = c.rgb;
		o.Alpha = c.a;
	}
	ENDCG

		//-----------------------------------------------------
		//Third
		CGPROGRAM
#pragma surface surf Lambert alpha:fade

		sampler2D _ThirdTex;
	fixed4 _Color;

	struct Input
	{
		float2 uv_ThirdTex;
	};

	void surf(Input IN, inout SurfaceOutput o)
	{
		fixed4 c = tex2D(_ThirdTex, IN.uv_ThirdTex) * _Color;
		o.Albedo = c.rgb;
		o.Alpha = c.a;
	}
	ENDCG

		//-----------------------------------------------------
		//fourth
		CGPROGRAM
#pragma surface surf Lambert alpha:fade

		sampler2D _FourthTex;
	fixed4 _Color;

	struct Input
	{
		float2 uv_FourthTex;
	};

	void surf(Input IN, inout SurfaceOutput o)
	{
		fixed4 c = tex2D(_FourthTex, IN.uv_FourthTex) * _Color;
		o.Albedo = c.rgb;
		o.Alpha = c.a;
	}
	ENDCG
	}
		
			Fallback "Legacy Shaders/Diffuse"
}
