using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Newtonsoft.Json;

namespace EcoIsland
{
	public class SaveIsland : MonoBehaviour
	{
		public Tilemap[] maps;
		public string[] ignoreMaps;
		public TileBase[] tiles;
		public GameObject[] prefabObjects;
		public string[] saveTags;
		private SaveSystem saveSys;
		private string tilemapFilePath;
		private string objectsFilePath;

		// Start is called before the first frame update
		void Start()
		{
			this.saveSys = new SaveSystem();
			this.tilemapFilePath = Application.persistentDataPath + "/tilemaps.json";
			this.objectsFilePath = Application.persistentDataPath + "/objects.json";

			this.maps = GameObject.FindObjectsOfType<Tilemap>();

			// Load Map
			if (this.saveSys.fileExists(this.tilemapFilePath) == true) {
				this.loadTilemaps();
			}
			if (this.saveSys.fileExists(this.objectsFilePath) == true) {
				this.loadObjects();
			}
		}

		public void saveObjects()
		{
			// Loop through objects with tag
			List<ObjectPrefab> allObjects = new List<ObjectPrefab>();

			foreach (string tag in saveTags)
			{
				GameObject[] objects = GameObject.FindGameObjectsWithTag(tag);

				foreach (GameObject selectedObject in objects)
				{
					//Debug.Log(selectedObject.name.Split(' ')[0]);
					ObjectPrefab temp = new ObjectPrefab();
					temp.name = selectedObject.name.Split('(')[0];
					temp.tag = selectedObject.tag;
					temp.xPos = selectedObject.transform.position.x;
					temp.yPos = selectedObject.transform.position.y;
					temp.zPos = selectedObject.transform.position.z;
					temp.xRotation = selectedObject.transform.rotation.x;
					temp.yRotation = selectedObject.transform.rotation.y;
					temp.zRotation = selectedObject.transform.rotation.z;
					temp.wRotation = selectedObject.transform.rotation.w;

					allObjects.Add(temp);
				}
			}

			string jsonResult = JsonConvert.SerializeObject(allObjects);

			// When finished saving all the tiles, then save to file
			if (this.saveSys.fileExists(this.objectsFilePath))
			{
				this.saveSys.overwriteFileContent(this.objectsFilePath, jsonResult);
			}
			else
			{
				this.saveSys.createFile(this.objectsFilePath);
				this.saveSys.overwriteFileContent(this.objectsFilePath, jsonResult);
			}
		}

		public void loadObjects()
		{
			string data = this.saveSys.readFile(this.objectsFilePath);

			List<ObjectPrefab> result = JsonConvert.DeserializeObject<List<ObjectPrefab>>(data);

			Dictionary<string, GameObject> gameObjects = new Dictionary<string, GameObject>();

			foreach (GameObject item in this.prefabObjects)
			{
				gameObjects.Add(item.name, (GameObject)item);
			}

			// Clean up first!
			foreach (string tag in this.saveTags)
			{
				GameObject[] removeObjects = GameObject.FindGameObjectsWithTag(tag);

				foreach (GameObject gameObject in removeObjects)
				{
					Destroy(gameObject);
				}
			}

			foreach (ObjectPrefab item in result)
			{
				Instantiate(gameObjects[item.name], new Vector3(item.xPos, item.yPos, item.zPos), new Quaternion(item.xRotation, item.yRotation, item.zRotation, item.wRotation));
			}

		}

		public void saveTilemaps()
		{
			List<Tile> result = new List<Tile>();

			foreach (Tilemap map in this.maps)
			{
				bool skip = false;
				foreach (string ignoreMap in this.ignoreMaps)
				{
					if (map.name == ignoreMap)
					{
						skip = true;
					}
				}

				if (skip == true) {
					continue;
				}
				

				BoundsInt bounds = map.cellBounds;
				TileBase[] allTiles = map.GetTilesBlock(bounds);

				foreach (var pos in map.cellBounds.allPositionsWithin)
				{
					Vector3Int localPlace = new Vector3Int(pos.x, pos.y, pos.z);
					Vector3 place = map.CellToWorld(localPlace);
					if (map.HasTile(localPlace))
					{
						TileBase tile = map.GetTile(localPlace);

						Tile temp = new Tile();
						temp.mapName = map.name;
						temp.tileType = tile.name;
						temp.xPos = localPlace.x;
						temp.yPos = localPlace.y;
						temp.zPos = localPlace.z;

						result.Add(temp);
					}
				}
			}


			string jsonResult = JsonConvert.SerializeObject(result);

			// When finished saving all the tiles, then save to file
			if (this.saveSys.fileExists(this.tilemapFilePath))
			{
				this.saveSys.overwriteFileContent(this.tilemapFilePath, jsonResult);
			}
			else
			{
				this.saveSys.createFile(this.tilemapFilePath);
				this.saveSys.overwriteFileContent(this.tilemapFilePath, jsonResult);
			}

		}

		public void loadTilemaps()
		{
			string data = this.saveSys.readFile(this.tilemapFilePath);

			List<Tile> result = JsonConvert.DeserializeObject<List<Tile>>(data);

			Dictionary<string, TileBase> tiles = new Dictionary<string, TileBase>();

			foreach (TileBase item in this.tiles)
			{
				tiles.Add(item.name, (TileBase)item);
			}

			// Clearing the maps
			foreach (Tilemap map in this.maps)
			{
				// Ignore Maps
				bool skip = false;
				foreach (string ignoreMap in this.ignoreMaps)
				{
					if (map.name == ignoreMap)
					{
						skip = true;
					}
				}

				if (skip == true) {
					continue;
				}

				// Clearing all the tiles
				map.ClearAllTiles();
				//Debug.Log("Clearing map : " + map.name);
			}

			foreach (Tilemap map in this.maps)
			{
				// Ignore ground map
				bool skip = false;
				foreach (string ignoreMap in this.ignoreMaps)
				{
					if (map.name == ignoreMap)
					{
						skip = true;
					}
				}

				if (skip == true) {
					continue;
				}

				// Setting all the tiles back onto the map
				foreach (Tile tile in result)
				{
					if (tile.mapName == map.name)
					{
						map.SetTile(new Vector3Int(tile.xPos, tile.yPos, tile.zPos), tiles[tile.tileType]);
					}
				}
			}
		}
	}
}
