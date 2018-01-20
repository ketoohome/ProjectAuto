// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Custom/Rim" {
	Properties {
		_Color("Main Color", Color) = (0.5,0.5,0.5,1.0)
		_RimColor("Rim Color", Color) = (0.5,0.5,0.5,1.0)
		_RimRange("Rim Offset", Range(0,16)) = 4
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
                    float3  normal : NORMAL;
                    float3	worldRefl : TEXCOORD2;
                }; 
                uniform float4 _Color;
				uniform int _RimRange;
				uniform float4 _RimColor;
                
                v2f vert (appdata_tan v)
                {
                    v2f o;
                    o.pos = UnityObjectToClipPos(v.vertex);
					o.normal = mul(unity_ObjectToWorld, float4(v.normal.x,v.normal.y,v.normal.z,0)).xyz;
 					o.worldRefl = normalize(_WorldSpaceCameraPos - mul(unity_ObjectToWorld,v.vertex).xyz);
                    return o;
                }

                float4 frag(v2f i) : COLOR
                {
					half rim =  1.5 - dot(i.worldRefl, i.normal);
					rim = pow(rim,_RimRange);
                    return _Color * 2 + rim*_RimColor;
                }
            ENDCG
        }
	} 
	FallBack "ThreeKingdoms/Base"
}