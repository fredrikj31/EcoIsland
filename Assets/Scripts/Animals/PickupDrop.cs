using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupDrop : MonoBehaviour
{

	void OnMouseDown()
	{
		Destroy(this.gameObject);
		Debug.Log("I got clicked");
	}
}
