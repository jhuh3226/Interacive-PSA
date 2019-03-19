using UnityEngine;
using System.Collections;

public class CamControl : MonoBehaviour {
	public Transform normPos;
	public Transform topPos;
	float timer;
//	Transform current;
//	Vector3 smoothPos,smoothRot;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		timer += Time.fixedDeltaTime;
		if (CarController.gameOver) {
			transform.position = Vector3.Lerp (transform.position, topPos.position, (Vector3.Distance(transform.position, topPos.position)/5000));
			transform.rotation = Quaternion.Lerp (transform.rotation, topPos.rotation, (Vector3.Distance(transform.position, topPos.position)/2000));
		} else {
			transform.position = Vector3.Lerp (transform.position, normPos.position, Time.deltaTime * 2*timer*timer);
			transform.rotation = Quaternion.Lerp (transform.rotation, normPos.rotation, Time.deltaTime/2*timer*timer);
		}
	}
}
