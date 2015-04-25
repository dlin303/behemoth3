using UnityEngine;
using System.Collections;

public class TextCreator : MonoBehaviour {
	public GUIText guiTextFirst;
	public GUIText guiTextSecond;
	public float delayTime;
	public bool isFinished = false;

	private string textFirst;
	private string textSecond;
	private bool firstStarted = false;
	private bool secondStarted = false;
	private bool firstFinished = false;

	// Use this for initialization
	void Start () {
		if (guiTextFirst != null) {
			textFirst = guiTextFirst.text;
			guiTextFirst.text = "";
		}

		if (guiTextSecond != null) {
			textSecond = guiTextSecond.text;
			guiTextSecond.text = "";
		}

	}
	
	// Update is called once per frame
	void Update () {
		if (!firstStarted) {
			StartCoroutine (printSlowly (guiTextFirst, textFirst));
			firstStarted = true;
		}

		if (firstFinished && !secondStarted) {
			StartCoroutine (printSlowly (guiTextSecond, textSecond));
			secondStarted = true;
		}
 	}

	IEnumerator printSlowly(GUIText guiText, string text) {
		if (guiText != null) {
			for (int i=0; i < text.Length; i++){
				guiText.text += text[i];
				yield return new WaitForSeconds(delayTime);
			}
			firstFinished = true;
		}
	}
}
