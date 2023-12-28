Shader "Custom/RaindropFX/WetSurfaceGrab" {
	Properties{
		_BumpAmt("Distortion", range(0,32)) = 10
		_IOR("IOR", range(0,1)) = 0.1
		_BumpDetailAmt("DetailDistortion", range(0,1)) = 0.5
		_TintAmt("Tint Amount", Range(0,1)) = 0.1
		_Roughness("Roughness", Range(0,30)) = 1.0
		_RoughIter("RoughIteration", Range(0.01,10)) = 0.2
		_Reflect("Reflect", Range(0,1)) = 0.3
		_FogAmt("Fog", Range(0,1)) = 0
		_FogItr("FogIteration", Range(0,10)) = 1

		_FogCol("FogColor", Color) = (1, 1, 1, 1)

		_MainTex("TintColor(RGB)", 2D) = "white" {}
		_BumpMap("NormalMap", 2D) = "bump" {}
		_FogMaskMap("WetMap", 2D) = "white" {}
		_Cube("Enviroment", Cube) = "_Skybox"{}
	}

	Category{
		Tags { "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Opaque" }

		SubShader {
			// Horizontal blur
			GrabPass {
				Tags { "LightMode" = "Always" }
			}

			Pass {
				Tags { "LightMode" = "Always" }

				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				#pragma fragmentoption ARB_precision_hint_fastest
				#include "UnityCG.cginc"

				sampler2D _FogMaskMap;

				sampler2D _GrabTexture;
				float4 _GrabTexture_TexelSize;
				float _Roughness;
				float _RoughIter;

				struct appdata_t {
					float4 vertex : POSITION;
					float2 texcoord: TEXCOORD0;
				};

				struct v2f {
					float4 vertex : POSITION;
					float4 uvgrab : TEXCOORD0;
					float2 uv : TEXCOORD1;
				};

				v2f vert(appdata_t v) {
					v2f o;
					o.vertex = UnityObjectToClipPos(v.vertex);
					o.uv = v.texcoord;
					#if UNITY_UV_STARTS_AT_TOP
					float scale = -1.0;
					#else
					float scale = 1.0;
					#endif
					o.uvgrab.xy = (float2(o.vertex.x, o.vertex.y * scale) + o.vertex.w) * 0.5;
					o.uvgrab.zw = o.vertex.zw;
					return o;
				}

				float4 frag(v2f i) : COLOR {
					float4 sum = float4(0,0,0,0);
					#define GRABPIXEL(weight,kernelx) tex2Dproj(_GrabTexture, UNITY_PROJ_COORD(float4(i.uvgrab.x + _GrabTexture_TexelSize.x * kernelx*_Roughness, i.uvgrab.y, i.uvgrab.z, i.uvgrab.w))) * weight
					sum += GRABPIXEL(0.05, -4.0);
					sum += GRABPIXEL(0.09, -3.0);
					sum += GRABPIXEL(0.12, -2.0);
					sum += GRABPIXEL(0.15, -1.0);
					sum += GRABPIXEL(0.18,  0.0);
					sum += GRABPIXEL(0.15, +1.0);
					sum += GRABPIXEL(0.12, +2.0);
					sum += GRABPIXEL(0.09, +3.0);
					sum += GRABPIXEL(0.05, +4.0);

					float fogMask = tex2D(_FogMaskMap, i.uv);
					float4 col = tex2Dproj(_GrabTexture, UNITY_PROJ_COORD(i.uvgrab));
					sum = lerp(sum, col, clamp(pow(fogMask, 1.0/_RoughIter) * 10.0, 0, 1));

					return sum;
				}
				ENDCG
			}

			// Vertical blur
			GrabPass {
				Tags { "LightMode" = "Always" }
			}

			Pass {
				Tags { "LightMode" = "Always" }

				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				#pragma fragmentoption ARB_precision_hint_fastest
				#include "UnityCG.cginc"

				sampler2D _FogMaskMap;

				sampler2D _GrabTexture;
				float4 _GrabTexture_TexelSize;
				float _Roughness;
				float _RoughIter;

				struct appdata_t {
					float4 vertex : POSITION;
					float2 texcoord: TEXCOORD0;
				};

				struct v2f {
					float4 vertex : POSITION;
					float4 uvgrab : TEXCOORD0;
					float2 uv : TEXCOORD1;
				};

				v2f vert(appdata_t v) {
					v2f o;
					o.vertex = UnityObjectToClipPos(v.vertex);
					o.uv = v.texcoord;
					#if UNITY_UV_STARTS_AT_TOP
					float scale = -1.0;
					#else
					float scale = 1.0;
					#endif
					o.uvgrab.xy = (float2(o.vertex.x, o.vertex.y * scale) + o.vertex.w) * 0.5;
					o.uvgrab.zw = o.vertex.zw;
					return o;
				}

				float4 frag(v2f i) : COLOR {
					float4 sum = float4(0,0,0,0);
					#define GRABPIXEL(weight,kernely) tex2Dproj( _GrabTexture, UNITY_PROJ_COORD(float4(i.uvgrab.x, i.uvgrab.y + _GrabTexture_TexelSize.y * kernely*_Roughness, i.uvgrab.z, i.uvgrab.w))) * weight
					sum += GRABPIXEL(0.05, -4.0);
					sum += GRABPIXEL(0.09, -3.0);
					sum += GRABPIXEL(0.12, -2.0);
					sum += GRABPIXEL(0.15, -1.0);
					sum += GRABPIXEL(0.18,  0.0);
					sum += GRABPIXEL(0.15, +1.0);
					sum += GRABPIXEL(0.12, +2.0);
					sum += GRABPIXEL(0.09, +3.0);
					sum += GRABPIXEL(0.05, +4.0);

					float fogMask = tex2D(_FogMaskMap, i.uv);
					float4 col = tex2Dproj(_GrabTexture, UNITY_PROJ_COORD(i.uvgrab));
					sum = lerp(sum, col, clamp(pow(fogMask, 1.0 / _RoughIter) * 10.0, 0, 1));

					return sum;
				}
				ENDCG
			}

			// Distortion
			GrabPass {
				Tags { "LightMode" = "Always" }
			}
			Pass {
				Tags { "LightMode" = "Always" }

				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				#pragma fragmentoption ARB_precision_hint_fastest
				#include "UnityCG.cginc"

				float _IOR;
				float _FogAmt;
				float _FogItr;
				float _Reflect;
				float _Roughness;
				float _BumpAmt;
				float _BumpDetailAmt;
				half _TintAmt;
				//float4 _RefWeight_ST;
				float4 _BumpMap_ST;
				float4 _MainTex_ST;
				float4 _FogCol;

				samplerCUBE _Cube;
				sampler2D _BumpMap;
				sampler2D _MainTex;
				//sampler2D _RefWeight;
				sampler2D _FogMaskMap;
				sampler2D _GrabTexture;
				float4 _GrabTexture_TexelSize;

				struct appdata_t {
					float4 vertex : POSITION;
					float2 texcoord: TEXCOORD0;
					float3 normal : NORMAL;
				};

				struct v2f {
					float4 vertex : POSITION;
					float4 uvgrab : TEXCOORD0;
					float2 uvbump : TEXCOORD1;
					float2 uvmain : TEXCOORD2;
					float3 reflex : NORMAL;
				};

				v2f vert(appdata_t v) {
					v2f o;
					o.vertex = UnityObjectToClipPos(v.vertex);
					#if UNITY_UV_STARTS_AT_TOP
					float scale = -1.0;
					#else
					float scale = 1.0;
					#endif
					o.uvgrab.xy = (float2(o.vertex.x, o.vertex.y * scale) + o.vertex.w) * 0.5;
					o.uvgrab.zw = o.vertex.zw;
					o.uvbump = TRANSFORM_TEX(v.texcoord, _BumpMap);
					o.uvmain = TRANSFORM_TEX(v.texcoord, _MainTex);

					float3 worldNormal = UnityObjectToWorldNormal(v.normal);
					float3 worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
					//float3 worldViewDir = WorldSpaceViewDir(v.vertex);
					float3 worldViewDir = UnityWorldSpaceViewDir(worldPos);
					o.reflex = reflect(-worldViewDir, worldNormal);
					return o;
				}

				float4 frag(v2f i) : COLOR {
					float4 tint = tex2D(_MainTex, i.uvmain);
					fixed3 bump = UnpackNormal(tex2D(_BumpMap, i.uvbump)).rgb;
					float2 offset = bump * _BumpAmt * 10.0 * _GrabTexture_TexelSize.xy + (0.05, 0.05) * (tint * _BumpDetailAmt + _IOR);
					i.uvgrab.xy = offset / i.uvgrab.z + i.uvgrab.xy;

					float4 col = tex2Dproj(_GrabTexture, UNITY_PROJ_COORD(i.uvgrab));

					float fogMask = tex2D(_FogMaskMap, i.uvmain);
					float4 ref = texCUBE(_Cube, i.reflex + bump * clamp(fogMask + 0.2, 0, 1) * _BumpAmt);
					//float4 ref = texCUBE(_Cube, i.reflex);
					float4 fcol = lerp(col, ref, _Reflect);
					fcol = lerp(fcol, tint, _TintAmt);
					col = lerp(col, tint, _TintAmt);

					float4 wet = clamp(pow(tex2D(_FogMaskMap, i.uvmain), 0.5) * _FogItr, 0, 1);
					col = lerp(col, col * wet + (_FogCol + col * 0.5) * (1.0 - wet), _FogAmt);
					col = lerp(col, ref, _Reflect * clamp(wet * wet, 0, 1));
					col = lerp(col, fcol, 1.0 - clamp(_FogAmt * 5, 0, 1));

					UNITY_APPLY_FOG(i.fogCoord, col);
					return col;
				}
				ENDCG
			}
		}
	}
}