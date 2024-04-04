using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IManager
{
    void ActivateAnimation(Animator anim, AnimationClip clip);
    void ActivateImmidiate(GameObject gameObjectName);
    void AnimationEvent(int animationCase);
}
