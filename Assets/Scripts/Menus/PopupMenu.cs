using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace EcoIsland
{
	public class PopupMenu : MonoBehaviour
	{
		public Vector3 hiddenPos;
		private Vector3 position;
		private GameObject menuObject;


		// Start is called before the first frame update
		void Start()
		{
			// Default values
			this.transform.position = this.hiddenPos;
		}

		void Update()
		{
			if (Input.GetMouseButtonDown(0))
			{
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

				if (hit.collider != null && hit.collider.name == "PopupMenu")
				{
					return;
				}
				else
				{
					this.closePopup();
				}
			}
		}
		public void setObject(GameObject inputObject)
		{
			this.menuObject = Instantiate(inputObject);
			this.menuObject.transform.SetParent(this.transform.GetChild(0));
			this.menuObject.transform.localPosition = inputObject.transform.position;
		}

		public void setPosition(Vector3 pos)
		{
			pos.y = pos.y + 1.25f;
			this.position = pos;
		}

		public void openPopup()
		{
			this.transform.position = this.position;
		}

		public void closePopup()
		{
			this.transform.position = this.hiddenPos;

			// Dispose old content
			foreach (Transform child in this.transform.GetChild(0).transform)
			{
				GameObject.Destroy(child.gameObject);
			}
		}
	}
}
