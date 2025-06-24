Shader "AdventuresOfBlink/NeonGlow"
{
    Properties
    {
        _BaseColor ("Base Color", Color) = (1,1,1,1)
        _EmissionColor ("Emission Color", Color) = (0,1,1,1)
        _EmissionStrength ("Emission Strength", Range(0,10)) = 1
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" "RenderPipeline"="UniversalRenderPipeline" }
        LOD 100

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
            };

            Varyings vert(Attributes v)
            {
                Varyings o;
                o.positionHCS = TransformObjectToHClip(v.positionOS);
                return o;
            }

            half4 _BaseColor;
            half4 _EmissionColor;
            half _EmissionStrength;

            half4 frag(Varyings i) : SV_Target
            {
                half3 col = _BaseColor.rgb;
                half3 emission = _EmissionColor.rgb * _EmissionStrength;
                return half4(col + emission, _BaseColor.a);
            }
            ENDHLSL
        }
    }
}
