using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EcoIsland
{
	
public class BakeryMenu : MonoBehaviour
{
	private Bakery bakeryController;
	private BarnStorage barn;
	private GameObject bakeryMenu;

	void Start() {
		this.bakeryController = GameObject.FindGameObjectWithTag("BakeryController").GetComponent<Bakery>();
		this.barn = GameObject.FindGameObjectWithTag("Barn").GetComponent<BarnStorage>();
		this.bakeryMenu = GameObject.FindGameObjectWithTag("BakeryMenu");
	}

	void OnMouseDown()
	{
		if (this.bakeryController.isFinished == true) {
			this.barn.addItem(ItemTypes.Bread);
			this.bakeryController.isFinished = false;
		} else {
			this.openMenu();
		}
	}

	public void openMenu()
	{
		this.bakeryMenu.transform.GetChild(0).gameObject.SetActive(true);
	}

	public void closeMenu()
	{
		this.bakeryMenu.transform.GetChild(0).gameObject.SetActive(false);
	}
}
}
