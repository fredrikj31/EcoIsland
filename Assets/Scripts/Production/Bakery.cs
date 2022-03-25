using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bakery : MonoBehaviour
{
	public GameObject bakeryMenu;
	public int bakeryStatus;
	private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        this.animator = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
		this.animator.SetInteger("bakeryStatus", this.bakeryStatus);
    }

	void OnMouseDown() {
		this.bakeryMenu.SetActive(true);
	}

	public void closeMenu() {
		this.bakeryMenu.SetActive(false);
	}
}
