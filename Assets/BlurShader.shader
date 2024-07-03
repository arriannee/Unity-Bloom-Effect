Shader "Custom/BlurShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _BlurAmount ("Blur Amount", Range(0, 1)) = 0.1
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

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _BlurAmount;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float2 uv = i.uv;
                float2 blurOffset = _BlurAmount / _ScreenParams.xy;
                fixed4 color = tex2D(_MainTex, uv);

                fixed4 blurColor = 0;
                blurColor += tex2D(_MainTex, uv + blurOffset);
                blurColor += tex2D(_MainTex, uv - blurOffset);
                blurColor += tex2D(_MainTex, uv + float2(blurOffset.x, -blurOffset.y));
                blurColor += tex2D(_MainTex, uv - float2(blurOffset.x, -blurOffset.y));
                blurColor *= 0.25;

                // Mezclar el color original y el desenfoque de manera más sutil
                return lerp(color, blurColor, _BlurAmount * 0.5);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
