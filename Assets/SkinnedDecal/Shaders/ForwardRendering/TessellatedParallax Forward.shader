Shader "SkinnedDecals/Forward Rendering/Tessellated Parallax" {
    Properties {
        _MainTex ("Albedo (RGB) Alpha (A)", 2D) = "white" {}
        _Color ("Main Color (RGB) Alpha (A)", Color) = (1, 1, 1, 1)
        _BumpMap ("Normals", 2D) = "bump" {}
        _SpecularMap("Specular map", 2D) = "white" {}
        _SpecularColor ("Specular Color", Color) = (1, 1, 1, 1)
        _Shininess ("Shininess", Range(0,1)) = 0.5
        _ParallaxMap ("Height", 2D) = "gray" {}
        _Tess ("Tessellation", Range(1,32)) = 4
        _Displacement ("Displacement", Range(0, 0.1)) = 0.01
        _Parallax ("Parallax Amount", Range(0.005, 0.08)) = 0.02
        _MinDistance ("Minimum Distance", Range(0, 50))= 10
        _MaxDistance ("Max Distance", Range(0, 50))= 25
 
        _AlphaClip ("Alpha Clip", Range(0, 1))= 0.01
    }
    SubShader {
        Tags { "RenderType"="Opaque" "Queue"="Geometry+5" }
        Fog { Mode Off }
        Offset -1, -1
        LOD 600
 
        Blend One OneMinusSrcAlpha
     
        CGPROGRAM
        #pragma surface surf StandardSpecular noshadow vertex:disp tessellate:tessDistance nolightmap alpha
        #pragma target 4.6
 
        #include "Tessellation.cginc"
 
        sampler2D _MainTex;
        sampler2D _BumpMap;
        sampler2D _SpecularMap;
        sampler2D _ParallaxMap;
 
        float4 _Color;
        float4 _SpecularColor;
 
        half _Shininess;
        float _Tess;
        float _Displacement;
        float _Parallax;
        float _MinDistance, _MaxDistance;
 
        float _AlphaClip;
 
        struct appdata {
                float4 vertex : POSITION;
                float4 tangent : TANGENT;
                float3 normal : NORMAL;
                float2 texcoord : TEXCOORD0;
            };
               
        float4 tessDistance (appdata v0, appdata v1, appdata v2) {
            return UnityDistanceBasedTess(v0.vertex, v1.vertex, v2.vertex, _MinDistance, _MaxDistance, _Tess);
        }
 
        void disp (inout appdata v)
        {
            float d = saturate(tex2Dlod(_ParallaxMap, float4(v.texcoord.xy,0,0)).r -0.515) * 2 * _Displacement;
            v.vertex.xyz += v.normal * d;
        }
 
        struct Input {
            float2 uv_MainTex;
            float3 viewDir;
        };
 
        void surf (Input IN, inout SurfaceOutputStandardSpecular o) {
            // Parallax
            fixed2 uvs= IN.uv_MainTex;
            fixed4 height = tex2D(_ParallaxMap, IN.uv_MainTex);
            float clampedHeight = min(0.5, height.r);
            float2 parallax = ParallaxOffset(clampedHeight, _Parallax, IN.viewDir);
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
            clip(o.Alpha - _AlphaClip);
        }
 
        ENDCG
    }
    FallBack "Diffuse"
}