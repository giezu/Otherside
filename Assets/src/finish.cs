using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class finish : MonoBehaviour
{
	public	screen_fader	fader	=	null;

	public	int		HP			=	100;
	public	float	fade_timer	=	2.0f;

	void Update()
	{
		if (HP < 0)
		{
			if (fade_timer > 0.0f)
			{
				fade_timer	-=	Time.deltaTime;
				fader.FadeToBlack();
			}

			else
			{
				SceneManager.LoadScene("credits");
			}
		}
	}
}
