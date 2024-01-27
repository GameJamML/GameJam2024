#ifndef SOBELEDGE_OUTLINES
#define SOBELEDGE_OUTLINES

/////////////////////////////////
// Inspired by Ned Makes Games //
/////////////////////////////////

// Sample offsets for each cell in the matrix relative to the center pixel
static float2 sobelSamplePoints[9] =
{
	float2(-1, 1), float2(0, 1), float2(1, 1),
	float2(-1, 0), float2(0, 0), float2(1, 1),
	float2(-1, -1), float2(0, -1), float2(1, -1)
};

// Weights
static float sobelVerticalMatrix[9] =
{
	1, 2, 1,
	0, 0, 0,
	-1, -2, -1
};
static float sobelHorizontalMatrix[9] =
{
	1, 0, -1,
	2, 0, -2,
	1, 0, -1
};

// Runs the Sobel algorithm over the depth texture
void SobelDepth_float(float2 UV, float Thickness, out float OUT)
{
	float2 aux = 0;

	// unrool tells the compiler to optimize the loop since it's a constant
	[unroll] for (int i = 0; i < 9; i++)
	{
		float depth = SHADERGRAPH_SAMPLE_SCENE_DEPTH(UV + sobelSamplePoints[i] * Thickness);
		aux += depth * float2(sobelHorizontalMatrix[i], sobelVerticalMatrix[i]);
	}

	OUT = length(aux);
}

void SobelColor_float(float2 UV, float Thickness, out float OUT)
{
	float2 sobelR = 0;
	float2 sobelB = 0;
	float2 sobelG = 0;

	[unroll] for (int i = 0; i < 9; i++)
	{
		float3 rgb = SHADERGRAPH_SAMPLE_SCENE_COLOR(UV + sobelSamplePoints[i] * Thickness);
		
		float2 kernel = float2(sobelHorizontalMatrix[i], sobelVerticalMatrix[i]);

		sobelR += rgb.r * kernel;
		sobelG += rgb.g * kernel;
		sobelB += rgb.b * kernel;
	}

	// Combine RGB by taking the largest sobel value
	OUT = max(length(sobelR), max(length(sobelG), length(sobelB)));
}

#endif