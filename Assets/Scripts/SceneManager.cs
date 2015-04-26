using UnityEngine;
using System.Collections;

public class SceneManager : MonoBehaviour {
	// Use this for initialization
	public string nextLevel;
	public float sceneTime = 3f;
	public bool isTitleScreen = false;
	public bool isEndingScreen = false;
	public bool isLosingScreen = true;

	public AudioClip losingSound;
	public AudioClip winningSound;

	private AudioSource source; 

	float startTime;
	bool playedSound = false;

	void Start() {
		startTime = Time.time;
		source = GetComponent<AudioSource> ();
	}

	// Update is called once per frame
	void Update () {
		if (isEndingScreen && !playedSound) {
			if (isLosingScreen) {
				playSoundEffect(losingSound);
			}
			else {
				playSoundEffect(winningSound);
			}

			playedSound = true;
		}

		if (!isTitleScreen && !isEndingScreen) {
			float inputX = Input.GetAxis ("Horizontal");
			if (Time.time - startTime > sceneTime || (inputX > 0 && !string.IsNullOrEmpty (nextLevel))) {
				Application.LoadLevel (nextLevel);
			}

		} else if (isEndingScreen && Input.GetKeyDown(KeyCode.Return)) {
			MusicPlayerSingleton.Instance.playCutSceneMusic();
			Application.LoadLevel("IntroScene1");
		} else {
			if (Input.GetKeyDown (KeyCode.Return)) {
				Application.LoadLevel (nextLevel);
			}
		}

		if (Input.GetKeyDown ("space")) {
			Application.LoadLevel ("GameInstructions");
		}

	}

	void playSoundEffect(AudioClip clip) {
		if (source != null) {
			source.PlayOneShot(clip);
		}
	}
}
