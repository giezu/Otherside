using UnityEngine;
using System.Collections;

public class new_triangle_controller : MonoBehaviour
{
	private	Vector3		actual_rotation	=	Vector3.zero;
	private	Vector3		next_rotation	=	Vector3.zero;
	private	Vector4		color			=	Vector3.zero;

	private	Material	shader			=	null;

	private	float		rotation_timer	=	0.0f;
	private	float		max_timer		=	5.0f;
	private	float		min_timer		=	5.0f;
	private	float		duration		=	0.0f;

	void Start()
	{
		duration		=	min_timer	+	max_timer	*	Random.value;;
		next_rotation	=	new Vector3(360.0f * Random.value, 360.0f * Random.value, 360.0f * Random.value);
		Renderer	mesh_renderer	=	GetComponent<Renderer>();
		shader	=	mesh_renderer.material;
		float	color_channel	=	Random.value;
		color	=	new Vector4(color_channel, color_channel, color_channel, 1.0f);
	}
	
	void Update()
	{
		rotation_timer	+=	Time.deltaTime;

		shader.SetVector("rotation", Vector3.Lerp(actual_rotation, next_rotation, rotation_timer / duration));
		shader.SetVector("_Color", color);

		if (rotation_timer	>=	duration)
		{
			rotation_timer	=	0.0f;
			duration		=	min_timer	+	max_timer	*	Random.value;
			actual_rotation	=	next_rotation;
			next_rotation	=	new Vector3(360.0f * Random.value, 360.0f * Random.value, 360.0f * Random.value);
		}
	}
}
