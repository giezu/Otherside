using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class clean_controller : MonoBehaviour
{
	public	GameManager	game_manager	=	null;

	private	Collider[]	colliders;

	public	bool		good_place		=	false;

	public	List<string>	items_names	=	new	List<string>();

	private	Dictionary<string, bool>	items	=	new Dictionary<string, bool>();

	void Start()
	{
		foreach (string name in items_names)
		
			items.Add(name, false);
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag	==	"Item")
		{
			string	item_name	=	other.GetComponent<item_noe_controller>().item_name;
			items[item_name]	=	true;
			Debug.Log(item_name);
			bool	any_more_items	=	false;
			foreach (KeyValuePair<string, bool> item in items)
			{
				if (!items[item.Key])
				{
					any_more_items	=	true;
					break;
				}
			}

			if (!any_more_items)
			{
				game_manager.load_scene("hub", good_place);
				colliders	=	GetComponents<Collider>();
				for (int i = 0; i < colliders.Length; ++i)
					colliders[i].enabled	=	false;
			}
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag	==	"Item")
		{
			string	item_name	=	other.GetComponent<item_noe_controller>().item_name;
			items[item_name]	=	false;
		}		
	}
}
