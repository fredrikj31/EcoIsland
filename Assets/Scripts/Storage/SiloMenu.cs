using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace EcoIsland
{
	public class SiloMenu : MonoBehaviour
	{
		public GameObject siloMenu;
		public GameObject siloItems;
		private SiloStorage siloStorage;

		// Start is called before the first frame update
		void Start()
		{
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
				GameObject selectObject = siloItems.transform.Find(item.cropName).gameObject;
				Text cropText = selectObject.transform.GetChild(1).GetComponent<Text>();

				cropText.text = item.cropAmount.ToString();
			}
		}
	}
}
