Shader "Screen Effects/Pulsing"
{
	Properties
	{
		[HideInInspector] _MainTex("Texture", 2D) = "white" {}
		_Color("Color", Color) = (0.0, 0.0, 0.0, 1.0)
		_Power("Power", Float) = 1.0
		_Amount("Amount", Float) = 1.0
	}

	SubShader
	{
		Cull Off
		ZWrite Off
		ZTest Always

		Pass
		{
			CGPROGRAM

			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			struct v2f
			{
				float4 vertex : SV_POSITION;
				float2 texcoord : TEXCOORD0;
			};

			sampler2D _MainTex;
			float4 _MainTex_TexelSize;
			float _Power, _Amount;
			float3 _Color;

			void vert(appdata_base v, out v2f o)
			{
				UNITY_INITIALIZE_OUTPUT(v2f, o);
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.texcoord = v.texcoord;
			}
			void frag(v2f i, out fixed4 col : SV_TARGET)
			{
				col = tex2D(_MainTex, i.texcoord);

				float2 tx = -i.texcoord * i.texcoord + i.texcoord;
				float ratio = _MainTex_TexelSize.y / _MainTex_TexelSize.x;
				ratio *= ratio;
				ratio *= tx.x;
				ratio += tx.y;
				ratio *= 2.0;

				float3 vignette = lerp(_Color, col.rgb, saturate(ratio * _Power));
				col.rgb = lerp(col.rgb, vignette, _Amount);
			}

			ENDCG
		}
	}
}
