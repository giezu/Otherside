using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System;

public class GameManager : MonoBehaviour
{
	public	int			bad_decisions;
	public	int			good_decisions;
	public	int			all_decisions	=	3;
	public 	float		ratio			=	0.5f;
	public	int			room_number;
	public	Transform	doors;

	public	GameObject	fader		=	null;
	private	screen_fader	scr_fader;

	private	float		max_blur	=	2.5f;
	private	float		blur_speed	=	3.0f;

	public	bool		is_paused	=	false;
	private	bool		decision	=	false;
	private	bool		next_scene	=	false;
	private	string		next_scene_name;

	private	float		fade_timer	=	2.0f;

	private	debug_log	debug_log	=	null;

	public	UnityStandardAssets.ImageEffects.BlurOptimized	blur;

	void Start()
	{
		// PlayerPrefs.SetInt("good_decisions", good_decisions);
		// PlayerPrefs.SetInt("bad_decisions", bad_decisions);
		// PlayerPrefs.SetInt("room_number", room_number);
		debug_log		=	GameObject.FindWithTag("DebugLog").GetComponent<debug_log>();
		good_decisions	=	PlayerPrefs.GetInt("good_decisions", 0);
		bad_decisions	=	PlayerPrefs.GetInt("bad_decisions", 0);
		room_number		=	PlayerPrefs.GetInt("room_number", 0);

		debug_log.Log("Info\n");
		debug_log.Log("[" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "]\n");
		debug_log.Log("Player good_decisions: " + good_decisions + '\n');
		debug_log.Log("Player bad_decisions: " + bad_decisions + '\n');
		debug_log.Log("Player room_number: " + room_number + '\n');


		scr_fader		=	fader.GetComponent<screen_fader>();
		Cursor.visible	=	false;
		ratio			=	ratio	+	((float)good_decisions	/	all_decisions)	*	0.5f	-
										((float)bad_decisions	/	all_decisions)	*	0.5f;
		if (doors != null)
		{
			for (int i = 0; i < doors.childCount; ++i)
			{
				if (i == (room_number - 1))
				{
					doors.GetChild(i).GetComponent<door_controller>().open();
					break;
				}

				doors.GetChild(i).gameObject.SetActive(false);
			}		
		}
	}

	void Update()
	{
		if (Input.GetButtonDown("Pause"))
		{
			is_paused	=	!is_paused;
		}

		if (is_paused	&&	blur.blurSize	<	max_blur)
		{
			blur.enabled	=	true;
			blur.blurSize	+=	Time.fixedDeltaTime	*	blur_speed;
			Time.timeScale	=	0.0f;
		}

		else
		{
			if (blur.blurSize	>	0.0f)
				
				blur.blurSize	-=	Time.fixedDeltaTime	*	blur_speed;
			
			else
			{
				blur.enabled	=	false;
				Time.timeScale	=	1.0f;
			}
		}

		if (next_scene)
		{
			if (fade_timer	>	0)
			{
				if (decision)

					scr_fader.FadeToWhite();

				else

					scr_fader.FadeToBlack();
				
				fade_timer	-=	Time.deltaTime;
			}

			else
			{
				debug_log.save();
				SceneManager.LoadScene(next_scene_name);
			}
		}
	}

	void LateUpdate()
	{
		if (Input.GetButtonDown("Screenshot"))
		{
			ScreenCapture.CaptureScreenshot(System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + ".png", 4);
		}
	}

	public void	load_scene(string scene_name)
	{
		next_scene		=	true;
		next_scene_name	=	scene_name;
		decision		=	true;
		debug_log.Log("Info\n");
		debug_log.Log("[" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "]\n");
		debug_log.Log("Player loaded level: " + scene_name + '\n');
	}

	public void load_scene(string scene_name, bool good_decision)
	{
		next_scene		=	true;
		decision		=	good_decision;
		next_scene_name	=	scene_name;

		if (!decision)

			PlayerPrefs.SetInt("bad_decisions", PlayerPrefs.GetInt("bad_decisions", 0) + 1);
		
		else

			PlayerPrefs.SetInt("good_decisions", PlayerPrefs.GetInt("good_decisions", 0) + 1);

		PlayerPrefs.SetInt("room_number", PlayerPrefs.GetInt("room_number", 0) + 1);
		debug_log.Log("Info\n");
		debug_log.Log("[" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "]\n");
		debug_log.Log("Room number: " + PlayerPrefs.GetInt("room_number", 0) + '\n');

		debug_log.Log("Info\n");
		debug_log.Log("[" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "]\n");
		debug_log.Log("Player loaded level: " + scene_name + '\n');
	}
}
