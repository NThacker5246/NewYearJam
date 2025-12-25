Shader "Z/Water"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
        _FACT("Factor", Vector) = (1,1,1,1)
    }
    SubShader
    {
        Tags { 
            "RenderType"="Transparent"
            "RenderQueue"="Transparent"
        }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows alpha:fade vertex:vert

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
        };

        static const uint permutation[] = { 151,160,137,91,90,15,                          // Hash lookup table as defined by Ken Perlin.  This is a randomly
            131,13,201,95,96,53,194,233,7,225,140,36,103,30,69,142,8,99,37,240,21,10,23,    // arranged array of all numbers from 0-255 inclusive.
            190, 6,148,247,120,234,75,0,26,197,62,94,252,219,203,117,35,11,32,57,177,33,
            88,237,149,56,87,174,20,125,136,171,168, 68,175,74,165,71,134,139,48,27,166,
            77,146,158,231,83,111,229,122,60,211,133,230,220,105,92,41,55,46,245,40,244,
            102,143,54, 65,25,63,161, 1,216,80,73,209,76,132,187,208, 89,18,169,200,196,
            135,130,116,188,159,86,164,100,109,198,173,186, 3,64,52,217,226,250,124,123,
            5,202,38,147,118,126,255,82,85,212,207,206,59,227,47,16,58,17,182,189,28,42,
            223,183,170,213,119,248,152, 2,44,154,163, 70,221,153,101,155,167, 43,172,9,
            129,22,39,253, 19,98,108,110,79,113,224,232,178,185, 112,104,218,246,97,228,
            251,34,242,193,238,210,144,12,191,179,162,241, 81,51,145,235,249,14,239,107,
            49,192,214, 31,181,199,106,157,184, 84,204,176,115,121,50,45,127, 4,150,254,
            138,236,205,93,222,114,67,29,24,72,243,141,128,195,78,66,215,61,156,180
        };

        float surflet(float2 uv, int2 grid, int2 per)
        {
            float2 diff = uv - grid;
            float2 poly = smoothstep(1.0f, 0.0f, abs(diff));
            float hash = permutation[(permutation[grid.x % per.x % 256] + grid.y) % per.y % 256];
            float s, c;
            sincos(hash * 6.28318530718f / 256.0f, s, c);
            float grad = diff.x * c + diff.y * s;
            return poly.x * poly.y * grad;
        }

        float noise(float2 uv, int2 per, uint numOctaves)
        {
            float v = 0;
            float amp = 1.0f;
            float freq = 1.0f;
            for (uint i = 0; i < numOctaves; ++i)
            {
                const float2 coord = uv * freq;
                const float o00 = surflet(coord, (int2)coord + int2(0, 0), per * int(freq));
                const float o10 = surflet(coord, (int2)coord + int2(1, 0), per * int(freq));
                const float o01 = surflet(coord, (int2)coord + int2(0, 1), per * int(freq));
                const float o11 = surflet(coord, (int2)coord + int2(1, 1), per * int(freq));
                v += amp * (o00 + o10 + o01 + o11);
                amp *= 0.5f;
                freq *= 2.0f;
            }
            return v * 0.5f + 0.5f;
        }
        float4 _FACT;

        float random(in float2 uv){
            //return sin((cos(sin(uv.x + uv.y) / uv.y + cos(uv.y - uv.x) * uv.x / uv.y)) * uv.x);
            uv.x += cos(_Time.x)/10000;
            return frac(sin(dot(uv, float2(12.9898 * _FACT.x, 78.233 * _FACT.y))) * 43758.5453123 * _FACT.z) * (cos(uv.x - uv.y) / cos(uv.x + uv.y));
        }


        void vert(inout appdata_full v, out Input o) {
            UNITY_INITIALIZE_OUTPUT(Input, o);
            // fixed tcc = noise(v.vertex.xz, int2(32, 32), 2);
            // v.vertex.y += tcc;
            v.vertex.y += random(v.vertex.xz) * 0.0625;
        }

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // Albedo comes from a texture tinted by color

            fixed4 c = _Color;
            o.Albedo = c.rgb;
            // Metallic and smoothness come from slider variables
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
