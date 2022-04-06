using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EcoIsland
{


	public class TreeChopping : MonoBehaviour
	{
		// Tree Chopping Dialog
		public GameObject treeChopDialog;

		// Barn system
		private BarnStorage barn;

		// Popup Menu
		private GameObject popupMenu;
		private PopupMenu menu;

		// Start is called before the first frame update
		void Start()
		{
			this.popupMenu = GameObject.FindGameObjectWithTag("PopupMenu");
			this.menu = this.popupMenu.GetComponent<PopupMenu>();
			StartCoroutine(findBarn());
		}

		// Update is called once per frame
		void Update()
		{

		}

		void OnMouseDown()
		{
			Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Vector3 pos = new Vector3(mouseWorldPos.x, mouseWorldPos.y, 0);

			if (this.menu.isVisible == false) {
				menu.closePopup();
				menu.setPosition(pos);
				menu.setObject(this.treeChopDialog);
				menu.openPopup();
				this.menu.isVisible = true;
			}
		}

		public void deleteTree() {
			Destroy(this);
		}

		private IEnumerator findBarn() {
			yield return new WaitForSeconds(0.2f);

			this.barn = GameObject.FindGameObjectWithTag("Barn").GetComponent<BarnStorage>();
		}
	}
}
