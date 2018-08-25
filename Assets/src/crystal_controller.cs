using UnityEngine;
using System.Collections;

public class crystal_controller : MonoBehaviour
{
	public	Transform	player			=	null;

	private	finish		finish_ctrl		=	null;

	private	Vector3		new_position	=	Vector3.zero;
	private	Vector3		original_position;

	private	bool		is_inside		=	false;

	private	float		radius			=	2.5f;
	private	float		speed			=	0.25f;
	private	float		distance		=	0.0f;
	private	float		random_x		=	0.0f;
	private	float		random_y		=	0.0f;
	private	float		random_z		=	0.0f;
	private	float		scale			=	5.0f;
	private	float		lerp_timer		=	0.0f;

	void Start()
	{
		original_position	=	transform.TransformPoint(transform.localPosition);
		player				=	GameObject.FindWithTag("MainCamera").GetComponent<Transform>();
		finish_ctrl			=	GameObject.FindWithTag("finish").GetComponent<finish>();
	}
	
	void FixedUpdate()
	{
		distance	=	Vector3.Distance(player.position, transform.position);
		
		if (distance < radius)
		{
			if (!is_inside)
			{
				is_inside			=	true;
				finish_ctrl.HP--;
			}
			if (lerp_timer	<	1.0f)
				lerp_timer		+=	Time.deltaTime * speed;
		}

		if (is_inside)
		{
			float	phi			=	Mathf.PerlinNoise(original_position.x + Time.time * speed, original_position.z + Time.time * speed) * 4.0f * Mathf.PI;
			float	psi			=	Mathf.PerlinNoise(original_position.z + Time.time * speed, original_position.y + Time.time * speed) * 4.0f * Mathf.PI - 2.0f * Mathf.PI;
			new_position		=	Vector3.Lerp(transform.position, sphere_parametric(phi, psi), lerp_timer);
		}

		else
		{
			new_position		=	original_position;
			random_x			=	Mathf.PerlinNoise(original_position.y + Time.time * speed, original_position.z + Time.time * speed) * scale;
			random_y			=	Mathf.PerlinNoise(original_position.x + Time.time * speed, original_position.z + Time.time * speed) * scale;
			random_z			=	Mathf.PerlinNoise(original_position.x + Time.time * speed, original_position.y + Time.time * speed);
			new_position		+=	new Vector3(random_x, random_y, random_z);
		}

		transform.position		=	new_position;
	}

	private	Vector3	sphere_parametric(float phi, float psi)
	{
		return new Vector3(	player.position.x + (radius - 1.0f) * Mathf.Cos(psi) * Mathf.Cos(phi),
							player.position.y + (radius - 1.0f) * Mathf.Cos(psi) * Mathf.Sin(phi),
							player.position.z + (radius - 1.0f) * Mathf.Sin(psi));
	}
}
