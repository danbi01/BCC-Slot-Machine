Shader "Custom/theRestOfCakeShader" 
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType" = "Transparent" "Queue" = "Transparent" }
        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off
        Cull Off

        Pass 
        {
                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag

                #include "UnityCG.cginc"

                struct appdata {
                    float4 vertex : POSITION;
                    float2 uv : TEXCOORD0;
                };

                struct v2f {
                    float2 uv : TEXCOORD0;
                    float4 vertex : SV_POSITION;
                };

                sampler2D _MainTex;
                float4 _MainTex_ST;

                v2f vert(appdata v) {
                    v2f o;
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                    return o;
                }

                half3 AdjustContrast(half3 color, half contrast) {
                    #if !UNITY_COLORSPACE_GAMMA
                    color = LinearToGammaSpace(color);
                    #endif
                    color = saturate(lerp(half3(0.5, 0.5, 0.5), color, contrast));
                    #if !UNITY_COLORSPACE_GAMMA
                    color = GammaToLinearSpace(color);
                    #endif
                    return color;
                }

                fixed4 frag(v2f i) : SV_Target
                {
                    fixed4 col = tex2D(_MainTex, i.uv);
                    col.rgb = col.rgb - 0.3; // Brightness
                    col.rgb = AdjustContrast(col.rgb, 0.3); // Contrast


                    float grey = dot(col.rgb, float3(0.299, 0.587, 0.114));

                    return fixed4(grey, grey, grey, col.a);
                }
                ENDCG
        }        
    }
    FallBack "Diffuse"
}