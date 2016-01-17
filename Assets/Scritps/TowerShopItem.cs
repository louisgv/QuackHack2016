using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class TowerShopItem : MonoBehaviour
{
	public float width;
	public float height;

	//data access
	public TowerData.TowerDefinition definition;
	public Sprite sprite_built{ get { return definition.sprite_built; } }
	public int cost{ get { return definition.resource_cost; } }

	//data users
	public Image image;
	public Building generic_building_prefab;
	private Building my_building_prefab;
	public GameObject root;

	public MouseBehavior mouse_behavior;

	void Awake ()
	{
		image.sprite = sprite_built;

		image.transform.localScale = Vector3.one;

		my_building_prefab = root.InstantiateChild<Building> (generic_building_prefab);
		my_building_prefab.definition = definition;
		my_building_prefab.GetComponent<SpriteRenderer> ().sprite = sprite_built;
		my_building_prefab.gameObject.SetActive (false);
	}

	void Start ()
	{
		my_building_prefab.gameObject.SetActive (false);
	}

	public void SetMouseToInstantate ()
	{
		Action<Building> init = (Building b) => {
			b.definition = definition;
			//b.gameObject.GetComponent<SpriteRenderer>().sprite = definition.sprite_built;
		};

		mouse_behavior.setMouseToSpawnOnClick<Building> (my_building_prefab, root, init);
	}
}
