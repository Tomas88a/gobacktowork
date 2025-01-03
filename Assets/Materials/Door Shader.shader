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
                // ת������Ϊ�ü�����
                o.pos = UnityObjectToClipPos(v.vertex);
                // ������Ļ�ռ� UV ����
                float4 screenPos = ComputeScreenPos(o.pos);
                o.uv = screenPos.xy / screenPos.w; // ��׼�� UV
                return o;
            }

            float4 frag (v2f i) : SV_Target
            {
                // �����������ؽ��
                float4 col = tex2D(_MainTex, i.uv);
                return col;
            }
            ENDCG
        }
    }
}
