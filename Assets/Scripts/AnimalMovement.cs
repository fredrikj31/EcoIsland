using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class AnimalMovement : MonoBehaviour
{
	public Tilemap ground;
	public float[] moveIntervalMinMax = new float[2];
	public float speed = 2f;
	private Rigidbody2D rb;
	private Vector3 newPos;
	private SpriteRenderer sprite;

	void Start() {
		this.rb = this.GetComponent<Rigidbody2D>();
		this.newPos = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);

		this.sprite = this.GetComponent<SpriteRenderer>();

		Invoke("moveAnimal", 3f);
	}

	void Update() {
		if (this.transform.position == this.newPos) {
			return;
		} else {
			if (Vector3.Distance(this.transform.position, this.newPos) > 0) {
				float step = this.speed * Time.deltaTime;
				this.transform.position = Vector3.MoveTowards(this.transform.position, this.newPos, step);
			}
		}
	}

	void moveAnimal() {
		float xValue = Random.Range(-1f, 1f);
		float yValue = Random.Range(-1f, 1f);

		Vector3Int tempPos = Vector3Int.FloorToInt(new Vector3((this.transform.position.x + xValue), (this.transform.position.y + yValue), this.ground.transform.position.z));
	
		//Debug.Log(this.ground.GetTile(tempPos));

		if (this.ground.GetTile(tempPos) != null) {
			if (this.ground.GetTile(tempPos).name == "Grass") {
				if (xValue < 0) {
					// Left
					this.sprite.flipX = false;
				} else {
					// Right
					this.sprite.flipX = true;
				}

				this.newPos = new Vector3((this.transform.position.x + xValue), (this.transform.position.y + yValue), this.transform.position.z);
				// Move animal again
				Invoke("moveAnimal", Random.Range(this.moveIntervalMinMax[0], this.moveIntervalMinMax[1]));
			} else {
				this.moveAnimal();
			}
		} else {
			this.moveAnimal();
		}
	}
}
