using UnityEngine;
using System.Collections;

public class triangle_controller : MonoBehaviour
{
	private	Quaternion	new_rotation;

	public	float	rotation_timer;
	private	float	max_timer	=	5.0f;
	private	float	min_timer	=	1.0f;

	void Start()
	{
		rotation_timer	=	min_timer + max_timer	*	Random.value;
		new_rotation.eulerAngles	=	Vector3.one * 360 * Random.value;
	}

	void FixedUpdate()
	{
		if (rotation_timer > 0.0f)
		{
			transform.rotation	=	Quaternion.Slerp(transform.rotation, new_rotation, Time.deltaTime);
			rotation_timer		-=	Time.deltaTime;
		}

		else
		{
			rotation_timer	=	min_timer + max_timer	*	Random.value;
			calculate_new_rotation();
		}
	}

	private void calculate_new_rotation()
	{
		new_rotation.eulerAngles	=	Vector3.one * 90 * Random.value;
	}
}
