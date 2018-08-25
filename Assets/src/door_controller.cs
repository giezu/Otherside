using UnityEngine;
using System.Collections;

public class door_controller : MonoBehaviour
{
	public void open()
	{
		transform.GetChild(1).gameObject.SetActive(false);
		transform.GetChild(2).gameObject.SetActive(true);
	}
}
