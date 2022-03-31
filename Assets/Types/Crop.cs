using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace EcoIsland
{
	public class Crop
	{
		public CropTypes cropType;
		public DateTime plantedTime;
		public Vector3Int position;

		public Crop(CropTypes type, DateTime plantedTime, Vector3Int pos)
		{
			this.cropType = type;
			this.plantedTime = plantedTime;
			this.position = pos;
		}

		// Times (in Seconds)
		private Dictionary<CropTypes, double> growTime = new Dictionary<CropTypes, double>() {
			{CropTypes.Wheat, 120},
			{CropTypes.Corn, 300},
			{CropTypes.Carrot, 600},
		};

		// Check time
		public int checkTime()
		{
			// Calculate time
			DateTime now = DateTime.Now;
			TimeSpan difference = now.Subtract(this.plantedTime);
			double procents = (difference.TotalSeconds / this.growTime[this.cropType]) * 100;

			// Stages: (0 = level 1), (1 = level 2), (2 = level 3)
			if (procents <= 50.0)
			{
				return 0;
			}
			else if (procents < 100.0)
			{
				return 1;
			}
			else
			{
				return 2;
			}
		}

		public double getProcents() {
			// Calculate time
			DateTime now = DateTime.Now;
			TimeSpan difference = now.Subtract(this.plantedTime);

			double procents = (difference.TotalSeconds / this.growTime[this.cropType]) * 100;

			return procents;
		}

		public TimeSpan getRemainingTime() {
			DateTime plantTime = this.plantedTime;

			DateTime finishedDate = plantTime.AddSeconds(this.growTime[this.cropType]);
			TimeSpan remainingTime = finishedDate.Subtract(DateTime.Now);

			return remainingTime;
		}
	}

}
