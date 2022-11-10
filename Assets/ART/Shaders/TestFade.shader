Shader "Custom/TestFade"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
        _FuzzyFade("FuzzyFade", Range(0,1)) = 0.0
        _FuzzyPatternSize ("Fuzzy Pattern Size", Range(0.005,10)) = 1.22
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
            float4 screenPos;
        };

        half _Glossiness;
        half _Metallic;
        half _FuzzyFade;
        float _FuzzyPatternSize;
        fixed4 _Color;


        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {

            if (_FuzzyFade > 0.0f) {
                float2 screenPos = IN.screenPos.xy / IN.screenPos.w;
                screenPos.x *= _ScreenParams.x / _ScreenParams.y;
                screenPos /= float2(_FuzzyPatternSize, _FuzzyPatternSize);
                screenPos += float2(sin(screenPos.x * 55.0f + screenPos.y * 35.0f), sin(screenPos.x * 85.0f + screenPos.y * 66.0f)) * 0.01f;
                float2 roundedPos = round(screenPos * 25.0f) / 25.0f;

                if (length(screenPos - roundedPos) > (1.0f / 25.0f * (1.0f - _FuzzyFade) / 2.0f * 1.414213562f)) discard;
            }

            // Albedo comes from a texture tinted by color
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;

            o.Albedo = c.rgb;
            
            // Metallic and smoothness come from slider variables
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = 1.0f;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
