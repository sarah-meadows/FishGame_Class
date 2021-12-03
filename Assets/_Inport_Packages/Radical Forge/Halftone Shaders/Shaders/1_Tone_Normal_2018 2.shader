// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "HalftoneShaders/1_Tone_Normal_2018"
{
	Properties
	{
		_ASEOutlineColor( "Outline Color", Color ) = (0,0,0,1)
		_ASEOutlineWidth( "Outline Width", Float ) = 0.00075
		_Shadow_Amount("Shadow_Amount", Float) = 1
		_Edge_Colour("Edge_Colour", Color) = (0,0,0,1)
		_Edge_Width("Edge_Width", Float) = 1
		_Color_Base("Color_Base", Color) = (1,1,1,1)
		_Color_Step_1("Color_Step_1", Color) = (0.2666667,0.05882353,0.4313726,1)
		_Step_1("Step_1", Float) = 2.08
		[Toggle(_INVERT_TEX_ON)] _Invert_Tex("Invert_Tex", Float) = 0
		[Toggle(_STEP_1_HALFTONE_ON)] _Step_1_Halftone("Step_1_Halftone", Float) = 0
		_Halftone_Texture("Halftone_Texture", 2D) = "white" {}
		_Halftone_Power("Halftone_Power", Float) = 1
		[Toggle(_USE_SCREEN_SPACE_ON)] _Use_Screen_Space("Use_Screen_Space", Float) = 0
		_Half_Tone_UVs("Half_Tone_UVs", Float) = 18
		_Normal_Map("Normal_Map", 2D) = "bump" {}
		_Normal_Intensity("Normal_Intensity", Float) = 1
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
		#pragma shader_feature _STEP_1_HALFTONE_ON
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
		uniform float4 _Color_Base;
		uniform float _Step_1;
		uniform sampler2D _Halftone_Texture;
		uniform float _Half_Tone_UVs;
		uniform float _Halftone_Power;
		uniform float4 _Edge_Colour;
		uniform float _Edge_Width;

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
			SurfaceOutputStandard s47 = (SurfaceOutputStandard ) 0;
			s47.Albedo = float3( 0,0,0 );
			float2 uv_Normal_Map = i.uv_texcoord * _Normal_Map_ST.xy + _Normal_Map_ST.zw;
			float4 tex2DNode50 = tex2D( _Normal_Map, uv_Normal_Map );
			float3 appendResult48 = (float3(( (tex2DNode50).rg * _Normal_Intensity ) , tex2DNode50.b));
			s47.Normal = WorldNormalVector( i , appendResult48 );
			float clampResult2 = clamp( ( ase_lightAtten + _Shadow_Amount ) , 0.0 , 1.0 );
			#if defined(LIGHTMAP_ON) && UNITY_VERSION < 560 //aselc
			float4 ase_lightColor = 0;
			#else //aselc
			float4 ase_lightColor = _LightColor0;
			#endif //aselc
			float4 temp_cast_0 = (( _Step_1 * 0.05 )).xxxx;
			float4 temp_cast_1 = (1.0).xxxx;
			float4 ase_screenPos = float4( i.screenPos.xyz , i.screenPos.w + 0.00000000001 );
			float2 appendResult43 = (float2(ase_screenPos.x , ase_screenPos.y));
			#ifdef _USE_SCREEN_SPACE_ON
				float2 staticSwitch34 = ( _Half_Tone_UVs * appendResult43 );
			#else
				float2 staticSwitch34 = ( i.uv_texcoord * _Half_Tone_UVs );
			#endif
			float4 temp_cast_2 = (_Halftone_Power).xxxx;
			float4 temp_output_31_0 = pow( tex2D( _Halftone_Texture, staticSwitch34 ) , temp_cast_2 );
			#ifdef _INVERT_TEX_ON
				float4 staticSwitch29 = ( 1.0 - temp_output_31_0 );
			#else
				float4 staticSwitch29 = temp_output_31_0;
			#endif
			#ifdef _STEP_1_HALFTONE_ON
				float4 staticSwitch27 = staticSwitch29;
			#else
				float4 staticSwitch27 = temp_cast_1;
			#endif
			float3 ase_worldPos = i.worldPos;
			#if defined(LIGHTMAP_ON) && UNITY_VERSION < 560 //aseld
			float3 ase_worldlightDir = 0;
			#else //aseld
			float3 ase_worldlightDir = normalize( UnityWorldSpaceLightDir( ase_worldPos ) );
			#endif //aseld
			float3 ase_worldNormal = WorldNormalVector( i, float3( 0, 0, 1 ) );
			float dotResult44 = dot( ase_worldlightDir , ase_worldNormal );
			float4 lerpResult19 = lerp( _Color_Step_1 , _Color_Base , step( temp_cast_0 , ( staticSwitch27 * dotResult44 ) ).r);
			float3 ase_worldViewDir = normalize( UnityWorldSpaceViewDir( ase_worldPos ) );
			float fresnelNdotV16 = dot( ase_worldNormal, ase_worldViewDir );
			float fresnelNode16 = ( 0.0 + 1.0 * pow( 1.0 - fresnelNdotV16, 5.0 ) );
			float4 lerpResult8 = lerp( lerpResult19 , _Edge_Colour , step( ( 0.7 * _Edge_Width ) , fresnelNode16 ));
			s47.Emission = ( clampResult2 * ( ase_lightColor * lerpResult8 ) ).rgb;
			s47.Metallic = 0.0;
			s47.Smoothness = 0.0;
			s47.Occlusion = 1.0;

			data.light = gi.light;

			UnityGI gi47 = gi;
			#ifdef UNITY_PASS_FORWARDBASE
			Unity_GlossyEnvironmentData g47 = UnityGlossyEnvironmentSetup( s47.Smoothness, data.worldViewDir, s47.Normal, float3(0,0,0));
			gi47 = UnityGlobalIllumination( data, s47.Occlusion, s47.Normal, g47 );
			#endif

			float3 surfResult47 = LightingStandard ( s47, viewDir, gi47 ).rgb;
			surfResult47 += s47.Emission;

			c.rgb = surfResult47;
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
7;29;2546;1004;3034.515;615.1525;1.845024;True;True
Node;AmplifyShaderEditor.ScreenPosInputsNode;41;-6722.707,53.01741;Float;False;1;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.DynamicAppendNode;43;-6469.503,81.73143;Float;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;37;-6546.882,-386.8188;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;38;-6522.083,-158.4119;Float;False;Property;_Half_Tone_UVs;Half_Tone_UVs;11;0;Create;True;0;0;False;0;18;18;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;35;-6197.093,-347.6633;Float;True;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;36;-6198.398,-85.32162;Float;True;2;2;0;FLOAT;0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.StaticSwitch;34;-5816.46,-181.0383;Float;False;Property;_Use_Screen_Space;Use_Screen_Space;10;0;Create;True;0;0;False;0;0;0;0;True;;Toggle;2;Key0;Key1;9;1;FLOAT2;0,0;False;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT2;0,0;False;6;FLOAT2;0,0;False;7;FLOAT2;0,0;False;8;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;33;-5231.458,125.7615;Float;False;Property;_Halftone_Power;Halftone_Power;9;0;Create;True;0;0;False;0;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;32;-5382.258,-197.938;Float;True;Property;_Halftone_Texture;Halftone_Texture;8;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PowerNode;31;-4818.053,-31.53802;Float;False;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.OneMinusNode;30;-4559.36,47.76199;Float;False;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.WorldSpaceLightDirHlpNode;45;-4183.427,593.001;Float;False;False;1;0;FLOAT;0;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.StaticSwitch;29;-4201.856,-41.94343;Float;False;Property;_Invert_Tex;Invert_Tex;6;0;Create;True;0;0;False;0;0;0;0;True;;Toggle;2;Key0;Key1;9;1;COLOR;0,0,0,0;False;0;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;3;COLOR;0,0,0,0;False;4;COLOR;0,0,0,0;False;5;COLOR;0,0,0,0;False;6;COLOR;0,0,0,0;False;7;COLOR;0,0,0,0;False;8;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;28;-4197.957,192.0618;Float;False;Constant;_Float2;Float 2;9;0;Create;True;0;0;False;0;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.WorldNormalVector;46;-4134.057,779.1873;Float;False;False;1;0;FLOAT3;0,0,1;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.RangedFloatNode;25;-3731.257,-100.4379;Float;False;Property;_Step_1;Step_1;5;0;Create;True;0;0;False;0;2.08;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;26;-3729.958,10.06209;Float;False;Constant;_Float1;Float 1;8;0;Create;True;0;0;False;0;0.05;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.DotProductOpNode;44;-3657.308,691.7369;Float;False;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StaticSwitch;27;-3796.258,192.0616;Float;False;Property;_Step_1_Halftone;Step_1_Halftone;7;0;Create;True;0;0;False;0;0;0;0;True;;Toggle;2;Key0;Key1;9;1;COLOR;0,0,0,0;False;0;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;3;COLOR;0,0,0,0;False;4;COLOR;0,0,0,0;False;5;COLOR;0,0,0,0;False;6;COLOR;0,0,0,0;False;7;COLOR;0,0,0,0;False;8;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;17;-2471.429,589.7995;Float;False;Constant;_Float0;Float 0;4;0;Create;True;0;0;False;0;0.7;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;18;-2470.429,718.7995;Float;False;Property;_Edge_Width;Edge_Width;2;0;Create;True;0;0;False;0;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;23;-3449.156,-6.83783;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;24;-3451.756,198.5622;Float;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.FresnelNode;16;-2194.429,829.7995;Float;False;Standard;WorldNormal;ViewDir;False;5;0;FLOAT3;0,0,1;False;4;FLOAT3;0,0,0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;5;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;15;-2195.313,594.4663;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;20;-3029.257,-591.837;Float;False;Property;_Color_Step_1;Color_Step_1;4;0;Create;True;0;0;False;0;0.2666667,0.05882353,0.4313726,1;0,0,0,0;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StepOpNode;22;-2952.556,101.0624;Float;False;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;21;-3033.155,-171.9373;Float;False;Property;_Color_Base;Color_Base;3;0;Create;True;0;0;False;0;1,1,1,1;0,0,0,0;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;6;-1448.313,54.46628;Float;False;Property;_Shadow_Amount;Shadow_Amount;0;0;Create;True;0;0;False;0;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;19;-2475.46,-175.8377;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;13;-1940.412,396.766;Float;False;Property;_Edge_Colour;Edge_Colour;1;0;Create;True;0;0;False;0;0,0,0,1;0,0,0,0;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;50;-1605.224,-685.59;Float;True;Property;_Normal_Map;Normal_Map;12;0;Create;True;0;0;False;0;None;None;True;0;False;bump;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StepOpNode;14;-1866.313,614.4663;Float;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LightAttenuation;5;-1452.313,-87.53372;Float;False;0;1;FLOAT;0
Node;AmplifyShaderEditor.LightColorNode;7;-1445.313,208.4663;Float;False;0;3;COLOR;0;FLOAT3;1;FLOAT;2
Node;AmplifyShaderEditor.LerpOp;8;-1443.313,417.4663;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.ComponentMaskNode;51;-1209.538,-786.8806;Float;True;True;True;False;False;1;0;COLOR;0,0,0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleAddOpNode;4;-1101.313,35.46628;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;52;-1080.972,-451.6182;Float;False;Property;_Normal_Intensity;Normal_Intensity;13;0;Create;True;0;0;False;0;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;49;-783.585,-575.6211;Float;False;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;3;-1097.313,292.4663;Float;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ClampOpNode;2;-899.3224,254.4139;Float;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;48;-537.774,-427.4758;Float;True;FLOAT3;4;0;FLOAT2;0,0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;1;-536.3488,251.5627;Float;True;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.CustomStandardSurface;47;-226.6677,201.9866;Float;False;Metallic;Tangent;6;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,1;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;1;False;1;FLOAT3;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;11;51.57639,-10.9737;Float;False;True;2;Float;ASEMaterialInspector;0;0;CustomLighting;HalftoneShaders/1_Tone_Normal_2018;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;False;0;False;Opaque;;Geometry;ForwardOnly;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;False;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;-1;False;-1;-1;False;-1;0;True;0.00075;0,0,0,1;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;43;0;41;1
WireConnection;43;1;41;2
WireConnection;35;0;37;0
WireConnection;35;1;38;0
WireConnection;36;0;38;0
WireConnection;36;1;43;0
WireConnection;34;1;35;0
WireConnection;34;0;36;0
WireConnection;32;1;34;0
WireConnection;31;0;32;0
WireConnection;31;1;33;0
WireConnection;30;0;31;0
WireConnection;29;1;31;0
WireConnection;29;0;30;0
WireConnection;44;0;45;0
WireConnection;44;1;46;0
WireConnection;27;1;28;0
WireConnection;27;0;29;0
WireConnection;23;0;25;0
WireConnection;23;1;26;0
WireConnection;24;0;27;0
WireConnection;24;1;44;0
WireConnection;15;0;17;0
WireConnection;15;1;18;0
WireConnection;22;0;23;0
WireConnection;22;1;24;0
WireConnection;19;0;20;0
WireConnection;19;1;21;0
WireConnection;19;2;22;0
WireConnection;14;0;15;0
WireConnection;14;1;16;0
WireConnection;8;0;19;0
WireConnection;8;1;13;0
WireConnection;8;2;14;0
WireConnection;51;0;50;0
WireConnection;4;0;5;0
WireConnection;4;1;6;0
WireConnection;49;0;51;0
WireConnection;49;1;52;0
WireConnection;3;0;7;0
WireConnection;3;1;8;0
WireConnection;2;0;4;0
WireConnection;48;0;49;0
WireConnection;48;2;50;3
WireConnection;1;0;2;0
WireConnection;1;1;3;0
WireConnection;47;1;48;0
WireConnection;47;2;1;0
WireConnection;11;13;47;0
ASEEND*/
//CHKSM=C2B63D4FC27E38C786A2504CE8F202E0EBBED4DD