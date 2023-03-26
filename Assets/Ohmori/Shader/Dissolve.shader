Shader "Custom/Dissolve"
{
    Properties
    {
        [ParRendererData]_MainTex ("Texture", 2D) = "white" {}
        [HDR]_Color ("Main Color", Color) = (1, 1, 1, 1)
        _DissolveTex ("Dissolve Tex", 2D) = "white" {}
        [HDR]_DissolveColor ("Dissolve Color", Color) = (1, 1, 1, 1)
        _DissolveAmount ("Dissolve Amount", Range(0, 1)) = 0.2
        _DissolveRange ("Dissolve Range", Range(0, 1)) = 0.1
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100
        Blend One OneMinusSrcAlpha

        Pass
        {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct appdata
            {
                float4 vertex : POSITION;
                float4 color : COLOR;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float2 duv : TEXCOORD1;
                float4 color : COLOR;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _TextureSampleAdd;

            //Dissolve
            sampler2D _DissolveTex;
            float4 _DissolveTex_ST;
            float4 _DissolveColor;
            float _DissolveAmount;
            float _DissolveRange;


            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = TransformObjectToHClip(v.vertex);
                o.color = v.color;
                o.uv = TRANSFORM_TEX(v.uv, _MainTex) * _MainTex_ST;
                o.duv = TRANSFORM_TEX(v.uv, _DissolveTex) * _DissolveTex_ST;
                return o;
            }

            float remap (float value, float outMin)
            {
                return value * ((1 - outMin) / 1) + outMin;
            }

            float4 frag (v2f i) : SV_Target
            {
                float4 col = (tex2D(_MainTex, i.uv) + _TextureSampleAdd) * i.color;

                // Dissolve
                float dissolveAlpha = tex2D(_DissolveTex, i.duv).r;
                float amount = remap(_DissolveAmount, -_DissolveRange);

                if (dissolveAlpha < amount + _DissolveRange)
                {
                    col.rgb += _DissolveColor.rgb;
                }

                if (dissolveAlpha < amount)
                {
                    col.a = 0;
                }

                col.rgb *= col.a;

                return col;
            }
            ENDHLSL
        }
    }
}
