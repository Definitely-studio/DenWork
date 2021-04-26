Shader "Hidden/NewImageEffectShader"
{
    Properties
    {
    }
    SubShader
    {
         Tags{ "RenderType"="Opaque" "Queue"="Geometry-1"}
         LOD 100

        Blend Zero One
        ZWrite Off

        Pass
        {

            Stencil{
                Ref 1
                Comp always
                Pass replace
            }
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                return o;
            }

            sampler2D _MainTex;

            fixed4 frag (v2f i) : SV_Target
            {
                
                return 0;
            }
            ENDCG
        }
    }
}
