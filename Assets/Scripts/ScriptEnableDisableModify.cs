using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptEnableDisableModify : MonoBehaviour
{
    public GameObject gameObjectContainingScriptToEnble; //drag the gameobject which have that script you want to disable, in the inspector.
    public GameObject gameObContainingScript; //

    public bool script1Disabled = false;

    void Update()
    {
        EnableDisableSceneOverall EnableDisableSceneOverallScript = gameObContainingScript.GetComponent<EnableDisableSceneOverall>();

        UserAvatarMatcher1A script1A;
        script1A = gameObject.GetComponent<UserAvatarMatcher1A>();

        UserAvatarMatcher1B script1B;
        script1B = gameObject.GetComponent<UserAvatarMatcher1B>();

        UserAvatarMatcher2A script2A;
        script2A = gameObject.GetComponent<UserAvatarMatcher2A>();

        UserAvatarMatcher3A script3A;
        script3A = gameObject.GetComponent<UserAvatarMatcher3A>();

        UserAvatarMatcher4A script4A;
        script4A = gameObject.GetComponent<UserAvatarMatcher4A>();

        UserAvatarMatcher4B script4B;
        script4B = gameObject.GetComponent<UserAvatarMatcher4B>();
        //-------------------------------------------------------------------------

        //scene1
        if (EnableDisableSceneOverallScript.scene1AOn == true)
        {
            script1A.enabled = true;
        }

        else if (EnableDisableSceneOverallScript.scene1BOn == true)
        {
            //disable script
            script1A.enabled = false;

            //enble script
            script1B.enabled = true;
        }

        //-------------------------------------------------------------------------
        //scene2
        else if (EnableDisableSceneOverallScript.scene2On == true)
        {
            script2A.enabled = true;
        }

        //-------------------------------------------------------------------------
        //scene3
        else if (EnableDisableSceneOverallScript.scene3On == true)
        {
            script3A.enabled = true;
        }

        //-------------------------------------------------------------------------
        //scene4
        else if (EnableDisableSceneOverallScript.scene4AOn == true)
        {
            script4A.enabled = true;
        }

        else if (EnableDisableSceneOverallScript.scene4BOn == true)
        {
            //disable 1st script
            script4A.enabled = false;

            //enble script
            script4B.enabled = true;
        }
    }
}
