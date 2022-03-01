using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Crop
{
	public string cropType;
	public DateTime plantedTime;
	public Vector3Int position;

	public Crop(string type, DateTime plantedTime, Vector3Int pos) {
		this.cropType = type;
		this.plantedTime = plantedTime;
		this.position = pos;
	}

	// Times
	private Dictionary<string, float> growTime = new Dictionary<string, float>() {
		{"Wheat", 2},
		{"Corn", 5},
		{"Carrot", 10},
	};

	// Check time
	public int checkTime() {
		// Calculate time
		DateTime now = DateTime.Now;
		TimeSpan difference = now.Subtract(this.plantedTime);

		double procents = (difference.Minutes / this.growTime[this.cropType]) * 100;

		// Stages: (0 = level 1), (1 = level 2), (2 = level 3)
		if (procents <= 33.3) {
			return 0;
		} else if (procents > 33.3 && procents <= 66.6) {
			return 1;
		} else {
			return 2;
		}
	}
}
