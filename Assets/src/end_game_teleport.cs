using System;
using UnityEngine;
using System.Collections;

public class end_game_teleport : MonoBehaviour
{
	private	GameManager game_manager;
	private	bool		good_ending		=	false;
	public	debug_log	debug_log		=	null;

	void Start()
	{
		game_manager	=	GameObject.FindWithTag("GameController").GetComponent<GameManager>();
	}

	void Update()
	{
		if (game_manager.ratio > 0.5)
			good_ending	=	true;		
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			Vector3	dir	=	(other.gameObject.transform.position - gameObject.transform.position);
			Vector2	dir_2d	=	new Vector2(dir.x, dir.z).normalized;
			Vector2	transform_2d	=	new Vector2(transform.forward.x, transform.forward.z).normalized;
			
			if (Angle(transform_2d, dir_2d) > 0)
			{
				debug_log.Log("Info\n");
				debug_log.Log("[" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "]\n");
				debug_log.Log("Player teleported\n");
				if (good_ending)

					game_manager.load_scene("good_ending");

				else

					game_manager.load_scene("bad_ending");
			}
		}
	}

	private float Angle(Vector2 v1, Vector2 v2)
	{
		float	perp_dot	=	v1.x * v2.y - v1.y * v2.x;
		return Mathf.Atan2(perp_dot, Vector2.Dot(v1, v2)) * Mathf.Rad2Deg;
	}
}
