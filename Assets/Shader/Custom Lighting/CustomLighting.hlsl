#ifndef CUSTOM_LIGHTING_INCLUDED
#define CUSTOM_LIGHTING_INCLUDED

void MainLight_float(float3 WorldPos, out float3 Direction, out float3 Color, out float DistanceAtten, out float ShadowAtten)
{
#if SHADERGRAPH_PREVIEW
    Direction = float3(0.5, 0.5, 0);
    Color = 1;
    DistanceAtten = 1;
    ShadowAtten = 1;
#else
#if SHADOWS_SCREEN
    float4 clipPos = TransformWorldToHClip(WorldPos);
    float4 shadowCoord = ComputeScreenPos(clipPos);
#else
    float4 shadowCoord = TransformWorldToShadowCoord(WorldPos);
#endif
    Light mainLight = GetMainLight(shadowCoord);
    Direction = mainLight.direction;
    Color = mainLight.color;
    DistanceAtten = mainLight.distanceAttenuation;
    ShadowSamplingData shadowSamplingData = GetMainLightShadowSamplingData();
    float shadowStrength = GetMainLightShadowStrength();
    ShadowAtten = SampleShadowmap(shadowCoord, TEXTURE2D_ARGS(_MainLightShadowmapTexture, sampler_MainLightShadowmapTexture), shadowSamplingData, shadowStrength, false);
#endif
}

void DirectSpecular_float(float3 Specular, float Smoothness, float3 Direction, float3 Color, float3 WorldNormal, float3 WorldView, out float3 Out)
{
#if SHADERGRAPH_PREVIEW
    Out = 0;
#else
    Smoothness = exp2(10 * Smoothness + 1);
    WorldNormal = normalize(WorldNormal);
    WorldView = SafeNormalize(WorldView);
    Out = LightingSpecular(Color, Direction, WorldNormal, WorldView, float4(Specular, 0), Smoothness);
#endif
}

void AdditionalLights_float(float3 SpecColor, float Smoothness, float3 WorldPosition, float3 WorldNormal, float3 WorldView, out float3 Diffuse, out float3 Specular)
{
    float3 diffuseColor = 1;  // Initialize to zero
    float3 specularColor = 1; // Initialize to zero

#ifndef SHADERGRAPH_PREVIEW
    Smoothness = exp2(10 * Smoothness + 1);
    WorldNormal = normalize(WorldNormal);
    WorldView = SafeNormalize(WorldView);
    int pixelLightCount = GetAdditionalLightsCount();

    int numSteps = 1; // Set to the number of desired steps

    for (int i = 0; i < pixelLightCount; ++i)
    {
        Light light = GetAdditionalLight(i, WorldPosition);
        half3 attenuatedLightColor = light.color * (light.distanceAttenuation * light.shadowAttenuation);

        // Calculate Lambertian and Specular contributions
        float lambertian = LightingLambert(attenuatedLightColor, light.direction, WorldNormal);
        float specular = LightingSpecular(attenuatedLightColor, light.direction, WorldNormal, WorldView, float4(SpecColor, 0), Smoothness);

        // Apply step function to the contributions
        lambertian = round(lambertian * (numSteps)) / (numSteps);
        specular = round(specular * (numSteps)) / (numSteps);

        // Accumulate contributions
        diffuseColor += lambertian * attenuatedLightColor; // Multiply by attenuatedLightColor
        specularColor += specular * attenuatedLightColor; // Multiply by attenuatedLightColor
    }
#endif

    Diffuse = diffuseColor;
    Specular = specularColor;
}
#endif