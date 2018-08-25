using UnityEngine;
using System.Collections;

public class constant_animation : MonoBehaviour
{
	public	string		animation_name;
	private	Animation	animation_component;

	void Start()
	{
		animation_component	=	GetComponent<Animation>();
	}
	
	void Update()
	{
		if (!animation_component.IsPlaying(animation_name))
			animation_component.Play(animation_name);
	}
}
