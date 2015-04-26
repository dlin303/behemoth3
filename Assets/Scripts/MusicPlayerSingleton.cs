using UnityEngine;
using System.Collections;

public class MusicPlayerSingleton : MonoBehaviour {
	public static MusicPlayerSingleton instance;
	public AudioClip cutSceneMusic;
	public AudioClip gameSceneMusic;

	AudioSource source;
	// Use this for initialization
	void Awake () {
		if (instance != null && instance != this) {
			Destroy (this.gameObject);
		} else {
			instance = this;
			DontDestroyOnLoad(this.gameObject);
		}

		source = GetComponent<AudioSource> ();
		Debug.Log ("reached the end of awake");
	}

	void Start() {
		playCutSceneMusic ();
	}

	public void playGameMusic() {
		playMusic (gameSceneMusic);
	}

	public void playCutSceneMusic() {
		playMusic (cutSceneMusic);
	}

	public void playMusic(AudioClip clip) {
		if (source != null) {
			source.Stop ();
			source.clip = clip;
			source.Play ();
		}
	}

	public void stopMusic() {
		if (source != null) {
			source.Stop ();
		}
	}
	
	public static MusicPlayerSingleton GetInstance() {
		return instance;
	}

	public static MusicPlayerSingleton Instance {
		get { return instance; }
	}
	
	
	// Update is called once per frame
	void Update () {
	
	}
}
