using UnityEngine;
using System.Collections;

public static class GameObjectExtensions
{
	public static T InstantiateChild<T> (this GameObject gameObject, T prefab, string name = "")
	where T : Component
	{
		T ret = GameObject.Instantiate<T> (prefab);
		if (!string.IsNullOrEmpty (name))
			ret.gameObject.name = name;
		ret.transform.SetParent (gameObject.transform);
		ret.gameObject.layer = gameObject.layer;
		return ret;
	}
}
