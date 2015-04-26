using UnityEngine;
using System.Collections;

public class PlayerFace : MonoBehaviour {
	public float goInterval = 4f;
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
		flipToStop = false;
		setNextFlipTime ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void setNextFlipTime() {
		decrease = Random.Range(1f, 2f);

		if(flipToStop) {
	  		goInterval -= decrease;
			nextFlipTime += stopInterval;
			Debug.Log("go Intervall " + goInterval);

		}else{
			stopInterval += decrease;
			nextFlipTime += goInterval;
			Debug.Log ("stopInterval " + stopInterval);

		}

		//nextFlipTime = flipToStop ? nextFlipTime + stopInterval : nextFlipTime + goInterval; 

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
		flipToStop = !flipToStop;
		setNextFlipTime();
		setPlayerFace(flipToStop);
	}

	public void setFaceToExploding() {
		setPlayerFace(true);
	}

	public void setFaceToHappy() {
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
