using UnityEngine;
using System.Collections;

public class walking_human : MonoBehaviour
{
	private	Animation	anim_ctrl;
	public	Vector3		original_position;

	public	string		animation_name;

	public	float		a		=	1.0f;
	public	float		speed	=	1.0f;

	void Start()
	{
		anim_ctrl			=	GetComponent<Animation>();
		original_position	=	transform.position;
	}
	
	void Update()
	{
		if (!anim_ctrl.IsPlaying(animation_name))
			anim_ctrl.Play(animation_name);

		float	time			=	speed * Time.time;
		transform.position		=	original_position + new Vector3(a * Mathf.Cos(time), 0.0f, 0.0f);
		transform.eulerAngles	=	new Vector3(0.0f,
												Mathf.Sign(Mathf.Cos(time) - Mathf.Cos(time + Time.deltaTime)) * -90.0f,
												0.0f);
		// transform.eulerAngles	=	new Vector3(0.0f,
		// 										// Mathf.Sign(Mathf.Cos(time) - Mathf.Cos(time + Time.deltaTime)) * -90.0f
		// 										Mathf.Lerp(	90.0f, -90.0f,
		// 													Mathf.Min(	Mathf.Max(	transform.position.x - original_position.x,
		// 																			Mathf.Sign(Mathf.Cos(time) - Mathf.Cos(time + Time.deltaTime)) * a * 9.0f / 10.0f),
		// 																transform.position.x - original_position.x)),
		// 										0.0f);

	}
}
