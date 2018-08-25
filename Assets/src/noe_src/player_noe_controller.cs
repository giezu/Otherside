using UnityEngine;
using System.Collections;

public class player_noe_controller : player_controller
{
	public	bool		is_using	=	false;
	public	GameObject	using_item	=	null;

	private	string		object_name	=	"";

	private	float		ray_length	=	3.0f;

	void Start ()
	{
		if (GetComponent<Rigidbody>())
			GetComponent<Rigidbody>().freezeRotation = true;
		originalRotation = transform.localRotation;
	}

	void FixedUpdate()
	{
		forward	=	Camera.main.transform.TransformDirection(Vector3.forward);
		bool	is_item_hit	=	false;
		RaycastHit[]	hit_info	=	Physics.SphereCastAll(Camera.main.transform.position, 0.5f, forward, ray_length);

		for (int i = 0; i < hit_info.Length; ++i)
		{
			if (hit_info[i].collider.gameObject.tag	==	"Item")
			{
				if (last_looked_obj	!=	null	&&
					object_name	!=	hit_info[i].collider.gameObject.name)
				{
					last_looked_obj.GetComponent<item_noe_controller>().looking	=	false;
				}

				last_looked_obj	=	hit_info[i].collider.gameObject;
				last_looked_obj.GetComponent<item_noe_controller>().looking	=	true;
				is_item_hit		=	true;
				object_name		=	last_looked_obj.name;

				if (Input.GetButtonDown("Fire1"))
				{
					hit_info[i].collider.gameObject.GetComponent<item_noe_controller>().Use();
				}
			}
		}


		if (!is_item_hit)
		{
			if (last_looked_obj)
			{
				last_looked_obj.GetComponent<item_noe_controller>().looking	=	false;
				last_looked_obj	=	null;
			}			
		}

		/*
		RaycastHit	hit_info;

		if (Physics.Raycast(Camera.main.transform.position, forward, out hit_info, ray_length, Physics.DefaultRaycastLayers))
		{
			if (hit_info.collider.gameObject.tag	==	"Item")
			{
				if (last_looked_obj	!=	null	&&
					object_name	!=	hit_info.collider.gameObject.name)
				{
					last_looked_obj.GetComponent<item_noe_controller>().looking	=	false;
				}
				last_looked_obj	=	hit_info.collider.gameObject;
				last_looked_obj.GetComponent<item_noe_controller>().looking	=	true;
				object_name		=	last_looked_obj.name;

				if (Input.GetButtonDown("Fire1"))
				{
					hit_info.collider.gameObject.GetComponent<item_noe_controller>().Use();
				}
			}

			else
			{
				if (last_looked_obj != null)
				last_looked_obj.GetComponent<item_noe_controller>().looking	=	false;
			}
		}

		else if (last_looked_obj	!=	null	&&
				 last_looked_obj.GetComponent<item_noe_controller>().looking)
		{
			last_looked_obj.GetComponent<item_noe_controller>().looking	=	false;
		}
		*/
		if (axes == RotationAxes.MouseXAndY)
		{
			rotationX += Input.GetAxis("RightH") * sensitivityX;
			rotationY += Input.GetAxis("RightV") * sensitivityY;
			rotationX = ClampAngle (rotationX, minimumX, maximumX);
			rotationY = ClampAngle (rotationY, minimumY, maximumY);
			Quaternion xQuaternion = Quaternion.AngleAxis (rotationX, Vector3.up);
			Quaternion yQuaternion = Quaternion.AngleAxis (rotationY, -Vector3.right);
			transform.localRotation = originalRotation * xQuaternion * yQuaternion;
		}
		else if (axes == RotationAxes.MouseX)
		{
			rotationX += Input.GetAxis("RightH") * sensitivityX;
			rotationX = ClampAngle(rotationX, minimumX, maximumX);
			Quaternion xQuaternion = Quaternion.AngleAxis(rotationX, Vector3.up);
			transform.localRotation = originalRotation * xQuaternion;
		}
		else
		{
			rotationY += Input.GetAxis("RightV") * sensitivityY;
			rotationY = ClampAngle(rotationY, minimumY, maximumY);
			Quaternion yQuaternion = Quaternion.AngleAxis(-rotationY, Vector3.right);
			transform.localRotation = originalRotation * yQuaternion;
		}

		hoz_move	=	Input.GetAxis("Horizontal")	*	move_speed	*	Time.deltaTime;
		ver_move	=	Input.GetAxis("Vertical")	*	move_speed	*	Time.deltaTime;
		transform.Translate(Vector3.forward * ver_move);
		transform.Translate(Vector3.right * hoz_move);
	}
}
