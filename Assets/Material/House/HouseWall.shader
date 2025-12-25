Shader "Z/HouseWall"
{
    Properties
    {
        _C1 ("Color 1", Color) = (1,1,1,1)
        _C2 ("Color 2", Color) = (1,1,1,1)
        _C3 ("Color 3", Color) = (1,1,1,1)
        _C4 ("Color 4", Color) = (1,1,1,1)
        _C5 ("Color 5", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows
        #pragma target 3.0

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
        };

        fixed4 _C1, _C2, _C3, _C4, _C5;

        UNITY_INSTANCING_BUFFER_START(Props)
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            float col = clamp(IN.uv_MainTex.x + IN.uv_MainTex.y, 0, 1);
            // float col = length(_LightColor0)
            if(col > 0.5) o.Albedo = _C5;
            else if(col > 0.3) o.Albedo = _C4;
            else if(col > 0.2) o.Albedo = _C3;
            else if(col > 0.1) o.Albedo = _C2;
            else o.Albedo = _C1;
            o.Alpha = 1;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
