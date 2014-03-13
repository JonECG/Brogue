sampler s0;
float4 PixelShaderFunction(float4 bcolor : COLOR0, float2 coords: TEXCOORD0) : COLOR0  
{  
	float4 color = tex2D(s0, coords) * bcolor;
	return color;
}

technique Technique1  
{  
    pass Pass1
    {  
        PixelShader = compile ps_2_0 PixelShaderFunction();  
    }
}