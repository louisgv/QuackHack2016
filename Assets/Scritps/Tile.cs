using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {
	public eTerrain terrain;
	public GameObject occupant;
    public SpriteRenderer sprite;
    public int x;
    public int y;
    public Vector3 road_direction = new Vector3();
}
