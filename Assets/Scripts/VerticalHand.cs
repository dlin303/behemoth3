using UnityEngine;
using System.Collections;

public class VerticalHand : MonoBehaviour {
	public string vertical = "w";
	public string grab = "Grab_P1";
	public bool movingForward = false;
	public Sprite emptyHand;
	public Sprite clenchedEmpty;
	public Sprite clenchedHand;
	public Vector2 speed = new Vector2 (30f, 30f);
	public AudioClip grabFailSound;
	public AudioClip grabSuccessSound;
	public AudioClip reachSound;
	public float upForce = 40f;
	public float retractMagnitude = 1;

	private Rigidbody2D rb2D; 
	private SpriteRenderer spriteRenderer;
	private Vector2 movement; 
	private bool grabbing = false;
	private GameObject target;
	private BoxCollider2D selfCollider;
	private BoxCollider2D targetCollider;
	private float volume = 2.0f;
	private AudioSource source;

	bool playerWins;
	bool inputDisabled;
	
	// Use this for initialization
	void Start () {
		rb2D = GetComponent<Rigidbody2D> ();
		spriteRenderer = GetComponent<SpriteRenderer> ();
		source = GetComponent<AudioSource> ();
		if (spriteRenderer != null) {
			spriteRenderer.sprite = emptyHand;
		}

		selfCollider = GetComponent<BoxCollider2D> ();
		playerWins = false;
		inputDisabled = false;
	}
	
	// Update is called once per frame
	void Update () {

		//float inputY = Input.GetAxis (vertical);
		float grabInput = Input.GetAxis (grab);

		if (Input.GetKeyDown(vertical) && !inputDisabled) {
			playSoundEffect(reachSound);
			movement = new Vector2(0, upForce);
			movingForward = true;
		} else {
			movement = new Vector2 (0, 10f*-1f*retractMagnitude);
			movingForward = false;
		}

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

		if (target != null && selfCollider.bounds.Intersects (targetCollider.bounds) && grabbing) {
			playSoundEffect(grabSuccessSound);
			spriteRenderer.sprite = clenchedHand;
			inputDisabled = true;
			playerWins = true;
			Destroy (target);
		} else if (grabbing && !playerWins) {
			playSoundEffect(grabFailSound);
			spriteRenderer.sprite = clenchedEmpty;
		} else if (!playerWins) {
			//kind of meh. we are reassigning the sprite every update :/
			spriteRenderer.sprite = emptyHand;
		}
	}

	public bool didWin() {
		return playerWins;
	}

	void FixedUpdate() {
			rb2D.velocity = movement;
	}

	public void disableInput() {
		inputDisabled = true;
	}

	void playSoundEffect(AudioClip clip) {
		if (source != null && clip != null) {
			source.PlayOneShot(clip, volume);
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Target") {
			target = other.gameObject;
			targetCollider = target.GetComponent<BoxCollider2D>();
		}
	}
}
