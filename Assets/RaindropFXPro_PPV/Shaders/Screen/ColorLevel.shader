Shader "Hidden/Custom/ColorLevel" {
	Properties{
		_MainTex("Base", 2D) = "" {}
	}

		CGINCLUDE

#include "UnityCG.cginc"

		struct v2f {
			float4 pos : POSITION;
			float2 uv : TEXCOORD0;
		};

		sampler2D _MainTex;
		float4 _MainTex_ST;

		float _inBlack;
		float _inWhite;
		float _outWhite;
		float _outBlack;

		v2f vert(appdata_img v) {
			v2f o;
			o.pos = UnityObjectToClipPos(v.vertex);
			o.uv = v.texcoord.xy;
			return o;
		}

		float GetPixelLevel(float inPixel) {
			return (((inPixel * 255.0) - _inBlack) / (_inWhite - _inBlack) * (_outWhite - _outBlack) + _outBlack) / 255.0;
		}

		half4 frag(v2f i) : COLOR{
			float4 mainColor = tex2D(_MainTex, i.uv);
			float4 resColor = float4(GetPixelLevel(mainColor.r), GetPixelLevel(mainColor.g), GetPixelLevel(mainColor.b), 1.0);
			return resColor;
		}

			ENDCG

			Subshader {
			Pass{
				ZTest Always Cull Off ZWrite Off
				Fog{
					Mode off
				}

				CGPROGRAM
				#pragma fragmentoption ARB_precision_hint_fastest 
				#pragma vertex vert
				#pragma fragment frag
				ENDCG
			}
		}

		Fallback off
}