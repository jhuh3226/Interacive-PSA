using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour {
	
	Text text;

	void Start(){
		text = GetComponent<Text> ();
	}

	void Update(){
		text.text = CarController.score + "";
	}
}
