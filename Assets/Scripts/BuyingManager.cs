using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EcoIsland
{
	public class BuyingManager : MonoBehaviour
	{
		public GameObject saveManager;

		private SaveMoney moneyManager;

		void Start()
		{
			this.moneyManager = this.saveManager.GetComponent<SaveMoney>();
		}

		public void addCoins(int amount)
		{
			// Add Money
			this.moneyManager.addMoney(amount);
		}
	}
}