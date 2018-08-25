using UnityEngine;
using System.Collections;

public class floor_edge_controller : MonoBehaviour {

	private	float	noise_strength	=	1.0f;
    private float   max_noise       =   2.0f;
    private	float	height			=	2.2f;
    private float   radius          =   5.0f;

    private Transform   player_position =   null;

    private Vector3 new_position;
    private	Vector3	height_position;

    void Start()
    {
        GameManager game_manager    =   GameObject.FindWithTag("GameController").GetComponent<GameManager>();
		player_position				=	GameObject.FindWithTag("Player").GetComponent<Transform>();
        new_position                =   new Vector3(transform.position.x,
                                                    transform.position.y,
                                                    transform.position.z);
        noise_strength              =   (1.0f - game_manager.ratio) * max_noise;
        height_position             =	new	Vector3(transform.position.x, transform.position.y + height, transform.position.z);
    }

    void FixedUpdate()
    {
        float   distance    =   Vector3.Distance(transform.position, player_position.position);
        
        if (distance < radius)
        {
        	transform.position  =   Vector3.Lerp(transform.position, height_position, Time.deltaTime);
        }

        else
        {
            calculate_new_pos();
            transform.position  =   Vector3.Lerp(transform.position, new_position, Time.deltaTime);        	
        }
    }

    private void calculate_new_pos()
    {
        new_position.y	=	Mathf.PerlinNoise(new_position.x, new_position.z + Mathf.Sin(Time.time * 0.1f)) * noise_strength - 1.5f;
    }
}
