using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class credits_controller : MonoBehaviour
{
	public	float	scroll_speed	=	1.0f;
	public	float	max_height		=	0.0f;
	public	float	start_timer		=	5.0f;
	public	float	fade_timer		=	2.0f;
	
	private	float	max_fade		=	0.0f;
	private	float	screen_ratio	=	1.0f;

	public	Image	fader	=	null;

	public	GameObject	scroller	=	null;

	void Start()
	{
		max_fade		=	fade_timer;
		screen_ratio	=	Screen.height / 720.0f;
		max_height		*=	screen_ratio;
	}
	
	void Update ()
	{
		if (fade_timer	>	0.0f)
		{
			fade_timer	-=	Time.deltaTime;
			Color	fader_color	=	fader.color;
			fader_color.a	=	Mathf.Lerp(0.0f, 1.0f, fade_timer / max_fade);
			fader.color	=	fader_color;
		}

		else
		{
			if (start_timer	>	0.0f)
			{
				start_timer	-=	Time.deltaTime;
			}

			else
			{
				scroller.transform.position	+=	new	Vector3(0.0f, scroll_speed * Time.deltaTime, 0.0f);

				if (scroller.transform.position.y > max_height)
				{
					Debug.Log("Quit");
					Application.Quit();
				}
			}
		}
	}
}
