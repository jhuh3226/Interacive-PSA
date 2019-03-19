// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced '_Projector' with 'unity_Projector'

// Upgrade NOTE: replaced '_Projector' with 'unity_Projector'

Shader "iRobi/RestorePC"
{
    Properties
    {
        _Dec ("Decal", 2D) = "gray" { }//TexGen ObjectLinear }
        _Color ("Main Color", Color) = (1,1,1,0.5)
        _DeepAngle ("Deep Angle", Range(0,1)) =0.5
    _MainTex ("Base Texture", 2D) = "white" { }
    _OrigTex ("Original Texture", 2D) = "white" { }
//    _ShadowMapTex ("Original Texture2", 2D) = "white" { }
   // [HideInInspector]
    }
 
    Subshader
    {
        Tags { "RenderType"="Transparent-1" }
        
        
//        Pass {
//            Name "Outline1"
//            Tags {
//            }
//            Cull off
//            Offset -1000, -1000
//            
//            CGPROGRAM
//            #pragma vertex vert
//            #pragma fragment frag
//            #include "UnityCG.cginc"
//sampler2D _MainTex;
//         
//            struct VertexInput {
//                float4 vertex : POSITION;
//                float3 normal : NORMAL;
//            };
//            struct VertexOutput {
//                float4 pos : SV_POSITION;
//    //float2  uv : TEXCOORD0;
//    float4 scrPos: TEXCOORD0;
//            };
//float4 _MainTex_ST;
//            VertexOutput vert (VertexInput v,appdata_base g) {
//                VertexOutput o;
//                UNITY_INITIALIZE_OUTPUT(VertexOutput, o);
//               if((_ScreenParams.x/_ScreenParams.y)!=1)
//		{
//
//		
//		//o.pos = mul(UNITY_MATRIX_MVP, float4(0,1,0,1));
//		o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
//		
//		
//		
//		}
//		else {
//               o.pos =  float4(2*(g.texcoord.x+g.normal.x)-1.0f, -2*(g.texcoord.y+g.normal.z)+1.0f, 0, 1.0f);
//               }
//		o.scrPos = ComputeScreenPos(o.pos);
//                return o;
//            }
//            fixed4 frag(VertexOutput i) : COLOR {
//                float2 wcoord = (i.scrPos.xy/i.scrPos.w);
//    half4 texcol = tex2D (_MainTex, wcoord);
//         
//    return texcol;
//            }
//            ENDCG
//        }
//        
//        
//        Pass {
//            Name "Outline3"
//            Tags {
//            }
//            Cull off
//            Offset -1000, -1000
//            
//            CGPROGRAM
//            #pragma vertex vert
//            #pragma fragment frag
//            #include "UnityCG.cginc"
//sampler2D _MainTex;
//            struct VertexInput {
//                float4 vertex : POSITION;
//                float3 normal : NORMAL;
//            };
//            struct VertexOutput {
//                float4 pos : SV_POSITION;
//    //float2  uv : TEXCOORD0;
//    float4 scrPos:TEXCOORD0;
//            };
//float4 _MainTex_ST;
//            VertexOutput vert (VertexInput v,appdata_img g) {
//                VertexOutput o;
//                UNITY_INITIALIZE_OUTPUT(VertexOutput, o);
//               if((_ScreenParams.x/_ScreenParams.y)!=1)
//		{o.pos = mul(UNITY_MATRIX_MVP, v.vertex);}
//		else {
//               o.pos = float4(2*g.texcoord.x-1.0f, -2*g.texcoord.y+1.0f, 0.0f, 1.0f);
//               }
//		o.scrPos = ComputeScreenPos(o.pos);
//                return o;
//            }
//            fixed4 frag(VertexOutput i) : COLOR {
//                float2 wcoord = (i.scrPos.xy/i.scrPos.w);
//    half4 texcol = tex2D (_MainTex, wcoord);
//         
//    return texcol;
//            }
//            ENDCG
//        }
        
        
        Pass

  {

   Lighting Off

   Cull Off

//   ZWrite Off

//            Offset -10000, -10000

   

   Tags { "Queue" = "Transparent" "RenderType"="Opaque" }
//   AlphaTest Greater 0.05
//            ColorMask RGB
//   Blend SrcAlpha OneMinusSrcAlpha
// SrcAlpha    DstAlpha   OneMinusSrcAlpha   OneMinusDstAlpha

//   Blend SrcAlpha SrcAlpha
//   Blend SrcAlpha DstAlpha
//   Blend DstAlpha SrcAlpha
//   Blend DstAlpha DstAlpha
//
//   Blend OneMinusSrcAlpha OneMinusSrcAlpha
//   Blend OneMinusDstAlpha OneMinusSrcAlpha
//   Blend OneMinusSrcAlpha OneMinusDstAlpha
//   Blend OneMinusDstAlpha OneMinusDstAlpha
//
//   Blend OneMinusSrcAlpha SrcAlpha //!
//   Blend SrcAlpha OneMinusSrcAlpha
//   Blend OneMinusSrcAlpha DstAlpha
//   Blend DstAlpha OneMinusSrcAlpha
//
//   Blend OneMinusDstAlpha SrcAlpha
//   Blend DstAlpha OneMinusDstAlpha
//   Blend OneMinusDstAlpha DstAlpha //!
//   Blend SrcAlpha OneMinusDstAlpha

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
   };
   struct v2f
   {
    float4 pos : SV_POSITION;
    float2 uv_Main  : TEXCOORD0;
    float3 normalDir : TEXCOORD1;
    float2 scrPos: TEXCOORD2;
			float dpth : fixed;
			float4 sh : TEXCOORD3;
   };
   ////////////////////////////////
   sampler2D _Dec;
   sampler2D _OrigTex;
   float4 _Color;
   float4x4 unity_Projector;
   float d;
        float _DeepAngle;

		sampler2D _MainTex;
   			sampler2D _FalloffTex;

		float4x4 _ShadowProjectionMatrix;

		sampler2D _ShadowMapTex;
		///////////////////////////
   v2f vert(appdata_img g,Input i)
   {
   v2f o;
                UNITY_INITIALIZE_OUTPUT(v2f, o);
//    if((_ScreenParams.x/_ScreenParams.y)!=1)
//		{
//		o.pos = mul(UNITY_MATRIX_MVP, i.vertex);
//		}
//             else {
              float x = g.texcoord.x;
             if(x!=1)
             x = x - floor(x);

             float y = g.texcoord.y;
             if(y!=1)
             y = y - floor(y);
            
             x = 2*x-1;

             #if UNITY_UV_STARTS_AT_TOP
             y = -2*y+1;
             #else
             y = 2*y-1;
             #endif

              o.pos = float4(x, y, 0, 1);
//              }
              o.normalDir = i.normal;
    o.uv_Main = mul (unity_Projector, i.vertex);//ComputeScreenPos(o.pos); o.uv_Main = mul (_Projector, i.vertex);
		o.scrPos = ComputeScreenPos(o.pos);
//		o.scrPos.xy = o.scrPos.xy/o.scrPos.w;


    o.sh = mul( unity_ObjectToWorld, i.vertex );
			o.sh = mul( _ShadowProjectionMatrix, o.sh );
			o.dpth = ( o.sh.z / o.sh.w );
//			o.uv_Main.xy = (o.uv_Main.xy/o.uv_Main.w) ;
    return o;
   }

   half sampleShadowmap( float2 uv, float depth )
		{
			half4 enc = tex2D (_ShadowMapTex, uv);
			return 1 - step(enc.r, depth);
		}

   half4 frag (v2f i) : COLOR
   {
    float Depth = sampleShadowmap( i.sh.xy, 1-i.dpth*0.995 );
   float3 normView = normalize(-float3(unity_Projector[2][0],unity_Projector[2][1], unity_Projector[2][2]));
    d = dot(i.normalDir, normView);
    half4 comp = tex2D(_Dec, i.uv_Main)*_Color; 

    	half4 falloff = tex2D(_FalloffTex, i.uv_Main);
    
    float2 wcoord = i.scrPos;
    half4 texcol0 = tex2D (_MainTex, wcoord);
    half4 texcol = tex2D (_OrigTex, wcoord);
    
//    return fixed4(texcol.rgb,texcol0.a-texcol.a*comp.a*Depth*min(d + _DeepAngle,1)*falloff.r);
//    return fixed4(texcol.rgb,texcol.a*comp.a*Depth*min(d + _DeepAngle,1)*falloff.r;
//texcol .a*
	fixed alpha = comp.a*(1-Depth)*min(d + _DeepAngle,1)*falloff.r;
    return fixed4(lerp(texcol0.rgb,texcol.rgb,alpha),lerp(texcol0.a,texcol.a,alpha));
//    return fixed4(lerp(texcol0.rgb,texcol.rgb,alpha),lerp(texcol0.a,texcol.a,alpha));
//    return fixed4(texcol.rgb,lerp(texcol0.a,texcol .a,alpha));

//    if(i.uv_Main.x>0&&i.uv_Main.x<1&&i.uv_Main.y>0&&i.uv_Main.y<1)
//    return fixed4(texcol.rgb,texcol .a*comp.a*Depth*min(d + _DeepAngle,1)*falloff.r);
//    else
//    return fixed4(texcol.rgb,texcol0 .a);
//    return fixed4(float3(i.uv_Main.x,i.uv_Main.y,0),texcol0 .a);
   }

   ENDCG

  }
    
    }
}
 

