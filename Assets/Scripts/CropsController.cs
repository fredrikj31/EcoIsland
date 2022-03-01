using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace EcoIsland {
	public class CropsController : MonoBehaviour
	{
		public Grid grid;
		public Tilemap cropsMap;
		public TileBase[] wheatTiles = new TileBase[3];
		public TileBase[] cornTiles = new TileBase[3];
		public TileBase[] carrotTiles = new TileBase[3];
		public Dictionary<TileBase, Crop> crops = new Dictionary<TileBase, Crop>();

		// Time management
		private float downClickTime;
		private float ClickDeltaTime = 0.2F;

		// Start is called before the first frame update
		void Start()
		{
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
							Debug.Log(this.cropsMap.GetTile(this.getMousePosition()).name);
						}
					}
				}
			}
		}

		Vector3Int getMousePosition()
		{
			Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			return grid.WorldToCell(mouseWorldPos);
		}

		private void getAllTiles()
		{
			BoundsInt bounds = this.cropsMap.cellBounds;
			TileBase[] allTiles = this.cropsMap.GetTilesBlock(bounds);

			Debug.Log(allTiles.Length);
		}

		private void updateCropsTime()
		{
			foreach (KeyValuePair<TileBase, Crop> crop in this.crops)
			{
				int stage = crop.Value.checkTime();
				switch (crop.Value.cropType)
				{
					case "Wheat":
						this.cropsMap.SetTile(crop.Value.position, this.wheatTiles[stage]);
						break;
					case "Corn":
						this.cropsMap.SetTile(crop.Value.position, this.cornTiles[stage]);
						break;
					case "Carrot":
						this.cropsMap.SetTile(crop.Value.position, this.wheatTiles[stage]);
						break;
					default:
						Debug.Log("Fuck mand");
						break;
				}
			}
		}

		public void harvestCrop()
		{

		}

		public void plantCrop(CropTypes type)
		{
			DateTime plantTime = DateTime.Now;
			this.crops.Add(this.cropsMap.GetTile(this.getMousePosition()), new Crop(type.ToString(), plantTime, this.getMousePosition()));

			switch (type)
			{
				case CropTypes.Wheat:
					break;
				default:
					break;
			}
		}


	}
}
