using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BakeryMenu : MonoBehaviour
{
	public GameObject menu;

	public void closeMenu()
	{
		this.menu.SetActive(false);
	}
}
