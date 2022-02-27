using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CropsController : MonoBehaviour
{

	public Tilemap cropsMap;
	public List<Crop> crops;

    // Start is called before the first frame update
    void Start()
    {
		InvokeRepeating("updateCropsTime", 0f, 1f);
    }

	private void updateCropsTime() {
		foreach (Crop crop in this.crops) {
			// Calculate time
			DateTime now = DateTime.Now;

			TimeSpan difference = now.Subtract(crop.plantedTime);

			double procents = (difference.Minutes / crop.growTime) * 100;
		}
	}

	public void plantCrop(Crop plantedCrop) {
		this.crops.Add(plantedCrop);
	} 
}
