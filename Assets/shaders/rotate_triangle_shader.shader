// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Custom/rotate_triangle_shader"
{
	SubShader
	{
		Pass
		{
		    CGPROGRAM

		    #pragma fragment frag
		    #pragma vertex vert

			uniform	float4	_Color;

			uniform	float3	rotation;

		    struct vertInput
		    {
		        float4 	position	:	POSITION;
		        float3	normal		:	NORMAL;
		    };  

		    struct vertOutput
		    {
		        float4	position	:	SV_POSITION;
		        float4	color		:	COLOR;
		    };

			float4x4
			rotate(float3 angle)
			{
				float	alpha_x	=	angle.x	*	3.14 / 180.0;
				float	alpha_y	=	angle.y	*	3.14 / 180.0;
				float	alpha_z	=	angle.z	*	3.14 / 180.0;

				float	sina_x;
				float	sina_y;
				float	sina_z;

				float	cosa_x;
				float	cosa_y;
				float	cosa_z;

				sincos(alpha_x, sina_x, cosa_x);
				sincos(alpha_y, sina_y, cosa_y);
				sincos(alpha_z, sina_z, cosa_z);

				float4x4	x_axis	=	float4x4(
					1,		0,				0,			0,
					0,		cosa_x,			-sina_x,	0,
					0,		sina_x,			cosa_x,		0,
					0,		0,				0,			1
				);

				float4x4	y_axis	=	float4x4(
					cosa_y,		0,			sina_y,		0,
					0,			1,			0,			0,
					-sina_y,	0,			cosa_y,		0,
					0,			0,			0,			1
				);

				float4x4	z_axis	=	float4x4(
					cosa_z,		-sina_z,	0,			0,
					sina_z,		cosa_z,		0,			0,
					0,			0,			1,			0,
					0,			0,			0,			1
				);
				
				return	x_axis	*	y_axis	*	z_axis;
			}

		    vertOutput
		    vert(vertInput input)
		    {
		        vertOutput	output;
		        output.position		=	mul(rotate(rotation), input.position);
		        output.position		=	UnityObjectToClipPos(output.position);
		        output.color		=	float4(1.0, 1.0, 1.0, 1.0);

		        return	output;
		    }

		    half4
		    frag(vertOutput output) : COLOR
		    {
		        return	_Color;
		    }

		    ENDCG
		}
	}
}
