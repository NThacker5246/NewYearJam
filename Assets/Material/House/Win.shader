Shader "Z/Win"
{
    Properties
    {
        _Emmite ("Color", Color) = (1,1,1,1)
        _Strength ("Strength", Float) = 3
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows
        #pragma target 3.0

        struct Input
        {
            float2 uv_MainTex;
        };

        float4 _Emmite;
        float _Strength;

        UNITY_INSTANCING_BUFFER_START(Props)
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
        	o.Emission = _Emmite * _Strength;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
