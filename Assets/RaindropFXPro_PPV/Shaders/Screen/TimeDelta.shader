Shader "Hidden/Custom/TimeDelta" {
    Properties {
		_MainTex("Base", 2D) = "" {}
		_WipeTex("Wipe", 2D) = "" {}
	}

	CGINCLUDE

	#include "UnityCG.cginc"

	struct v2f {
		float4 pos : POSITION;
		float2 uv : TEXCOORD0;
	};

	sampler2D _MainTex;
	sampler2D _WipeTex;
	float _ratio;

	float2 hash22(float2 p) {
		p = float2(dot(p, float2(127.1, 311.7)), dot(p, float2(269.5, 183.3)));
		return -1.0 + 2.0 * frac(sin(p) * 43758.5453123);
	}

	float perlin(float2 p) {
		float2 pi = floor(p);
		float2 pf = p - pi;
		float2 w = pf * pf * (3.0 - 2.0 * pf);
		return lerp(lerp(dot(hash22(pi + float2(0.0, 0.0)), pf - float2(0.0, 0.0)),
			dot(hash22(pi + float2(1.0, 0.0)), pf - float2(1.0, 0.0)), w.x),
			lerp(dot(hash22(pi + float2(0.0, 1.0)), pf - float2(0.0, 1.0)),
				dot(hash22(pi + float2(1.0, 1.0)), pf - float2(1.0, 1.0)), w.x), w.y);
	}

	float fbm(float2 p) {
		float f = 0.0;
		p = p * 4.0;
		f += 1.0 * perlin(p);
		p = 2.0 * p;
		f += 0.5 * perlin(p);
		p = 2.0 * p;
		f += 0.25 * perlin(p);
		p = 2.0 * p;
		f += 0.125 * perlin(p);
		p = 2.0 * p;
		f += 0.0625 * perlin(p);
		return f;
	}

	v2f vert(appdata_img v) {
		v2f o;
		o.pos = UnityObjectToClipPos(v.vertex);
		o.uv = v.texcoord.xy;
		return o;
	}

	half4 frag(v2f i) : COLOR {
		float perlin_s = clamp(fbm(float2(i.uv.x * _ratio, i.uv.y) * 2.0), 0, 1);
		float4 mainColor = tex2D(_MainTex, i.uv);
		float alpha = tex2D(_WipeTex, i.uv).a;
		mainColor = clamp(mainColor * 0.988 * (1.0 - pow(perlin_s * 0.1, 2.0)) + alpha, 0, 1);

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
