using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace EcoIsland {
	public class SellItem : MonoBehaviour
	{
		private BarnStorage barn;
		private SaveMoney moneyController;

		private Dictionary<ItemTypes, int> prices;

		// Start is called before the first frame update
		void Start()
		{
			StartCoroutine(getBuilding());
			this.prices = new Dictionary<ItemTypes, int>() {
				{ ItemTypes.Bread, 25 },
			};
		}

		private IEnumerator getBuilding() {
			yield return new WaitForSeconds(0.2f);
			this.barn = GameObject.FindGameObjectWithTag("Barn").GetComponent<BarnStorage>();
			this.moneyController = GameObject.FindGameObjectWithTag("SaveManager").GetComponent<SaveMoney>();
		}

		public void sellItem(string inputItem) {
			ItemTypes item = (ItemTypes)Enum.Parse(typeof(ItemTypes), inputItem);

			int price = this.prices[item];

			if (this.barn.hasAmount(item, 1) == true) {
				this.barn.removeItem(item);
				this.moneyController.addMoney(price);
				return;
			} else {
				return;
			}
		}
	}
}
