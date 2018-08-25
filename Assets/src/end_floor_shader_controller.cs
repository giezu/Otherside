using UnityEngine;
using System.Collections;

public class end_floor_shader_controller : MonoBehaviour
{
	public	Transform	player_position	=	null;
	public	float		radius			=	1.0f;

	private	Material	shader	=	null;


	void Start()
	{
		Renderer	mesh_renderer	=	GetComponent<Renderer>();
		shader	=	mesh_renderer.material;
	}
	
	void Update()
	{
		shader.SetVector("_player_position", player_position.position);
		shader.SetFloat("_radius", radius);
	}
}
