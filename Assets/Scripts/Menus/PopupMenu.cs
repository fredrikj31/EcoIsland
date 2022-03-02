using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupMenu : MonoBehaviour
{
	public Vector3 hiddenPos;
	private Vector3 position;
	private string text;

	private GameObject textObject;

    // Start is called before the first frame update
    void Start()
    {
        this.textObject = this.transform.GetChild(1).gameObject;
		
		// Default values
		this.transform.position = this.hiddenPos;
		this.textObject.GetComponent<Text>().text = this.text;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
			this.closePopup();
		}
    }

	public void setText(string text) {
		this.textObject.GetComponent<Text>().text = text;
	}

	public void setPosition(Vector3 pos) {
		pos.y = pos.y + 1.25f;
		this.position = pos;
	}

	public void openPopup() {
		this.transform.position = this.position;
	}

	public void closePopup() {
		this.transform.position = this.hiddenPos;
	}
}
