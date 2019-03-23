using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionScene1 : MonoBehaviour {

    public bool frameFallen = false;

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "FloorScene2")
        {
            print("frame fallen");
            frameFallen = true;
        }
    }
}
