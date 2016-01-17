using UnityEngine;
using System.Collections;

public class MouseHover : MonoBehaviour {
    [SerializeField] private TileGrid tile_grid;
    [SerializeField] private SpriteRenderer hover_icon;

	void Update () {
        Tile t = tile_grid.getContainingTile(Camera.main.ScreenPointToRay(Input.mousePosition).origin);
        if (t == null)
            hover_icon.gameObject.SetActive(false);
        else
        {
            hover_icon.gameObject.SetActive(true);
            hover_icon.transform.position = t.transform.position;
            hover_icon.transform.localScale = tile_grid.getScaleToFillTile(hover_icon);
        }
    }
}
