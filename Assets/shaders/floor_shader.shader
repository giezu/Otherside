// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Unlit/floor_shader"
{
    Properties
    {
        [NoScaleOffset] _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Pass
        {
            // indicate that our pass is the "base" pass in forward
            // rendering pipeline. It gets ambient and main directional
            // light data set up; light direction in _WorldSpaceLightPos0
            // and color in _LightColor0
            Tags {"LightMode"="ForwardBase"}
        
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc" // for UnityObjectToWorldNormal
            #include "UnityLightingCommon.cginc" // for _LightColor0

            uniform	float4	_Color;
            uniform	float	_distance;
            uniform	float	_noise_height;
            uniform	float	_original_height;
			
            const	float	radius	=	50.0f;

            struct v2f
            {
                fixed4 diff : COLOR0; // diffuse lighting color
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata_base v)
            {
                v2f o;
                o.vertex	=	v.vertex;

                if (_distance < radius)
                {
                	o.vertex.y	-=	lerp(_original_height, _noise_height, _distance / radius);
                }

                else
                {
	                o.vertex.y	-=	_noise_height;
                }

                o.vertex = UnityObjectToClipPos(o.vertex);
                // get vertex normal in world space
                half3 worldNormal = UnityObjectToWorldNormal(v.normal);
                // dot product between normal and light direction for
                // standard diffuse (Lambert) lighting
                half nl = max(0, dot(worldNormal, _WorldSpaceLightPos0.xyz));
                // factor in the light color
                o.diff = nl * _LightColor0;
                // o.diff	=	nl	*	_Color;
                return o;
            }
            
            sampler2D _MainTex;

            fixed4 frag (v2f i) : SV_Target
            {
                // fixed4 col = tex2D(_MainTex, i.uv);
                // multiply by lighting
                // _Color *= i.diff;
                return i.diff;
            }
            ENDCG
        }
    }
}
