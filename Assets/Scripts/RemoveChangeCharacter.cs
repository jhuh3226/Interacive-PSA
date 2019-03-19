using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveChangeCharacter : MonoBehaviour
{
    public GameObject gameObContainingScript;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        UserAvatarMatcher userAvatarMatcherScript = gameObContainingScript.GetComponent<UserAvatarMatcher>();
        //on clicking R for removed, certian chracter dissapears
        if (userAvatarMatcherScript.firstCharacterTracked == true)
        {
            Destroy(GameObject.FindWithTag("InjuredBoy1"));
            userAvatarMatcherScript.firstCharacterTracked = false;
        }

        ScriptEnableDisable ScriptEnableDisableScript = gameObContainingScript.GetComponent<ScriptEnableDisable>();
        if(ScriptEnableDisableScript.script1Disabled == true)
        {
            Destroy(GameObject.FindWithTag("SadBoy3"));
            ScriptEnableDisableScript.script1Disabled = false;
        }
    }
}
