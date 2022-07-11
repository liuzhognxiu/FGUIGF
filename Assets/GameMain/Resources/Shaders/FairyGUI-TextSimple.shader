// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "FairyGUI/TextSimple"
{
	Properties
	{
		_MainTex ("Alpha (A)", 2D) = "white" {}
	}

	SubShader
	{
		LOD 100

		Tags
		{
			"Queue" = "Transparent"
			"IgnoreProjector" = "True"
			"RenderType" = "Transparent"
		}

		Cull Off
		Lighting Off
		ZWrite Off
		Fog { Mode Off }
		Blend SrcAlpha OneMinusSrcAlpha

		Pass
		{
			CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag

				#include "UnityCG.cginc"

				struct appdata_t
				{
					float4 vertex : POSITION;
					fixed4 color : COLOR;
					float2 texcoord : TEXCOORD0;
				};

				struct v2f
				{
					float4 vertex : SV_POSITION;
					fixed4 color : COLOR;
					float2 texcoord : TEXCOORD0;
				};

				sampler2D _MainTex;

				v2f vert (appdata_t v)
				{
					v2f o;
					o.vertex = UnityObjectToClipPos(v.vertex);
					#if !defined(UNITY_COLORSPACE_GAMMA) && (UNITY_VERSION >= 540)
					o.color.rgb = GammaToLinearSpace(v.color.rgb);
					o.color.a = v.color.a;
					#else
					o.color = v.color;
					#endif
					
					o.texcoord = v.texcoord;

					return o;
				}

				fixed4 frag (v2f i) : SV_Target
				{
					fixed4 col = i.color;
					col.a *= tex2D(_MainTex, i.texcoord).a;

					return col;
				}
			ENDCG
		}
	}
}
