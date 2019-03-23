using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionScene2 : MonoBehaviour
{
    public bool bookFallen = false;

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "FloorScene2")
        {
            print("book fallen");
            bookFallen = true;
        }
    }
}
