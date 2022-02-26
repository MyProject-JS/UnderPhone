Shader "Custom/Light_Shader"
{
    Properties
    {
        _R("R", Range(0.0, 2.0)) = 1.0
        _G("G", Range(0.0, 2.0)) = 1.0
        _B("B", Range(0.0, 2.0)) = 1.0
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}    
        //_Red("R", Range(1,10)) = 5;
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Standard
        #pragma target 3.0

        sampler2D _MainTex;

        float _R;
        float _G;
        float _B;

        struct Input
        {
            float2 uv_MainTex;
        };

        fixed4 _Color;

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            o.Emission = float3(_R, _G, _B);
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
