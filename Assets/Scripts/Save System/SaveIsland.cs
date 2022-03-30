using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Newtonsoft.Json;

public class SaveIsland : MonoBehaviour
{
	public Tilemap[] maps;
	public string ignoreMap;
	public TileBase[] tiles;
	public string[] saveTags;
	private SaveSystem saveSys;
	private	string tilemapFilePath;
	private string objectsFilePath;

    // Start is called before the first frame update
    void Start()
    {
		this.saveSys = new SaveSystem();
		this.tilemapFilePath = Application.persistentDataPath + "/tilemaps.json";
		this.objectsFilePath = Application.persistentDataPath + "/objects.json";

        this.maps = GameObject.FindObjectsOfType<Tilemap>();
    }

	public void saveObjects() {
		// Loop through objects with tag
		List<SaveObject> allObjects = new List<SaveObject>();

		foreach (string tag in saveTags)
		{
			GameObject[] objects = GameObject.FindGameObjectsWithTag(tag);
			
			foreach (GameObject selectedObject in objects)
			{
				Debug.Log(selectedObject.transform.position.x);
				SaveObject temp = new SaveObject();
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
		if (this.saveSys.fileExists(this.objectsFilePath)) {
			this.saveSys.overwriteFileContent(this.objectsFilePath, jsonResult);
		} else {
			this.saveSys.createFile(this.objectsFilePath);
			this.saveSys.overwriteFileContent(this.objectsFilePath, jsonResult);
		}
	}

	public void loadObjects() {
		string data = this.saveSys.readFile(this.objectsFilePath);

		List<SaveObject> result = JsonConvert.DeserializeObject<List<SaveObject>>(data);

		Object[] resourceObjects = Resources.LoadAll("Prefabs");

		Dictionary<string, GameObject> objects = new Dictionary<string, GameObject>();

		foreach (Object item in resourceObjects)
		{
			objects.Add(item.name, (GameObject)item);
		}

		// Clean up first!
		foreach (string tag in saveTags)
		{
			GameObject[] removeObjects = GameObject.FindGameObjectsWithTag(tag);
			
			foreach (GameObject gameObject in removeObjects)
			{
				Destroy(gameObject);
			}
		}

		foreach (GameObject gameObject in resourceObjects)
		{	
			foreach (SaveObject item in result)
			{
				if (gameObject.tag == item.tag) {
					Instantiate(gameObject, new Vector3(item.xPos, item.yPos, item.zPos), new Quaternion(item.xRotation, item.yRotation, item.zRotation, item.wRotation));
				}
			}
		}

	}

	public void saveTilemaps() {
		List<SaveTile> result = new List<SaveTile>();

		foreach (Tilemap map in this.maps)
		{
			if (map.name == this.ignoreMap) {
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

					SaveTile temp = new SaveTile();
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
		if (this.saveSys.fileExists(this.tilemapFilePath)) {
			this.saveSys.overwriteFileContent(this.tilemapFilePath, jsonResult);
		} else {
			this.saveSys.createFile(this.tilemapFilePath);
			this.saveSys.overwriteFileContent(this.tilemapFilePath, jsonResult);
		}

	}

	public void loadTilemaps() {
		string data = this.saveSys.readFile(this.tilemapFilePath);

		List<SaveTile> result = JsonConvert.DeserializeObject<List<SaveTile>>(data);

		Dictionary<string, TileBase> tiles = new Dictionary<string, TileBase>();

		foreach (TileBase item in this.tiles)
		{
			tiles.Add(item.name, (TileBase)item);
		}

		// Clearing the maps
		foreach (Tilemap map in this.maps)
		{
			// Ignore ground map
			if (map.name == this.ignoreMap) {
				continue;
			}

			// Clearing all the tiles
			map.ClearAllTiles();
			//Debug.Log("Clearing map : " + map.name);
		}

		foreach (Tilemap map in this.maps)
		{
			// Ignore ground map
			if (map.name == this.ignoreMap) {
				continue;
			}

			// Setting all the tiles back onto the map
			foreach (SaveTile tile in result)
			{
				if (tile.mapName == map.name) {
					map.SetTile(new Vector3Int(tile.xPos, tile.yPos, tile.zPos), tiles[tile.tileType]);
				}
			}
		}
	}
}
