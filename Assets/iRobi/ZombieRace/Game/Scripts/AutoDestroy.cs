using UnityEngine;
using System.Collections;

public class AutoDestroy : MonoBehaviour {
	public float Time_To_Destroy=0.5f;
	float time;

	void Update () {
		time += Time.deltaTime;
	if (time > Time_To_Destroy) 
		{
			Destroy(gameObject);
		}
	}
}
