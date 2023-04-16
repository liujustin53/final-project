Shader "Sprites/Grayscale"
{
    Properties
    {
        [PerRendererData] _MainTex ("Texture", 2D) = "white" {}
        _Saturation ("Saturation", Range(0, 1)) = 1
        _Contrast ("Contrast", Range(0, 1)) = 1
        _Brightness ("Brightness", Range(0, 1)) = 1
    }
    SubShader
    {
        tags {
            "Queue"="Transparent"
            "IgnoreProjector"="True"
            "RenderType"="Transparent"
            "PreviewType"="Plane"
            "CanUseSpriteAtlas"="True"
        }

        cull Off
        Lighting Off
        ZWrite Off
        Fog { Mode Off }
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            float _Brightness;
            float _Saturation;
            float _Contrast;

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

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                // just invert the colors
                float lum = dot(col, float3(0.216, 0.7152, 0.0722));
                col.rgb = lerp(lum.xxx, col.rgb, _Saturation);
                float invContrast = 1 - _Contrast;
                col.rgb = lerp(col.rgb, 0.5.xxx, invContrast * invContrast);
                col.rgb *= _Brightness;
                return col;
            }
            ENDCG
        }
    }
}
