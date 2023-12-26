Shader "Yunasawa/2D/Sprite Blur/Box Blur" 
{
    Properties 
    {
        _MainTex ("Sprite Texture", 2D) = "white" {}
        _BlurAmount ("Blur Amount", Range(0, 0.2)) = 0.1
    }
 
    SubShader 
    {
        Tags { "Queue" = "Transparent" "RenderType" = "Transparent" }
        Pass {
            CGPROGRAM

            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
 
            struct appdata_t 
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };
 
            struct v2f 
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };
 
            sampler2D _MainTex;
            float _BlurAmount;
 
            v2f vert (appdata_t v) 
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }
 
            half4 frag (v2f i) : SV_Target 
            {
                half4 color = tex2D(_MainTex, i.uv);
                half4 blurredColor = 0;
                int blurIterations = 5;
 
                for (int j = 0; j < blurIterations; j++) 
                {
                    float2 offset = float2(j - blurIterations / 2, 0) * _BlurAmount;
                    blurredColor += tex2D(_MainTex, i.uv + offset);
                }
 
                blurredColor /= blurIterations;
                return blurredColor;
            }
            ENDCG
        }
    }
}
