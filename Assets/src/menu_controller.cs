using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class menu_controller : MonoBehaviour
{
	public	GameObject[]	options;
	public	GameManager		game_manager	=	null;
	public	GameObject		menu_handle		=	null;

	private	int				cursor	=	0;

	public	float			slider_speed	=	1.0f;
	private	bool			is_changing		=	false;
	private	bool			set_prefs		=	false;

	private	float			volume			=	1.0f;
	private	bool			music_on		=	true;

	void Update()
	{
		if (game_manager.is_paused)
		{
			set_prefs	=	false;
			menu_handle.SetActive(true);
			options[cursor].transform.GetChild(0).gameObject.SetActive(true);

			Slider	slider_component	=	null;
			Toggle	toggle_component	=	null;
			Button	button_component	=	null;

			slider_component	=	options[cursor].GetComponent<Slider>();
			toggle_component	=	options[cursor].GetComponent<Toggle>();
			button_component	=	options[cursor].GetComponent<Button>();

			if (Input.GetButtonDown("Fire1"))
			{
				if (toggle_component != null)
				{
					if (toggle_component.gameObject.name == "Music")
					{
						toggle_component.isOn	=	!toggle_component.isOn;
						music_on				=	toggle_component.isOn;						
					}
				}
			}

			if (Input.GetButtonDown("Fire1"))
			{
				if (button_component != null)
				{
					if (button_component.gameObject.name == "Quit")
						
						Application.Quit();
				}
			}

			if (Input.GetAxis("Horizontal") > 0.1f)
			{
				if (slider_component != null)
				{
					if (slider_component.gameObject.name == "Volume")
					{
						slider_component.value	+=	Time.fixedDeltaTime;
						volume					=	slider_component.value;						
					}
				}
			}

			else if (Input.GetAxis("Horizontal") < -0.1f)
			{
				if (slider_component != null)
				{
					if (slider_component.gameObject.name == "Volume")
					{
						slider_component.value	-=	Time.fixedDeltaTime;
						volume					=	slider_component.value;						
					}
				}
			}

			if (Input.GetAxis("Vertical") > 0.1f	&&
				!is_changing)
			{
				options[cursor].transform.GetChild(0).gameObject.SetActive(false);
				--cursor;
				if (cursor	<	0)
					cursor	=	options.Length - 1;
				is_changing	=	true;
			}

			else if (Input.GetAxis("Vertical") < -0.1f	&&
					!is_changing)
			{
				options[cursor].transform.GetChild(0).gameObject.SetActive(false);
				cursor	=	(cursor	+	1)	%	options.Length;
				is_changing	=	true;
			}

			else if (Input.GetAxis("Vertical") > -0.1f	&&
					Input.GetAxis("Vertical") < 0.1f)
			{
				is_changing	=	false;
			}
		}

		else
		{
			if (!set_prefs)
			{
				PlayerPrefs.SetInt("music_on", music_on ? 1 : 0);
				PlayerPrefs.SetFloat("volume", volume);

				set_prefs	=	true;
			}
			menu_handle.SetActive(false);
		}
	}
}
