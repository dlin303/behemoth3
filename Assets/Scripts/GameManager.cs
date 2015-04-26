using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {
	public GameObject p1FaceGameObject;
	public GameObject p2FaceGameObject;
	public GameObject p1ArmGameObject;
	public GameObject p2ArmGameObject;
	public bool p1_worried;
	public bool p2_worried;
	public float explodeDelay;

    VerticalHand p1Arm;
	VerticalHand p2Arm;
	Animator p1Anim;
	Animator p2Anim;
	PlayerFace p1PlayerFace;
	PlayerFace p2PlayerFace;

	float gameStartTime; 
	float gameTotalTime = 15.0f;
	bool isGameEnding = false;
	bool isGameOver = false;

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
	}
	
	// Update is called once per frame
	void Update () {
		float currentTime = Time.time;

		if(isGameOver) {
			return;
		}

		if (isGameEnding) {
			if(checkIfGameHasEnded())
				gameOver (new List<PlayerFace>{p1PlayerFace, p2PlayerFace});
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
			p1PlayerFace.setFaceToHappy();
		}

		if(p2Arm.didWin()) {
			p1PlayerFace.setFaceToHappy();
		}

		if(p1Arm.didWin() && p2Arm.didWin()) {
			isGameOver = true;
			Invoke("loadWinningEnding", 3);
		}
	}

	bool checkIfGameHasEnded() {
		return Time.time - gameStartTime > gameTotalTime;
	}

	void gameOver(List<PlayerFace> players) {
		p1Arm.disableInput();
		p2Arm.disableInput();
		
		players.ForEach(delegate (PlayerFace player){
			player.lose();
		});

		isGameOver = true;
		Invoke("loadGameOverEnding", 3);
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
