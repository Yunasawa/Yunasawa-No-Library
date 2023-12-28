Shader "Hidden/Custom/ScreenBlendEffect" {
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
	sampler2D _HeightMap;
	sampler2D _WetMap;
	sampler2D _WipeMap;
	sampler2D _CullMask;

	vector _MainTex_TexelSize;
	float _PixelSize;
	float4 _TintColor;
	float _FogIntensity;
	float _Distortion;
	int _FogIteration;
	int _IsUseFog;
	int _TintWeight;
	int _IsEnableWip;

	v2f vert(appdata_img v) {
		v2f o;
		o.pos = UnityObjectToClipPos(v.vertex);
		o.uv = v.texcoord.xy;
		return o;
	}

	half4 frag(v2f i) : COLOR {
		float4 mainColor;

		float ratioX = (int)(i.uv.x * _PixelSize) / _PixelSize;
		float ratioY = (int)(i.uv.y * _PixelSize) / _PixelSize;

		float2 nowPos = float2(ratioX, ratioY);
		if (_PixelSize < 0) nowPos = float2(i.uv.x, i.uv.y);
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
		mainColor = tex2D(_MainTex, i.uv + bump * _Distortion * tex2D(_CullMask, i.uv).r);

		// tint
		mainColor = lerp(mainColor, mainColor * _TintColor, pow(tex2D(_HeightMap, i.uv), 1.0 / _TintWeight));

		if (_IsUseFog) {
			float4 wet = clamp(pow(tex2D(_WetMap, i.uv) + (_IsEnableWip ? tex2D(_WipeMap, i.uv) : 0), 0.5) * _FogIteration, 0, 1);
			//float4 wet = clamp(pow(tex2D(_WetMap, i.uv), 0.5) * _FogIteration, 0, 1);
			mainColor = mainColor * wet + ((1, 1, 1, 1) * _FogIntensity + mainColor * 0.5) * (1.0 - wet);
		}

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