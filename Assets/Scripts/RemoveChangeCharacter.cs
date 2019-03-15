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

        //on clicking R for removed, the current character dissapears
        if (userAvatarMatcherScript.firstCharacterTracked == true)
        {
            Destroy(GameObject.FindWithTag("InjuredBoy1"));
            userAvatarMatcherScript.firstCharacterTracked = false;
        }

    }
}
