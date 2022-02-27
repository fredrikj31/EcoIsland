using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Crop
{
    public enum CropType {
		Carrot,
		Corn,
		Wheat
	}
	public DateTime plantedTime;
	public double growTime;
	public Vector3Int position;
}
