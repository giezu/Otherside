using UnityEngine;
using System.Collections;

public class audio_controller : MonoBehaviour
{
	void Start()
	{
		bool	music_on	=	PlayerPrefs.GetInt("music_on", 1) == 1 ? true : false;
		this.gameObject.SetActive(music_on);
		AudioSource	source	=	GetComponent<AudioSource>();
		source.volume	=	PlayerPrefs.GetFloat("volume", 1.0f);
	}
}
