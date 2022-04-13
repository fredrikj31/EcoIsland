using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace EcoIsland
{
	public class Bread : MonoBehaviour
	{
		public DateTime startedTime;
		public int bakeTimeSeconds = 30;

		public Bread(DateTime startedTime)
		{
			this.startedTime = startedTime;
		}

		// Check time
		public int checkTime()
		{
			// Calculate time
			DateTime now = DateTime.Now;
			TimeSpan difference = now.Subtract(this.startedTime);
			double procents = (difference.TotalSeconds / this.bakeTimeSeconds) * 100;

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

		public double getProcents()
		{
			// Calculate time
			DateTime now = DateTime.Now;
			TimeSpan difference = now.Subtract(this.startedTime);

			double procents = (difference.TotalSeconds / this.bakeTimeSeconds) * 100;

			return procents;
		}

		public TimeSpan getRemainingTime()
		{
			DateTime plantTime = this.startedTime;

			DateTime finishedDate = plantTime.AddSeconds(this.bakeTimeSeconds);
			TimeSpan remainingTime = finishedDate.Subtract(DateTime.Now);

			return remainingTime;
		}
	}
}
