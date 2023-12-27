Shader "#NVJOB/Shaders/Boids Lite" {


//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



Properties{
//----------------------------------------------

_Color("Main Color", Color) = (1,1,1,1)
_Emission("Emission Color", Color) = (0, 0, 0, 1)
_MainTex("Base (RGB)", 2D) = "white" {}
_BumpMap("Normalmap", 2D) = "bump" {}
_IntensityNm("Intensity Normalmap", Range(-20, 20)) = 1
[Header(Bird Flapping)][Space(5)]
_FlappingSpeed("Flapping Speed", Range(0, 50)) = 10
_FlappYPower("Flapping Y Power", Range(0, 50)) = 2
_FlappYOffset("Flapping Y Offset", Range(-15, 15)) = 0.1
_FlappXPower("Flapping X Power", Range(0, 50)) = 1
_FlappXOffset("Flapping X Offset", Range(-15, 15)) = 0.1
_FlappXCenter("Flapping X Center Indent", Range(0, 15)) = 0.1
_FlappZPower("Flapping Z Power", Range(-10, 10)) = 0.1
_WaveY("Wave Y", Range(0, 30)) = 0
_WaveYSpeed("Wave Y Speed", Range(0, 30)) = 1
[Toggle(BUTTERFLY)]
_FillWithRed("Butterfly", Float) = 0

//----------------------------------------------
}


//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


SubShader{
///////////////////////////////////////////////////////////////////////////////////////////////////////////////

Tags { "RenderType" = "Opaque" }
LOD 200

CGPROGRAM
#pragma surface surf Lambert vertex:vert exclude_path:prepass nolightmap nometa noforwardadd nolppv noshadowmask noforwardadd interpolateview novertexlights

//----------------------------------------------

sampler2D _MainTex;
fixed4 _Color, _Emission;
sampler2D _BumpMap;
half _IntensityNm;
half _FlappingSpeed, _FlappYPower, _FlappYOffset, _FlappXPower, _FlappXOffset, _FlappXCenter, _FlappZPower, _WaveY, _WaveYSpeed;

//----------------------------------------------

struct Input {
float2 uv_MainTex;
float2 uv_BumpMap;
};

//----------------------------------------------

struct appdata {
float4 vertex : POSITION;
float3 normal : NORMAL;
float4 texcoord : TEXCOORD0;
float4 tangent : TANGENT;
UNITY_VERTEX_INPUT_INSTANCE_ID
};

//----------------------------------------------

void vert(inout appdata v) {
UNITY_SETUP_INSTANCE_ID(v);
float3 wp = mul(unity_ObjectToWorld, half4(1, 1, 1, 1)).xyz;
half yf = v.vertex.y + _FlappYOffset;
half xf = abs(v.vertex.x) + _FlappXOffset;
float timeY = _Time.y;
half tmul;
#ifdef BUTTERFLY
xf = xf * 0.5;
tmul = 0.2;
#else
xf = xf * xf * xf;
tmul = 0.5;
#endif
float flap = sin(v.vertex.y / 5) * yf * xf * cos((timeY + sin(wp.y * tmul)) * _FlappingSpeed);
v.vertex.y += flap * _FlappYPower;
if (v.vertex.x > _FlappXCenter) v.vertex.x -= flap * _FlappXPower * v.vertex.x;
else if (v.vertex.x < -_FlappXCenter) v.vertex.x -= flap * _FlappXPower * v.vertex.x;
v.vertex.z += flap * _FlappZPower;
if (_WaveY > 0) v.vertex.y += sin((timeY + (cos((wp.x + wp.z) * _WaveY * 0.1) * 3)) * _WaveYSpeed) * _WaveY;
}

//----------------------------------------------

void surf(Input IN, inout SurfaceOutput o) {
fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
o.Albedo = c.rgb;

fixed3 normal = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));
normal.x *= _IntensityNm;
normal.y *= _IntensityNm;
o.Normal = normalize(normal);

o.Emission = _Emission.rgb;
}

//----------------------------------------------

ENDCG

///////////////////////////////////////////////////////////////////////////////////////////////////////////////
}

Fallback "Legacy Shaders/VertexLit"

//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}