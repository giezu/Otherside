using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class subtitles_controller : MonoBehaviour
{
	public	Text	subtitle	=	null;
	public	Image	background	=	null;
	
	private	string[]	text	=	new string[0];
	private	int			text_ctr	=	0;

	private	float	text_timer	=	0.0f;
	private	float	speed		=	0.15f;

	public	bool	displaying	=	false;

	void Start()
	{
		background.enabled	=	false;
		subtitle.enabled	=	false;
	}
	
	void Update()
	{
		if (text_timer > 0.0f)
		{
			text_timer			-=	Time.deltaTime;
		}

		else
		{
			++text_ctr;
			if (text_ctr < text.Length)
			{
				subtitle.text		=	text[text_ctr];
				text_timer			=	subtitle.text.Length * speed;		
			}

			else
			{
				background.enabled	=	false;
				subtitle.enabled	=	false;
				displaying			=	false;		
			}
		}
	}

	public void display_text(string[] _text)
	{
		text_ctr			=	0;
		text				=	new string[_text.Length];
		text				=	_text;
		subtitle.text		=	text[0];
		text_timer			=	subtitle.text.Length * speed;
		background.enabled	=	true;
		subtitle.enabled	=	true;
		displaying			=	true;
	}
}
