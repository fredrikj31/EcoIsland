using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EcoIsland
{	
public class CoinsMenu : MonoBehaviour
{
    private GameObject barnItems;
		private GameObject coinsMenu;

		// Start is called before the first frame update
		void Start()
		{
			this.coinsMenu = GameObject.FindGameObjectWithTag("CoinsMenu").transform.GetChild(0).gameObject;
		}

		public void openMenu()
		{
			this.coinsMenu.SetActive(true);
		}

		public void closeMenu()
		{
			this.coinsMenu.SetActive(false);
		}
	}
}
