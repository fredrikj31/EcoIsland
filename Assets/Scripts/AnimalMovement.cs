using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalMovement : MonoBehaviour
{
	public float accelerationTime = 2f;
	public float maxSpeed = 5f;
	private Vector2 movement;
	private float timeLeft;
	private Rigidbody2D rb;

	void Start() {
		this.rb = this.GetComponent<Rigidbody2D>();
	}


	void Update()
	{
		timeLeft -= Time.deltaTime;
		if (timeLeft <= 0)
		{
			movement = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
			timeLeft += accelerationTime;
		}
	}

	void FixedUpdate()
	{
		this.rb.AddForce(movement * maxSpeed);
	}
}
