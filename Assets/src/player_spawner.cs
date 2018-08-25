using UnityEngine;
using System.Collections;

public class player_spawner : MonoBehaviour
{
	private	Vector3		player_position;
	public	GameObject	player	=	null;
	
	void Start()
	{
		player_position	=	new	Vector3(PlayerPrefs.GetFloat("player_pos_x", 19.0f),
										PlayerPrefs.GetFloat("player_pos_y", -0.38f),
										PlayerPrefs.GetFloat("player_pos_z", 5.0f));

		player.transform.position	=	player_position;
	}
}
