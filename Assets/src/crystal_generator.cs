using UnityEngine;
using System.Collections;

public class crystal_generator : MonoBehaviour
{
	public	GameObject	crystal	=	null;

	[Range(0, 1000)]
	public	int			no_of_crystals;
	
	[Range(0, 500)]
	public	float		max_x;
	
	[Range(0, 500)]
	public	float		max_y;
	
	[Range(0, 500)]
	public	float		max_z;

	void Start()
	{
		for (int i = 0; i < no_of_crystals; ++i)
		{
			Vector3	pos	=	new Vector3(transform.localPosition.x + Random.value	*	max_x,
									transform.localPosition.y + Random.value	*	max_y,
									transform.localPosition.z + Random.value	*	max_z);
			Instantiate(crystal,
						pos,
						Quaternion.identity);
		}
	}
}
