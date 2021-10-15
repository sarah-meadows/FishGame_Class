// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "HalftoneShaders/Inklines_Normal_2018"
{
	Properties
	{
		_Color_Base("Color_Base", Color) = (1,1,1,1)
		_Shadow_Step("Shadow_Step", Float) = 0
		_Tone_Power("Tone_Power", Float) = 1
		_Color_Shadow("Color_Shadow", Color) = (0.3882353,0.3882353,0.3882353,1)
		_Halftone_Texture("Halftone_Texture", 2D) = "white" {}
		_UV_Scale("UV_Scale", Float) = 34
		_Normal_Map("Normal_Map", 2D) = "bump" {}
		_Normal_Intensity("Normal_Intensity", Float) = 1
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" }
		Cull Back
		CGPROGRAM
		#include "UnityPBSLighting.cginc"
		#include "UnityCG.cginc"
		#pragma target 3.0
		#pragma surface surf StandardCustomLighting keepalpha noshadow exclude_path:deferred 
		struct Input
		{
			float3 worldNormal;
			INTERNAL_DATA
			float2 uv_texcoord;
			float3 worldPos;
			float4 screenPos;
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
		uniform float4 _Color_Shadow;
		uniform float4 _Color_Base;
		uniform float _Shadow_Step;
		uniform sampler2D _Halftone_Texture;
		uniform float _UV_Scale;
		uniform float _Tone_Power;

		inline half4 LightingStandardCustomLighting( inout SurfaceOutputCustomLightingCustom s, half3 viewDir, UnityGI gi )
		{
			UnityGIInput data = s.GIData;
			Input i = s.SurfInput;
			half4 c = 0;
			SurfaceOutputStandard s58 = (SurfaceOutputStandard ) 0;
			s58.Albedo = float3( 0,0,0 );
			float2 uv_Normal_Map = i.uv_texcoord * _Normal_Map_ST.xy + _Normal_Map_ST.zw;
			float4 tex2DNode53 = tex2D( _Normal_Map, uv_Normal_Map );
			float3 appendResult57 = (float3(( (tex2DNode53).rg * _Normal_Intensity ) , tex2DNode53.b));
			s58.Normal = WorldNormalVector( i , appendResult57 );
			float3 ase_worldPos = i.worldPos;
			#if defined(LIGHTMAP_ON) && UNITY_VERSION < 560 //aseld
			float3 ase_worldlightDir = 0;
			#else //aseld
			float3 ase_worldlightDir = normalize( UnityWorldSpaceLightDir( ase_worldPos ) );
			#endif //aseld
			float3 ase_worldNormal = WorldNormalVector( i, float3( 0, 0, 1 ) );
			float dotResult44 = dot( ase_worldlightDir , ase_worldNormal );
			float4 ase_screenPos = float4( i.screenPos.xyz , i.screenPos.w + 0.00000000001 );
			float2 appendResult43 = (float2(ase_screenPos.x , ase_screenPos.y));
			float4 lerpResult19 = lerp( _Color_Shadow , _Color_Base , step( _Shadow_Step , ( dotResult44 * pow( tex2D( _Halftone_Texture, ( _UV_Scale * appendResult43 ) ).r , _Tone_Power ) ) ));
			s58.Emission = lerpResult19.rgb;
			s58.Metallic = 0.0;
			s58.Smoothness = 0.0;
			s58.Occlusion = 1.0;

			data.light = gi.light;

			UnityGI gi58 = gi;
			#ifdef UNITY_PASS_FORWARDBASE
			Unity_GlossyEnvironmentData g58 = UnityGlossyEnvironmentSetup( s58.Smoothness, data.worldViewDir, s58.Normal, float3(0,0,0));
			gi58 = UnityGlobalIllumination( data, s58.Occlusion, s58.Normal, g58 );
			#endif

			float3 surfResult58 = LightingStandard ( s58, viewDir, gi58 ).rgb;
			surfResult58 += s58.Emission;

			c.rgb = surfResult58;
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
7;29;2546;1004;3490.132;1694.94;2.860298;True;True
Node;AmplifyShaderEditor.ScreenPosInputsNode;41;-3360.196,311.217;Float;True;1;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;38;-3319.894,157.5107;Float;False;Property;_UV_Scale;UV_Scale;5;0;Create;True;0;0;False;0;34;18;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;43;-3105.992,339.931;Float;True;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;36;-2833.209,231.6008;Float;True;2;2;0;FLOAT;0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;51;-2429.171,206.2518;Float;True;Property;_Halftone_Texture;Halftone_Texture;4;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.WorldNormalVector;46;-2303.811,-175.9776;Float;False;False;1;0;FLOAT3;0,0,1;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.WorldSpaceLightDirHlpNode;45;-2353.181,-362.1634;Float;False;False;1;0;FLOAT;0;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.RangedFloatNode;50;-2153.927,448.4667;Float;False;Property;_Tone_Power;Tone_Power;2;0;Create;True;0;0;False;0;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.DotProductOpNode;44;-1827.062,-263.4278;Float;False;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.PowerNode;49;-1843.819,231.9413;Float;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;53;-946.8943,-1085.639;Float;True;Property;_Normal_Map;Normal_Map;6;0;Create;True;0;0;False;0;None;None;True;0;False;bump;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;47;-1526.113,-135.5546;Float;False;Property;_Shadow_Step;Shadow_Step;1;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;48;-1524.278,58.95117;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ComponentMaskNode;55;-598.8937,-1092.639;Float;False;True;True;False;False;1;0;COLOR;0,0,0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;56;-606.8937,-864.6389;Float;False;Property;_Normal_Intensity;Normal_Intensity;7;0;Create;True;0;0;False;0;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;21;-1253.143,-398.5847;Float;False;Property;_Color_Base;Color_Base;0;0;Create;True;0;0;False;0;1,1,1,1;0,0,0,0;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;20;-1249.245,-818.485;Float;False;Property;_Color_Shadow;Color_Shadow;3;0;Create;True;0;0;False;0;0.3882353,0.3882353,0.3882353,1;0,0,0,0;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StepOpNode;22;-1172.543,-125.5852;Float;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;54;-351.8942,-1095.639;Float;False;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.DynamicAppendNode;57;-129.8943,-983.6389;Float;False;FLOAT3;4;0;FLOAT2;0,0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.LerpOp;19;-695.4471,-402.4851;Float;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.CustomStandardSurface;58;175.9362,-422.7124;Float;False;Metallic;Tangent;6;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,1;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;1;False;1;FLOAT3;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;11;463.5146,-654.4101;Float;False;True;2;Float;ASEMaterialInspector;0;0;CustomLighting;HalftoneShaders/Inklines_Normal_2018;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;False;0;False;Opaque;;Geometry;ForwardOnly;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;False;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;-1;False;-1;-1;False;-1;0;False;0.00075;0,0,0,1;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;43;0;41;1
WireConnection;43;1;41;2
WireConnection;36;0;38;0
WireConnection;36;1;43;0
WireConnection;51;1;36;0
WireConnection;44;0;45;0
WireConnection;44;1;46;0
WireConnection;49;0;51;1
WireConnection;49;1;50;0
WireConnection;48;0;44;0
WireConnection;48;1;49;0
WireConnection;55;0;53;0
WireConnection;22;0;47;0
WireConnection;22;1;48;0
WireConnection;54;0;55;0
WireConnection;54;1;56;0
WireConnection;57;0;54;0
WireConnection;57;2;53;3
WireConnection;19;0;20;0
WireConnection;19;1;21;0
WireConnection;19;2;22;0
WireConnection;58;1;57;0
WireConnection;58;2;19;0
WireConnection;11;13;58;0
ASEEND*/
//CHKSM=4D81522C04DA1A73E10BC5FCE151091AA6036A8A