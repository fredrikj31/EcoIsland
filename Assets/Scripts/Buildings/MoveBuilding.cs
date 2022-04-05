using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace EcoIsland
{

	public class MoveBuilding : MonoBehaviour
	{
		public double timeDelayThreshold = 2.0f;
		private DateTime beginPressed;
		private DateTime pressed;
		public GameObject mainCamera;
		private CameraMovement movement;

		// Start is called before the first frame update
		void Start()
		{
			this.mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
			this.movement = this.mainCamera.GetComponent<CameraMovement>();
		}

		// Update is called once per frame
		void Update()
		{
			checkForLongPress();
		}

		private void checkForLongPress()
		{
			if (Input.touchCount == 0)
			{
				return;
			}

			if (Input.GetTouch(0).phase == TouchPhase.Began)
			{ // If the user puts her finger on screen...
				this.beginPressed = DateTime.Now;
			}

			if (Input.GetTouch(0).phase == TouchPhase.Moved)
			{ // If the user raises her finger from screen
			  //print("I got raised");
				this.pressed = DateTime.Now;

				if (this.pressed > this.beginPressed.AddSeconds(this.timeDelayThreshold))
				{
					this.movement.toggleMoveable(false);

					// Is the time pressed greater than our time delay threshold?
					//Do whatever you want
					Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
					this.transform.position = new Vector3(mouseWorldPos.x, mouseWorldPos.y, 0);
					
				}
			}

			if (Input.GetTouch(0).phase == TouchPhase.Ended)
			{
				this.movement.toggleMoveable(true);
			}

		}
	}
}
