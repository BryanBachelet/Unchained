Shader "Unlit/TexOffset"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
	
	_ColorOne("ColorRed", Color) = (1,1,1,1)
		_ColorTwo("ColorGreen",Color) = (1,1,1,1)
		_ColorThree("ColorBlue",Color) = (1,1,1,1)

		_SpeedRedChannel("Red Channel Speed", float) = 0.2
		_SpeedGreenChannel("Green Channel Speed", float) = 0.2
		_SpeedBlueChannel("Blue Channel Speed", float) = 0.2



	}
		SubShader
		{
			Tags { "RenderType" = "Opaque" }
			LOD 100

			Pass
			{
				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				// make fog work
				#pragma multi_compile_fog

				#include "UnityCG.cginc"

				struct appdata
				{
					float4 vertex : POSITION;
					float2 uv : TEXCOORD0;
				};

				struct v2f
				{
					float2 uv : TEXCOORD0;
					UNITY_FOG_COORDS(1)
					float4 vertex : SV_POSITION;
				};

				sampler2D _MainTex;
				float4 _MainTex_ST;
				
				float4 _ColorOne;
				float4 _ColorTwo;
				float4 _ColorThree;

				float _SpeedRedChannel;
				float _SpeedGreenChannel;
				float _SpeedBlueChannel;

				v2f vert(appdata v)
				{
					v2f o;
					o.vertex = UnityObjectToClipPos(v.vertex);
					o.uv = TRANSFORM_TEX(v.uv, _MainTex);
					UNITY_TRANSFER_FOG(o,o.vertex);
					return o;
				}

				fixed4 frag(v2f i) : SV_Target
				{
				float2 scrollUVR = i.uv;
				float2 scrollUVG = i.uv;
				float2 scrollUVB = i.uv;
				float xScrollValue = 0 * _Time.x;
				float yScrollValueR = -_SpeedRedChannel * _Time.y;
				float yScrollValueG = -_SpeedGreenChannel * _Time.y;
				float yScrollValueB = -_SpeedBlueChannel * _Time.y;

				scrollUVR += float2(xScrollValue, yScrollValueR);
				scrollUVG += float2(xScrollValue, yScrollValueG);
				scrollUVB += float2(xScrollValue, yScrollValueB);


				float4 col;
				col.r = tex2D(_MainTex, scrollUVR).r;
				col.g = tex2D(_MainTex, scrollUVG).g;
				col.b = tex2D(_MainTex, scrollUVB).b;

				float4 releaseColor;
				releaseColor = (col.g * col.b);
				releaseColor += col.r;
				float4 red = releaseColor.r * _ColorOne ;
				float4 green = (1-releaseColor.g )* _ColorTwo;
				float4 blue = (1 - releaseColor.b) *_ColorThree;

				releaseColor = red + green +blue;

					UNITY_APPLY_FOG(i.fogCoord, releaseColor);
					return releaseColor;
				}
				ENDCG
			}
		}
}
