// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced '_Projector' with 'unity_Projector'

// Upgrade NOTE: replaced '_Projector' with 'unity_Projector'

Shader "Hidden/iRobi/Projector"
{ 

 Properties

 {

  _Color ("Main Colour", Color) = (1,1,1,1)

  _MainTex ("Cookie", 2D) = "gray" { TexGen ObjectLinear }

//   _FalloffTex ("FallOff", 2D) = "gray" { TexGen ObjectLinear   }

 }

 

 SubShader

 {


  Pass

  {

   Lighting Off

   Cull Off

   ZWrite Off

   Offset -1, -1

   


   AlphaTest Greater 0.05

   ColorMask RGB

   Blend SrcAlpha OneMinusSrcAlpha

   CGPROGRAM

   #pragma vertex vert

   #pragma fragment frag

   #pragma fragmentoption ARB_fog_exp2

   #pragma fragmentoption ARB_precision_hint_fastest

   #include "UnityCG.cginc"

 

   struct Input

   {

    float4 vertex : POSITION;

    float3 normal : NORMAL;

    //float4 color : COLOR;

   };

   

   struct v2f

   {

    float4 pos : SV_POSITION;

    float4 uv_Main  : TEXCOORD0;


    float koef : COLOR;

   };

 

   sampler2D _MainTex;

   sampler2D _FalloffTex;

   float4 _Color;

   float4x4 unity_Projector;


 

   v2f vert(Input v)

   {

    v2f o;
                UNITY_INITIALIZE_OUTPUT(v2f, o);

    o.pos = UnityObjectToClipPos (v.vertex);

    o.uv_Main = mul (unity_Projector, v.vertex);

   

    

    return o;

   }

 

   half4 frag (v2f i) : COLOR

   {

    half4 falloff = tex2Dproj(_FalloffTex, i.uv_Main);
    half4 tex = tex2Dproj(_MainTex, i.uv_Main) * _Color ;

    return half4(tex.rgb,tex.a*falloff.r);
    //return tex;

   }

   ENDCG

  }

 }

}
