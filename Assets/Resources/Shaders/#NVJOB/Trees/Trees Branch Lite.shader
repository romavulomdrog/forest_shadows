Shader "#NVJOB/Shaders/Trees/Branch Lite" {


//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	

Properties{
//----------------------------------------------

_Color("Main Color", Color) = (1,1,1,1)
_MainTex("Base (RGB)", 2D) = "white" {}
_WindTex("WindTex", 2D) = "black" {}
_Cutoff("Alpha cutoff", Range(0,1)) = 0.5
_LocalWindScale("Local Wind Scale", float) = 1
_LocalWindPower("Local Wind Power", float) = 1

//----------------------------------------------
}


//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


SubShader{
///////////////////////////////////////////////////////////////////////////////////////////////////////////////

Tags{ "Queue" = "Transparent" "RenderType" = "TransparentCutout" "IgnoreProjector" = "True" }
Cull Off
LOD 200

///////////////////////////////////////////////////////////////////////////////////////////////////////////////

Pass {
//----------------------------------------------

Tags{ "LightMode" = "ForwardBase" }

CGPROGRAM
#pragma vertex vert
#pragma fragment frag
#pragma multi_compile_fog
#include "UnityCG.cginc" 
#pragma multi_compile_fwdbase
#include "AutoLight.cginc"
#pragma multi_compile_instancing

//----------------------------------------------

sampler2D _MainTex, _WindTex;
float4 _MainTex_ST;
half _Cutoff, _LocalWindScale, _LocalWindPower;
half _TreeWindWorld, _TreeWindStrength, _TreeWindDirX , _TreeWindDirZ;
fixed4 _Color;

//----------------------------------------------

struct v2f {
float4 pos : SV_POSITION;
LIGHTING_COORDS(0,1)
float2 uv : TEXCOORD2;
UNITY_FOG_COORDS(1)
UNITY_VERTEX_OUTPUT_STEREO
};

//----------------------------------------------

v2f vert(appdata_base v) {
v2f o;
UNITY_SETUP_INSTANCE_ID(v);
UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);

_TreeWindWorld *= _LocalWindScale;
float3 worldPos = frac(mul(unity_ObjectToWorld, v.vertex).xyz) * 500;
float x = sin((worldPos.x / _TreeWindWorld) + _TreeWindDirX);
float z = sin((worldPos.z / _TreeWindWorld) + _TreeWindDirZ);
float3 offset = tex2Dlod(_WindTex, float4(x, 0, z, 0)).xyz;

v.vertex.xyz += mul((float3x3)unity_WorldToObject, float3(offset.x * x, 0, offset.z * z) * (_TreeWindStrength * 0.03)) * _LocalWindPower;

o.pos = UnityObjectToClipPos(v.vertex);
o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
TRANSFER_VERTEX_TO_FRAGMENT(o);
UNITY_TRANSFER_FOG(o, o.pos);
return o;
}

//----------------------------------------------

fixed4 frag(v2f i) : COLOR {
fixed4 color = tex2D(_MainTex, i.uv) * _Color;
clip(color.a - _Cutoff);
UNITY_APPLY_FOG(i.fogCoord, color);
UNITY_OPAQUE_ALPHA(color.a);
float attenuation = LIGHT_ATTENUATION(i);
return color * attenuation;
}

//----------------------------------------------

ENDCG
}

///////////////////////////////////////////////////////////////////////////////////////////////////////////////

Pass {
//----------------------------------------------

Tags{ "LightMode" = "ForwardAdd" }
Blend One One

CGPROGRAM
#pragma vertex vert 
#pragma fragment frag 
#include "UnityCG.cginc" 
#pragma multi_compile_fwdadd_fullshadows 
#include "AutoLight.cginc"

//----------------------------------------------

sampler2D _MainTex;
float4 _MainTex_ST;

//----------------------------------------------

struct v2f {
float4 pos : SV_POSITION;
LIGHTING_COORDS(0,1)
float2 uv : TEXCOORD2;
};

//----------------------------------------------

v2f vert(appdata_base v) {
v2f o;
o.pos = UnityObjectToClipPos(v.vertex);
o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
TRANSFER_VERTEX_TO_FRAGMENT(o);
return o;
}

//----------------------------------------------

fixed4 frag(v2f i) : COLOR {
float attenuation = LIGHT_ATTENUATION(i);
return tex2D(_MainTex, i.uv) * attenuation;
}

//----------------------------------------------

ENDCG
}


///////////////////////////////////////////////////////////////////////////////////////////////////////////////
}


Fallback "Transparent/Cutout/VertexLit"

//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}