using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenMenu : MonoBehaviour
{
    private GameObject helicopterMenu;

	// Start is called before the first frame update
	void Start()
	{
		this.helicopterMenu = GameObject.FindGameObjectWithTag("HelicopterMenu").transform.GetChild(0).gameObject;
	}

	void OnMouseDown() {
		this.helicopterMenu.SetActive(true);
	}
}
