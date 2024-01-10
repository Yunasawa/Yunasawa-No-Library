Shader "Custom/GaussianBlur" {
    Properties {
        _MainTex ("Texture", 2D) = "white" {}
        _Radius ("Radius", Range(0, 30)) = 5
        _Resolution ("Resolution", float) = 800
        _Step ("Step", Range(0, 0.1)) = 0.005
    }
    SubShader {
        Tags { "Queue" = "Transparent" "IgnoreProjector" = "true" "RenderType" = "Transparent" }
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha
        Cull Off
        Pass {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma fragmentoption ARB_precision_hint_fastest
            #include "UnityCG.cginc"

            struct appdata_t {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float _Radius;
            float _Resolution;
            float _Step;

            v2f vert (appdata_t v) {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            half4 GaussianBlur(float2 uv, float2 offset) {
                half4 col = half4(0, 0, 0, 0);
                float kernelSum = 0;
                int upper = (int)((_Radius - 1) / 2);
                int lower = -upper;
                for (int x = lower; x <= upper; ++x) {
                    for (int y = lower; y <= upper; ++y) {
                        kernelSum++;
                        float2 blurOffset = float2(_Step * x, _Step * y);
                        col += tex2D(_MainTex, uv + offset + blurOffset);
                    }
                }
                col /= kernelSum;
                return col;
            }

            half4 frag (v2f i) : SV_Target {
                half4 horizontalBlur = GaussianBlur(i.uv, float2(_Step, 0));
                half4 verticalBlur = GaussianBlur(i.uv, float2(0, _Step));
                half4 diagonalBlur1 = GaussianBlur(i.uv, float2(_Step, _Step));
                half4 diagonalBlur2 = GaussianBlur(i.uv, float2(-_Step, _Step));

                half4 finalColor = (horizontalBlur + verticalBlur + diagonalBlur1 + diagonalBlur2) / 4.0;
                return finalColor;
            }
            ENDCG
        }
    }
    Fallback "Sprites/Default"
}
