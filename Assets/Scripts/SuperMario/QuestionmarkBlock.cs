using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class QuestionmarkBlock : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField][Range(0,5)] private int coinsAmount;
    [SerializeField] private AudioClip coinClip;
    [SerializeField] private MarioManager marioManager;
    private AudioSource audioSource;
    private AudioClip bumpClip;
    private float oldGravity;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        bumpClip = audioSource.clip; //should be bump
        oldGravity = -15f;
    }
    void OnCollisionEnter(Collision other)
    {
        if(other.transform.name == "Head")
        {
            if(coinsAmount > 0)
            {
                audioSource.clip = coinClip;
                coinsAmount--;
                marioManager.CollectCoin();
            }
            else
            {
                audioSource.clip = bumpClip;
            }
            audioSource.Play();
            FirstPersonController fpc = other.transform.parent.GetComponent<FirstPersonController>();
            fpc.Gravity = -500f;
        }
    }
    void OnCollisionExit(Collision other)
    {
        
        if(other.transform.name == "Head")
        {
            Debug.Log("Exit");
            FirstPersonController fpc= other.transform.parent.GetComponent<FirstPersonController>();
            fpc.Gravity = oldGravity;
        }

    }
}
