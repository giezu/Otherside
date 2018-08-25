using UnityEngine;
using System.Collections;

public class screen_fader : MonoBehaviour
{
	public float fadeSpeed = 1.5f;

	void Awake()
	{
		GetComponent<GUITexture>().pixelInset = new Rect(0f, 0f, Screen.width, Screen.height);
		GetComponent<GUITexture>().enabled 	= 	true;
		// GetComponent<GUITexture>().color	=	Color.clear;
	}
	
	public void FadeToClear()
	{
		// Lerp the colour of the texture between itself and transparent.
		GetComponent<GUITexture>().color = Color.Lerp(GetComponent<GUITexture>().color, Color.clear, fadeSpeed * Time.deltaTime);
	}
	
	
	public void FadeToBlack()
	{
		// Lerp the colour of the texture between itself and black.
		GetComponent<GUITexture>().color = Color.Lerp(GetComponent<GUITexture>().color, Color.black, fadeSpeed * Time.deltaTime);
	}

	public void FadeToWhite()
	{
		GetComponent<GUITexture>().color	=	Color.Lerp(GetComponent<GUITexture>().color, Color.white, fadeSpeed * Time.deltaTime);
	}
}