using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MouseBehavior : MonoBehaviour
{
	[SerializeField]
	private Building
		prefab;
	[SerializeField]
	private GameObject
		root;
	[SerializeField]
	private Camera
		world_camera;
    [SerializeField]
    private TileGrid tile_grid;

	[SerializeField] private MoneyManager money_manager;

	private Action click_behavior = doNothing;
	private static void doNothing ()
	{
	} //if the mouse doesn't have an action bound to it, do this

	void Awake ()
	{
		prefab.gameObject.SetActive (false);
	}

	public void setMouseToSpawnDotOnClick ()
	{
		setMouseToSpawnOnClick<Building> (prefab, root);
	}

	public void setMouseToSpawnOnClick<T> (T prefab, GameObject root, Action<T> initialize_prefab = null)
	where T : Component
	{
		if (initialize_prefab == null)
			initialize_prefab = (T t) => {};
        Debug.Log("MOUSE WANTS TO SPAWN SOMETHING");
		click_behavior = () =>
		{
			spawnOnClick (prefab, root, initialize_prefab);
            Debug.Log("MOUSE SPAWNED SOMETHING");
		};
	}
	
	private void spawnOnClick<T> (T prefab, GameObject root, Action<T> initialize_prefab)
	where T : Component
	{
		//find & validate spawn position
        Vector3 mouse_position_unity = world_camera.ScreenToWorldPoint(Input.mousePosition);
        mouse_position_unity = Vector3.Scale(mouse_position_unity, new Vector3(1.0f, 1.0f, 0.0f));
        Tile tile_under_mouse = tile_grid.getContainingTile(mouse_position_unity);
        if (   tile_under_mouse == null
            || tile_under_mouse.terrain != eTerrain.open
            || tile_under_mouse.occupant != null
			|| !money_manager.AttemptPurchase(15)
			){
            Debug.Log("CAN'T SPAWN ACTOR OUTSIDE OF TILE GRID");
            return;
        }

        //create the actor
        T spawned_actor = root.InstantiateChild<T>(prefab);
        initialize_prefab(spawned_actor);
        click_behavior = doNothing;
        spawned_actor.transform.position = tile_under_mouse.transform.position;

        //DEBUG - if you're having problems, it might be because of the GetComponent in this line
        spawned_actor.transform.localScale = tile_grid.getScaleToFillTile(spawned_actor.gameObject.GetComponent<SpriteRenderer>()) * 0.8f;

        tile_under_mouse.occupant = spawned_actor.gameObject;

        spawned_actor.gameObject.SetActive (true);
	}
	
	void Update ()
	{
		if (Input.GetMouseButtonDown (0))
			click_behavior ();
	}
}
