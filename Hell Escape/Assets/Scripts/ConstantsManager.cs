using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantsManager : MonoBehaviour {

    // Custom tags.
    const string OBSTACLE_TAG = "Obstacle";

    // Animation names.
    const string ANIMATION_IDLE = "Idle";
    const string ANIMATION_JUMP = "Jump";
    const string ANIMATION_FLY = "Fly";

    // Shader texture property name.
    const string SHADER_SKYDOME_OFFSET_TEXTURE_NAME = "_MainTex";

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
            case Animations.idle:
                return ANIMATION_IDLE;
            case Animations.jump:
                return ANIMATION_JUMP;
            case Animations.fly:
                return ANIMATION_FLY;
            default:
                return "";
        }
    }

    // Get shader texture offset name.
    public static string GetShaderTextureOffSetName()
    {
        return SHADER_SKYDOME_OFFSET_TEXTURE_NAME;
    }
}