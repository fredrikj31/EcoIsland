using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlaceObjects : MonoBehaviour
{
    public GameObject scrollView;
	public GameObject placedObject;
	private ScrollRect scroll;
	private bool isPlacing;

	// Start is called before the first frame update
	void Start()
	{
		this.scroll = this.scrollView.GetComponent<ScrollRect>();
	}

	// Update is called once per frame
	void Update()
	{
		// Dragging
		if (this.isPlacing) {
			if (Input.touchCount > 0)
			{
				Touch touch = Input.GetTouch(0); // get first touch since touch count is greater than zero

				if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved)
				{
					Debug.Log("Im Dragging...");
				}
			}
		}
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		// OnClick code goes here ...


	}

	public void OnPointerDown(PointerEventData eventData)
	{
		this.scroll.vertical = false;
		this.isPlacing = true;
	}
}
