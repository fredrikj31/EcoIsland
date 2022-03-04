using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace EcoIsland
{
	public class CropsController : MonoBehaviour
	{
		public Grid grid;
		public Tilemap cropsMap;
		public TileBase emptyField;
		public TileBase[] wheatTiles = new TileBase[3];
		public TileBase[] cornTiles = new TileBase[3];
		public TileBase[] carrotTiles = new TileBase[3];
		public Dictionary<Vector3Int, Crop> crops = new Dictionary<Vector3Int, Crop>();
		public GameObject statusMenu;
		public GameObject selectCropMenu;

		// Time management
		private float downClickTime;
		private float ClickDeltaTime = 0.2F;

		// Position
		private Vector3Int selectedTile;

		// Popup Menu
		private GameObject popupMenu;

		// Start is called before the first frame update
		void Start()
		{
			this.popupMenu = GameObject.FindGameObjectWithTag("PopupMenu");

			InvokeRepeating("updateCropsTime", 0f, 1f);
		}

		void Update()
		{
			if (Input.GetMouseButtonDown(0))
			{
				this.downClickTime = Time.time;
			}

			if (Input.GetMouseButtonUp(0))
			{
				if (Time.time - downClickTime <= ClickDeltaTime)
				{
					if (this.cropsMap.GetTile(this.getMousePosition()) != null)
					{
						TileBase clickedTile = this.cropsMap.GetTile(this.getMousePosition());
						PopupMenu menu = this.popupMenu.GetComponent<PopupMenu>();
						if (clickedTile.name == "Field")
						{
							Vector3 cellPos = this.getCellPosition();
							this.selectedTile = getMousePosition();

							menu.setPosition(cellPos);
							menu.setObject(this.selectCropMenu);
							menu.openPopup();
						}
						else
						{
							Crop data = this.getDataFromTile(this.getMousePosition());

							// Get cell position in the world
							Vector3 cellWorldPos = this.getCellPosition();
							TimeSpan remainingTime = data.getRemainingTime();

							// If crop is finished growing
							if (data.checkTime() == 2)
							{
								this.harvestCrop(this.getMousePosition());
								return;
							}

							// Formatting string and setting up the menu
							this.statusMenu.GetComponent<Text>().text = this.formatData(data);
							menu.setPosition(cellWorldPos);
							menu.setObject(this.statusMenu);
							menu.openPopup();
						}
					}
				}
			}
		}

		private Vector3Int getMousePosition()
		{
			Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			return grid.WorldToCell(mouseWorldPos);
		}

		private Vector3 getCellPosition()
		{
			return grid.GetCellCenterWorld(this.getMousePosition());
		}

		private string formatData(Crop data)
		{
			TimeSpan remainingTime = data.getRemainingTime();
			string popupText = $"Type: {data.cropType.ToString()}\nGrowth: {Math.Round(data.getProcents(), 0)}%";

			if (data.checkTime() == 2)
			{
				popupText = $"Type: {data.cropType.ToString()}\nGrowth: 100%";
			}
			else
			{
				if (remainingTime.Hours > 0)
				{
					popupText = $"Type: {data.cropType.ToString()}\nGrowth: {Math.Round(data.getProcents(), 0)}%\n{remainingTime.Hours} hrs {remainingTime.Minutes} min {remainingTime.Seconds} sec";
				}
				else if (remainingTime.Minutes > 0)
				{
					popupText = $"Type: {data.cropType.ToString()}\nGrowth: {Math.Round(data.getProcents(), 0)}%\n{remainingTime.Minutes} min {remainingTime.Seconds} sec";
				}
				else
				{
					popupText = $"Type: {data.cropType.ToString()}\nGrowth: {Math.Round(data.getProcents(), 0)}%\n{remainingTime.Seconds} sec";
				}
			}

			return popupText;
		}

		private void updateCropsTime()
		{
			foreach (KeyValuePair<Vector3Int, Crop> crop in this.crops)
			{
				int stage = crop.Value.checkTime();

				// Update crop status
				if (GameObject.FindGameObjectWithTag("FieldStatus") != null) {
					GameObject.FindGameObjectWithTag("FieldStatus").GetComponent<Text>().text = this.formatData(crop.Value);
				}

				switch (crop.Value.cropType)
				{
					case CropTypes.Wheat:
						this.cropsMap.SetTile(crop.Value.position, this.wheatTiles[stage]);
						break;
					case CropTypes.Corn:
						this.cropsMap.SetTile(crop.Value.position, this.cornTiles[stage]);
						break;
					case CropTypes.Carrot:
						this.cropsMap.SetTile(crop.Value.position, this.carrotTiles[stage]);
						break;
					default:
						Debug.Log("A type that does not exists.");
						break;
				}
			}
		}

		public Crop getDataFromTile(Vector3Int pos)
		{
			Crop data = this.crops[pos];
			return data;
		}

		public void harvestCrop(Vector3Int pos)
		{
			this.cropsMap.SetTile(pos, this.emptyField);

			// Remove crop from list over planted crops
			this.crops.Remove(pos);

			// Add Croptype to inventory
			Debug.Log("This crop is finished");
		}

		public void plantCrop(CropTypes type, Vector3Int pos)
		{
			PopupMenu menu = this.popupMenu.GetComponent<PopupMenu>();
			DateTime plantTime = DateTime.Now;
			Vector3Int placementPos = pos;
			this.crops.Add(placementPos, new Crop(type, plantTime, placementPos));

			switch (type)
			{
				case CropTypes.Wheat:
					this.cropsMap.SetTile(placementPos, this.wheatTiles[0]);
					break;
				case CropTypes.Corn:
					this.cropsMap.SetTile(placementPos, this.cornTiles[0]);
					break;
				case CropTypes.Carrot:
					this.cropsMap.SetTile(placementPos, this.carrotTiles[0]);
					break;
				default:
					break;
			}
			menu.closePopup();
		}

		public void plantSelectedCrop(string crop)
		{
			if (crop == "Wheat")
			{
				this.plantCrop(CropTypes.Wheat, this.selectedTile);
			}
			else if (crop == "Corn")
			{
				this.plantCrop(CropTypes.Corn, this.selectedTile);
			}
			else if (crop == "Carrot")
			{
				this.plantCrop(CropTypes.Carrot, this.selectedTile);
			}
			else
			{
				Debug.Log("You have misspelled one or more crops.");
			}
		}
	}
}
