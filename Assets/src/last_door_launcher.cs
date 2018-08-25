using UnityEngine;
using System.Collections;

public class last_door_launcher : MonoBehaviour
{
	public	GameObject	door	=	null;
	public	GameObject	floor	=	null;

	public	GameManager	game_manager	=	null;

	void Start()
	{
		if (game_manager.bad_decisions + game_manager.good_decisions >= game_manager.all_decisions)
		{
			door.SetActive(true);
			floor.SetActive(true);
		}
	}
}
