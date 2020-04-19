Shader "Unlit/Bar"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
		_Color("Color Tint", Color) = (1, 1, 1, 1)
		_Energy("_Energy", float) = 10
    }
    SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            sampler2D _MainTex;
			fixed4 _Color;
			float _Energy;

            fixed4 frag (v2f i) : SV_Target
            {
				fixed4 col;
				if (i.uv.x < _Energy / 100)
				{
					col = tex2D(_MainTex, i.uv) * _Color;
					clip(col.a - 0.7);
				}
				else
				{
					clip(-1);
				}
                
                // just invert the colors
                return col;
            }
            ENDCG
        }
    }
}
