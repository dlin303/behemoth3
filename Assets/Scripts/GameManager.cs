﻿using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	public GameObject player1;
	public GameObject player2;
	public bool p1_worried;
	public bool p2_worried;

	Animator p1_anim;
	Animator p2_anim;
	// Use this for initialization
	void Start () {
		p1_anim = player1.GetComponent<Animator>();
		p2_anim = player2.GetComponent<Animator>();
		p1_anim.SetBool("worried", p1_worried);
		p2_anim.SetBool("worried", p2_worried);
		p2_anim.SetTrigger("exploding");
	}
	
	// Update is called once per frame
	void Update () {
		//setplayerfaces 
		//if player face is exploding and player moves, then player loses
		//if player time is up, then the player loses 


	
	}

	void setPlayerFaces() {


	}

	void PlayerLoses() {
		//game ends 
		//call player controller 


	}
}
