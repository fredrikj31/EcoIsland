using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlidingMenu : MonoBehaviour
{
	public float speed;
	public float openPos;
	public float closePos;
	private RectTransform panelTransform;
	private bool isMoving;
	private bool isOpen = false;
	private EffectPlayer effectPlayer;

    // Start is called before the first frame update
    void Start()
    {
		this.effectPlayer = GameObject.FindGameObjectWithTag("EffectController").GetComponent<EffectPlayer>();
        this.panelTransform = this.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
		if (this.isOpen == true) {
			if (this.isMoving == true) {
				float step = speed * Time.deltaTime * 100;
				this.panelTransform.anchoredPosition = Vector2.MoveTowards(this.panelTransform.anchoredPosition, new Vector2(this.openPos, this.panelTransform.anchoredPosition.y), step);
			
				// Check for distance
				if (Vector2.Distance(this.panelTransform.anchoredPosition, new Vector2(this.openPos, this.panelTransform.anchoredPosition.y)) < 0.02f) {
					this.isMoving = false;
				}
			}
		} else {
			if (this.isMoving == true) {
				float step = speed * Time.deltaTime * 100;
				this.panelTransform.anchoredPosition = Vector2.MoveTowards(this.panelTransform.anchoredPosition, new Vector2(this.closePos, this.panelTransform.anchoredPosition.y), step);
			
				// Check for distance
				if (Vector2.Distance(this.panelTransform.anchoredPosition, new Vector2(this.closePos, this.panelTransform.anchoredPosition.y)) < 0.02f) {
					this.isMoving = false;
				}
			}
		}
    }

	public void OpenMenu() {
		this.isOpen = true;
		this.isMoving = true;
		//Debug.Log(this.panelTransform.anchoredPosition.);
		// Play Effect Sound
		this.effectPlayer.playEffect("button_press");
	}
	public void CloseMenu() {
		this.isOpen = false;
		this.isMoving = true;
		//Debug.Log(this.panelTransform.anchoredPosition.);
		this.effectPlayer.playEffect("button_press");
	}
}
