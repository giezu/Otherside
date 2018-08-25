using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System;

public class item_controller : MonoBehaviour
{
	public	GameManager	game_manager	=	null;

	public	debug_log	debug_log		=	null;

	public	bool		decision;

	public	float		texture_timer	=	0.5f;

	private	Color		original_color;

	private	Animation	anim_ctrl;

	private	Renderer	rend;

	private	Material[]	selected_mats	=	new	Material[2];

	private	int			animations_cursor	=	0;

	private	bool		loading_scene		=	false;
	private bool		played_last_anim	=	false;
	private	bool		went				=	false;

	private	float		color_timer		=	-1.0f/2.0f;

	
	[Header("Materiały")]
	public	Material	selected_material	=	null;

	[Header("Controlsy")]
	public	bool		looking			=	false;
	public	float		color_speed;

	[Header("Animacje")]
	public	string[]	animations;

	[Header("Scena")]
	public	string		scene_name;
	
	void Start()
	{
		rend				=	transform.GetChild(1).GetComponent<Renderer>();
		debug_log			=	GameObject.FindWithTag("DebugLog").GetComponent<debug_log>();
		anim_ctrl			=	GetComponent<Animation>();
		selected_mats[0]	=	rend.material;
		selected_mats[1]	=	selected_material;
		original_color		=	rend.material.color;
	}

	void Update()
	{
		if (looking)

			Glow();	

		else
		{
			color_timer	=	-1.0f/2.0f;
			Fade();
		}

		if (anim_ctrl != null)
		{
			if (anim_ctrl.IsPlaying(animations[animations.Length - 1]))
			
				played_last_anim	=	true;
			
			if (played_last_anim	&&
				!anim_ctrl.IsPlaying(animations[animations.Length - 1]))	
				
				if (scene_name	!=	"" &&
					!loading_scene)
				{
					game_manager.load_scene(scene_name, decision);
					loading_scene	=	true;
				}
		}

		else
		{
			if (scene_name	!=	"" &&
				!loading_scene	&&
				went)
			{
				game_manager.load_scene(scene_name, decision);
				loading_scene	=	true;
			}
		}
	}

	void Glow()
	{
		debug_log.Log("Info\n");
		debug_log.Log("[" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "]\n");
		debug_log.Log("Player looked at object: " + gameObject.name + '\n');

		rend.material.color	=	Color.Lerp(original_color, Color.white, (Mathf.Sin(color_timer * color_speed * Mathf.PI) + 1.0f) / 2.0f);
		color_timer	+=	Time.deltaTime;
	}

	void Fade()
	{
		if (rend != null)
			rend.material.color	=	Color.Lerp(rend.material.color, original_color, Time.deltaTime);
	}

	public void Use()
	{
		if (!anim_ctrl.IsPlaying(animations[animations_cursor])	&&
			animations_cursor	<=	animations.Length - 1)
		{
			anim_ctrl.Play(animations[animations_cursor]);
			++animations_cursor;		
		}

		debug_log.Log("Info\n");
		debug_log.Log("[" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "]\n");
		debug_log.Log("Player used object: " + gameObject.name + '\n');
	}

	void
	OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			Debug.Log("Player");
			went	=	true;
		}
	}
}
