Shader "Custom/CircleOverlap" {
  Properties {
	_Color ("Color", Color) = (1,1,1,1)
  }
  SubShader {
    Tags { "Queue"="Geometry+1" }
    LOD 200
    pass
    {
      Blend ZERO ONE
      Offset 1,-1000
      ZTest off
      CGPROGRAM
      #pragma vertex vert
      #pragma fragment frag
      #include "UnityCG.cginc"
  
      struct VIn {
        float4 vertex : POSITION;
        float2 texcoord : TEXCOORD0;
      };
      
      struct VOut {
        float4 vertex : SV_POSITION;
        float2 uv : TEXCOORD0;
      };
  
      VOut vert (VIn vin) {
        VOut vout;
        vout.vertex = mul(UNITY_MATRIX_MVP, vin.vertex);
        vout.uv = vin.texcoord;
        return vout;
      }
      
      float4 frag(VOut vout) : COLOR
      {
        float circle = distance(float2(0.5,0.5),vout.uv)*2;
        float innerCircle = step(0.98,circle);
        clip(-innerCircle);
        return float4(1,1,1,1);
      }
      
      ENDCG
    }
    pass
    {
      //Blend SrcAlpha OneMinusSrcAlpha
      ZTest LEqual
      CGPROGRAM
      #pragma vertex vert
      #pragma fragment frag
      #include "UnityCG.cginc"
  
      struct VIn {
        float4 vertex : POSITION;
        float2 texcoord : TEXCOORD0;
      };
      
      struct VOut {
        float4 vertex : SV_POSITION;
        float2 uv : TEXCOORD0;
      };

	  fixed4 _Color;
  
      VOut vert (VIn vin) {
        VOut vout;
        vout.vertex = mul(UNITY_MATRIX_MVP, vin.vertex);
        vout.uv = vin.texcoord;
        return vout;
      }
      
      float4 frag(VOut vout) : COLOR
      {
		
        //return float4(1,1,1,1);
        float circle = distance(float2(0.5,0.5),vout.uv)*2;
        float outerCircle = step(1,circle);
        clip(-outerCircle);
        return _Color;
      }
      
      ENDCG
    }
  } 
  FallBack "Diffuse"
}