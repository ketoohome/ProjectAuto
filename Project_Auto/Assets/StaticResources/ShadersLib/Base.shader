// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/Base" { // 基础纹理无光照
	Properties {
		_Color("Main Color", Color) = (0.5,0.5,0.5,1.0)
		_MainTex ("Base (RGB)", 2D) = "white" {}
	}
	
	SubShader {
        Tags { "IgnoreProjector"="True" "Queue" = "Geometry" "RenderType"="Opaque"}
		LOD 200
		Lighting Off
        Pass
        {
            CGPROGRAM
// Upgrade NOTE: excluded shader from DX11 and Xbox360; has structs without semantics (struct v2f members worldRefl)
//#pragma exclude_renderers d3d11 xbox360
                #pragma vertex vert	
                #pragma fragment frag
                #include "UnityCG.cginc"

                struct v2f
                {
                    float4  pos : SV_POSITION;
                    float4  uv : TEXCOORD1;
                }; 
                uniform float4 _Color;
                uniform sampler2D _MainTex;
                uniform float4 _MainTex_ST;
                
                v2f vert (appdata_tan v)
                {
                    v2f o;
                    o.pos = UnityObjectToClipPos(v.vertex);
                   	o.uv.xy = TRANSFORM_TEX(v.texcoord.xy,_MainTex).xy;
                    return o;
                }

                float4 frag(v2f i) : COLOR
                {				
                    return _Color*tex2D(_MainTex,i.uv.xy)*2;
                }
            ENDCG
        }
	} 
	FallBack "Diffuse"
}