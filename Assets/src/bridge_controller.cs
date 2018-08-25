using UnityEngine;
using System.Collections;

public class bridge_controller : MonoBehaviour
{
	private	MeshRenderer	mesh			=	null;

	private	float			radius			=	3.0f;

	public	Transform		player_position	=	null;

	void Start()
	{
		mesh	=	GetComponent<MeshRenderer>();
	}
	
	void Update()
	{
		float	distance	=	Vector3.Distance(transform.localPosition, player_position.position);

		if (distance < radius)
			mesh.enabled	=	true;

		else
			mesh.enabled	=	false;
	}
}
