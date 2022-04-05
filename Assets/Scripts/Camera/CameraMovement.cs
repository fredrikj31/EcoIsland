using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace EcoIsland
{
	public class CameraMovement : MonoBehaviour
	{
		public bool isMoveable;
		public float leftLimit;
		public float rightLimit;
		public float bottomLimit;
		public float upperLimit;

		private BuildingManager manager;
		private Camera cam;
		private bool moveAllowed;
		private Vector3 touchPos;

		void Start()
		{
			this.cam = this.GetComponent<Camera>();
		}

		void Update()
		{
			if (this.isMoveable == false) {
				return;
			}

			if (Input.touchCount > 0)
			{
				if (Input.touchCount == 2)
				{
					// Zooming (Fuk this)
					return;
				}
				else
				{
					// Moving
					Touch touch = Input.GetTouch(0);

					switch (touch.phase)
					{
						case TouchPhase.Began:

							if (EventSystem.current.IsPointerOverGameObject(touch.fingerId))
							{
								this.moveAllowed = false;
							}
							else
							{
								this.moveAllowed = true;
							}

							this.touchPos = this.cam.ScreenToWorldPoint(touch.position);
							break;
						case TouchPhase.Moved:
							if (this.moveAllowed == true)
							{
								Vector3 direction = this.touchPos - this.cam.ScreenToWorldPoint(touch.position);
								this.cam.transform.position += direction;

								this.transform.position = new Vector3(Mathf.Clamp(this.transform.position.x, this.leftLimit, this.rightLimit), Mathf.Clamp(this.transform.position.y, this.bottomLimit, this.upperLimit), this.transform.position.z);
							}
							break;
						default:
							break;
					}
				}
			}
		}

		public void toggleMoveable(bool active) {
			this.isMoveable = active;
		}

		private void OnDrawGizmos()
		{
			Gizmos.color = Color.yellow;
			Gizmos.DrawWireCube(new Vector3((this.rightLimit - Mathf.Abs(this.leftLimit) / 2.0f), (this.upperLimit - Mathf.Abs(this.bottomLimit) / 2.0f)), new Vector3(this.rightLimit - this.leftLimit, this.upperLimit - this.bottomLimit));
		}
	}
}
