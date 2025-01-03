Shader "Custom/ScreenSpaceShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
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

            sampler2D _MainTex;
            float4 _MainTex_ST;

            struct appdata
            {
                float4 vertex : POSITION;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            v2f vert (appdata v)
            {
                v2f o;
                // 转换顶点为裁剪坐标
                o.pos = UnityObjectToClipPos(v.vertex);
                // 计算屏幕空间 UV 坐标
                float4 screenPos = ComputeScreenPos(o.pos);
                o.uv = screenPos.xy / screenPos.w; // 标准化 UV
                return o;
            }

            float4 frag (v2f i) : SV_Target
            {
                // 采样纹理并返回结果
                float4 col = tex2D(_MainTex, i.uv);
                return col;
            }
            ENDCG
        }
    }
}
