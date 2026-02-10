// Pixel Art Lit Shader for Emberveil HD-2D
// For 3D environment surfaces with pixel-art textures.
// Lit by URP lights, receives and casts shadows, point-filtered textures.
//
// Features:
// - Point-filtered texture sampling enforced in shader
// - Full URP lighting (directional, point, spot lights)
// - Shadow casting and receiving
// - No specular — flat matte surface appropriate for pixel art
// - Tint color for variation and hover highlighting
Shader "Emberveil/PixelArtLit"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _BaseColor ("Base Color", Color) = (1,1,1,1)
        [Toggle] _PointFilter ("Force Point Filtering", Float) = 1
    }

    SubShader
    {
        Tags
        {
            "RenderType" = "Opaque"
            "Queue" = "Geometry"
            "RenderPipeline" = "UniversalPipeline"
        }

        Pass
        {
            Name "ForwardLit"
            Tags { "LightMode" = "UniversalForward" }

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile _ _MAIN_LIGHT_SHADOWS _MAIN_LIGHT_SHADOWS_CASCADE
            #pragma multi_compile _ _ADDITIONAL_LIGHTS
            #pragma multi_compile_fog

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"

            struct Attributes
            {
                float4 positionOS : POSITION;
                float3 normalOS : NORMAL;
                float2 uv : TEXCOORD0;
            };

            struct Varyings
            {
                float4 positionCS : SV_POSITION;
                float2 uv : TEXCOORD0;
                float3 normalWS : TEXCOORD1;
                float3 positionWS : TEXCOORD2;
                float fogCoord : TEXCOORD3;
            };

            TEXTURE2D(_MainTex);
            // Use a point-filter sampler for crisp pixel art
            SamplerState sampler_point_clamp;

            CBUFFER_START(UnityPerMaterial)
                float4 _MainTex_ST;
                float4 _MainTex_TexelSize;
                half4 _BaseColor;
                half _PointFilter;
            CBUFFER_END

            Varyings vert(Attributes input)
            {
                Varyings output;
                VertexPositionInputs vertexInput = GetVertexPositionInputs(input.positionOS.xyz);
                VertexNormalInputs normalInput = GetVertexNormalInputs(input.normalOS);

                output.positionCS = vertexInput.positionCS;
                output.positionWS = vertexInput.positionWS;
                output.normalWS = normalInput.normalWS;
                output.uv = TRANSFORM_TEX(input.uv, _MainTex);
                output.fogCoord = ComputeFogFactor(vertexInput.positionCS.z);

                return output;
            }

            half4 frag(Varyings input) : SV_Target
            {
                // Sample with point filtering for crisp pixel art
                half4 texColor = SAMPLE_TEXTURE2D(_MainTex, sampler_point_clamp, input.uv);
                half4 color = texColor * _BaseColor;

                // Lighting
                float3 normalWS = normalize(input.normalWS);

                // Main light
                float4 shadowCoord = TransformWorldToShadowCoord(input.positionWS);
                Light mainLight = GetMainLight(shadowCoord);
                half NdotL = saturate(dot(normalWS, mainLight.direction));
                half3 diffuse = mainLight.color.rgb * NdotL * mainLight.shadowAttenuation;

                // Ambient
                half3 ambient = SampleSH(normalWS);

                // Additional lights (point lights for interiors, Glove glow, etc.)
                #ifdef _ADDITIONAL_LIGHTS
                uint additionalLightsCount = GetAdditionalLightsCount();
                for (uint i = 0u; i < additionalLightsCount; ++i)
                {
                    Light additionalLight = GetAdditionalLight(i, input.positionWS);
                    half addNdotL = saturate(dot(normalWS, additionalLight.direction));
                    diffuse += additionalLight.color.rgb * addNdotL * additionalLight.distanceAttenuation * additionalLight.shadowAttenuation;
                }
                #endif

                color.rgb *= (ambient + diffuse);

                // Fog
                color.rgb = MixFog(color.rgb, input.fogCoord);

                return color;
            }
            ENDHLSL
        }

        // Shadow caster pass
        Pass
        {
            Name "ShadowCaster"
            Tags { "LightMode" = "ShadowCaster" }

            ZWrite On
            ZTest LEqual

            HLSLPROGRAM
            #pragma vertex ShadowVert
            #pragma fragment ShadowFrag

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct Attributes
            {
                float4 positionOS : POSITION;
                float3 normalOS : NORMAL;
            };

            struct Varyings
            {
                float4 positionCS : SV_POSITION;
            };

            float3 _LightDirection;

            Varyings ShadowVert(Attributes input)
            {
                Varyings output;
                float3 positionWS = TransformObjectToWorld(input.positionOS.xyz);
                float3 normalWS = TransformObjectToWorldNormal(input.normalOS);
                positionWS = positionWS + normalWS * 0.001; // Bias
                output.positionCS = TransformWorldToHClip(positionWS);
                return output;
            }

            half4 ShadowFrag(Varyings input) : SV_Target
            {
                return 0;
            }
            ENDHLSL
        }

        // Depth pass for depth of field
        Pass
        {
            Name "DepthOnly"
            Tags { "LightMode" = "DepthOnly" }

            ZWrite On
            ColorMask 0

            HLSLPROGRAM
            #pragma vertex DepthVert
            #pragma fragment DepthFrag

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct Attributes
            {
                float4 positionOS : POSITION;
            };

            struct Varyings
            {
                float4 positionCS : SV_POSITION;
            };

            Varyings DepthVert(Attributes input)
            {
                Varyings output;
                output.positionCS = TransformObjectToHClip(input.positionOS.xyz);
                return output;
            }

            half4 DepthFrag(Varyings input) : SV_Target
            {
                return 0;
            }
            ENDHLSL
        }
    }

    FallBack "Universal Render Pipeline/Lit"
}
