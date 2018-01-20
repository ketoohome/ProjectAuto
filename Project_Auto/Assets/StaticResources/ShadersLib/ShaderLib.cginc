#ifndef MY_CG_INCLUDE
#define MY_CG_INCLUDE

/////////////////////////////////////////////////////////////////////////
// Blend Color ( 颜色混合)
/////////////////////////////////////////////////////////////////////////

// Opacity 不透明度:C=d*A+(1-d)*B
half4 Blend_Opacity(half4 A,half4 B,float d)
{
	return d*A+(1-d)*B;
}
// Darken 变暗: C = (B<=A)?B:A
half4 Blend_Darken(half4 A,half4 B)
{
	return min(B,A); 
}
// Lighten 变亮:C = (B<=A)?A:B
half4 Blend_Lighten(half4 A,half4 B)
{
	return max(B,A); 
}
// Multiply 正片叠底 : C=A*B
half4 Blend_Multiply(half4 A,half4 B)
{
	return A*B;
}
// Screen 滤色 : C=1-(1-A)*(1-B)
half4 Blend_Screen(half4 A,half4 B)
{
	return 1-(1-A)*(1-B);
}
// Color Dodge 颜色减淡 C=B/(1-A)
half4 Blend_Dodge(half4 A,half4 B)
{
	return B/(1-A);
}
//Color Burn 颜色加深 C=1-(1-B)/A
half4 Blend_Burn(half4 A,half4 B)
{
	return 1- (1-B)/A;
}
//Linear Dodge 线形减淡C=A+B

//Linear Burn 线形加深C=A+B-1

//Overlay 叠加 B<=0.5: C=2*A*B    B>0.5: C=1-2*(1-A)*(1-B)

//Hard Light 强光 A<=0.5: C=2*A*B     A>0.5: C=1-2*(1-A)*(1-B)

//Soft Light 柔光 A<=0.5: C=(2*A-1)*(B-B*B)+B      A>0.5: C=(2*A-1)*(sqrt(B)-B)+B

//Vivid Light 亮光 A<=0.5: C=1-(1-B)/2*A      A>0.5: C=B/(2*(1-A))

//Linear Light 线形光 C=B+2*A-1

//Pin Light 点光 B<2*A-1: C=2*A-1  2*A-1<B<2*A: C=B  B>2*A: C=2*A

//Hard Mix 实色混合 A<1-B: C=0  A>1-B: C=1

//Difference 差值 C=|A-B|

//Exclusion 排除 C=A+B-2*A*B


/////////////////////////////////////////////////////////////////////////
// 21次采样高斯模糊采样 
/////////////////////////////////////////////////////////////////////////
float4 blur_x21( sampler2D mainTex, float _blurSize, float4 texcoord){
	float2 screenPos = texcoord.xy / texcoord.w;
	float depth= _blurSize*0.0005;
 
    screenPos.x = (screenPos.x + 1) * 0.5;
 
    screenPos.y = 1-(screenPos.y + 1) * 0.5;
 
    half4 sum = half4(0.0h,0.0h,0.0h,0.0h);   
    sum += tex2D( mainTex, float2(screenPos.x-5.0 * depth, screenPos.y+5.0 * depth)) * 0.025;    
    sum += tex2D( mainTex, float2(screenPos.x+5.0 * depth, screenPos.y-5.0 * depth)) * 0.025;
    
    sum += tex2D( mainTex, float2(screenPos.x-4.0 * depth, screenPos.y+4.0 * depth)) * 0.05;
    sum += tex2D( mainTex, float2(screenPos.x+4.0 * depth, screenPos.y-4.0 * depth)) * 0.05;
 
    
    sum += tex2D( mainTex, float2(screenPos.x-3.0 * depth, screenPos.y+3.0 * depth)) * 0.09;
    sum += tex2D( mainTex, float2(screenPos.x+3.0 * depth, screenPos.y-3.0 * depth)) * 0.09;
    
    sum += tex2D( mainTex, float2(screenPos.x-2.0 * depth, screenPos.y+2.0 * depth)) * 0.12;
    sum += tex2D( mainTex, float2(screenPos.x+2.0 * depth, screenPos.y-2.0 * depth)) * 0.12;
    
    sum += tex2D( mainTex, float2(screenPos.x-1.0 * depth, screenPos.y+1.0 * depth)) *  0.15;
    sum += tex2D( mainTex, float2(screenPos.x+1.0 * depth, screenPos.y-1.0 * depth)) *  0.15;
    
	
 
    sum += tex2D( mainTex, screenPos-5.0 * depth) * 0.025;    
    sum += tex2D( mainTex, screenPos-4.0 * depth) * 0.05;
    sum += tex2D( mainTex, screenPos-3.0 * depth) * 0.09;
    sum += tex2D( mainTex, screenPos-2.0 * depth) * 0.12;
    sum += tex2D( mainTex, screenPos-1.0 * depth) * 0.15;    
    sum += tex2D( mainTex, screenPos) * 0.16; 
    sum += tex2D( mainTex, screenPos+5.0 * depth) * 0.15;
    sum += tex2D( mainTex, screenPos+4.0 * depth) * 0.12;
    sum += tex2D( mainTex, screenPos+3.0 * depth) * 0.09;
    sum += tex2D( mainTex, screenPos+2.0 * depth) * 0.05;
    sum += tex2D( mainTex, screenPos+1.0 * depth) * 0.025;
       
	return sum/2;
}
#endif  