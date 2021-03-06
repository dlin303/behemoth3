﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

public class GameManager : MonoBehaviour {
	public GameObject p1FaceGameObject;
	public GameObject p2FaceGameObject;
	public GameObject p1ArmGameObject;
	public GameObject p2ArmGameObject;
	public bool p1_worried;
	public bool p2_worried;
	public float explodeDelay;
	public float gameTotalTime = 25.0f;
	public AudioClip[] fartSounds;

    VerticalHand p1Arm;
	VerticalHand p2Arm;
	Animator p1Anim;
	Animator p2Anim;
	PlayerFace p1PlayerFace;
	PlayerFace p2PlayerFace;

	float gameStartTime; 
	bool isGameEnding = false;
	bool isGameOver = false;
	AudioSource source; 

	// Use this for initialization
	void Start () {
		p1Anim = p1FaceGameObject.GetComponent<Animator>();
		p2Anim = p2FaceGameObject.GetComponent<Animator>();
		p1Arm =  p1ArmGameObject.GetComponent<VerticalHand>();
		p2Arm =  p2ArmGameObject.GetComponent<VerticalHand>();
		p1Anim.SetBool("worried", p1_worried);
		p2Anim.SetBool("worried", p2_worried);
		gameStartTime = Time.time;
	    p1PlayerFace = p1FaceGameObject.GetComponent<PlayerFace>(); 
		p2PlayerFace = p2FaceGameObject.GetComponent<PlayerFace>();
		source = GetComponent<AudioSource> ();

		MusicPlayerSingleton.Instance.playGameMusic ();
	}
	
	// Update is called once per frame
	void Update () {
		float currentTime = Time.time;

		if(isGameOver) {
			return;
		}

		if (isGameEnding) {
			if(checkIfGameHasEnded()) {
				gameOver(new List<PlayerFace>{p1PlayerFace, p2PlayerFace});
			}
			checkIfPlayerLoses();
			//let exploding animation play out
			return;
		}

		checkIfPlayerWins();

		checkIfPlayerLoses();

		if(currentTime - gameStartTime > gameTotalTime - 5) {
			p1PlayerFace.setFaceToExploding();
			p2PlayerFace.setFaceToExploding();
			//can do other things to indicate game is ending 
			isGameEnding = true;
			return;
		}

		if (currentTime - gameStartTime > p1PlayerFace.nextFlipTime) {
			p1PlayerFace.flip ();
		}

		if(currentTime - gameStartTime > p2PlayerFace.nextFlipTime) {
			p2PlayerFace.flip();
		}
	}

	void checkIfPlayerLoses() {
		float currentTime = Time.time;

		if(p1Arm.movingForward && p1PlayerFace.isExploding() && currentTime - p1PlayerFace.explodingStartTime > explodeDelay)
		{
			p2PlayerFace.setUnimpressed(true);
			gameOver (new List<PlayerFace> {p1PlayerFace});
		}

		if(p2Arm.movingForward && p2PlayerFace.isExploding() && currentTime - p1PlayerFace.explodingStartTime > explodeDelay)
		{
			p1PlayerFace.setUnimpressed(true);
			gameOver (new List<PlayerFace> {p2PlayerFace});
		}
	}

	void checkIfPlayerWins() {
		if(p1Arm.didWin())
		{
			p1PlayerFace.wins();
		}

		if(p2Arm.didWin()) {
			p2PlayerFace.wins();
		}

		if(p1Arm.didWin() && p2Arm.didWin()) {
			isGameOver = true;
			Invoke("loadWinningEnding", 2);
			MusicPlayerSingleton.Instance.stopMusic ();
		}
	}

	bool checkIfGameHasEnded() {
		return Time.time - gameStartTime > gameTotalTime;
	}

	void gameOver(List<PlayerFace> players) {
		p1Arm.disableInput();
		p2Arm.disableInput();
		MusicPlayerSingleton.Instance.stopMusic ();
		
		players.ForEach(delegate (PlayerFace player){
			if(player.isWinner()) {
				player.setUnimpressed(true);
			} else {
				AudioClip fartSound = fartSounds[Random.Range (0, fartSounds.Length)];
				if (source != null && fartSound != null) {
					source.PlayOneShot(fartSound, 1.0f);
				}

				player.lose();
			}
		});

		isGameOver = true;
		Invoke("loadGameOverEnding", 2);
	}

	void loadGameOverEnding() {
		if(p1PlayerFace.isLoser() && p2PlayerFace.isLoser())
			Application.LoadLevel("BothLose");
		else if(p1PlayerFace.isLoser())
			Application.LoadLevel ("P1Lose"); 
		else
			Application.LoadLevel ("P2Lose"); 
	}

	void loadWinningEnding() {
		Application.LoadLevel ("PlayersWin");

	}
}
