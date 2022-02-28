using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CropsController : MonoBehaviour
{
	public Grid grid;
	public Tilemap cropsMap;
	public List<Crop> crops = new List<Crop>();


	// Time management
     private float downClickTime;
     private float ClickDeltaTime = 0.2F;

    // Start is called before the first frame update
    void Start()
    {
		InvokeRepeating("updateCropsTime", 0f, 1f);
    }

	void Update() 
	{
		if (Input.GetMouseButtonDown(0)) {
			this.downClickTime = Time.time;
		}

		if (Input.GetMouseButtonUp(0)) {
			if(Time.time - downClickTime <= ClickDeltaTime) {
            	Debug.Log(this.cropsMap.GetTile(this.getMousePosition()).name);
			}
		}
	}

	Vector3Int getMousePosition() {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return grid.WorldToCell(mouseWorldPos);
    }

	private void updateCropsTime() {
		if (this.crops.Count > 0) {	
			foreach (Crop crop in this.crops) {
				// Calculate time
				DateTime now = DateTime.Now;

				TimeSpan difference = now.Subtract(crop.plantedTime);

				double procents = (difference.Minutes / crop.growTime) * 100;
			}
		}
	}

	public void plantCrop(Crop plantedCrop) {
		this.crops.Add(plantedCrop);
	} 
}
