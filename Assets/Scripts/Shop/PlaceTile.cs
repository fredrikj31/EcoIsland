using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlaceTile : MonoBehaviour, IPointerDownHandler
{
	public List<Tilemap> tilemaps;
	public GameObject scrollView;
	public TileBase placedTile;
	public Tilemap placeMap;
	private ScrollRect scroll;
	private Tilemap placeholderMap;
	private bool isPlacing;
	private Vector3Int position;
	private EffectPlayer effectPlayer;

	// Start is called before the first frame update
	void Start()
	{
		this.effectPlayer = GameObject.FindGameObjectWithTag("EffectController").GetComponent<EffectPlayer>();
		this.scroll = this.scrollView.GetComponent<ScrollRect>();
		this.placeholderMap = GameObject.FindGameObjectWithTag("PlaceholderMap").GetComponent<Tilemap>();
	}

	// Update is called once per frame
	void Update()
	{
		// Dragging
		if (this.isPlacing) {
			if (Input.touchCount > 0)
			{
				Touch touch = Input.GetTouch(0); // get first touch since touch count is greater than zero

				if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved)
				{
					this.placeholderMap.ClearAllTiles();

					Vector2 screenPosition = new Vector2(touch.position.x, touch.position.y);
					Vector2 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);

					Vector3Int currentCell = this.placeholderMap.WorldToCell(worldPosition);
					currentCell.z = 0;
					this.position = currentCell;

					this.placeholderMap.SetTile(currentCell, this.placedTile);
				}
			} else {
				// Place tile
				this.isPlacing = false;
				this.placeTile(this.position);
				this.placeholderMap.ClearAllTiles();
				// Play Effect
				this.effectPlayer.playEffect("tile_placement");
			}
		}
	}

	private void placeTile(Vector3Int pos) {
		// TODO: Remove all tiles at the position of all tilemaps, then place the tile on the correct tilemap.
	
		// Removes all the tiles from all tilemaps at the position
		foreach (Tilemap map in this.tilemaps)
		{
			map.SetTile(pos, null);
		}

		// Setting the new tile at the pos
		this.placeMap.SetTile(pos, this.placedTile);
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		this.scroll.vertical = false;
		this.isPlacing = true;
	}
}
