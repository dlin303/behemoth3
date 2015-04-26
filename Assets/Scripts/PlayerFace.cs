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
	bool loser = false;
	bool winner = false;

	
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
	}

	void setPlayerFace(bool isExploding, bool overrideCheck = false) {
		if(isExploding != anim.GetBool("exploding") && !overrideCheck){
			anim.SetBool("exploding", isExploding);
			if(isExploding){
				explodingStartTime = Time.time;
			}
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
		Debug.Log ("Set face to happy!");
		anim.SetBool("worried", false);
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

	public void wins() {
		setFaceToHappy ();
		winner = true;

	}

	public bool isLoser() {
		return loser;
	}

	public bool isWinner() {
		return winner;
	}
}
