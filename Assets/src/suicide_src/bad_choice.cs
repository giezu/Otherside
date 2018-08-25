using UnityEngine;
using System.Collections;
using System;

public class bad_choice : MonoBehaviour
{
	public	GameManager	game_manager	=	null;

	public	debug_log	debug_log		=	null;

	public	bool		decision		=	false;

	[Header("Scena")]
	public	string		scene_name;

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			debug_log.Log("Info\n");
			debug_log.Log("[" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "]\n");
			debug_log.Log("Player made: " + decision + "decision\n");

			game_manager.load_scene(scene_name, decision);
		}
	}
}
