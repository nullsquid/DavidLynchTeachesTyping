// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Hidden/TOZ/ImageFX/Wiggle" {
	Properties {
		_MainTex("Base (RGB)", 2D) = "white" {}
	}

	SubShader {
		Pass {
			Name "BASE"
			Tags { "LightMode" = "Always" }

			ZTest Always Cull Off ZWrite Off Fog { Mode off }

			CGPROGRAM
			#include "UnityCG.cginc"
			#pragma vertex vert
			#pragma fragment frag

			uniform sampler2D _MainTex;
			uniform float4 _MainTex_TexelSize;
			uniform float _Speed;
			uniform float _Amplitude;

			v2f_img vert(appdata_img v) {
				v2f_img o;
				o.pos = UnityObjectToClipPos(v.vertex);
				o.uv = v.texcoord.xy;
				return o;
			}

			fixed4 frag(v2f_img i) : SV_Target {
				i.uv.x += sin(_Time.y + i.uv.x * _Speed) * _Amplitude;
				i.uv.y += cos(_Time.y + i.uv.y * _Speed) * _Amplitude;
				return tex2D(_MainTex, i.uv);
			}
			ENDCG
		}
	}

	Fallback off
}