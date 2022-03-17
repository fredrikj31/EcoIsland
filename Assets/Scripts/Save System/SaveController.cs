using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Newtonsoft.Json;

public class SaveController : MonoBehaviour
{
	public Tilemap[] maps;
	
	private SaveSystem saveSys;
	private	string filePath;

    // Start is called before the first frame update
    void Start()
    {
		this.saveSys = new SaveSystem();
		this.filePath = Application.persistentDataPath + "/tilemaps.json";

        this.maps = GameObject.FindObjectsOfType<Tilemap>();
    }

	public void saveTilemaps() {
		List<SaveTile> result = new List<SaveTile>();

		foreach (Tilemap map in this.maps)
		{
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
		if (this.saveSys.fileExists(this.filePath)) {
			this.saveSys.overwriteFileContent(this.filePath, jsonResult);
		} else {
			this.saveSys.createFile(this.filePath);
			this.saveSys.overwriteFileContent(this.filePath, jsonResult);
		}

	}

	public void loadTilemaps() {
		string data = this.saveSys.readFile(this.filePath);

		List<SaveTile> result = JsonConvert.DeserializeObject<List<SaveTile>>(data);

		Object[] resourceTiles = Resources.LoadAll("Tiles");

		Dictionary<string, TileBase> tiles = new Dictionary<string, TileBase>();

		foreach (Object item in resourceTiles)
		{
			tiles.Add(item.name, (TileBase)item);
		}

		// Clearing the maps
		foreach (Tilemap map in this.maps)
		{
			// Clearing all the tiles
			map.ClearAllTiles();
			//Debug.Log("Clearing map : " + map.name);
		}

		foreach (Tilemap map in this.maps)
		{
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
