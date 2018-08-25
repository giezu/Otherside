using UnityEngine;
using System.Collections;
using System;
using System.IO;

public class debug_log : MonoBehaviour
{
	private	StreamWriter	logfile;
	private	string			text_log	=	"";
	
	void Start()
	{
		logfile	=	new StreamWriter("player_log.txt", true);
	}

	void Update()
	{
		if (text_log.Split('\n').Length	>	1)
		{
			logfile.Write(text_log);
			text_log	=	"";
		}
	}

	public void Log(string text)
	{
		text_log	+=	text;
	}

	public void save()
	{
		logfile.Close();
	}
}
