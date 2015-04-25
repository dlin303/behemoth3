using UnityEngine;
using System.Collections;

public class VerticalHand : MonoBehaviour {
	public string vertical = "Vertical";
	public Vector2 speed = new Vector2(30, 30);
	public float retractMagnitude = 4;
	private Rigidbody2D rb2D; 
	private Vector2 movement; 

	// Use this for initialization
	void Start () {
		rb2D = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		float inputY = Input.GetAxis (vertical);
		if (inputY > 0) {
			movement = new Vector2 (0, speed.y * inputY);
		} else {
			movement = new Vector2 (0, speed.y*-1*retractMagnitude);
		}

	}

	void FixedUpdate() {
		rb2D.velocity = movement;
	}
}
