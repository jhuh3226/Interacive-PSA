//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class ScriptEnableDisable : MonoBehaviour
//{

//    public GameObject gameObjectContainingScriptToEnble; //drag the gameobject which have that script you want to disable, in the inspector.
//    public GameObject gameObContainingScript; //

//    public bool script1Disabled = false;

//    void Update()
//    {
//        EnableDisableSceneOverall EnableDisableSceneOverallScript = gameObContainingScript.GetComponent<EnableDisableSceneOverall>();

//        //scene1
//        if (EnableDisableSceneOverallScript.scene1On == true)
//        {
//            if(Input.GetKeyDown(KeyCode.Alpha1))
//            {
//                UserAvatarMatcher1A script1A;
//                script1A = gameObject.GetComponent<UserAvatarMatcher1A>();
//                script1A.enabled = true;
//            }

//            else if (Input.GetKeyDown(KeyCode.Alpha2))
//            {
//                //disable script
//                UserAvatarMatcher1A script1A;
//                script1A = gameObject.GetComponent<UserAvatarMatcher1A>();
//                script1A.enabled = false;

//                //enble script
//                UserAvatarMatcher1B script1B;
//                script1B = gameObject.GetComponent<UserAvatarMatcher1B>();
//                script1B.enabled = true;
//            }
//        }

//        //-------------------------------------------------------------------------
//        //scene2
//        if (EnableDisableSceneOverallScript.scene2On == true)
//        {
//            if (Input.GetKeyDown(KeyCode.Alpha1))
//            {
//                UserAvatarMatcher2A script2A;
//                script2A = gameObject.GetComponent<UserAvatarMatcher2A>();
//                script2A.enabled = true;
//            }

//            else if (Input.GetKeyDown(KeyCode.Alpha3))
//            {
//                //disable 1st script
//                UserAvatarMatcher2A script2A;
//                script2A = gameObject.GetComponent<UserAvatarMatcher2A>();
//                script2A.enabled = false;

//                //enble script
//                UserAvatarMatcher2B script2B;
//                script2B = gameObject.GetComponent<UserAvatarMatcher2B>();
//                script2B.enabled = true;
//            }
//        }

//        //-------------------------------------------------------------------------
//        //scene3
//        if (EnableDisableSceneOverallScript.scene3On == true)
//        {
//            if (Input.GetKeyDown(KeyCode.Alpha1))
//            {
//                UserAvatarMatcher3A script3A;
//                script3A = gameObject.GetComponent<UserAvatarMatcher3A>();
//                script3A.enabled = true;
//            }

//            else if (Input.GetKeyDown(KeyCode.Alpha3))
//            {
//                //disable 1st script
//                UserAvatarMatcher3A script3A;
//                script3A = gameObject.GetComponent<UserAvatarMatcher3A>();
//                script3A.enabled = false;

//                //enble script
//                UserAvatarMatcher3B script3B;
//                script3B = gameObject.GetComponent<UserAvatarMatcher3B>();
//                script3B.enabled = true;
//            }
//        }

//        //-------------------------------------------------------------------------
//        //scene4
//        if (EnableDisableSceneOverallScript.scene4On == true)
//        {
//            if (Input.GetKeyDown(KeyCode.Alpha1))
//            {
//                UserAvatarMatcher4A script4A;
//                script4A = gameObject.GetComponent<UserAvatarMatcher4A>();
//                script4A.enabled = true;
//            }

//            else if (Input.GetKeyDown(KeyCode.Alpha3))
//            {
//                //disable 1st script
//                UserAvatarMatcher4A script4A;
//                script4A = gameObject.GetComponent<UserAvatarMatcher4A>();
//                script4A.enabled = false;

//                //enble script
//                UserAvatarMatcher4B script4B;
//                script4B = gameObject.GetComponent<UserAvatarMatcher4B>();
//                script4B.enabled = true;
//            }
//        }
//    }

//}
