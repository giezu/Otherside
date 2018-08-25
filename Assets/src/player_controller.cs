using System;
using UnityEngine;
using System.Collections;

public class player_controller : MonoBehaviour
{
	public		float		move_speed	=	1.0f;
	protected	float		hoz_move	=	0.0f;
	protected	float		ver_move	=	0.0f;
	protected	Vector3		forward;
	protected	GameObject	last_looked_obj	=	null;
	public	string		use_anim	=	null;

	public	enum 	RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
	public	RotationAxes axes = RotationAxes.MouseXAndY;
	
	public	float 	sensitivityX 	=	15.0f;
	public	float 	sensitivityY 	=	15.0f;
	public	float 	minimumX 		=	-360.0f;
	public	float 	maximumX 		=	360.0f;
	public	float 	minimumY 		=	-60.0f;
	public	float 	maximumY 		=	60.0f;
	protected	float 	rotationX 		=	0.0f;
	protected	float 	rotationY 		=	0.0f;
	
	protected	Quaternion 	originalRotation;

	void Start ()
	{
		if (GetComponent<Rigidbody>())
			GetComponent<Rigidbody>().freezeRotation = true;
		originalRotation = transform.localRotation;
	}

	void FixedUpdate()
	{
		forward	=	Camera.main.transform.TransformDirection(Vector3.forward);

		RaycastHit	hit_info;

		if (Physics.Raycast(Camera.main.transform.position, forward, out hit_info, forward.magnitude, Physics.DefaultRaycastLayers))
		{
			if (hit_info.collider.gameObject.tag	==	"Item")
			{
				last_looked_obj	=	hit_info.collider.gameObject;
				last_looked_obj.GetComponent<item_controller>().looking	=	true;

				if (Input.GetButtonDown("Fire1"))
				{
					last_looked_obj.GetComponent<item_controller>().Use();
				}
			}
		}

		else
		{
			if (last_looked_obj)
			{
				last_looked_obj.GetComponent<item_controller>().looking	=	false;
				last_looked_obj	=	null;
			}			
		}

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

	public static float ClampAngle(float angle, float min, float max)
	{
		if (angle < -360F)
			angle += 360F;
		if (angle > 360F)
			angle -= 360F;
		return Mathf.Clamp (angle, min, max);
	}
}
