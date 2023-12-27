Shader "#NVJOB/Shaders/Specular/Reflective Transparent" {


//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


Properties {
//----------------------------------------------

_Color ("Main Color", Color) = (1,1,1,1)
_SpecColor ("Specular Color", Color) = (0.5,0.5,0.5,1)
_Shininess ("Shininess", Range (0.01, 1)) = 0.078125
_ReflectColor ("Reflection Color", Color) = (1,1,1,0.5)
_MainTex ("Base (RGB) RefStrGloss (A)", 2D) = "white" {}
_Cube ("Reflection Cubemap", Cube) = "" {}
_BumpMap("Normalmap", 2D) = "bump" {}
_IntensityNm("Intensity Normalmap", Range(-20, 20)) = 1

//----------------------------------------------
}


//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


SubShader {
///////////////////////////////////////////////////////////////////////////////////////////////////////////////

Tags {"Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent"}
LOD 200
CGPROGRAM
#pragma surface surf BlinnPhong alpha:fade exclude_path:prepass nolightmap nometa noforwardadd nolppv noshadowmask noforwardadd interpolateview novertexlights
#pragma target 3.0

//----------------------------------------------

sampler2D _MainTex;
sampler2D _BumpMap;
samplerCUBE _Cube;
fixed4 _Color;
fixed4 _ReflectColor;
half _Shininess, _IntensityNm;

//----------------------------------------------

struct Input {
float2 uv_MainTex;
float2 uv_BumpMap;
float3 worldRefl;
INTERNAL_DATA
};

//----------------------------------------------

void surf (Input IN, inout SurfaceOutput o) {
fixed4 tex = tex2D(_MainTex, IN.uv_MainTex);
fixed4 c = tex * _Color;
o.Albedo = c.rgb;

o.Gloss = tex.a;
o.Specular = _Shininess;

fixed3 normal = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));
normal.x *= _IntensityNm;
normal.y *= _IntensityNm;
o.Normal = normalize(normal);

float3 worldRefl = WorldReflectionVector (IN, o.Normal);
fixed4 reflcol = texCUBE (_Cube, worldRefl);
reflcol *= tex.a;
o.Emission = reflcol.rgb * _ReflectColor.rgb;
o.Alpha = c.a * _Color.a;
}

//----------------------------------------------

ENDCG

///////////////////////////////////////////////////////////////////////////////////////////////////////////////
}

FallBack "Legacy Shaders/Reflective/Bumped Diffuse"

//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}