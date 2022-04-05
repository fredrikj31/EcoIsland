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
		private bool isFinished;
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
			this.storage = this.barn.GetComponent<BarnStorage>();
			this.bakeryMenu = GameObject.FindGameObjectWithTag("BakeryMenu").transform.GetChild(0).gameObject;
			this.animator = this.GetComponent<Animator>();

			// Resets its state
			this.bread = null;
			this.isBaking = false;

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
			print(this.isBaking);
			if (this.isBaking == false) {
				//print("I'm not baking");
				return;
			}

			int stage = this.bread.checkTime();
			Debug.Log(this.bakeryMenu.name);
			this.bakeryMenu.transform.GetChild(3).transform.GetChild(0).GetComponent<Text>().text = this.formatData(this.bread);
			if (stage == 2) {
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

		public void bakeBread() {
			this.isBaking = true;
			if (this.isBaking == false) {
				//print("Baking now");
				Bread bread = new Bread(DateTime.Now);
				this.bread = bread;
				this.isBaking = true;
			} else {
				//print("Hej");
			}
		}
	}
}
