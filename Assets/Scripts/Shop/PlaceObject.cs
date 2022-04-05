using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace EcoIsland
{
	public class PlaceObject : MonoBehaviour, IPointerDownHandler
	{
		public GameObject scrollView;
		public GameObject placedObject;
		public GameObject saveManager;
		public int price;
		public int allowedObjects;
		public GameObject noMoneyDialog;
		public GameObject noMoreObjects;
		private SaveMoney moneyManager;
		private ScrollRect scroll;
		private GameObject spawnedObject;
		private bool isPlacing;
		private Vector3Int position;
		private EffectPlayer effectPlayer;
		private GameObject popupMenu;
		private SaveIsland islandManager;

		// Start is called before the first frame update
		void Start()
		{
			this.popupMenu = GameObject.FindGameObjectWithTag("PopupMenu");
			this.moneyManager = this.saveManager.GetComponent<SaveMoney>();
			this.islandManager = GameObject.FindGameObjectWithTag("SaveManager").GetComponent<SaveIsland>();
			this.effectPlayer = GameObject.FindGameObjectWithTag("EffectController").GetComponent<EffectPlayer>();
			this.scroll = this.scrollView.GetComponent<ScrollRect>();
		}

		// Update is called once per frame
		void Update()
		{
			// Dragging
			if (this.isPlacing)
			{
				if (Input.touchCount > 0)
				{
					Touch touch = Input.GetTouch(0); // get first touch since touch count is greater than zero

					if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved)
					{
						Vector2 screenPosition = new Vector2(touch.position.x, touch.position.y);
						Vector2 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);

						this.position = Vector3Int.RoundToInt(worldPosition);
						this.spawnedObject.transform.position = worldPosition;
					}
				}
				else
				{
					int moneyAmount = this.moneyManager.getMoney();
					if ((moneyAmount - this.price) >= 0)
					{
						print(this.saveManager.GetComponent<SaveIsland>().boughtObjects(this.placedObject.name));
						if (this.saveManager.GetComponent<SaveIsland>().boughtObjects(this.placedObject.name) < this.allowedObjects) {
							this.isPlacing = false;
							// Play Effect
							this.effectPlayer.playEffect("tile_placement");
							// Remove Money
							this.moneyManager.removeMoney(this.price);
							// Save the map
							this.islandManager.saveObjects();

							// Reload Objects
							this.islandManager.reloadControllers();
							return;
						} else {
							this.isPlacing = false;
							// Play Effect
							this.effectPlayer.playEffect("tile_placement");
							// Removing the object
							Destroy(this.spawnedObject);
							// Setting the menu.
							StartCoroutine(this.displayNoMoreObjects(this.position));
							//Debug.Log("No money to that.");
							return;
						}

					}
					else
					{
						this.isPlacing = false;
						// Play Effect
						this.effectPlayer.playEffect("tile_placement");
						// Removing the object
						Destroy(this.spawnedObject);
						// Setting the menu.
						StartCoroutine(this.displayNoMoneyDialog(this.position));
						//Debug.Log("No money to that.");
						return;
					}
				}
			}
		}

		private IEnumerator displayNoMoreObjects(Vector3 pos)
		{
			PopupMenu menu = this.popupMenu.GetComponent<PopupMenu>();
			menu.setPosition(pos);
			menu.setObject(this.noMoreObjects);
			menu.openPopup();

			yield return new WaitForSeconds(2f);
			menu.closePopup();
		}

		private IEnumerator displayNoMoneyDialog(Vector3 pos)
		{
			PopupMenu menu = this.popupMenu.GetComponent<PopupMenu>();
			menu.setPosition(pos);
			menu.setObject(this.noMoneyDialog);
			menu.openPopup();

			yield return new WaitForSeconds(2f);
			menu.closePopup();
		}

		public void OnPointerDown(PointerEventData eventData)
		{
			this.spawnedObject = Instantiate(this.placedObject);
			this.scroll.vertical = false;
			this.isPlacing = true;
		}
	}
}
