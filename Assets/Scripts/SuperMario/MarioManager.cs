using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
using TMPro;

public class MarioManager : MonoBehaviour, IManager
{
    public bool isEnterance;
    public GameObject fadeCanvas;
    public GameObject player;
    public GameObject door;
    public Transform endPos;
    [SerializeField] private AudioClip deathClip;
    [SerializeField] private AudioClip downThePoleClip;
    [SerializeField] private AudioClip winClip;
    private SceneManagerCustom sceneManagerCustom;
    [SerializeField] private TextMeshProUGUI tmpCoin;
    private int coinsCollected = 0;
    private FirstPersonController fpc;
    private bool alreadyDead = false;
    private AudioSource audioSource;

    private void Start()
    {
        fpc = player.GetComponent<FirstPersonController>();
        audioSource = GetComponent<AudioSource>();
        sceneManagerCustom = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<SceneManagerCustom>();
    }
    public void ActivateAnimation(Animator anim, AnimationClip clip)
    {
    }

    public void ActivateImmidiate(GameObject gameObject)
    {
        string gameObjectName = gameObject.name;
        switch (gameObjectName)
        {
            case "PipeInteract":
                GoDownPipe(gameObject.transform.parent.GetChild(1));
                break;
            default:
                break;
        }
    }


    public void AnimationEvent(int animationCase)
    {
    }

    private void GoDownPipe(Transform exit)
    {
        fpc.enabled = false;
        StartCoroutine(fadeCanvas.GetComponent<FadeManager>().FadeOut(()=>
        {
            player.transform.transform.position = exit.position;
            exit.GetComponent<AudioSource>().Play();
            fadeCanvas.GetComponent<FadeManager>();
            StartCoroutine(RevertFPC(exit));
        },
        1,
        true));
    }

    private IEnumerator RevertFPC(Transform exit)
    {
        yield return new WaitForSeconds(0.2f); // needs to wait for position to take place.. and give some time so we make sure its updated. Then enable fpc again.
        fpc.enabled = true;
    }
    public IEnumerator Die()
    {
        fpc.MoveSpeed = 0;
        if(audioSource.isPlaying)
        {
            audioSource.Stop(); //stop main theme
        }
        audioSource.clip = deathClip;
        audioSource.volume = 1;
        audioSource.Play();
        yield return new WaitForSeconds(audioSource.clip.length);
        sceneManagerCustom.ReloadScene();
    }

    public void CollectCoin()
    {
        coinsCollected++;
        string newText = "x" + coinsCollected;
        tmpCoin.SetText(newText);
    }

    public IEnumerator Win()
    {
        fpc.enabled = false;
        if(audioSource.isPlaying)
        {
            audioSource.Stop(); //stop main theme
        }
        audioSource.volume = 1;
        audioSource.loop = false;
        audioSource.clip = downThePoleClip;
        audioSource.Play();
        StartCoroutine(LerpPosition(player, new Vector3(player.transform.position.x,0.2f,player.transform.position.z), audioSource.clip.length, true ));
        yield return new WaitForSeconds(audioSource.clip.length);

        if(audioSource.isPlaying)
        {
            audioSource.Stop(); //stop main theme
        }
        audioSource.volume = 1;
        audioSource.clip = winClip;
        audioSource.Play();
        StartCoroutine(LerpPosition(player, endPos.position, audioSource.clip.length, true ));
        yield return new WaitForSeconds(audioSource.clip.length + 0.2f);

        StartCoroutine(fadeCanvas.GetComponent<FadeManager>().FadeOut(()=>
        {
            sceneManagerCustom.LoadPrevScene();
        }, 
        1,
        true));

    }
    IEnumerator LerpPosition(GameObject objectLerped,Vector3 targetPosition, float duration, bool lookAt=false)
    {
        float time = 0;
        Vector3 startPosition = objectLerped.transform.localPosition;
        while (time < duration)
        {
            objectLerped.transform.localPosition = Vector3.Lerp(startPosition, targetPosition, time / duration);
            objectLerped.transform.LookAt(door.transform); 
            time += Time.deltaTime;
            yield return null;
        }
        objectLerped.transform.localPosition = targetPosition;
    }

}
