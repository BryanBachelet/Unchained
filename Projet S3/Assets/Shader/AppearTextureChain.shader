

Shader "Unlit/AppearTextureChain"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
   
        _Radius("Radius", Range(0 ,100)) =0
        
    }
    SubShader
    {
        Tags { "RenderType"="Transoparent" }
        LOD 100

        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha 
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
                float3 normal : Normal;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float3 wpos : TEXCOORD1;
                float3 localPos: TEXCOORD2;
                float4 vertex : SV_POSITION;
                float4 color : COlOR;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
        
            float _Radius;
           


            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.wpos = mul(unity_ObjectToWorld, v.vertex).xyz;
                o.localPos = o.wpos - mul(unity_ObjectToWorld, float4(0,0,0,1)).xyz;
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
           
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float a = 0;
                fixed4 col = tex2D(_MainTex,i.uv);
                float dir = length(i.localPos.xyz - float3(0,0,0));
                if(dir <_Radius)
                {
                    a =1;
                }
             
                col.a = a; 
  
               
                return col;
            }
            ENDCG
        }
    }
}
