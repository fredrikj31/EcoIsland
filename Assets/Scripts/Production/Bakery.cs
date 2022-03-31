using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace EcoIsland
{
	public class Bakery : MonoBehaviour
	{
		public int bakeryStatus;
		public GameObject barn;
		private bool isFinished;
		private BarnStorage storage;
		private GameObject bakeryMenu;
		private Animator animator;
		private Bread bread;

		// Start is called before the first frame update
		void Start()
		{
			this.storage = this.barn.GetComponent<BarnStorage>();
			this.bakeryMenu = GameObject.FindGameObjectWithTag("BakeryMenu").transform.GetChild(0).gameObject;
			this.animator = this.GetComponent<Animator>();

			// Updates time and so on...
			InvokeRepeating("updateBreadTime", 0f, 1f);
		}

		// Update is called once per frame
		void Update()
		{
		}

		void OnMouseDown()
		{
			if (this.isFinished == true) {
				this.storage.addItem(ItemTypes.Bread);
			} else {
				this.bakeryMenu.SetActive(true);
			}
		}

		private void updateBreadTime() {
			int stage = bread.checkTime();
			if (stage == 2) {
				this.isFinished = true;
				this.bread = null;
			} else if (stage == 1 || stage == 0) {
				this.animator.SetInteger("bakeryStatus", 1);
			} else {
				this.animator.SetInteger("bakeryStatus", 0);
			}
		}
		private string formatData(Bread data)
		{
			TimeSpan remainingTime = data.getRemainingTime();
			string popupText = $"";

			if (data.checkTime() == 2)
			{
				popupText = $"Finished";
			}
			else
			{
				if (remainingTime.Hours > 0)
				{
					popupText = $"{remainingTime.Hours} hrs {remainingTime.Minutes} min {remainingTime.Seconds} sec";
				}
				else if (remainingTime.Minutes > 0)
				{
					popupText = $"{remainingTime.Minutes} min {remainingTime.Seconds} sec";
				}
				else
				{
					popupText = $"{remainingTime.Seconds} sec";
				}
			}

			return popupText;
		}

		public void closeMenu()
		{
			this.bakeryMenu.SetActive(false);
		}

		public void bakeBread() {
			if (this.bread == null) {
				Bread bread = new Bread(DateTime.Now);
				this.bread = bread;
			}
		}
	}
}
