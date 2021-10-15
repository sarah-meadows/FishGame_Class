// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "HalftoneShaders/Ceil_Tone_Normal_2018"
{
	Properties
	{
		_ASEOutlineColor( "Outline Color", Color ) = (0,0,0,1)
		_ASEOutlineWidth( "Outline Width", Float ) = 0.00075
		_Shadow_Amount("Shadow_Amount", Float) = 1
		_Color_Step_2("Color_Step_2", Color) = (0.6039216,0.4470589,1,1)
		_Color_Step_1("Color_Step_1", Color) = (0.2666667,0.05882353,0.4313726,1)
		[Toggle(_INVERT_TEX_ON)] _Invert_Tex("Invert_Tex", Float) = 0
		_Halftone_Texture("Halftone_Texture", 2D) = "white" {}
		_Halftone_Power("Halftone_Power", Float) = 1
		[Toggle(_USE_SCREEN_SPACE_ON)] _Use_Screen_Space("Use_Screen_Space", Float) = 0
		_Half_Tone_UVs("Half_Tone_UVs", Float) = 18
		_Normal_Intensity("Normal_Intensity", Float) = 1
		_Normal_Map("Normal_Map", 2D) = "bump" {}
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ }
		Cull Front
		CGPROGRAM
		#pragma target 3.0
		#pragma surface outlineSurf Outline nofog  keepalpha noshadow noambient novertexlights nolightmap nodynlightmap nodirlightmap nometa noforwardadd vertex:outlineVertexDataFunc 
		
		
		
		struct Input {
			half filler;
		};
		uniform half4 _ASEOutlineColor;
		uniform half _ASEOutlineWidth;
		void outlineVertexDataFunc( inout appdata_full v, out Input o )
		{
			UNITY_INITIALIZE_OUTPUT( Input, o );
			v.vertex.xyz += ( v.normal * _ASEOutlineWidth );
		}
		inline half4 LightingOutline( SurfaceOutput s, half3 lightDir, half atten ) { return half4 ( 0,0,0, s.Alpha); }
		void outlineSurf( Input i, inout SurfaceOutput o )
		{
			o.Emission = _ASEOutlineColor.rgb;
			o.Alpha = 1;
		}
		ENDCG
		

		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" }
		Cull Back
		CGPROGRAM
		#include "UnityPBSLighting.cginc"
		#include "UnityShaderVariables.cginc"
		#include "UnityCG.cginc"
		#pragma target 3.0
		#pragma shader_feature _INVERT_TEX_ON
		#pragma shader_feature _USE_SCREEN_SPACE_ON
		#pragma surface surf StandardCustomLighting keepalpha noshadow exclude_path:deferred 
		struct Input
		{
			float3 worldNormal;
			INTERNAL_DATA
			float2 uv_texcoord;
			float4 screenPos;
			float3 worldPos;
		};

		struct SurfaceOutputCustomLightingCustom
		{
			half3 Albedo;
			half3 Normal;
			half3 Emission;
			half Metallic;
			half Smoothness;
			half Occlusion;
			half Alpha;
			Input SurfInput;
			UnityGIInput GIData;
		};

		uniform sampler2D _Normal_Map;
		uniform float4 _Normal_Map_ST;
		uniform float _Normal_Intensity;
		uniform float _Shadow_Amount;
		uniform float4 _Color_Step_1;
		uniform float4 _Color_Step_2;
		uniform sampler2D _Halftone_Texture;
		uniform float _Half_Tone_UVs;
		uniform float _Halftone_Power;

		inline half4 LightingStandardCustomLighting( inout SurfaceOutputCustomLightingCustom s, half3 viewDir, UnityGI gi )
		{
			UnityGIInput data = s.GIData;
			Input i = s.SurfInput;
			half4 c = 0;
			#ifdef UNITY_PASS_FORWARDBASE
			float ase_lightAtten = data.atten;
			if( _LightColor0.a == 0)
			ase_lightAtten = 0;
			#else
			float3 ase_lightAttenRGB = gi.light.color / ( ( _LightColor0.rgb ) + 0.000001 );
			float ase_lightAtten = max( max( ase_lightAttenRGB.r, ase_lightAttenRGB.g ), ase_lightAttenRGB.b );
			#endif
			#if defined(HANDLE_SHADOWS_BLENDING_IN_GI)
			half bakedAtten = UnitySampleBakedOcclusion(data.lightmapUV.xy, data.worldPos);
			float zDist = dot(_WorldSpaceCameraPos - data.worldPos, UNITY_MATRIX_V[2].xyz);
			float fadeDist = UnityComputeShadowFadeDistance(data.worldPos, zDist);
			ase_lightAtten = UnityMixRealtimeAndBakedShadows(data.atten, bakedAtten, UnityComputeShadowFade(fadeDist));
			#endif
			SurfaceOutputStandard s53 = (SurfaceOutputStandard ) 0;
			s53.Albedo = float3( 0,0,0 );
			float2 uv_Normal_Map = i.uv_texcoord * _Normal_Map_ST.xy + _Normal_Map_ST.zw;
			float4 tex2DNode55 = tex2D( _Normal_Map, uv_Normal_Map );
			float3 appendResult58 = (float3(( (tex2DNode55).rg * _Normal_Intensity ) , tex2DNode55.b));
			s53.Normal = WorldNormalVector( i , appendResult58 );
			float clampResult2 = clamp( ( ase_lightAtten + _Shadow_Amount ) , 0.0 , 1.0 );
			#if defined(LIGHTMAP_ON) && UNITY_VERSION < 560 //aselc
			float4 ase_lightColor = 0;
			#else //aselc
			float4 ase_lightColor = _LightColor0;
			#endif //aselc
			float4 ase_screenPos = float4( i.screenPos.xyz , i.screenPos.w + 0.00000000001 );
			float2 appendResult43 = (float2(ase_screenPos.x , ase_screenPos.y));
			#ifdef _USE_SCREEN_SPACE_ON
				float2 staticSwitch34 = ( _Half_Tone_UVs * appendResult43 );
			#else
				float2 staticSwitch34 = ( i.uv_texcoord * _Half_Tone_UVs );
			#endif
			float4 temp_cast_0 = (_Halftone_Power).xxxx;
			float4 temp_output_31_0 = pow( tex2D( _Halftone_Texture, staticSwitch34 ) , temp_cast_0 );
			#ifdef _INVERT_TEX_ON
				float4 staticSwitch29 = ( 1.0 - temp_output_31_0 );
			#else
				float4 staticSwitch29 = temp_output_31_0;
			#endif
			float3 ase_worldPos = i.worldPos;
			#if defined(LIGHTMAP_ON) && UNITY_VERSION < 560 //aseld
			float3 ase_worldlightDir = 0;
			#else //aseld
			float3 ase_worldlightDir = normalize( UnityWorldSpaceLightDir( ase_worldPos ) );
			#endif //aseld
			float3 ase_worldNormal = WorldNormalVector( i, float3( 0, 0, 1 ) );
			float dotResult44 = dot( ase_worldlightDir , ase_worldNormal );
			float4 lerpResult19 = lerp( _Color_Step_1 , _Color_Step_2 , ( ceil( ( staticSwitch29 * ( 5.0 * dotResult44 ) ) ) / 5.0 ).r);
			s53.Emission = ( clampResult2 * ( ase_lightColor * lerpResult19 ) ).rgb;
			s53.Metallic = 0.0;
			s53.Smoothness = 0.0;
			s53.Occlusion = 1.0;

			data.light = gi.light;

			UnityGI gi53 = gi;
			#ifdef UNITY_PASS_FORWARDBASE
			Unity_GlossyEnvironmentData g53 = UnityGlossyEnvironmentSetup( s53.Smoothness, data.worldViewDir, s53.Normal, float3(0,0,0));
			gi53 = UnityGlobalIllumination( data, s53.Occlusion, s53.Normal, g53 );
			#endif

			float3 surfResult53 = LightingStandard ( s53, viewDir, gi53 ).rgb;
			surfResult53 += s53.Emission;

			c.rgb = surfResult53;
			c.a = 1;
			return c;
		}

		inline void LightingStandardCustomLighting_GI( inout SurfaceOutputCustomLightingCustom s, UnityGIInput data, inout UnityGI gi )
		{
			s.GIData = data;
		}

		void surf( Input i , inout SurfaceOutputCustomLightingCustom o )
		{
			o.SurfInput = i;
			o.Normal = float3(0,0,1);
		}

		ENDCG
	}
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=15401
7;29;2546;1004;2776.222;740.4664;1.722148;True;True
Node;AmplifyShaderEditor.ScreenPosInputsNode;41;-5484.791,691.823;Float;False;1;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.DynamicAppendNode;43;-5231.587,720.537;Float;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;37;-5307.288,310.7096;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;38;-5282.489,539.1166;Float;False;Property;_Half_Tone_UVs;Half_Tone_UVs;7;0;Create;True;0;0;False;0;18;18;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;36;-4958.804,612.2068;Float;True;2;2;0;FLOAT;0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;35;-4957.499,349.8651;Float;True;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.StaticSwitch;34;-4578.544,457.7673;Float;False;Property;_Use_Screen_Space;Use_Screen_Space;6;0;Create;True;0;0;False;0;0;0;0;True;;Toggle;2;Key0;Key1;9;1;FLOAT2;0,0;False;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT2;0,0;False;6;FLOAT2;0,0;False;7;FLOAT2;0,0;False;8;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;32;-4144.342,440.8676;Float;True;Property;_Halftone_Texture;Halftone_Texture;4;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;33;-4050.542,764.5671;Float;False;Property;_Halftone_Power;Halftone_Power;5;0;Create;True;0;0;False;0;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.WorldNormalVector;46;-3598.957,1419.866;Float;False;False;1;0;FLOAT3;0,0,1;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.PowerNode;31;-3580.14,607.2676;Float;False;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.WorldSpaceLightDirHlpNode;45;-3648.327,1233.68;Float;False;False;1;0;FLOAT;0;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.RangedFloatNode;51;-3134.291,992.3973;Float;False;Constant;_Ceils;Ceils;12;0;Create;True;0;0;False;0;5;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;30;-3321.445,686.5676;Float;False;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.DotProductOpNode;44;-3122.211,1332.416;Float;False;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;24;-2899.591,837.3444;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StaticSwitch;29;-2963.941,596.8621;Float;False;Property;_Invert_Tex;Invert_Tex;3;0;Create;True;0;0;False;0;0;0;0;True;;Toggle;2;Key0;Key1;9;1;COLOR;0,0,0,0;False;0;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;3;COLOR;0,0,0,0;False;4;COLOR;0,0,0,0;False;5;COLOR;0,0,0,0;False;6;COLOR;0,0,0,0;False;7;COLOR;0,0,0,0;False;8;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;50;-2548.32,651.2852;Float;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.CeilOpNode;49;-2259.887,689.155;Float;False;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;21;-2288.59,255.2372;Float;False;Property;_Color_Step_2;Color_Step_2;1;0;Create;True;0;0;False;0;0.6039216,0.4470589,1,1;0,0,0,0;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleDivideOpNode;47;-1994.779,704.4662;Float;False;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;6;-1448.313,54.46628;Float;False;Property;_Shadow_Amount;Shadow_Amount;0;0;Create;True;0;0;False;0;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;20;-2288.064,-92.21667;Float;False;Property;_Color_Step_1;Color_Step_1;2;0;Create;True;0;0;False;0;0.2666667,0.05882353,0.4313726,1;0,0,0,0;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LightAttenuation;5;-1452.313,-87.53372;Float;False;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;55;-1519.187,-515.8605;Float;True;Property;_Normal_Map;Normal_Map;9;0;Create;True;0;0;False;0;None;None;True;0;False;bump;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;19;-1734.266,323.7825;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.LightColorNode;7;-1445.313,208.4663;Float;False;0;3;COLOR;0;FLOAT3;1;FLOAT;2
Node;AmplifyShaderEditor.SimpleAddOpNode;4;-1101.313,35.46628;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ComponentMaskNode;56;-1130.187,-515.8605;Float;False;True;True;False;False;1;0;COLOR;0,0,0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;54;-1108.187,-378.8607;Float;False;Property;_Normal_Intensity;Normal_Intensity;8;0;Create;True;0;0;False;0;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;2;-864.3126,66.46628;Float;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;3;-1097.313,292.4663;Float;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;57;-760.1868,-512.8604;Float;False;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;1;-516.3126,265.4663;Float;True;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.DynamicAppendNode;58;-542.3256,-345.3716;Float;False;FLOAT3;4;0;FLOAT2;0,0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.CustomStandardSurface;53;-257.2773,216.908;Float;False;Metallic;Tangent;6;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,1;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;1;False;1;FLOAT3;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;11;0,0;Float;False;True;2;Float;ASEMaterialInspector;0;0;CustomLighting;HalftoneShaders/Ceil_Tone_Normal_2018;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;False;0;False;Opaque;;Geometry;ForwardOnly;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;False;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;-1;False;-1;-1;False;-1;0;True;0.00075;0,0,0,1;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;43;0;41;1
WireConnection;43;1;41;2
WireConnection;36;0;38;0
WireConnection;36;1;43;0
WireConnection;35;0;37;0
WireConnection;35;1;38;0
WireConnection;34;1;35;0
WireConnection;34;0;36;0
WireConnection;32;1;34;0
WireConnection;31;0;32;0
WireConnection;31;1;33;0
WireConnection;30;0;31;0
WireConnection;44;0;45;0
WireConnection;44;1;46;0
WireConnection;24;0;51;0
WireConnection;24;1;44;0
WireConnection;29;1;31;0
WireConnection;29;0;30;0
WireConnection;50;0;29;0
WireConnection;50;1;24;0
WireConnection;49;0;50;0
WireConnection;47;0;49;0
WireConnection;47;1;51;0
WireConnection;19;0;20;0
WireConnection;19;1;21;0
WireConnection;19;2;47;0
WireConnection;4;0;5;0
WireConnection;4;1;6;0
WireConnection;56;0;55;0
WireConnection;2;0;4;0
WireConnection;3;0;7;0
WireConnection;3;1;19;0
WireConnection;57;0;56;0
WireConnection;57;1;54;0
WireConnection;1;0;2;0
WireConnection;1;1;3;0
WireConnection;58;0;57;0
WireConnection;58;2;55;3
WireConnection;53;1;58;0
WireConnection;53;2;1;0
WireConnection;11;13;53;0
ASEEND*/
//CHKSM=AE4C95C39D0F3D16A9350409EDF22C5ACDB11F55