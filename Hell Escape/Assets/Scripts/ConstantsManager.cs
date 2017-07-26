﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantsManager : MonoBehaviour {

    // Custom tags.
    const string OBSTACLE_TAG = "Obstacle";

    // Animation names.
    const string ANIMATION_JUMP = "Jump";

    // ************************** PUBLIC ************************** //

    // Get tag for specific gameobject.
    public static string GetTag(ObjectTags gameObject)
    {
        switch (gameObject)
        {
            case ObjectTags.obstacle:
                return OBSTACLE_TAG;
            default:
                return "";
        }
    }

    // Get animation name.
    public static string GetAnimationName(Animations animation)
    {
        switch (animation)
        {
            case Animations.jump:
                return ANIMATION_JUMP;
            default:
                return "";
        }
    }
}