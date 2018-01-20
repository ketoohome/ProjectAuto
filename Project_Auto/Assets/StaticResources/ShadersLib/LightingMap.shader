// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: commented out 'float4 unity_LightmapST', a built-in variable
// Upgrade NOTE: commented out 'sampler2D unity_Lightmap', a built-in variable

// Upgrade NOTE: commented out 'float4 unity_LightmapST', a built-in variable
// Upgrade NOTE: commented out 'sampler2D unity_Lightmap', a built-in variable
// Upgrade NOTE: replaced tex2D unity_Lightmap with UNITY_SAMPLE_TEX2D

// Upgrade NOTE: commented out 'half4 unity_LightmapST', a built-in variable
// Upgrade NOTE: commented out 'sampler2D unity_Lightmap', a built-in variable

// Upgrade NOTE: commented out 'half4 unity_LightmapST', a built-in variable
// Upgrade NOTE: commented out 'sampler2D unity_Lightmap', a built-in variable

// Upgrade NOTE: commented out 'half4 unity_LightmapST', a built-in variable
// Upgrade NOTE: commented out 'sampler2D unity_Lightmap', a built-in variable

Shader "Custom/LightingMap" {
	Properties {
		_Color("Main Color", Color) = (0.5,0.5,0.5,1.0)
	}
	
	SubShader {
        Tags { "RenderType"="Opaque" "LightMode"="ForwardBase"}
        
        Pass
        {
            CGPROGRAM
            	#include "UnityCG.cginc"
                #pragma vertex vert	
                #pragma fragment frag
		        
		        struct appdata {
		            float4 vertex : POSITION;
		            float2 texcoord1 : TEXCOORD1;
		        };

                struct v2f
                {
                    float4  pos : SV_POSITION;
                    #ifndef LIGHTMAP_OFF
        			float2 lmap : TEXCOORD1;
        			#endif
                }; 
                uniform float4 _Color;
                #ifndef LIGHTMAP_OFF
    			// float4 unity_LightmapST;
    			// sampler2D unity_Lightmap;
    			#endif
                v2f vert (appdata v)
                {
                    v2f o;
                    o.pos = UnityObjectToClipPos(v.vertex);
			        #ifndef LIGHTMAP_OFF
			        o.lmap = v.texcoord1.xy * unity_LightmapST.xy + unity_LightmapST.zw;
			        #endif
                    return o;
                }

                float4 frag(v2f i) : COLOR
                {
                	half4 oCol = _Color;
                    #ifndef LIGHTMAP_OFF
            		fixed3 lm = DecodeLightmap (UNITY_SAMPLE_TEX2D(unity_Lightmap, i.lmap));
            		oCol *= float4(lm.rgb,1);											// 烘焙
            		#endif
                    return oCol;
                }
            ENDCG
        }
	} 
	FallBack "Diffuse"
}