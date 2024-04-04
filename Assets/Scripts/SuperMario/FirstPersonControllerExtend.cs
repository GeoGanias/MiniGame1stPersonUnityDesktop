using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class FirstPersonControllerExtend : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip jump;
    public void OnJumpAudio()
    {
        audioSource.clip = jump;
        audioSource.Play();
    }

}
