using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace EcoIsland
{
	public class CropsController : MonoBehaviour
	{
		

		public Grid grid;
		public Tilemap cropsMap;
		public TileBase[] wheatTiles = new TileBase[3];
		public TileBase[] cornTiles = new TileBase[3];
		public TileBase[] carrotTiles = new TileBase[3];
		public Dictionary<Vector3Int, Crop> crops = new Dictionary<Vector3Int, Crop>();

		// Time management
		private float downClickTime;
		private float ClickDeltaTime = 0.2F;

		// Popup Menu
		private GameObject popupMenu;

		// Start is called before the first frame update
		void Start()
		{
			this.popupMenu = GameObject.FindGameObjectWithTag("PopupMenu");

			InvokeRepeating("updateCropsTime", 0f, 1f);
			this.getAllTiles();
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
						if (clickedTile.name == "Field")
						{
							//Debug.Log(this.cropsMap.GetTile(this.getMousePosition()).name);
							this.plantCrop(CropTypes.Wheat);
						} else {
							//Debug.Log("Hej med dig.");
							Crop data = this.getDataFromTile(this.getMousePosition());

							// Get cell position
							Vector3 cellPos = this.getCellPosition();
							TimeSpan remainingTime = data.getRemainingTime();
							string popupText = $"Type: {data.cropType.ToString()}\nGrowth: {Math.Round(data.getProcents(), 1)}%";
							if (remainingTime.Hours > 0) {
								popupText = $"Type: {data.cropType.ToString()}\nGrowth: {Math.Round(data.getProcents(), 1)}%\n{remainingTime.Hours} hrs {remainingTime.Minutes} min {remainingTime.Seconds} sec";
							} else if (remainingTime.Minutes > 0) {
								popupText = $"Type: {data.cropType.ToString()}\nGrowth: {Math.Round(data.getProcents(), 1)}%\n{remainingTime.Minutes} min {remainingTime.Seconds} sec";
							} else {
								popupText = $"Type: {data.cropType.ToString()}\nGrowth: {Math.Round(data.getProcents(), 1)}%\n{remainingTime.Seconds} sec";
							}

							// Menu
							PopupMenu menu = this.popupMenu.GetComponent<PopupMenu>();
							menu.setPosition(cellPos);
							menu.setText(popupText);
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

		private Vector3 getCellPosition() {
			return grid.GetCellCenterWorld(this.getMousePosition());
		}

		private void getAllTiles()
		{
			BoundsInt bounds = this.cropsMap.cellBounds;
			TileBase[] allTiles = this.cropsMap.GetTilesBlock(bounds);

			Debug.Log(allTiles.Length);
		}

		private void updateCropsTime()
		{
			foreach (KeyValuePair<Vector3Int, Crop> crop in this.crops)
			{
				int stage = crop.Value.checkTime();
				switch (crop.Value.cropType)
				{
					case CropTypes.Wheat:
						this.cropsMap.SetTile(crop.Value.position, this.wheatTiles[stage]);
						break;
					case CropTypes.Corn:
						this.cropsMap.SetTile(crop.Value.position, this.cornTiles[stage]);
						break;
					case CropTypes.Carrot:
						this.cropsMap.SetTile(crop.Value.position, this.wheatTiles[stage]);
						break;
					default:
						Debug.Log("Fuck mand");
						break;
				}
			}
		}

		public Crop getDataFromTile(Vector3Int pos) {
			Crop data = this.crops[pos];

			return data;
		}

		public void harvestCrop()
		{

		}

		public void plantCrop(CropTypes type)
		{
			DateTime plantTime = DateTime.Now;
			Vector3Int placementPos = this.getMousePosition();
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
		}


	}
}
