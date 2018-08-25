using System;
using UnityEngine;
using System.Collections;

public class teleport_controller : MonoBehaviour
{
	private	GameManager game_manager;
	public 	string 		lvl_name;
	public	debug_log	debug_log		=	null;
	public	GameObject	player_spawner	=	null;

	void Start()
	{
		game_manager	=	GameObject.FindWithTag("GameController").GetComponent<GameManager>();
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
				PlayerPrefs.SetFloat("player_pos_x", player_spawner.transform.position.x);
				PlayerPrefs.SetFloat("player_pos_y", player_spawner.transform.position.y);
				PlayerPrefs.SetFloat("player_pos_z", player_spawner.transform.position.z);
				game_manager.load_scene(lvl_name);
			}
		}
	}

	private float Angle(Vector2 v1, Vector2 v2)
	{
		float	perp_dot	=	v1.x * v2.y - v1.y * v2.x;
		return Mathf.Atan2(perp_dot, Vector2.Dot(v1, v2)) * Mathf.Rad2Deg;
	}
}
