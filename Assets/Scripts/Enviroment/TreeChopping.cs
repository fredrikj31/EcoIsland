using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EcoIsland
{


	public class TreeChopping : MonoBehaviour
	{
		// Tree Chopping Dialog
		public GameObject treeChopDialog;

		// Popup Menu
		private GameObject popupMenu;

		// Start is called before the first frame update
		void Start()
		{
			this.popupMenu = GameObject.FindGameObjectWithTag("PopupMenu");
		}

		// Update is called once per frame
		void Update()
		{

		}

		void OnMouseDown()
		{
			Touch touch = Input.GetTouch(0);
			Vector2 pos = touch.position;
			PopupMenu menu = this.popupMenu.GetComponent<PopupMenu>();

			print(menu);
			menu.setPosition(new Vector3(pos.x, pos.y, 0));
			menu.setObject(this.treeChopDialog);
			menu.openPopup();
		}
	}
}
