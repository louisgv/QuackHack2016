using UnityEngine;
using System.Collections.Generic;

public class TowerData : MonoBehaviour {

	[System.Serializable]
	public class TowerDefinition
	{
		public Sprite sprite_building;

		public Sprite sprite_built;

		public float health;
		public float construction_speed;
		public float attack_damage;
		public float attack_speed;
		public float attack_distance;
		public int resource_cost;
		public float construction_time = 0;
	}

	public List<TowerDefinition> definitions;

	public TowerShopItem shop_item_prefab;
	public GameObject shop_item_root;

	void Awake()
	{
		shop_item_prefab.gameObject.SetActive (false);
		Debug.Log (definitions.Count);
		for (int i=0; i<definitions.Count; i++)
			CreateShopItem (definitions[i]);
	}

	private void CreateShopItem(TowerDefinition definition)
	{
		TowerShopItem shop_item = shop_item_root.InstantiateChild<TowerShopItem> (shop_item_prefab);
		shop_item.definition = definition;
		shop_item.gameObject.SetActive (true);
	}
}
