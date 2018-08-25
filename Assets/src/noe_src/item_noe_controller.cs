using UnityEngine;
using System.Collections;
using System;

public class item_noe_controller : MonoBehaviour
{
	public	Transform	cursor	=	null;

	private	Color		original_color;

	public	debug_log	debug_log		=	null;

	private	Renderer	rend;

	public	string		item_name		=	"";

	public	Rigidbody	rb				=	null;

	[Header("Controlsy")]
	public	bool		looking			=	false;
	public	float		color_speed;

	private	float		color_timer		=	-1.0f/2.0f;

	private	bool		is_using		=	false;

	public	float		follow_speed	=	1.0f;
	private	float		push_force		=	200.0f;

	void Start()
	{
		cursor				=	GameObject.FindWithTag("Cursor").GetComponent<Transform>();
		debug_log			=	GameObject.FindWithTag("DebugLog").GetComponent<debug_log>();
		rb					=	GetComponent<Rigidbody>();
		rend				=	GetComponent<Renderer>();
		original_color		=	rend.material.color;
	}

	void Update()
	{
		if (looking && !is_using)

			Glow();

		else
		{
			color_timer	=	-1.0f/2.0f;
			Fade();
		}
	}

	void FixedUpdate()
	{
		if (is_using)
		{
			transform.position	=	Vector3.Lerp(	transform.position,
													cursor.position,
													Time.deltaTime * follow_speed);
		}
	}

	void Glow()
	{
		debug_log.Log("Info\n");
		debug_log.Log("[" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "]\n");
		debug_log.Log("Player looked at object: " + gameObject.name + '\n');
		rend.material.color	=	Color.Lerp(original_color, Color.white, (Mathf.Sin(color_timer * color_speed * Mathf.PI) + 1.0f) / 2.0f);
		color_timer	+=	Time.deltaTime;
	}

	void Fade()
	{
		rend.material.color	=	Color.Lerp(rend.material.color, original_color, Time.deltaTime);
	}

	public void Use()
	{
		is_using	=	!is_using;
		if (!is_using)
		{
			rb.AddForce(Camera.main.gameObject.transform.forward * push_force * rb.mass);
		}
		looking		=	is_using;
		rb.useGravity	=	!is_using;
	}
}
