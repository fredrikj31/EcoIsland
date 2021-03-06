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
			this.siloItems = this.siloMenu.transform.GetChild(2).gameObject;
			this.siloStorage = GameObject.FindGameObjectWithTag("Silo").GetComponent<SiloStorage>();

			StartCoroutine(this.updateUI());
		}

		void OnMouseDown()
		{
			StartCoroutine(this.updateUI());
			this.siloMenu.SetActive(true);
		}

		public void closeMenu() {
			StartCoroutine(this.updateUI());
			this.siloMenu.SetActive(false);
		}

		public IEnumerator updateUI()
		{
			yield return new WaitForSeconds(0.2f);

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
