// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'


Shader "Custom/CubeLight" { // 环境贴图灯光
	Properties {
		_Color("Main Color", Color) = (0.5,0.5,0.5,1.0)
		_LightColor("Light Color", Color) = (0.5,0.5,0.5,1.0)
		_CubeLightTex ("Cube Light Map(RGB)",Cube) = "white"{}
	}
	
	SubShader {
        Tags { "IgnoreProjector"="True" "Queue" = "Geometry" "RenderType"="Opaque"}
		Lighting Off
        
        Pass
        {
            CGPROGRAM
                #pragma vertex vert	
                #pragma fragment frag
                #include "UnityCG.cginc"

                struct v2f
                {
                    float4  pos : SV_POSITION;
                    float3  cubenormal : TEXCOORD0;
                }; 
                uniform float4 _Color;
                uniform float4 _LightColor;
                uniform samplerCUBE _CubeLightTex;
                
                v2f vert (appdata_tan v)
                {
                    v2f o;
                    o.pos = UnityObjectToClipPos(v.vertex);
					o.cubenormal = mul (UNITY_MATRIX_MV, float4(v.normal,0));
                    return o;
                }

                float4 frag(v2f i) : COLOR
                {
					half4 e = texCUBE (_CubeLightTex, i.cubenormal);
                    return e * _LightColor * _Color * 2;
                }
            ENDCG
        }
	} 
	FallBack "ThreeKingdoms/Base"
}