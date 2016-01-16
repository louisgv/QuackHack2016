using UnityEngine;
using System.Collections;

public class TileGrid : MonoBehaviour {
    private string[] tile_terrain_grid = {
        "oooooooooooooooooooo",
        "rrrooooooooooooooooo",
        "oororrrrrooooooooooo",
        "oorrrooorrrrrrrrrrrr",
        "oooooooooooooooooooo"
        };

    [SerializeField] private Tile tile_prefab;
    [SerializeField] private GameObject tile_root;
    
	public Tile[,] tile_grid;
	[SerializeField] private float tile_height;
	[SerializeField] private float tile_width;

    private int width_tiles;
    private int height_tiles;

    void Awake()
    {
        tile_prefab.gameObject.SetActive(false);
        GenerateTiles();
    }

    void GenerateTiles()
    {
        width_tiles = tile_terrain_grid[0].Length;
        height_tiles = tile_terrain_grid.Length;
        float width_unity = (float)width_tiles * tile_width;
        float height_unity = (float)height_tiles * tile_height;

        tile_grid = new Tile[width_tiles, height_tiles];

        for (int y = 0; y < height_tiles; ++y)
        {
            for (int x = 0; x < width_tiles; ++x)
            {
                float y_unity = ((float)y + 0.5f) * tile_height;
                float x_unity = ((float)x + 0.5f) * tile_width;

                Tile tile = tile_root.InstantiateChild<Tile>(tile_prefab);
                tile.x = x;
                tile.y = y;
                tile.transform.localPosition = new Vector3(x_unity, y_unity);
                tile.terrain = parseTerrain(tile_terrain_grid[y][x]);
                tile.gameObject.SetActive(true);
            }
        }
    }

    private static eTerrain parseTerrain(char c)
    {
        switch (c)
        {
            case 'o': return eTerrain.open;
            case 'r': return eTerrain.road;
            default: throw new System.ArgumentException("invalid character");
        }
    }

	public Tile getContainingTile(Vector3 coords)
	{
        Debug.Log(coords.ToString());
        //coords = tile_root.transform.TransformPoint(coords);
        int tile_x = Mathf.FloorToInt(coords.x / tile_width);
		int tile_y = Mathf.FloorToInt(coords.y / tile_height);
        //Debug.Log(tile_x.ToString() + " " + tile_y.ToString());
        
        if (   tile_x < 0
            || tile_x > width_tiles
            || tile_y < 0
            || tile_y > height_tiles
            )
            return null;
        return tile_grid[tile_x, tile_y];
    }

	public Vector3 getTileCoords(Tile tile)
	{
		return new Vector3 (
			(float)tile.x * tile_width,
			(float)tile.y * tile_height
		);
	}

    void Update()
    {
        getContainingTile(Camera.main.ScreenPointToRay(Input.mousePosition).origin);
    }
}
