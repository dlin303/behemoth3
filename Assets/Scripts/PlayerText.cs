using UnityEngine;
using System.Collections;

public class PlayerText : MonoBehaviour {
	string[] text;
	public float offset;
	public float gameStartTime;
	public float lastLabelTime;
	public float labelShowDuration;
	GUIText gText;

	// Use this for initialization
	void Start () {
		gameStartTime = Time.time;
		labelShowDuration = 3.0f;
		lastLabelTime = Time.time;
		gText = GetComponent<GUIText>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Time.time - lastLabelTime >  offset + labelShowDuration  ) {
			placeText(SelectRandomText());
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
			"That was great chinese food we had tonight!",
			"I just love dan dan noodles!",
			"Have you seen Lost?",
			"Wow, my date has really nice hands!",
			"Wow, I have really nice hands!",
			"I wonder if Game of Thrones is on?",
			"What would Tyrion do in this situation?",
			"Should we have gone to Olive Garden tonight?",
			"Really should do my laundry tomorrow.",
			"Oh god. I can feel it rumbling down below.",
			"SOMEONE HELP ME",
			"PLEASE NO",
			"Just a little more!!!!!!!",
			"I CAN DO IT.",
			"DON’T GIVE UP!!!",
			"WORK THOSE BUTT MUSCLES.",
			"CLENCH. CLENCH. CLENCH. CLENCH",
			"If you love her, don’t let go!",
			"Please don’t fart.",
			"PLEASE DON'T FART.",
			"I hate dan dan noodles!!!",
			"I wonder how my dog is doing.",
			"I wish this game wasn’t so heteronormative.",
			"I wish this game had better physics.",
			"Maybe I should get a smaller coffee table.",
			"Is there really such a thing as a soul?",
			"Where do we go when we die?",
			"What do you think of Hegel’s dialectic?",
			"I’m thinking of going blonde."
		};

		int index = Random.Range(0, texts.Length);
		return texts[index];
	}


}
