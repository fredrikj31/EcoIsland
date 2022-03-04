using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EcoIsland {
	public class SelectCrop : MonoBehaviour
	{
		private GameObject cropController;

		void Start() {
			this.cropController = GameObject.FindGameObjectWithTag("CropController");
		}

		public void selectCrop(string crop) {
			this.cropController.GetComponent<CropsController>().plantSelectedCrop(crop);
		}
	}
}
