Shader "Hidden/Custom/ScreenBlendEffect_Ex" {
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
	sampler2D _AdditionalColTex;
	float _ColBlendAmount;
	sampler2D _HeightMap;
	float _Distortion;
	vector _MainTex_TexelSize;

	v2f vert(appdata_img v) {
		v2f o;
		o.pos = UnityObjectToClipPos(v.vertex);
		o.uv = v.texcoord.xy;
		return o;
	}

	half4 frag(v2f i) : COLOR{
		float4 mainColor;

		float2 nowPos = i.uv;
		nowPos.x -= _MainTex_TexelSize.x;
		float4 leftColor = tex2D(_HeightMap, nowPos);
		float xLeft = (leftColor.r + leftColor.g + leftColor.b) / 3.0;

		nowPos.x += 2.0 * _MainTex_TexelSize.x;
		float4 rightColor = tex2D(_HeightMap, nowPos);
		float xRight = (rightColor.r + rightColor.g + rightColor.b) / 3.0;

		nowPos.x -= _MainTex_TexelSize.x;
		nowPos.y += _MainTex_TexelSize.y;
		float4 upColor = tex2D(_HeightMap, nowPos);
		float yUp = (upColor.r + upColor.g + upColor.b) / 3.0;

		nowPos.y -= 2.0 * _MainTex_TexelSize.y;
		float4 downColor = tex2D(_HeightMap, nowPos);
		float yDown = (downColor.r + downColor.g + downColor.b) / 3.0;

		float xDelta = ((xLeft - xRight) + 1.0) * 0.5;
		float yDelta = ((yUp - yDown) + 1.0) * 0.5;

		mainColor = float4(clamp(xDelta, 0.0, 1.0), clamp(yDelta, 0.0, 1.0), 1.0f, 1.0f);

		half2 bump = (mainColor * 2 - 1).rgb;
		float4 addCol = tex2D(_AdditionalColTex, i.uv);
		mainColor = tex2D(_MainTex, i.uv + bump * _Distortion) * (1.0 - _ColBlendAmount) + (addCol * 0.5 + tex2D(_MainTex, i.uv) * 0.5) * _ColBlendAmount;
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