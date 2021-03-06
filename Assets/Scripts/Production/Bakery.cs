using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace EcoIsland
{
	public class Bakery : MonoBehaviour
	{
		public int bakeryStatus;
		public bool isFinished;
		private GameObject barn;
		private BarnStorage storage;
		private GameObject bakeryMenu;
		private Animator animator;
		private Bread bread;
		private bool isBaking;

		// Start is called before the first frame update
		void Start()
		{
			this.barn = GameObject.FindGameObjectWithTag("Barn");
			if (this.barn != null) {
				this.storage = this.barn.GetComponent<BarnStorage>();
			}

			this.bakeryMenu = GameObject.FindGameObjectWithTag("BakeryMenu").transform.GetChild(0).gameObject;

			StartCoroutine(getBakeryThings());

			// Resets its state
			this.bread = null;
			this.isBaking = false;

			// Updates time and so on...
			InvokeRepeating("updateBreadTime", 0f, 1f);
		}

		private IEnumerator getBakeryThings() {
			yield return new WaitForSeconds(0.5f);

			if (GameObject.FindGameObjectWithTag("Bakery") != null) {
				this.animator = GameObject.FindGameObjectWithTag("Bakery").GetComponent<Animator>();
			}
		}

		private void updateBreadTime() {
			if (this.isBaking == false) {
				//print("I'm not baking");
				return;
			}

			if (this.isFinished == true) {
				return;
			}

			int stage = this.bread.checkTime();

			// Update Text
			this.bakeryMenu.transform.GetChild(3).GetComponent<Text>().text = this.formatData(this.bread);
			if (stage == 2) {
				this.bread = null;
				this.isFinished = true;
				this.isBaking = false;
				this.animator.SetInteger("bakeryStatus", 0);
			} else if (stage == 1 || stage == 0) {
				this.animator.SetInteger("bakeryStatus", 1);
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

		public void bakeBread() {
			if (this.isBaking == false) {
				//print("Baking now");
				Bread bread = new Bread(DateTime.Now);
				this.bread = bread;
				this.isBaking = true;
			} else {
				print("Hej");
			}
		}
	}
}
