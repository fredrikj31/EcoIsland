using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupMenu : MonoBehaviour
{
	public Vector3 hiddenPos;
	private Vector3 position;
	private GameObject menuObject;


    // Start is called before the first frame update
    void Start()
    {		
		// Default values
		this.transform.position = this.hiddenPos;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
			this.closePopup();
		}
    }

	public void setObject(GameObject inputObject) {
		this.menuObject = Instantiate(inputObject);
		this.menuObject.transform.SetParent(this.transform.GetChild(0));
		this.menuObject.transform.localPosition = inputObject.transform.position;
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
