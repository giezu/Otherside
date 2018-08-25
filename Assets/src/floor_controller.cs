using UnityEngine;
using System.Collections;

public class floor_controller : MonoBehaviour
{
	private	float	noise_strength	=   1.0f;
	private float	max_noise		=	1.5f;
	private	float	radius			=	5.0f;

	private	bool	is_door_on		=	false;

	private	Transform	player_position	=	null;

	private Vector3 original_position;
	private Vector3 new_position;

	void Start()
	{
		GameManager	game_manager	=	GameObject.FindWithTag("GameController").GetComponent<GameManager>();
		player_position				=	GameObject.FindWithTag("Player").GetComponent<Transform>();
		new_position     			=   transform.position;
		noise_strength				=	(1.0f - game_manager.ratio) * max_noise;
		original_position   		=   transform.position;

        RaycastHit[]	hit;

        float	temp_radius	=	0.0f;
        while (temp_radius	<	radius)
        {
        	hit	=	Physics.SphereCastAll(transform.position, temp_radius, Vector3.one, 1.0f);

        	for (int i = 0; i < hit.Length; ++i)
        	{
	        	if (hit[i].collider.gameObject.tag	==	"Door")
	        		is_door_on	=	true;        		
        	}

	        temp_radius	+=	radius	*	0.1f;        	
        }
	}

	void FixedUpdate()
	{
		float	distance	=	Vector3.Distance(transform.position, player_position.position);

		if (!is_door_on)
		{
			if (distance < radius)
			{
				transform.position  =   Vector3.Lerp(transform.position, original_position, Time.deltaTime);
			}

			else
			{
				calculate_new_pos();
				transform.position  =   Vector3.Lerp(transform.position, new_position, Time.deltaTime);
			}			
		}
	}

	private void calculate_new_pos()
	{
		new_position.y   =  Mathf.PerlinNoise(original_position.x, original_position.z + Mathf.Sin(Time.time * 0.1f)) * noise_strength;
	}
};