using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EcoIsland
{
	public class ChopTree : MonoBehaviour
	{

		private PopupMenu menu;

		// Start is called before the first frame update
		void Start()
		{
			this.menu = GameObject.FindGameObjectWithTag("PopupMenu").GetComponent<PopupMenu>();
		}

		// Update is called once per frame
		void Update()
		{

		}

		public void chopTree(string treeType)
		{
			//GameObject[] trees = GameObject.FindGameObjectsWithTag(treeType);
			Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

			Collider2D collider = Physics2D.OverlapCircle(mouseWorldPos, 2.0f);
			Destroy(collider.gameObject);
			this.menu.closePopup();
		}
	}
}
