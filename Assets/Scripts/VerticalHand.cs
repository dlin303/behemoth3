using UnityEngine;
using System.Collections;

public class VerticalHand : MonoBehaviour {
	public string vertical = "Vertical";
	public string grab = "Grab_P1";
	public bool movingForward = false;


	public Vector2 speed = new Vector2(30, 30);
	public float retractMagnitude = 4;
	private Rigidbody2D rb2D; 
	private Vector2 movement; 
	private bool grabbing = false;
	private GameObject target;

	// Use this for initialization
	void Start () {
		rb2D = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		float inputY = Input.GetAxis (vertical);
		float grabInput = Input.GetAxis (grab);

		if (inputY > 0) {
			movement = new Vector2 (0, speed.y * inputY);
			movingForward = true;
		} else {
			movement = new Vector2 (0, speed.y*-1*retractMagnitude);
			movingForward = false;
		}

		//TODO, this isn't used yet
		grabbing = grabInput > 0;

		//make sure hand doesn't leave camera
		var dist = (transform.position - Camera.main.transform.position).z;
		var leftBorder = Camera.main.ViewportToWorldPoint (new Vector3 (0, 0, dist)).x;
		var rightBorder = Camera.main.ViewportToWorldPoint (new Vector3 (1, 0, dist)).x;
		var topBorder = Camera.main.ViewportToWorldPoint (new Vector3 (0, 0, dist)).y;
		var bottomBorder = Camera.main.ViewportToWorldPoint (new Vector3 (0, 1, dist)).y;
		transform.position = new Vector3 (
			Mathf.Clamp (transform.position.x, leftBorder, rightBorder),
			Mathf.Clamp (transform.position.y, topBorder, bottomBorder),
			transform.position.z);
	}

	void FixedUpdate() {
		rb2D.velocity = movement;
	}
}
