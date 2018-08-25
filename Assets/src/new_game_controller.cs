using UnityEngine;
using System.Collections;

public class new_game_controller : MonoBehaviour
{
	void Start()
	{
		PlayerPrefs.SetInt("bad_decisions", 0);
		PlayerPrefs.SetInt("good_decisions", 0);
		PlayerPrefs.SetInt("room_number", 1);
	}
}
