using UnityEngine;
using System.Collections;

public class SceneManager : MonoBehaviour {
	// Use this for initialization
	public string nextLevel;
	public float sceneTime = 3f;
	public bool isTitleScreen = false;
	public bool isEndingScreen = false;

	float startTime;

	void Start() {
		startTime = Time.time;
	}

	// Update is called once per frame
	void Update () {
		if (!isTitleScreen && !isEndingScreen) {
			float inputX = Input.GetAxis ("Horizontal");
			if (Time.time - startTime > sceneTime || (inputX > 0 && !string.IsNullOrEmpty (nextLevel))) {
				Application.LoadLevel (nextLevel);
			}

		} else if (isEndingScreen && Input.GetKeyDown(KeyCode.Return)){
			Application.LoadLevel("IntroScene1");
		}else{
			if (Input.GetKeyDown (KeyCode.Return)) {
				Application.LoadLevel (nextLevel);
			}
		}

		if (Input.GetKeyDown ("space")) {
			Application.LoadLevel ("GameInstructions");
		}

	}
}
