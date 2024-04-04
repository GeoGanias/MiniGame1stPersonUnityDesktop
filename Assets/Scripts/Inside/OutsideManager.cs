using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class OutsideManager : MonoBehaviour
{
    public FirstPersonController fpc;
    public Animator doorAnimator;
    public AudioSource doorAudioSource;
    public AudioClip doorClose;
    private AudioSource audioSource;


    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void EnterCorridor()
    {
        fpc.JumpHeight = 0; // disable jump, cant fall off.
        fpc.MoveSpeed = 2; // make it a bit slower
        fpc.SprintSpeed = 2; // cant sprint
        audioSource.time = 22;
        audioSource.volume = 0;
        audioSource.Play();
        doorAnimator.Play("DoorClose");
        doorAudioSource.clip = doorClose;
        doorAudioSource.Play();

        StartCoroutine(LerpVolume(1, 4));
    }
    IEnumerator LerpVolume(float endValue, float duration)
    {
        float time = 0;
        float startValue = audioSource.volume;
        while (time < duration)
        {
            audioSource.volume = Mathf.Lerp(startValue, endValue, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        audioSource.volume = endValue;
    }
}
