Shader "Hidden/Custom/DropletBlur" {
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
	sampler2D _BlurredMainTex;
	sampler2D _MaskMap;
	float _focalize;

	v2f vert(appdata_img v) {
		v2f o;
		o.pos = UnityObjectToClipPos(v.vertex);
		o.uv = v.texcoord.xy;
		return o;
	}

	half4 frag(v2f i) : COLOR{
		float4 mainColor;

		half4 mask = tex2D(_MaskMap, i.uv);
		//float greyScale = pow((mask.r + mask.g + mask.b) / 3.0, 1.0 / _focalize);
		float greyScale = clamp(pow(mask.r, _focalize / 2.5) * 10.0, 0, 1);
		mainColor = tex2D(_MainTex, i.uv) * (1.0 - greyScale) + tex2D(_BlurredMainTex, i.uv) * greyScale;
		return mainColor;
	}

		ENDCG

		Subshader {
		Pass{
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