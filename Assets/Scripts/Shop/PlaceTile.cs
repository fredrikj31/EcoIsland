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
	public GameObject saveManager;
	public int price;
	public GameObject noMoneyDialog;
	private SaveMoney moneyManager;
	private ScrollRect scroll;
	private Tilemap placeholderMap;
	private bool isPlacing;
	private Vector3Int position;
	private EffectPlayer effectPlayer;
	private GameObject popupMenu;

	// Start is called before the first frame update
	void Start()
	{
		this.popupMenu = GameObject.FindGameObjectWithTag("PopupMenu");
		this.moneyManager = this.saveManager.GetComponent<SaveMoney>();
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
				int moneyAmount = this.moneyManager.getMoney();
				if ((moneyAmount - this.price) >= 0) {
					// Place tile
					this.isPlacing = false;
					this.placeTile(this.position);
					this.placeholderMap.ClearAllTiles();
					// Play Effect
					this.effectPlayer.playEffect("tile_placement");
					// Remove Money
					this.moneyManager.removeMoney(this.price);
					return;
				} else {
					this.isPlacing = false;
					this.placeholderMap.ClearAllTiles();
					// Play Effect
					this.effectPlayer.playEffect("tile_placement");
					// Setting the menu.
					StartCoroutine(this.displayDialog(this.position));
					//Debug.Log("No money to that.");
					return;
				}
			}
		}
	}

	private IEnumerator displayDialog(Vector3 pos) {
		PopupMenu menu = this.popupMenu.GetComponent<PopupMenu>();
		menu.setPosition(pos);
		menu.setObject(this.noMoneyDialog);
		menu.openPopup();

		yield return new WaitForSeconds(2f);
		menu.closePopup();
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
