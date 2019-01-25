Shader "Hidden/Camera Vibration Shader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
		_Vibration_X ("Horizontal offset", Range(-1,1)) = 0.0
		_Vibration_Y ("Vertical offset", Range(-1,1)) = 0
		_Vibration_Z ("z offset", Range(-1,1)) = 0
		_Inverse ("Color Inverse", Range(0,1)) = 0
		_WhiteBlack("Color W&B", Range(0,1)) = 0
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

			uniform float _Vibration_X;
			uniform float _Vibration_Y;
			uniform float _Vibration_Z;	
			uniform float _Inverse;
			uniform float _WhiteBlack;

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
                o.uv = v.uv + float2(_Vibration_X,_Vibration_Y)*0.25;
				o.uv = o.uv * (_Vibration_Z*0.25 + 1) - _Vibration_Z*0.125;
                return o;
            }

            sampler2D _MainTex;

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                // just invert the colors
                col.rgb = lerp(col.rgb,1 - col.rgb,_Inverse); 
				col.rgb = lerp(col.rgb,(col.r+col.g+col.b)*0.33,_WhiteBlack);
                return col;
            }
            ENDCG
        }
    }
}
