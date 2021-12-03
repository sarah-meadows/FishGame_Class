// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "HalftoneShaders/3_Tone_Normal_2018"
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
		_Color_Step_2("Color_Step_2", Color) = (0.6039216,0.4470589,1,1)
		_Step_2("Step_2", Float) = 5.6
		_Step_1("Step_1", Float) = 2.08
		[Toggle(_INVERT_TEX_ON)] _Invert_Tex("Invert_Tex", Float) = 0
		[Toggle(_STEP_1_HALFTONE_ON)] _Step_1_Halftone("Step_1_Halftone", Float) = 0
		_Halftone_Texture("Halftone_Texture", 2D) = "white" {}
		_Halftone_Power("Halftone_Power", Float) = 1
		_Edge_Step("Edge_Step", Float) = 0.94
		[Toggle(_USE_SCREEN_SPACE_ON)] _Use_Screen_Space("Use_Screen_Space", Float) = 0
		[Toggle(_EDGE_HALFTONE_ON)] _Edge_Halftone("Edge_Halftone", Float) = 0
		_Half_Tone_UVs("Half_Tone_UVs", Float) = 18
		[Toggle(_STEP_2_HALFTONE_ON)] _Step_2_Halftone("Step_2_Halftone", Float) = 0
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
		#pragma shader_feature _STEP_1_HALFTONE_ON
		#pragma shader_feature _INVERT_TEX_ON
		#pragma shader_feature _USE_SCREEN_SPACE_ON
		#pragma shader_feature _STEP_2_HALFTONE_ON
		#pragma shader_feature _EDGE_HALFTONE_ON
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
		uniform float _Step_1;
		uniform sampler2D _Halftone_Texture;
		uniform float _Half_Tone_UVs;
		uniform float _Halftone_Power;
		uniform float4 _Color_Base;
		uniform float _Step_2;
		uniform float4 _Edge_Colour;
		uniform float _Edge_Step;
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
			SurfaceOutputStandard s63 = (SurfaceOutputStandard ) 0;
			s63.Albedo = float3( 0,0,0 );
			float2 uv_Normal_Map = i.uv_texcoord * _Normal_Map_ST.xy + _Normal_Map_ST.zw;
			float4 tex2DNode68 = tex2D( _Normal_Map, uv_Normal_Map );
			float3 appendResult64 = (float3(( (tex2DNode68).rg * _Normal_Intensity ) , tex2DNode68.b));
			s63.Normal = WorldNormalVector( i , appendResult64 );
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
			float temp_output_31_0 = pow( tex2D( _Halftone_Texture, staticSwitch34 ).r , _Halftone_Power );
			#ifdef _INVERT_TEX_ON
				float staticSwitch29 = ( 1.0 - temp_output_31_0 );
			#else
				float staticSwitch29 = temp_output_31_0;
			#endif
			#ifdef _STEP_1_HALFTONE_ON
				float staticSwitch27 = staticSwitch29;
			#else
				float staticSwitch27 = 1.0;
			#endif
			float3 ase_worldPos = i.worldPos;
			#if defined(LIGHTMAP_ON) && UNITY_VERSION < 560 //aseld
			float3 ase_worldlightDir = 0;
			#else //aseld
			float3 ase_worldlightDir = normalize( UnityWorldSpaceLightDir( ase_worldPos ) );
			#endif //aseld
			float3 ase_worldNormal = WorldNormalVector( i, float3( 0, 0, 1 ) );
			float dotResult44 = dot( ase_worldlightDir , ase_worldNormal );
			float4 lerpResult19 = lerp( _Color_Step_1 , _Color_Step_2 , step( ( _Step_1 * 0.05 ) , ( staticSwitch27 * dotResult44 ) ));
			#ifdef _STEP_2_HALFTONE_ON
				float staticSwitch62 = staticSwitch29;
			#else
				float staticSwitch62 = 1.0;
			#endif
			float4 lerpResult55 = lerp( lerpResult19 , _Color_Base , step( ( _Step_2 * 0.05 ) , ( staticSwitch62 * dotResult44 ) ));
			#ifdef _EDGE_HALFTONE_ON
				float staticSwitch54 = staticSwitch29;
			#else
				float staticSwitch54 = 1.0;
			#endif
			float4 lerpResult47 = lerp( lerpResult55 , _Edge_Colour , step( ( staticSwitch54 * dotResult44 ) , ( -_Edge_Step * 0.1 ) ));
			float3 ase_worldViewDir = normalize( UnityWorldSpaceViewDir( ase_worldPos ) );
			float fresnelNdotV16 = dot( ase_worldNormal, ase_worldViewDir );
			float fresnelNode16 = ( 0.0 + 1.0 * pow( 1.0 - fresnelNdotV16, 5.0 ) );
			float4 lerpResult8 = lerp( lerpResult47 , _Edge_Colour , step( ( 0.7 * _Edge_Width ) , fresnelNode16 ));
			s63.Emission = ( clampResult2 * ( ase_lightColor * lerpResult8 ) ).rgb;
			s63.Metallic = 0.0;
			s63.Smoothness = 0.0;
			s63.Occlusion = 1.0;

			data.light = gi.light;

			UnityGI gi63 = gi;
			#ifdef UNITY_PASS_FORWARDBASE
			Unity_GlossyEnvironmentData g63 = UnityGlossyEnvironmentSetup( s63.Smoothness, data.worldViewDir, s63.Normal, float3(0,0,0));
			gi63 = UnityGlobalIllumination( data, s63.Occlusion, s63.Normal, g63 );
			#endif

			float3 surfResult63 = LightingStandard ( s63, viewDir, gi63 ).rgb;
			surfResult63 += s63.Emission;

			c.rgb = surfResult63;
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
7;29;2546;1004;2341.562;631.8455;1.6;True;True
Node;AmplifyShaderEditor.ScreenPosInputsNode;41;-7363.474,53.01741;Float;False;1;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;38;-7161.172,-99.68902;Float;False;Property;_Half_Tone_UVs;Half_Tone_UVs;15;0;Create;True;0;0;False;0;18;18;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;37;-7185.971,-328.096;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.DynamicAppendNode;43;-7110.27,81.73143;Float;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;35;-6836.182,-288.9405;Float;True;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;36;-6837.487,-26.59875;Float;True;2;2;0;FLOAT;0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.StaticSwitch;34;-6457.227,-181.0383;Float;False;Property;_Use_Screen_Space;Use_Screen_Space;13;0;Create;True;0;0;False;0;0;0;0;True;;Toggle;2;Key0;Key1;9;1;FLOAT2;0,0;False;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT2;0,0;False;6;FLOAT2;0,0;False;7;FLOAT2;0,0;False;8;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;32;-6023.025,-197.938;Float;True;Property;_Halftone_Texture;Halftone_Texture;10;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;33;-5872.225,125.7615;Float;False;Property;_Halftone_Power;Halftone_Power;11;0;Create;True;0;0;False;0;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.PowerNode;31;-5458.82,-31.53802;Float;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;30;-5200.127,47.76199;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StaticSwitch;29;-4842.623,-41.94343;Float;False;Property;_Invert_Tex;Invert_Tex;8;0;Create;True;0;0;False;0;0;0;0;True;;Toggle;2;Key0;Key1;9;1;FLOAT;0;False;0;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT;0;False;7;FLOAT;0;False;8;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;28;-4838.724,192.0618;Float;False;Constant;_Float2;Float 2;9;0;Create;True;0;0;False;0;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.WorldNormalVector;46;-5486.075,753.0768;Float;False;False;1;0;FLOAT3;0,0,1;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.WorldSpaceLightDirHlpNode;45;-5535.445,566.89;Float;False;False;1;0;FLOAT;0;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.StaticSwitch;27;-4437.025,192.0616;Float;False;Property;_Step_1_Halftone;Step_1_Halftone;9;0;Create;True;0;0;False;0;0;0;0;True;;Toggle;2;Key0;Key1;9;1;FLOAT;0;False;0;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT;0;False;7;FLOAT;0;False;8;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;26;-4370.725,10.06209;Float;False;Constant;_Float1;Float 1;8;0;Create;True;0;0;False;0;0.05;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;25;-4372.024,-100.4379;Float;False;Property;_Step_1;Step_1;7;0;Create;True;0;0;False;0;2.08;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.DotProductOpNode;44;-5009.327,665.6262;Float;False;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StaticSwitch;62;-4455.101,891.0403;Float;False;Property;_Step_2_Halftone;Step_2_Halftone;16;0;Create;True;0;0;False;0;0;0;0;True;;Toggle;2;Key0;Key1;9;1;FLOAT;0;False;0;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT;0;False;7;FLOAT;0;False;8;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;51;-4676.749,1710.386;Float;False;Property;_Edge_Step;Edge_Step;12;0;Create;True;0;0;False;0;0.94;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;23;-4089.922,-6.83783;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;24;-4092.522,198.5622;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;58;-4371.064,600.1464;Float;False;Property;_Step_2;Step_2;6;0;Create;True;0;0;False;0;5.6;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;59;-4369.766,710.6464;Float;False;Constant;_Float5;Float 5;8;0;Create;True;0;0;False;0;0.05;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;50;-4445.541,1869.616;Float;False;Constant;_Float3;Float 3;12;0;Create;True;0;0;False;0;0.1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;61;-4087.972,928.4317;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;60;-4088.964,693.7465;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.NegateNode;52;-4423.729,1714.749;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;20;-3029.257,-591.837;Float;False;Property;_Color_Step_1;Color_Step_1;4;0;Create;True;0;0;False;0;0.2666667,0.05882353,0.4313726,1;0,0,0,0;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StepOpNode;22;-2952.552,96.20811;Float;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StaticSwitch;54;-4431.658,1459.232;Float;False;Property;_Edge_Halftone;Edge_Halftone;14;0;Create;True;0;0;False;0;0;0;0;True;;Toggle;2;Key0;Key1;9;1;FLOAT;0;False;0;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT;0;False;7;FLOAT;0;False;8;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;56;-3027.472,-382.7155;Float;False;Property;_Color_Step_2;Color_Step_2;5;0;Create;True;0;0;False;0;0.6039216,0.4470589,1,1;0,0,0,0;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;19;-2475.46,-175.8377;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;21;-2605.181,1.919409;Float;False;Property;_Color_Base;Color_Base;3;0;Create;True;0;0;False;0;1,1,1,1;0,0,0,0;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;18;-2204.3,742.8796;Float;False;Property;_Edge_Width;Edge_Width;2;0;Create;True;0;0;False;0;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;53;-4088.13,1421.517;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;17;-2205.3,613.8796;Float;False;Constant;_Float0;Float 0;4;0;Create;True;0;0;False;0;0.7;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.StepOpNode;57;-2933.781,343.0697;Float;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;49;-4100.755,1730.396;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StepOpNode;48;-2932.95,615.6477;Float;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;55;-2190.64,-175.2746;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.FresnelNode;16;-1928.305,853.8794;Float;False;Standard;WorldNormal;ViewDir;False;5;0;FLOAT3;0,0,1;False;4;FLOAT3;0,0,0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;5;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;13;-2244.398,428.3433;Float;False;Property;_Edge_Colour;Edge_Colour;1;0;Create;True;0;0;False;0;0,0,0,1;0,0,0,0;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;15;-1929.189,618.5464;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StepOpNode;14;-1600.189,638.5464;Float;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;6;-1081.979,48.91578;Float;False;Property;_Shadow_Amount;Shadow_Amount;0;0;Create;True;0;0;False;0;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.LightAttenuation;5;-1085.979,-93.08423;Float;False;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;47;-1932.414,-14.35903;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;68;-871.4985,-393.4166;Float;True;Property;_Normal_Map;Normal_Map;18;0;Create;True;0;0;False;0;None;None;True;0;False;bump;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;4;-734.9787,29.91578;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;8;-1076.979,411.9158;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.LightColorNode;7;-1078.979,202.9158;Float;False;0;3;COLOR;0;FLOAT3;1;FLOAT;2
Node;AmplifyShaderEditor.RangedFloatNode;66;-562.4985,-135.4166;Float;False;Property;_Normal_Intensity;Normal_Intensity;17;0;Create;True;0;0;False;0;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ComponentMaskNode;67;-553.4985,-331.4166;Float;False;True;True;False;False;1;0;COLOR;0,0,0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.ClampOpNode;2;-497.9783,60.91577;Float;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;3;-730.9786,286.9158;Float;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;65;-281.4985,-255.4166;Float;False;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;1;-153.979,202.9158;Float;True;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.DynamicAppendNode;64;-88.49854,-112.4166;Float;False;FLOAT3;4;0;FLOAT2;0,0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.CustomStandardSurface;63;96.50146,153.5834;Float;False;Metallic;Tangent;6;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,1;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;1;False;1;FLOAT3;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;11;366.3337,-5.550508;Float;False;True;2;Float;ASEMaterialInspector;0;0;CustomLighting;HalftoneShaders/3_Tone_Normal_2018;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;False;0;False;Opaque;;Geometry;ForwardOnly;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;False;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;-1;False;-1;-1;False;-1;0;True;0.00075;0,0,0,1;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;43;0;41;1
WireConnection;43;1;41;2
WireConnection;35;0;37;0
WireConnection;35;1;38;0
WireConnection;36;0;38;0
WireConnection;36;1;43;0
WireConnection;34;1;35;0
WireConnection;34;0;36;0
WireConnection;32;1;34;0
WireConnection;31;0;32;1
WireConnection;31;1;33;0
WireConnection;30;0;31;0
WireConnection;29;1;31;0
WireConnection;29;0;30;0
WireConnection;27;1;28;0
WireConnection;27;0;29;0
WireConnection;44;0;45;0
WireConnection;44;1;46;0
WireConnection;62;1;28;0
WireConnection;62;0;29;0
WireConnection;23;0;25;0
WireConnection;23;1;26;0
WireConnection;24;0;27;0
WireConnection;24;1;44;0
WireConnection;61;0;62;0
WireConnection;61;1;44;0
WireConnection;60;0;58;0
WireConnection;60;1;59;0
WireConnection;52;0;51;0
WireConnection;22;0;23;0
WireConnection;22;1;24;0
WireConnection;54;1;28;0
WireConnection;54;0;29;0
WireConnection;19;0;20;0
WireConnection;19;1;56;0
WireConnection;19;2;22;0
WireConnection;53;0;54;0
WireConnection;53;1;44;0
WireConnection;57;0;60;0
WireConnection;57;1;61;0
WireConnection;49;0;52;0
WireConnection;49;1;50;0
WireConnection;48;0;53;0
WireConnection;48;1;49;0
WireConnection;55;0;19;0
WireConnection;55;1;21;0
WireConnection;55;2;57;0
WireConnection;15;0;17;0
WireConnection;15;1;18;0
WireConnection;14;0;15;0
WireConnection;14;1;16;0
WireConnection;47;0;55;0
WireConnection;47;1;13;0
WireConnection;47;2;48;0
WireConnection;4;0;5;0
WireConnection;4;1;6;0
WireConnection;8;0;47;0
WireConnection;8;1;13;0
WireConnection;8;2;14;0
WireConnection;67;0;68;0
WireConnection;2;0;4;0
WireConnection;3;0;7;0
WireConnection;3;1;8;0
WireConnection;65;0;67;0
WireConnection;65;1;66;0
WireConnection;1;0;2;0
WireConnection;1;1;3;0
WireConnection;64;0;65;0
WireConnection;64;2;68;3
WireConnection;63;1;64;0
WireConnection;63;2;1;0
WireConnection;11;13;63;0
ASEEND*/
//CHKSM=E03974BB9FE418BC2BCA8D3752F764D642A71ABA