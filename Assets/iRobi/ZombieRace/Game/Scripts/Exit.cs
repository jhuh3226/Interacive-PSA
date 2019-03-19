using UnityEngine;
using System.Collections;
using iRobi;

public class Exit : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ExitGame(){
		Application.Quit ();
	}

	public void RetryGame(){
		Application.LoadLevel("Game");
	}

	public void Garage(){
		BrushPaint.paintedTexture = CarController.bloodTexture;
		Application.LoadLevel("scene");
	}
}
