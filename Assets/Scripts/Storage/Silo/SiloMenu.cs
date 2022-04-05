using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace EcoIsland
{
	public class SiloMenu : MonoBehaviour
	{
		private GameObject siloItems;
		private GameObject siloMenu;
		private SiloStorage siloStorage;

		// Start is called before the first frame update
		void Start()
		{
			this.siloMenu = GameObject.FindGameObjectWithTag("SiloMenu").transform.GetChild(0).gameObject;
			this.siloItems = this.siloMenu.transform.GetChild(2).GetChild(0).GetChild(0).gameObject;
			this.siloStorage = this.GetComponent<SiloStorage>();

			this.updateUI();
		}

		void OnMouseDown()
		{
			this.siloMenu.SetActive(true);
		}

		public void closeMenu() {
			this.siloMenu.SetActive(false);
		}

		public void updateUI()
		{
			List<CropItem> result = this.siloStorage.getCropItems();

			foreach (CropItem item in result)
			{
				GameObject selectObject = this.siloItems.transform.Find(item.cropName).gameObject;
				Text cropText = selectObject.transform.GetChild(1).GetComponent<Text>();

				cropText.text = item.cropAmount.ToString();
			}
		}
	}
}
