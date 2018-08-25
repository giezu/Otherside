using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class final : MonoBehaviour
{
	public	screen_fader	fader	=	null;

	public	subtitles_controller	sub	=	null;

	public	bool	good_end	=	false;
	private	bool	displ_sub	=	false;

	public	float	max_timer	=	0.0f;
	private	float	fade_timer	=	0.0f;

	void Update()
	{
		if (sub.displaying)
		{
			displ_sub	=	true;
		}

		if (!sub.displaying && displ_sub)
		{
			fade_timer	+=	Time.deltaTime;
			if (good_end)
			{
				fader.FadeToWhite();
			}

			else
			{
				fader.FadeToBlack();
			}
		}

		if (fade_timer	>	max_timer)
		{
			SceneManager.LoadScene("credits");
		}
	}
}
