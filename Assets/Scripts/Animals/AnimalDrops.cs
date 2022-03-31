using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EcoIsland
{
	public class AnimalDrops : MonoBehaviour
	{
		public float[] moveIntervalMinMax = new float[2];
		public GameObject item;

		void Start()
		{
			Invoke("dropItem", Random.Range(this.moveIntervalMinMax[0], this.moveIntervalMinMax[1]));
		}

		void dropItem()
		{
			Vector3 pos = this.transform.position;
			pos.y = pos.y + 0.25f;
			Instantiate(this.item, pos, this.transform.rotation);
			Invoke("dropItem", Random.Range(this.moveIntervalMinMax[0], this.moveIntervalMinMax[1]));
		}
	}
}
