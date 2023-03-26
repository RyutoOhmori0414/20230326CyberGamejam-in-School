Shader "Custom/Glitch"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _LineColor ("LineColor", Color) = (0,0,0,0)
        _LineSpeed("LineSpeed",Range(0,10)) = 5
        _LineSize("LineSize",Range(0,1)) = 0.01
        _ColorGap("ColorGap",Range(0,1.0)) = 0.01
        _Alpha ("Alpha", Range(0,1)) = 0.5
        _FrameRate ("FrameRate", Range(0,30)) = 15
        _Frequency  ("Frequency", Range(0,1)) = 0.1
        _GlitchScale  ("GlitchScale", Range(1,10)) = 1
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Blend One One

        Pass
        {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag

           
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float2 line_uv : TEXCOORD1;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float2 line_uv : TEXCOORD1;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _LineColor;
            float _LineSpeed;
            float _Linesize;
            float _ColorGap;
            float _Alpha;
            float _FrameRate;
            float _Frequency;
            float _GlitchScale;

            //�����_��
            float rand(float2 co)
            {
                return frac(sin(dot(co, float2(12.9898, 78.233))) * 43758.5453);
            }

            float perlinNoise(float2 st)
            {
                float2 p = floor(st);
                float2 f = frac(st);
                float2 u = f * f * (3.0 - 2.0 * f);

                float v00 = rand(p + float2(0, 0));
                float v10 = rand(p + float2(1, 0));
                float v01 = rand(p + float2(0, 1));
                float v11 = rand(p + float2(1, 1));

                return lerp(lerp(dot(v00, f - float2(0, 0)), dot(v10, f - float2(1, 0)), u.x),
                            lerp(dot(v01, f - float2(0, 1)), dot(v11, f - float2(1, 1)), u.x),
                            u.y) + 0.5f;
            }

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = TransformObjectToHClip(v.vertex.xyz);
                o.uv = v.uv;
                o.line_uv = v.line_uv - _Time.z * _LineSpeed;
                return o;
            }

            float4 frag (v2f i) : SV_Target
            {
                float2 uv = i.uv;
                //RGB�����炷
                float r = tex2D(_MainTex, uv + _ColorGap * perlinNoise(_Time.z)).r;
                float b = tex2D(_MainTex, uv - _ColorGap * perlinNoise(_Time.z)).b;
                float2 ga = tex2D(_MainTex, uv).ga;
                float4 shiftColor = float4(r, ga.x, b, ga.y);
                //�m�Y�����C���̕⊮�l�̌v�Z
                float interpolation = step(frac(i.line_uv.y * 15), _Linesize);
                //�m�Y�����C�����܂ރs�N�Z���J���[
                float4 noiseLineColor = lerp(shiftColor, _LineColor, interpolation);
                float posterize = floor(frac(perlinNoise(frac(_Time)) * 10) / (1 / _FrameRate)) * (1 / _FrameRate);
                //uv.y�����̃m�C�Y�v�Z -1 < random < 1
                float noiseY = 2.0 * rand(posterize) - 0.5;
                //�O���b�`�̍����̕⊮�l�v�Z �x�̍����ɏo�����邩�͎��ԂŃ����_��
                float glitchLine1 = step(uv.y - noiseY, rand(uv));
                float glitchLine2 = step(uv.y - noiseY, 0);
                float glitch = saturate(glitchLine1 - glitchLine2);
                //uv.x�����̃m�C�Y�v�Z -0.1 < random < 1.0
                float noiseX = (2.0 * rand(posterize) - 0.5) * 0.1;
                float frequency = step(abs(noiseX), _Frequency);
                noiseX *= frequency;
                //�O���b�`�K�p
                uv.x = lerp(uv.x, uv.x + noiseX * _GlitchScale, glitch);
                float4 noiseColor = tex2D(_MainTex, uv);
                float4 finalColor = noiseLineColor * noiseColor;
                
                return finalColor;
            }
            ENDHLSL
        }
    }
}
