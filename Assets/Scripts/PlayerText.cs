using UnityEngine;
using System.Collections;
using System;
public class PlayerText : MonoBehaviour {
	public float offset;
	public float gameStartTime;
	public float lastLabelTime;
	public float labelShowDuration;
	public string playerText="";
	TextMesh gText;

	// Use this for initialization
	void Start () {
		gameStartTime = Time.time;
		labelShowDuration = 3.0f;
		lastLabelTime = Time.time;
		gText = GetComponent<TextMesh>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Time.time - lastLabelTime >  offset + labelShowDuration || !String.IsNullOrEmpty(playerText) ) {
			if(String.IsNullOrEmpty(playerText))
				placeText(SelectRandomText());
			else
			   placeText(playerText);
		}

		if(Time.time - lastLabelTime > labelShowDuration) {
			removeText();
		}
	}

	void removeText() {
		placeText("");
	}


	void placeText(string text) {
		lastLabelTime = Time.time;
		gText.text =text; 
	}

	string SelectRandomText(){
		string[] texts = new string[] {
			"(That was great chinese food!)",
			"(I just love dan dan noodles!)",
			"(Have you seen Lost?)",
			"(Wow, my date has really nice hands!)",
			"(Wow, I have really nice hands!)",
			"(I wonder if Game of Thrones is on?)",
			"(What would Tyrion do?)",
			"(Should we have gone to Olive Garden?)",
			"(Really should do my laundry tomorrow.)",
			"(Oh god. I can feel it.)",
			"(SOMEONE HELP ME)",
			"(PLEASE NO)",
			"(Just a little more!!!!!!!)",
			"<gurgle>",
			"<plurp plurp>",
			"<gergle gergle>",
			"< #$^#%$% :-( >",
			"<bloop bloop>",
			"<rumble rumble?>",
			"(Does she have a tumblr?)",
			"(BE THE CHANGE!!!)",
			"(I CAN DO IT.)",
			"(DON’T GIVE UP!!!)",
			"(WORK THOSE BUTT MUSCLES.)",
			"(CLENCH. CLENCH. CLENCH.)",
			"(GO GO GO)",
			"(Ughhhh my stomach)",
			"(Ughhh I got the bloats)",
			"(DON'T LET GO!!!!)",
			"(Please don’t fart.)",
			"(PLEASE DON'T FART.)",
			"(I hate dan dan noodles!!!)",
			"(I wonder how my dog is doing.)",
			"(I wish this game wasn’t so hetero.)",
			"(I wish this game had better physics.)",
			"(Do I really have a soul?)",
			"(Where do we go when we die?)",
			"(What do you think of Hegel?)",
			"(I’m thinking of going blonde.)",
			"(I wonder what Elon Musk's is up to.)",
			"<gurgle gurgle>",
			"(There is no spoon)",
			"(FREE YOUR MIND)",
			"(Get busy living or get busy dying)",
			"(Is this text centered?)"
		};

		int index = UnityEngine.Random.Range(0, texts.Length);
		return texts[index];
	}


}
