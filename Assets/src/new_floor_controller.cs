using UnityEngine;
using System.Collections;

public class new_floor_controller : MonoBehaviour
{
	public	Transform	player_position	=	null;

	private float	max_noise		=	1.5f;
	private	float	noise_strength	=	1.0f;

	private	Vector4	color	=	Vector4.one;

	private	Material	shader	=	null;

	void Start()
	{
		Renderer	mesh_renderer	=	GetComponent<Renderer>();
		shader		=	mesh_renderer.material;
		GameManager	game_manager	=	GameObject.FindWithTag("GameController").GetComponent<GameManager>();
		noise_strength				=	game_manager.ratio * max_noise;
	}
	
	void Update()
	{
		shader.SetFloat("_distance", Vector3.Distance(transform.position, player_position.position));
		shader.SetVector("_Color", color);
        shader.SetFloat("_noise_height", Mathf.PerlinNoise(transform.position.x, transform.position.z + Time.time) * noise_strength);
		shader.SetFloat("_original_height", transform.position.y);
	}
}
