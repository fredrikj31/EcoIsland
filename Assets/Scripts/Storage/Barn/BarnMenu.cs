using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace EcoIsland
{
	public class BarnMenu : MonoBehaviour
	{
		private GameObject barnItems;
		private GameObject barnMenu;
		private BarnStorage barnStorage;

		// Start is called before the first frame update
		void Start()
		{
			this.barnMenu = GameObject.FindGameObjectWithTag("BarnMenu").transform.GetChild(0).gameObject;
			this.barnItems = this.barnMenu.transform.GetChild(2).GetChild(0).GetChild(0).gameObject;
			this.barnStorage = GameObject.FindGameObjectWithTag("Barn").GetComponent<BarnStorage>();

			StartCoroutine(this.updateUI());
		}

		void OnMouseDown()
		{
			this.barnMenu.SetActive(true);
		}

		public void closeMenu()
		{
			this.barnMenu.SetActive(false);
		}

		public IEnumerator updateUI()
		{
			yield return new WaitForSeconds(0.2f);

			List<Item> result = this.barnStorage.getItems();

			foreach (Item item in result)
			{
				GameObject selectObject = barnItems.transform.Find(item.itemName).gameObject;
				Text cropText = selectObject.transform.GetChild(1).GetComponent<Text>();

				cropText.text = item.itemAmount.ToString();
			}
		}
	}
}
