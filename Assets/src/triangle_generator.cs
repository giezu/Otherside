using UnityEngine;
using System.Collections;

public class triangle_generator : MonoBehaviour
{
	public	GameObject	triangle	=	null;

	[Range(0, 1000)]
	public	int			no_of_triangles;
	
	[Range(0, 500)]
	public	float		max_x;
	
	[Range(0, 500)]
	public	float		max_y;
	
	[Range(0, 500)]
	public	float		max_z;

	public	float		factor		=	0.3f;
	public	float		min_scale	=	0.2f;

	void Start()
	{
		for (int i = 0; i < no_of_triangles; ++i)
		{
			GameObject clone = Instantiate(	triangle,
											new Vector3(transform.position.x + Random.value	*	max_x,
														transform.position.y + Random.value	*	max_y,
														transform.position.z + Random.value	*	max_z),
											Quaternion.identity) as GameObject;
			float	scale	=	min_scale + Random.value	*	factor;
			clone.GetComponent<Transform>().localScale	=	new Vector3(scale,
																		scale,
																		scale);
			clone.GetComponent<Transform>().parent		=	transform;
		}
	}
}
