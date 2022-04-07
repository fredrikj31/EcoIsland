using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EcoIsland
{
	public class ChopTree : MonoBehaviour
	{

		private PopupMenu menu;
		private SaveIsland saveIsland;

		// Start is called before the first frame update
		void Start()
		{
			this.menu = GameObject.FindGameObjectWithTag("PopupMenu").GetComponent<PopupMenu>();
			this.saveIsland = GameObject.FindGameObjectWithTag("SaveManager").GetComponent<SaveIsland>();
		}

		// Update is called once per frame
		void Update()
		{

		}

		public void chopTree(string treeType)
		{
			//GameObject[] trees = GameObject.FindGameObjectsWithTag(treeType);
			Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

			Collider2D[] colliders = Physics2D.OverlapCircleAll(mouseWorldPos, 2.0f);

			foreach (Collider2D collider in colliders)
			{
				if (collider.gameObject.tag == "Tree") {
					Destroy(collider.gameObject);
					break;
				}
			}

			this.menu.closePopup();
			this.saveIsland.saveObjects();
		}
	}
}
