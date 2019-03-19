using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptEnableDisable : MonoBehaviour {

    public GameObject gameObjectContainingScriptToEnble; //drag the gameobject which have that script you want to disable, in the inspector.

    public bool script1Disabled = false;

    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        { 
            //enble 2nd script
            UserAvatarMatcher2 script2;
            script2 = gameObject.GetComponent<UserAvatarMatcher2>();
            script2.enabled = true;

            //disable 1st script
            UserAvatarMatcher script;
            script = gameObject.GetComponent<UserAvatarMatcher>();
            script.enabled = false;
            if(script.enabled == false)
            {
                script1Disabled = true;
            }
        }
    }

}
