using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInteractable : MonoBehaviour
{
    private IManager manager;
    private AudioSource audioSource;
    [SerializeField] private GameObject managerComponent;
    [SerializeField] private bool isAnimated;
    [Tooltip("Leave Empty if isAnimated is false")][SerializeField] private AnimationClip clip;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        this.gameObject.layer = 3;
        if(managerComponent != null)
        {
            manager = managerComponent.GetComponent<IManager>();
        }
    }

    public void Activate()
    {
        if(audioSource!=null && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
        manager.ActivateImmidiate(this.gameObject); //do sth immidiatelly;
        if(isAnimated)
        {
            Animator anim = GetComponent<Animator>();
            manager.ActivateAnimation(anim, clip);
        }
    }

    public void AnimationEvent(int animationCase)
    {
        manager.AnimationEvent(animationCase);
    }
}
