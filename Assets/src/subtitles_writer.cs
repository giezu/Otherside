using UnityEngine;
using System.Collections;

public class subtitles_writer : MonoBehaviour
{
	public	subtitles_controller	subtitles_ctrlr	=	null;

	public	string[]				dialogs;

	private	bool					all_dialogs	=	false;

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			if (!all_dialogs)
			{
				subtitles_ctrlr.display_text(dialogs);
				all_dialogs	=	true;
			}
		}
	}
}
