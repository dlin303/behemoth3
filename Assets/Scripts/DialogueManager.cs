using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class DialogueManager : MonoBehaviour {
	public GameObject[] p1Dialogue;
	public GameObject[] p2Dialogue;
	public float delayTime = 1.5f;

	private int index = 0;
	bool p2Turn = false;
	float nextTime;
	float newTime;


	// Use this for initialization
	void Start () {
		for (int i=0; i < p1Dialogue.Length; i++) {
			p1Dialogue[i].SetActive(false);
			p2Dialogue[i].SetActive(false);
		}

		nextTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if (index < p1Dialogue.Length) {
			GameObject p1 = p1Dialogue [index];
			GameObject p2 = p2Dialogue [index];
			float newTime = Time.time;

			if (!p2Turn && (newTime - nextTime > delayTime)) {
				p1.SetActive (true);
				p2Turn = true;
				nextTime = newTime;
			}

			if (p2Turn && newTime - nextTime > delayTime) {
				p2.SetActive (true);
				p2Turn = false;
				nextTime = newTime;
				index++;
			}
		}
	}


	//IEnumerator 
	void setActive(GameObject obj, bool active, int waitTime) {
		obj.SetActive (active);
		//yield return new WaitForSeconds (waitTime);
	}
}
