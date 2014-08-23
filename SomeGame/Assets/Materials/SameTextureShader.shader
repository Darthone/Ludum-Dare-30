Shader "Tri-Planar World" {
  Properties {
		_Color ("Main Color", Color) = (1,0.5,0.5,1)
		_Side("Side", 2D) = "white" {}
		_SideScale("Side Scale", Float) = 2
	}
	
	SubShader {
		Tags {
			"Queue"="Geometry"
			"IgnoreProjector"="False"
			"RenderType"="Opaque"
		}
 
		Cull Back
		ZWrite On
		
		CGPROGRAM
		#pragma surface surf Lambert
		#pragma exclude_renderers flash
 
		sampler2D _Side;
		float _SideScale;
		float4 _Color;

		struct Input {
			float3 worldPos;
			float3 worldNormal;
		};
			
		void surf (Input IN, inout SurfaceOutput o) {
			float3 projNormal = saturate(pow(IN.worldNormal * 1.4, 4));
			float3 y = 0;
			// SIDE X
			//float3 x = tex2D(_Side, frac(IN.worldPos.zy * _SideScale)) * abs(IN.worldNormal.x);
			// SIDE Z	
			float3 z = tex2D(_Side, frac(IN.worldPos.xy * _SideScale)) * abs(IN.worldNormal.z);
			
			o.Albedo = z;
			o.Albedo *= _Color.rgb;
			//o.Albedo = lerp(o.Albedo, x, projNormal.x);
			//o.Albedo = lerp(o.Albedo, y, projNormal.y);
		} 
		ENDCG
	}
	Fallback "Diffuse"
}