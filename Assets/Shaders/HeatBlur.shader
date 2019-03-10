Shader "Custom/HeatBlur"
{
    Properties
    {
		_Noise("Noise", 2D) = "white" {}
		_StrengthFilter("Strength Filter", 2D) = "white" {}
		_Strength("Distort Strength", float) = 1.0
		_Speed("Distort Speed", float) = 1.0
	}

	

		SubShader
	{
		Cull Off ZWrite Off ZTest Always
		Tags { "Queue" = "Transparent" }


		GrabPass{
			"_BackgroundTexture"
		}

		Pass
		{
			

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_fog
			#include "UnityCG.cginc"

			//Properties
			sampler2D _Noise;
			sampler2D _StrengthFilter;
			sampler2D _BackgroundTexture;
			float _Strength;
			float _Speed;

            struct vertexInput
			{
				float4 vertex: POSITION;
				float3 texCoord: TEXCOORD0;
			};

			struct vertexOutput
			{
				float4 pos : SV_POSITION;
				float4 grabPos: TEXCOORD0;
			};

            sampler2D _MainTex;
            float4 _MainTex_ST;

            vertexOutput vert (vertexInput input)
            {
				vertexOutput output;

				//billboard to camera
				float4 pos = input.vertex / 10;

				//transform origin to view space
				float4 originInViewSpace = mul(UNITY_MATRIX_MV, float4(0, 0, 0, 1));
				//translate view space point by vertex position
				float4 vertInViewSpace = (originInViewSpace + float4(pos.x, pos.z, 0, 0));

				//convert from view space to projection space
				pos = mul(UNITY_MATRIX_P, vertInViewSpace);
				output.pos = pos;

				output.grabPos = ComputeGrabScreenPos(output.pos);
				float noise = tex2Dlod(_Noise, float4(input.texCoord, 0)).rgb;
				float3 filt = tex2Dlod(_StrengthFilter, float4(input.texCoord, 0)).rgb;
				output.grabPos.x += cos(noise * _Time.x * _Speed) * filt * _Strength;
				output.grabPos.y += sin(noise * _Time.x * _Speed) * filt * _Strength;
				return output;
            }

            fixed4 frag(vertexOutput input) : COLOR
            {
				//return float4(1,1,1,1); //Billboard Test
				return tex2Dproj(_BackgroundTexture, input.grabPos);
            }
            ENDCG
        }
    }
}
