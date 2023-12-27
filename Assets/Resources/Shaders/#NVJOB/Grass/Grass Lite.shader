Shader "#NVJOB/Shaders/Grass/Lite" {

//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


Properties {
//----------------------------------------------

[Header(Basic Settings)][Space(5)]
_Color("Main Color", Color) = (1,1,1,1)
_MainTex("Base (RGB) Gloss (A)", 2D) = "white" {}
[Header(Grass)][Space(5)]
_WindTex("WindTex", 2D) = "black" {}
_LocalWindPower("Local Wind Power", float) = 1
_LocalWindWorld("Local Wind World", float) = 1
_GrassFlex("Grass Flex", float) = 1
_GrassWindLod("Wind Lod", float) = 500

//----------------------------------------------
}


//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


SubShader {
///////////////////////////////////////////////////////////////////////////////////////////////////////////////

Tags { "RenderType"="Opaque" }
Cull Off
LOD 200


CGPROGRAM
#pragma surface surf Lambert vertex:vert exclude_path:prepass addshadow fullforwardshadows nolppv noforwardadd interpolateview novertexlights
#pragma multi_compile_instancing

//----------------------------------------------

fixed4 _Color;
sampler2D _MainTex, _WindTex;
half _GlobalWindGrassFlex, _GrassFlex, _GlobalWindPower, _GlobalWindDirX, _GlobalWindDirZ, _GlobalWindWorld, _GrassWindLod, _LocalWindPower, _LocalWindWorld;

//----------------------------------------------

struct Input {
float2 uv_MainTex;
float3 viewDir;
};

//----------------------------------------------

struct appdata {
float4 vertex : POSITION;
float3 normal : NORMAL;
float4 texcoord : TEXCOORD0;
float4 texcoord1 : TEXCOORD1;
float4 texcoord2 : TEXCOORD2;
float4 color : COLOR;
float4 tangent : TANGENT;
UNITY_VERTEX_INPUT_INSTANCE_ID
};

//----------------------------------------------

void vert(inout appdata v) {
UNITY_SETUP_INSTANCE_ID(v);
if (length(ObjSpaceViewDir(v.vertex)) < _GrassWindLod) {
if (v.texcoord1.x > 0.5) {
half amount = _GlobalWindGrassFlex;
if (v.texcoord1.y > 0.5) amount *= _GrassFlex;
float3 worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
_GlobalWindWorld *=_LocalWindWorld;
half x = sin((worldPos.x / _GlobalWindWorld) + _GlobalWindDirX);
half z = sin((worldPos.z / _GlobalWindWorld) + _GlobalWindDirZ);
half3 offset = tex2Dlod(_WindTex, half4(x, 0, z, 0)).xyz;
v.vertex.xyz += mul((float3x3)unity_WorldToObject, half3(offset.x * x, 0, offset.z * z) * _GlobalWindPower * _LocalWindPower * amount);
}
}
//else v.vertex.y = -9000000; // to hide, fake LOD
}

//----------------------------------------------

void surf (Input IN, inout SurfaceOutput o) {
fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
o.Albedo = c.rgb;
}

//----------------------------------------------

ENDCG

///////////////////////////////////////////////////////////////////////////////////////////////////////////////
}

Fallback "Legacy Shaders/VertexLit"

//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}