// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/end_floor_shader"
{
	Properties
	{
		_Color("Triangle_color", Color) = (0, 0, 0, 1)
		_player_position("Player position", Vector) = (0, 0, 0, 0)
		_radius("Radius", float) = 1.0
	}

	SubShader
	{
		Pass
		{
		    CGPROGRAM

		    #pragma fragment frag
		    #pragma vertex vert

			uniform	float4	_player_position;
			uniform	float	_radius;

			float4	_Color;

		    struct vertInput
		    {
		        float4 position : POSITION;
		    };  

		    struct vertOutput
		    {
		        float4	position	: SV_POSITION;
		        float4	color : COLOR;
		    };

		    vertOutput
		    vert(vertInput input)
		    {
		        vertOutput	output;
		        float4		position	=	input.position;
		        float4		position_modelspace	=	mul(UNITY_MATRIX_M, input.position);
		        output.color.a	=	0.0;
		    	if (distance(position_modelspace.xz, _player_position.xz) < _radius)
		    	{
		    		output.color.a	=	distance(position_modelspace.xz, _player_position.xz);
		    	}

		        output.position	=	UnityObjectToClipPos(position);		        
		        output.color.rgb	=	_Color.rgb;

		        return	output;
		    }

		    half4
		    frag(vertOutput output) : COLOR
		    {		    	
		    	if (output.color.a < 0.1)
		    	{
		    		discard;
		    	}
		    	return	output.color;
		        // return	lerp(float4(1.0, 1.0, 1.0, 1.0), output.color, output.color.a / _radius);
		    }

		    ENDCG
		}
	}
}
