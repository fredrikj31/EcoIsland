using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlaceTile : MonoBehaviour, IPointerClickHandler, IPointerDownHandler
{
	public GameObject scrollView;
	public TileBase placedTile;
	public Tilemap placeMap;
	private ScrollRect scroll;
	private Tilemap placeholderMap;
	private bool isPlacing;
	private Vector3Int position;

	// Start is called before the first frame update
	void Start()
	{
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
				this.placeMap.SetTile(this.position, this.placedTile);
				this.placeholderMap.ClearAllTiles();
			}
		}
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		// OnClick code goes here ...


	}

	public void OnPointerDown(PointerEventData eventData)
	{
		this.scroll.vertical = false;
		this.isPlacing = true;
	}
}
