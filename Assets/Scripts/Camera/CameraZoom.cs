using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EcoIsland
{
	public class CameraZoom : MonoBehaviour
	{
		public float maxZoomIn;
		public float maxZoomOut;
		public float zoomFactor;

		private Camera mainCamera;

		void Start()
		{
			this.mainCamera = this.GetComponent<Camera>();
		}

		public void zoomIn()
		{
			if (this.mainCamera.orthographicSize - this.zoomFactor >= this.maxZoomIn)
			{
				this.mainCamera.orthographicSize = this.mainCamera.orthographicSize - this.zoomFactor;
			}
			else
			{
				Debug.Log("You can't zoom more in.");
			}
		}

		public void zoomOut()
		{
			if (this.mainCamera.orthographicSize + this.zoomFactor <= this.maxZoomOut)
			{
				this.mainCamera.orthographicSize = this.mainCamera.orthographicSize + this.zoomFactor;
			}
			else
			{
				Debug.Log("You can't zoom more out.");
			}
		}
	}
}
