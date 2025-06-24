Shader "AdventuresOfBlink/HologramBarrier"
{
    Properties
    {
        _BaseColor ("Base Color", Color) = (0,1,1,0.2)
        _LineColor ("Line Color", Color) = (0,1,1,0.8)
        _LineFrequency ("Line Frequency", Float) = 20
    }
    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" "RenderPipeline"="UniversalRenderPipeline" }
        LOD 100
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct Attributes
            {
                float4 positionOS : POSITION;
            };

            struct Varyings
            {
                float4 positionHCS : SV_POSITION;
                float3 positionWS : TEXCOORD0;
            };

            Varyings vert(Attributes v)
            {
                Varyings o;
                o.positionHCS = TransformObjectToHClip(v.positionOS);
                o.positionWS = TransformObjectToWorld(v.positionOS.xyz);
                return o;
            }

            half4 _BaseColor;
            half4 _LineColor;
            float _LineFrequency;

            half4 frag(Varyings i) : SV_Target
            {
                float lineMask = step(0.5, frac(i.positionWS.x * _LineFrequency));
                half alpha = lerp(_BaseColor.a, _LineColor.a, lineMask);
                half3 color = _BaseColor.rgb + lineMask * _LineColor.rgb;
                return half4(color, alpha);
            }
            ENDHLSL
        }
    }
}
