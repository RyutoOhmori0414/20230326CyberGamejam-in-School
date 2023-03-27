Shader "Custom/PostEffect"
{
    SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always

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
                float4 color : COLOR;
                float2 line_uv : TEXCOORD1;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float4 color : COLOR;
                float2 line_uv : TEXCOORD1;
            };

            float rand (float2 co)
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
                            lerp(dot(v01, f - float2(0, 1)), dot(v10, f - float2(1, 1)), u.x),
                            u.y) + 0.5f;
            }

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _TextureSampleAdd;
            float4 _LineColor;
            float _LineSpeed;
            float _LineSize;
            float _ColorGap;
            float _FrameRate;
            float _Frequency;
            float _GlitchScale;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = TransformObjectToHClip(v.vertex.xyz);
                o.uv = v.uv;
                o.color = v.color;
                o.line_uv = v.line_uv - _Time.z * _LineSpeed;
                return o;
            }

            float4 frag (v2f i) : SV_Target
            {
                float2 uv = i.uv;
                //RGBをずらす
                float2 ra = tex2D(_MainTex, uv + _ColorGap * perlinNoise(_Time.z)).ra + _TextureSampleAdd.ra;
                ra.x *= ra.y;
                float2 ba = tex2D(_MainTex, uv - _ColorGap * perlinNoise(_Time.z)).ba + _TextureSampleAdd.ba;
                ba.x *= ba.y;
                float2 ga = tex2D(_MainTex, uv).ga + _TextureSampleAdd.ga;
                ga.x *= ga.y;
                float4 shiftColor = float4(ra.x, ga.x, ba.x, (ra.y + ga.y + ba.y) / 3);
                //ノズルラインの補完値の計算
                float interpolation = step(frac(i.line_uv.y * 15), _LineSize);
                //ノズルラインを含むピクセルカラー
                float4 noiseLineColor = lerp(shiftColor, _LineColor, interpolation);
                //float posterize = floor(frac(perlinNoise(frac(_Time)) * 10) / (1 / _FrameRate)) * (1 / _FrameRate);
                ////uv.y方向のノイズ計算
                //float noiseY = 2.0 * rand(posterize) - 0.5;
                ////グリッチの高さの補完値計算 どの高さに出現するかは時間でランダム
                //float glitchLine1 = step(uv.y - noiseY, rand(uv));
                //float glitchLine2 = step(uv.y - noiseY, 0);
                //float glitch = saturate(glitchLine1 - glitchLine2);
                ////uv.x方向のノイズ計算 -0.1  < random < 1.0;
                //float noiseX = (2.0 * rand(posterize) - 0.5) * 0.1;
                //float frequency = step(abs(noiseX), _Frequency);
                //noiseX += frequency;
                ////グリッチ適用
                //uv.x = lerp(uv.x, uv.x + noiseX * _GlitchScale, glitch);
                float4 noiseColor = tex2D(_MainTex, uv) + _TextureSampleAdd;
                noiseLineColor.rgb *= noiseLineColor.a;
                noiseColor *= 1 - noiseLineColor.a;
                float4 finalColor = noiseLineColor + noiseColor;

                return finalColor;
            }
            ENDHLSL
        }
    }
}
