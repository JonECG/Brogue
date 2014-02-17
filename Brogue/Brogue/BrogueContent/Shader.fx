sampler s0;

float4 PixelShaderFunction(float2 coords: TEXCOORD0) : COLOR0  
{  
    return tex2D(s0, coords);

}
  
float DistanceSquared(float2 p1, float2 p2)
{
    return (float)abs(pow(p1[0] - p2[0], 2) - (pow(p1[1] - p2[1], 2)));
}

technique Technique1  
{  
    pass Pass1  
    {  
        PixelShader = compile ps_2_0 PixelShaderFunction();  
    }  
}  