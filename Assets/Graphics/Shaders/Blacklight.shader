Shader "CustomRenderTexture/Blacklight"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainAlbTex ("Albedo (RGB)", 2D) = "white" {}
        _Smooth ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
        _LightDir ("Light Direction", Vector) = (0,0,1)
        _LightPos ("Light Position", Vector) = (0,0,0)
        _LightAngle ("Light Angle", Range(0,180)) = 45
        _StrengthScalar ("Strength", Float) = 50
        _GlowColour ("Glow Colour", Color) = (1,1,1,1)
        _GlowIntensity ("Glow Intensity", Range(0,1)) = 0.5
        _CutOff ("Cut Off (Alpha)", Range(0,1)) = 0.5
     }

     SubShader
     {
         Tags{"Queue" = "Transparent" "RenderType" = "Transparent"}
        Blend SrcAlpha OneMinusSrcAlpha
        LOD 200
        CGPROGRAM
        #pragma surface SurfaceReveal Standard fullforwardshadows alpha:fade
        #pragma target 3.0
        sampler2D _MainAlbTex;

        struct Input
        {
            float2 uv_MainAlbTex;
            float3 worldPos;
        };

        half _Smooth;
        half _Metallic;
        fixed4 _Color;
        float3 _LightDir;
        float3 _LightPos;
        float _LightAngle;
        float _StrengthScalar;
        fixed4 _GlowColour;
        float _GlowIntensity;
        float _CutOff;

        void SurfaceReveal(Input input, inout SurfaceOutputStandard R)
        {
            float3 Dir = normalize(_LightPos - input.worldPos);
            float Scale = dot(Dir, _LightDir);
            float Strength = Scale - cos(_LightAngle * (3.14 / 360));
            Strength = min(max(Strength * _StrengthScalar, 0), 1);
            fixed4 RC = tex2D(_MainAlbTex, input.uv_MainAlbTex) * _Color;
            R.Albedo = RC.rgb;
            R.Metallic = _Metallic;
            R.Smoothness = _Smooth;
            R.Alpha = Strength * RC.a;
            fixed4 Glow = _GlowColour * _GlowIntensity * Strength;
            R.Emission = Glow.rgb;
            clip(R.Alpha - _CutOff);
        }
        ENDCG
}
Fallback "Diffuse"
}
