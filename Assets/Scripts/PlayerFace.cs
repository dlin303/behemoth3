using UnityEngine;
using System.Collections;

public class PlayerFace : MonoBehaviour {
	public float goInterval = 5.0f;
	public float stopInterval = 1f;
	public float nextFlipTime = 0f;
	public string playerName;
	public float explodingStartTime = 0;
	Animator anim;
	bool flipToStop;
	float decrease = 0.0f;
	bool loser;

	
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		setNextFlipTime ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void setNextFlipTime() {
		decrease = Random.Range(0.0f, 0.50f);

		if(flipToStop) {
	  		goInterval -= decrease;
		}else{
			stopInterval += decrease;
		}

		nextFlipTime = flipToStop ? nextFlipTime + goInterval : nextFlipTime + stopInterval; 
		Debug.Log(string.Format("setting player {0} next flip time = {1}", playerName, nextFlipTime)); 
	}

	void setPlayerFace(bool isExploding, bool overrideCheck = false) {
		if(isExploding != anim.GetBool("exploding") && !overrideCheck){
			anim.SetBool("exploding", isExploding);
			if(isExploding){
				explodingStartTime = Time.time;
			}
			Debug.Log(string.Format("setting player {0} to exploding={1}", playerName, isExploding));
		}
	}

	public void flip(){
		setNextFlipTime();
		flipToStop = !flipToStop;
		setPlayerFace(flipToStop);
	}

	public void setFaceToExploding() {
		setPlayerFace(true);
	}

	public void setFaceToHappy() {
		anim.SetBool("happy", true);
	}

	public void setUnimpressed(bool unimpressed) {
		anim.SetBool ("unimpressed", unimpressed);
	}

	public bool isExploding() {
		return flipToStop;
	}

	public void lose() {
		anim.SetBool ("losing", true);
		loser = true;
	}

	public bool isLoser() {
		return loser;
	}
}
