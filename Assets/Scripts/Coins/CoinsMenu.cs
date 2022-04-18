using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EcoIsland
{	
public class CoinsMenu : MonoBehaviour
{
    private GameObject barnItems;
		private GameObject barnMenu;

		// Start is called before the first frame update
		void Start()
		{
			this.barnMenu = GameObject.FindGameObjectWithTag("CoinsMenu").transform.GetChild(0).gameObject;
		}

		void OnMouseDown()
		{
			this.barnMenu.SetActive(true);
		}

		public void closeMenu()
		{
			this.barnMenu.SetActive(false);
		}
	}
}
