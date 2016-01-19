Shader "Custom/CircleOverlap" {
  Properties {
	_Color ("Color", Color) = (1,1,1,1)
  }
  SubShader {
    Tags { "Queue"="Geometry+1" }
    LOD 200

    pass
    {
      Blend SrcAlpha OneMinusSrcAlpha
      ZTest LEqual
      CGPROGRAM
      #pragma vertex vert
      #pragma fragment frag
      #include "UnityCG.cginc"
  
      struct VIn {
        float4 vertex : POSITION;
        float2 texcoord : TEXCOORD0;
		float4 color : COLOR;
      };
      
      struct VOut {
        float4 vertex : SV_POSITION;
        float2 uv : TEXCOORD0;
		float4 color : COLOR;
      };

	  float4 _Color;
  
      VOut vert (VIn vin) {
        VOut vout;
        vout.vertex = mul(UNITY_MATRIX_MVP, vin.vertex);
        vout.uv = vin.texcoord;
        vout.color = vin.color;
        return vout;
      }
      
      float4 frag(VOut vout) : COLOR
      {
		
        //return float4(1,1,1,1);
        float circle = distance(float2(0.5,0.5),vout.uv)*2;
        float outerCircle = step(1,circle);
        clip(-outerCircle);
        if(vout.color.x == _Color.x && vout.color.y == _Color.y && vout.color.z == _Color.z && vout.color.w == _Color.w){
        	return float4(1,1,1,1);
        }
        if((degrees(atan2(vout.uv.x-0.5,vout.uv.y-0.5)+_Time.x*8)+180)%10 < 5){
        	return _Color;
        }
        else{
        	discard;
        	return vout.color;
        }
//        discard;
//        return float4(1,1,1,0.1);
      }
      
      ENDCG
    }
    pass
    {
      Blend SrcAlpha OneMinusSrcAlpha
      Offset 1,-1000
      ZTest LEqual
      CGPROGRAM
      #pragma vertex vert
      #pragma fragment frag
      #include "UnityCG.cginc"
  
      struct VIn {
        float4 vertex : POSITION;
        float2 texcoord : TEXCOORD0;
		float4 color : COLOR;
      };
      
      struct VOut {
        float4 vertex : SV_POSITION;
        float2 uv : TEXCOORD0;
		float4 color : COLOR;
      };

	  float4 _Color;

      VOut vert (VIn vin) {
        VOut vout;
        vout.vertex = mul(UNITY_MATRIX_MVP, vin.vertex);
        vout.uv = vin.texcoord;
        vout.color = vin.color;
        return vout;
      }
      
      float4 frag(VOut vout) : COLOR
      {
        float circle = distance(float2(0.5,0.5),vout.uv)*2;
        float innerCircle = step(0.98,circle);

        //half4 color = tex2D(_MainTex, i.uv);

        if(vout.color.x == _Color.x && vout.color.y == _Color.y && vout.color.z == _Color.z && vout.color.w == _Color.w){
        	return float4(1,1,1,1);
        }
        clip(-innerCircle);
        return float4(0,0,0,0);

      }
      
      ENDCG
    }
  } 
  FallBack "Diffuse"
}