using UnityEngine;
using System;
using System.Collections;

/// <summary>  </summary>
public class HumanoidCtrl : MonoBehaviour
{

    /// <summary>  </summary>
    private Animator anim;

    /// <summary>  </summary>
    private HumanPose _pose;

    /// <summary>  </summary>
    public HumanPose pose { get { return _pose; } }

    /// <summary>  </summary>
    private HumanPoseHandler hph;

    public enum MuscleKey
    {
        SpineFrontBack = 0,
        SpineLeftRight = 1,
        SpineTwist = 2,
        ChestFrontBack = 3,
        ChestLeftRight = 4,
        ChestTwist = 5,
        NeckDownUp = 6,
        NeckLeftRight = 7,
        NeckTurn = 8,
        HeadDownUp = 9,
        HeadTilt = 10,
        HeadTurn = 11,




        JawClose = 16,
        JawLeftRight = 17,
        UpperLeg_FrontBack_R = 18,
        UpperLeg_Twist_R = 19,
        LowerLeg_Twist_R = 20,
        LowerLeg_FrontBack_R = 21,
        Foot_Twist_R = 22,
        Foot_UpDown_R = 23,
        Foot_LeftRight_R = 24,
        Toes_UpDown_R = 25,
        UpperLeg_FrontBack_L = 26,
        UpperLeg_Twist_L = 27,
        LowerLeg_Twist_L = 28,
        LowerLeg_FrontBack_L = 29,
        Foot_Twist_L = 30,
        Foot_UpDown_L = 31,
        Foot_LeftRight_L = 32,
        Toes_UpDown_L = 33,
        ShoulderDownUp_R = 34,
        ShoulderFrontBack_R = 35,
        ArmDownUp_R = 36,
        ArmFrontBack_R = 37,
        ArmTwist_R = 38,
        ForearmStretch_R = 39,
        ForearmTwist_R = 40,
        HandDownUp_R = 41,
        HandInOut_R = 42,
        ShoulderDownUp_L = 43,
        ShoulderFrontBack_L = 44,
        ArmDownUp_L = 45,
        ArmFrontBack_L = 46,
        ArmTwist_L = 47,
        ForearmStretch_L = 48,
        ForearmTwist_L = 49,
        HandDownUp_L = 50,
        HandInOut_L = 51,

        Thumb1Stretched_R,
        ThumbSpread_R,
        Thumb2Stretched_R,
        Thumb3Stretched_R,
        Thumb1Stretched_L,
        ThumbSpread_L,
        Thumb2Stretched_L,
        Thumb3Stretched_L,

        Index1Stretched_R,
        IndexSpread_R,
        Index2Stretched_R,
        Index3Stretched_R,
        Index1Stretched_L,
        IndexSpread_L,
        Index2Stretched_L,
        Index3Stretched_L,

        Middle1Stretched_R,
        MiddleSpread_R,
        Middle2Stretched_R,
        Middle3Stretched_R,
        Middle1Stretched_L,
        MiddleSpread_L,
        Middle2Stretched_L,
        Middle3Stretched_L,

        Ring1Stretched_R,
        RingSpread_R,
        Ring2Stretched_R,
        Ring3Stretched_R,
        Ring1Stretched_L,
        RingSpread_L,
        Ring2Stretched_L,
        Ring3Stretched_L,

        Little1Stretched_R,
        LittleSpread_R,
        Little2Stretched_R,
        Little3Stretched_R,
        Little1Stretched_L,
        LittleSpread_L,
        Little2Stretched_L,
        Little3Stretched_L,
    }

    static public int MuscleMax { get { return Enum.GetNames(typeof(MuscleKey)).Length; } }

    // Use this for initialization
    public void Awake()
    {
        anim = GetComponent<Animator>();

        hph = new HumanPoseHandler(anim.avatar, transform);

        _pose = new HumanPose();

        hph.GetHumanPose(ref _pose);
        for (int i = 0; i < pose.muscles.Length; i++)
        {
            //pose.muscles[i] = 0;
        }
    }


    // Update is called once per frame
    public void Update()
    {
        hph.SetHumanPose(ref _pose);
    }
}
