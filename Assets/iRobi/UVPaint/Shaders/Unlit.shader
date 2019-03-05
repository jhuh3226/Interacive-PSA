Shader "iRobi/Unlit" {
    Properties {
        _MainTex ("Base (RGB)", 2D) = "white" {}
    }
    Category {
    Tags {"IgnoreProjector"="True" "Queue" = "Background" }
       Lighting Off
       Cull Back
       ZWrite Off 
       SubShader {
            Pass {
               SetTexture [_MainTex] {
                    Combine texture
                 }
            }
        } 
    }
}