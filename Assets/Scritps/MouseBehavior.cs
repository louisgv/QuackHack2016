using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MouseBehavior : MonoBehaviour
{
	[SerializeField]
	private SpriteRenderer
		DEBUG_prefab;
	[SerializeField]
	private GameObject
		DEBUG_root;
	[SerializeField]
	private Camera
		world_camera;

	private Action click_behavior = doNothing;
	private static void doNothing ()
	{
	} //if the mouse doesn't have an action bound to it, do this

	void Awake ()
	{
		DEBUG_prefab.gameObject.SetActive (false);
	}

	public void setMouseToSpawnDotOnClick ()
	{
		setMouseToSpawnOnClick<SpriteRenderer> (DEBUG_prefab, DEBUG_root);
	}

	public void setMouseToSpawnOnClick<T> (T prefab, GameObject root, Action<T> initialize_prefab = null)
	where T : Component
	{
		if (initialize_prefab == null)
			initialize_prefab = (T t) => {};
		click_behavior = () =>
		{
			spawnOnClick (prefab, root, initialize_prefab);
		};
	}
	
	private void spawnOnClick<T> (T prefab, GameObject root, Action<T> initialize_prefab)
	where T : Component
	{
		initialize_prefab (root.InstantiateChild<T> (prefab));
		click_behavior = doNothing;
		prefab.transform.position = world_camera.ScreenToWorldPoint (Input.mousePosition);
		prefab.gameObject.SetActive (true);
	}
	
	private bool mouseIsBlockedByUIElement ()
	{
		return UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject ();
	}
	
	void Update ()
	{
		if (Input.GetMouseButtonDown (0) && !mouseIsBlockedByUIElement ())
			click_behavior ();
	}
}
