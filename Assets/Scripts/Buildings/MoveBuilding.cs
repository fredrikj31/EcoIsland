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
		private GameObject mainCamera;
		private CameraMovement movement;
		private SaveIsland saveIsland;

		// Start is called before the first frame update
		void Start()
		{
			this.mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
			this.movement = this.mainCamera.GetComponent<CameraMovement>();
			this.saveIsland = GameObject.FindGameObjectWithTag("SaveManager").GetComponent<SaveIsland>();
		}

		// Update is called once per frame
		void Update()
		{
			checkForLongPress();
		}

		void checkForLongPress()
		{
			if (Input.touchCount == 0)
			{
				return;
			}

			Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			RaycastHit2D hitInformation = Physics2D.Raycast(mouseWorldPos, Camera.main.transform.forward);
	
			if (hitInformation.collider == null || this.GetComponent<Collider2D>() == null) {
				return;
			}

            if (hitInformation.collider.name != this.GetComponent<Collider2D>().name) {
                return;
            } else {
				if (Input.GetTouch(0).phase == TouchPhase.Began)
				{ // If the user puts her finger on screen...
					this.beginPressed = DateTime.Now;
				}

				if (Input.GetTouch(0).phase == TouchPhase.Moved)
				{
					this.pressed = DateTime.Now;

					if (this.pressed > this.beginPressed.AddSeconds(this.timeDelayThreshold))
					{
						this.movement.toggleMoveable(false);
						// Is the time pressed greater than our time delay threshold?
						//Do whatever you want
						this.transform.position = new Vector3(mouseWorldPos.x, mouseWorldPos.y, 0);
						
					}
				}

				if (Input.GetTouch(0).phase == TouchPhase.Ended)
				{
					this.movement.toggleMoveable(true);
					this.saveIsland.saveObjects();
				}
			}


		}
	}
}
