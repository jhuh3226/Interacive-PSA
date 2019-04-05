using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopMovement : MonoBehaviour {

    //public GameObject rigidbody;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "joint")  // or if(gameObject.CompareTag("YourWallTag"))
        {
            //print("joint collided with spine");
            //rigidbody.velocity = Vector3.zero;
        }
    }
}
