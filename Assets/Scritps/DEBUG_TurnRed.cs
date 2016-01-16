using UnityEngine;
using System.Collections;

public class DEBUG_TurnRed : MonoBehaviour
{
	[SerializeField]
	private SpriteRenderer
		my_sprite;
	
	private Color color = Color.red;
	private Color other_color = Color.blue;
	
	void OnMouseDown ()
	{
		if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject ())
			return;
		Color temp = color;
		color = other_color;
		other_color = temp;
		my_sprite.color = color;
	}
	
}
