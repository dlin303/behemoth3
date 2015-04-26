using UnityEngine;
using System.Collections;

public class SceneManager : MonoBehaviour {
	// Use this for initialization
	public string nextLevel;

	// Update is called once per frame
	void Update () {
		float inputX = Input.GetAxis("Horizontal");
		if (inputX > 0 && !string.IsNullOrEmpty(nextLevel)) {
			Application.LoadLevel(nextLevel);
		}
	}
}
