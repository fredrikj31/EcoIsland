using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EcoIsland {
	public class HelicopterMenu : MonoBehaviour
	{
		private GameObject helicopterMenu;

		// Start is called before the first frame update
		void Start()
		{
			this.helicopterMenu = GameObject.FindGameObjectWithTag("HelicopterMenu").transform.GetChild(0).gameObject;
		}

		public void openMenu()
		{
			this.helicopterMenu.SetActive(true);
		}

		public void closeMenu()
		{
			this.helicopterMenu.SetActive(false);
		}
	}
}
