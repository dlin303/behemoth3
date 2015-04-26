using UnityEngine;
using System.Collections;

public class MusicPlayerSIngleton : MonoBehaviour {
	public static MusicPlayerSIngleton instance;
	public AudioClip cutSceneMusic;
	public AudioClip gameSceneMusic;

	// Use this for initialization
	void Start () {
		if (instance != null && instance != this) {
			Destroy (this.gameObject);
		} else {
			instance = this;
			DontDestroyOnLoad(this.gameObject);
		}

	}

	void OnLevelWasLoaded(int level) {

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
