// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Custom/XRay"
{
	Properties
	{
		_ASEOutlineWidth( "Outline Width", Float ) = 0
		_TextureSample0("Texture Sample 0", 2D) = "white" {}
		_TextureMask("Mask", 2D) = "white" {}
		_MaskMultiplier("alpha", Float ) = 0
		_XRayPower("XRayPower", Float) = 0
		_XRayColor("XRayColor", Color) = (0,0,0,0)
		_XRayScale("XRayScale", Float) = 0
		_XRayBias("XRayBias", Float) = 0
		_XRayIntensity("XRayIntensity", Float) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Pass
		{
			ColorMask 0
			ZWrite On
		}

		Tags{ "RenderType" = "Transparent"  "Queue" = "Transparent+0"}
		ZWrite Off
		ZTest Always
		Cull Back
		CGPROGRAM
		#pragma target 3.0
		#pragma surface outlineSurf Outline nofog alpha:fade  keepalpha noshadow noambient novertexlights nolightmap nodynlightmap nodirlightmap nometa noforwardadd vertex:outlineVertexDataFunc 
		
		
		
		struct Input
		{
			float3 worldPos;
			float3 worldNormal;
			float2 uv_texcoord;
			INTERNAL_DATA
		};
		uniform sampler2D _TextureMask;
		uniform float4 _TextureMask_ST;
		uniform float _XRayBias;
		uniform float _XRayScale;
		uniform float _XRayPower;
		uniform float4 _XRayColor;
		uniform float _XRayIntensity;
		uniform half _ASEOutlineWidth;
		uniform float _MaskMultiplier;
		uniform float4 _CoeurPos;
		
		void outlineVertexDataFunc( inout appdata_full v, out Input o )
		{
			UNITY_INITIALIZE_OUTPUT( Input, o );
			v.vertex.xyz += ( v.normal * _ASEOutlineWidth );
		}
		inline half4 LightingOutline( SurfaceOutput s, half3 lightDir, half atten ) { return half4 ( 0,0,0, s.Alpha); }
		void outlineSurf( Input i, inout SurfaceOutput o )
		{
			float2 uv_Mask = i.uv_texcoord * _TextureMask_ST.xy + _TextureMask_ST.zw;
			float3 ase_worldPos = i.worldPos;
			float3 ase_worldViewDir = normalize( UnityWorldSpaceViewDir( ase_worldPos ) );
			float3 ase_worldNormal = WorldNormalVector( i, float3( 0, 0, 1 ) );
			float fresnelNdotV4 = dot( ase_worldNormal, ase_worldViewDir );
			float fresnelNode4 = ( _XRayBias + _XRayScale * pow( 1.0 - fresnelNdotV4, _XRayPower ) );
			//dot(c.rgb, half3(0.3333, 0.3333, 0.3333));
			float radius = _MaskMultiplier;
			float radius2 = _MaskMultiplier - 15;
			float currentdist = distance(_CoeurPos, i.worldPos) < radius ? 1 : 0;
			float currentdist2 = distance(_CoeurPos, i.worldPos) < radius2 ? 0 : 1;
			float grayscale = (dot(tex2D(_TextureMask, i.uv_texcoord).rgb, half3(0.333,0.333,0.333))) * currentdist;
			o.Emission = ( fresnelNode4 * _XRayColor ).rgb /* (tex2D(_TextureMask, i.uv_texcoord).rgba * _XRayColor)*/;
			o.Alpha = (( fresnelNode4 * (_XRayColor).a * _XRayIntensity ) * currentdist) * currentdist2;
			o.Normal = float3(0,0,-1);
		}
		ENDCG
		

		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+1" }
		Cull Back
		ZWrite On
		ZTest LEqual
		CGPROGRAM
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows vertex:vertexDataFunc 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform sampler2D _TextureSample0;
		uniform float4 _TextureSample0_ST;

		void vertexDataFunc( inout appdata_full v, out Input o )
		{
			UNITY_INITIALIZE_OUTPUT( Input, o );
			v.vertex.xyz += 0;
		}

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_TextureSample0 = i.uv_texcoord * _TextureSample0_ST.xy + _TextureSample0_ST.zw;
			o.Albedo = tex2D( _TextureSample0, uv_TextureSample0 ).rgb;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}