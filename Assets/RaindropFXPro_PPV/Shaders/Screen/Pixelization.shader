Shader "Hidden/Custom/PixelationShader" {
	Properties {
		_MainTex("Base", 2D) = "" {}
	}

	CGINCLUDE

	#include "UnityCG.cginc"

	struct v2f {
		float4 pos : POSITION;
		float2 uv : TEXCOORD0;
	};

	sampler2D _MainTex;

	vector _MainTex_TexelSize;
	float _PixelSize;

	v2f vert(appdata_img v) {
		v2f o;
		o.pos = UnityObjectToClipPos(v.vertex);
		o.uv = v.texcoord.xy;
		return o;
	}

	half4 frag(v2f i) : COLOR {
		float ratioX = (int)(i.uv.x * _PixelSize) / _PixelSize;
		float ratioY = (int)(i.uv.y * _PixelSize) / _PixelSize;

		float4 mainColor = tex2D(_MainTex, float2(ratioX, ratioY));

		return mainColor;
	}

	ENDCG

	Subshader {
		Pass {
			ZTest Always Cull Off ZWrite Off
			Fog {
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