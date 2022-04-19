using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace EcoIsland {
	public class SellCrop : MonoBehaviour
	{
		private SiloStorage silo;
		private SaveMoney moneyController;

		private Dictionary<CropTypes, int> prices;

		// Start is called before the first frame update
		void Start()
		{
			StartCoroutine(getBuilding());
			this.prices = new Dictionary<CropTypes, int>() {
				{ CropTypes.Wheat, 4 },
				{ CropTypes.Corn, 7 },
				{ CropTypes.Carrot, 10 },
			};
		}

		private IEnumerator getBuilding() {
			yield return new WaitForSeconds(0.2f);
			this.silo = GameObject.FindGameObjectWithTag("Silo").GetComponent<SiloStorage>();
			this.moneyController = GameObject.FindGameObjectWithTag("SaveManager").GetComponent<SaveMoney>();
		}

		public void sellCrop(string inputItem) {
			CropTypes item = (CropTypes)Enum.Parse(typeof(CropTypes), inputItem);

			int price = this.prices[item];

			if (this.silo.hasAmount(item, 1) == true) {
				this.silo.removeCrop(item);
				this.moneyController.addMoney(price);
				return;
			} else {
				return;
			}
		}
	}
}
