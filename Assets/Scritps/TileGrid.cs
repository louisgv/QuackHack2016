using UnityEngine;
using System.Collections;

public class TileGrid : MonoBehaviour
{
	[SerializeField]
	private Sprite open_sprite;
	[SerializeField]
	private Sprite road_sprite;


	private string[] tile_terrain_grid = {
        "oooooooooooooooooooooo",
        "rrrooooooooooooooooooo",
        "oororrrrrooooooooooooo",
        "oorrrooorrrrrrrrrrrrrr",
        "oooooooooooooooooooooo"
        };
	private string[] tile_road_grid = {
        "oooooooooooooooooooooo",
        "<<<ooooooooooooooooooo",
        "oo^ov<<<<ooooooooooooo",
        "oo^<<ooo^<<<<<<<<<<<<<",
        "oooooooooooooooooooooo"
        };

	private Tile[] road;

	[SerializeField]
	private Tile tile_prefab;
	[SerializeField]
	private GameObject tile_root;
    
	public Tile[,] tile_grid;
	public float tile_height;
	public float tile_width;

	private int width_tiles;
	private int height_tiles;

	void Awake ()
	{
		tile_prefab.gameObject.SetActive (false);
		GenerateTiles ();
	}

	void GenerateTiles ()
	{
		width_tiles = tile_terrain_grid [0].Length;
		height_tiles = tile_terrain_grid.Length;
//        float width_unity = (float)width_tiles * tile_width;
//        float height_unity = (float)height_tiles * tile_height;

		tile_grid = new Tile[width_tiles, height_tiles];

		for (int y = 0; y < height_tiles; ++y) {
			for (int x = 0; x < width_tiles; ++x) {
				float y_unity = ((float)y + 0.5f) * tile_height;
				float x_unity = ((float)x + 0.5f) * tile_width;

				Tile tile = tile_root.InstantiateChild<Tile> (tile_prefab);
				tile.x = x;
				tile.y = y;
				tile.transform.localPosition = new Vector3 (x_unity, y_unity);
				tile.terrain = parseTerrain (tile_terrain_grid [y] [x]);
				tile.road_direction = parseRoadDirection (tile_road_grid [y] [x]);
				setSprite (tile, tile.terrain);
				tile.transform.localScale = getScaleToFillTile (tile.sprite);
				tile.gameObject.SetActive (true);

				tile_grid [x, y] = tile;
			}
		}
	}

	public Vector3 getScaleToFillTile (SpriteRenderer sprite)
	{
		float scale_x = tile_width / sprite.bounds.size.x;
		float scale_y = tile_height / sprite.bounds.size.y;
		return (new Vector3 (1.0f, 1.0f)) * Mathf.Min (scale_x, scale_y);
	}

	private eTerrain parseTerrain (char c)
	{
		switch (c) {
		case 'o':
			return eTerrain.open;
		case 'r':
			return eTerrain.road;
		default:
			throw new System.ArgumentException ("invalid character");
		}
	}

	private Vector3 parseRoadDirection (char c)
	{
		Vector3 road_direction = new Vector3 ();
		switch (c) {
		case '<':
			road_direction.x = -1;
			break;
		case 'v':
			road_direction.y = 1;
			break;
		case '^':
			road_direction.y = -1;
			break;
		}
		return road_direction;
	}

	private void setSprite (Tile tile, eTerrain terrain)
	{
		switch (terrain) {
		case (eTerrain.open):
			tile.sprite.sprite = open_sprite;
			return;
		case (eTerrain.road):
			tile.sprite.sprite = road_sprite;
			return;
		}
	}

	public Tile getContainingTile (Vector3 coords)
	{
		coords = tile_root.transform.InverseTransformPoint (coords);
		int tile_x = Mathf.FloorToInt (coords.x / tile_width);
		int tile_y = Mathf.FloorToInt (coords.y / tile_height);

		if (tile_x < 0
			|| tile_x >= width_tiles
			|| tile_y < 0
			|| tile_y >= height_tiles
            )
			return null;
        
		return tile_grid [tile_x, tile_y];
	}
}
