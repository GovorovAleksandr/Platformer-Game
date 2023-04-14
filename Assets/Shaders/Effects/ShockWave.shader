Shader "Custom/ShockWave"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
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

            float _UpperFeather;
            float _BottomFeather;
            float _RippleIntensity;

            fixed4 frag (v2f i) : SV_Target
            {
                float2 newVU = i.uv * 2 - 1;

                float timer = frac(_Time.y);
                float len = length(newVU);
                float upperRing = smoothstep(len + _UpperFeather, len - _BottomFeather, timer);
                float inverseRing = 1 - upperRing;
                float finalRing = upperRing * inverseRing;
                float2 finalUV = i.uv - newVU * finalRing * _RippleIntensity * (1 - timer);
                fixed4 col = tex2D(_MainTex, finalUV);
                return fixed4(col.rgb, 1);
            }
            ENDCG
        }
    }
}
