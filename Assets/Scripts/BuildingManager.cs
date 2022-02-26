using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BuildingManager : MonoBehaviour
{
	public bool isPlacing = false;

	public Grid grid;
	public Tilemap map;
	public TileBase placementBuilding;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		if (Input.GetMouseButtonDown(0)) {
			if (this.isPlacing == true) {
				Debug.Log(this.getMousePosition());

				this.map.SetTile(this.getMousePosition(), this.placementBuilding);
			}

		}
    }
	Vector3Int getMousePosition() {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return grid.WorldToCell(mouseWorldPos);
    }
}
