Shader "Yunasawa/2D/Sprite Pixelate" 
{
    Properties 
    {
        _MainTex ("Texture", 2D) = "white" {}
        _PixelateFactor ("Pixelate Factor", Range(1, 2048)) = 5
    }
 
    SubShader 
    {
        Tags { "RenderType"="Opaque" }
        Pass 
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
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
            float _PixelateFactor;
 
            v2f vert (appdata_t v) {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }
 
            half4 frag (v2f i) : SV_Target {
                float2 uv = i.uv * _PixelateFactor;
                uv = floor(uv) / _PixelateFactor;
                return tex2D(_MainTex, uv);
            }
            ENDCG
        }
    }
}
