// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Unlit/GroundShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
		BounderyX("BounderyX", Float) = 1
		_BounderyX("_BounderyX", Float) = -1
		//name("BounderyY", Float) = 1;
		//name("_BounderyY", Float) = -1;
		_Color("Color Tint", Color) = (1, 1, 1, 1)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
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
				float3 worldPos : TEXCOORD1;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
			float BounderyX;
			float _BounderyX;
			fixed4 _Color;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
				o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
				fixed4 col = 0;
				if (i.worldPos.x > _BounderyX && i.worldPos.x < BounderyX &&
				i.worldPos.z > _BounderyX && i.worldPos.z < BounderyX)
				{
					// sample the texture
					col = tex2D(_MainTex, i.uv) * _Color;
				}
                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }
            ENDCG
        }
    }
}
