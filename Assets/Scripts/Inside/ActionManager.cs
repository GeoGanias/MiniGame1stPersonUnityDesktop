using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using StarterAssets;
using TMPro;


public class ActionManager : MonoBehaviour, IManager
{
    // Start is called before the first frame update
    public GameObject lightSource;
    public GameObject marioCartridge;
    public GameObject snes;
    public GameObject player;
    public GameObject mainCamera;
    public Transform cameraRoot;
    public Transform TV;
    public Material blackScreen;
    public BoxCollider sofaCollider;
    public BoxCollider pillowCollider;
    public AudioSource audioSourceTV;
    public VideoPlayer videoPlayer;
    public VideoClip clip1;
    public VideoClip clip2;
    public GameObject openTheLights;
    public TextMeshProUGUI subtitles;
    public Animator doorAnimator;
    public AudioSource doorAudioSource;
    public GameObject pressFtoStandUp;
    //[SerializeField][Range(0,50)] private float sitLerpSpeed=1f;
    private FirstPersonController firstPersonController;
    private Transform _sitPoint;
    private float initialJumpHeight;
    private float initialMoveSpeed;
    private float initialRotationSpeed;
    private Transform initialPlayerTransform;
    private SceneManagerCustom sceneManagerCustom;
    private bool sitDown = false;
    private bool standUp = false;
    private bool finished = false;
    private bool sitDownFinished = false;

    void Start()
    {
        firstPersonController = player.GetComponent<FirstPersonController>();
        initialJumpHeight = firstPersonController.JumpHeight;
        initialMoveSpeed = firstPersonController.MoveSpeed;
        initialRotationSpeed = firstPersonController.RotationSpeed;
        sceneManagerCustom = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<SceneManagerCustom>();
    }

    // Update is called once per frame
    void Update()
    {
        if(sitDown)
        {
            //player.transform.position = Vector3.Lerp(player.transform.position, new Vector3(_sitPoint.position.x, player.transform.position.y, _sitPoint.position.z), Time.deltaTime * sitLerpSpeed);
            //cameraRoot.transform.localPosition = Vector3.Lerp(cameraRoot.transform.localPosition, new Vector3(0, 1f , 0), Time.deltaTime * sitLerpSpeed);
            //player.transform.LookAt(TV);
            if(finished && Input.GetKeyDown(KeyCode.F))
            {
                sitDown = false;
                pressFtoStandUp.SetActive(false);
                StandUp();
            }
        }
        if(standUp)
        {
            //player.transform.position = Vector3.Lerp(player.transform.position, initialPlayerTransform.position, Time.deltaTime * sitLerpSpeed);
        }
    }
    public void AnimationEvent(int animationCase)
    {

        
    }

    public void ActivateAnimation(Animator animator, AnimationClip clip)
    {
        animator.Play(clip.name);
    }

    public void ActivateImmidiate(GameObject gameObject)
    {
        string gameObjectName = gameObject.name;
        switch (gameObjectName)
        {
            case "LightSwitch":
                Toggle(lightSource);
                break;
            case "pillowSitDown":

                if(lightSource.activeSelf)
                {
                    SitDown(gameObject);
                }
                else if(!openTheLights.activeSelf)
                {
                    openTheLights.SetActive(true);
                    Invoke("OpenLightsHintClose",2f);
                }
                break;
            default:
                break;
        }
        
    }
    public void OpenLightsHintClose()
    {
        openTheLights.SetActive(false);
    }

    private void SitDown(GameObject gameObject)
    {
        firstPersonController.MoveSpeed = 0;
        //firstPersonController.RotationSpeed = 0;
        firstPersonController.JumpHeight = 0;
        initialPlayerTransform = player.transform;

        _sitPoint = gameObject.transform;
        sitDown = true;
        sofaCollider.isTrigger = true;
        pillowCollider.isTrigger = true;

        StartCoroutine(LerpPosition(player,new Vector3(_sitPoint.position.x, player.transform.position.y, _sitPoint.position.z), 2));
        StartCoroutine(LerpPosition(cameraRoot.gameObject,new Vector3(0,1,0), 2));

        if(!sitDownFinished)
        {
            sitDownFinished = true;
            gameObject.GetComponent<Indicator>().DestroyIndicator();
            StartCoroutine(PlayVHS());
        }

        //disable player movements

        

        //lerp him through sitting down.
    }

    private IEnumerator PlayVHS()
    {
        yield return new WaitForSeconds(2);
        videoPlayer.Stop();
        audioSourceTV.Play();
        yield return new WaitForSeconds(audioSourceTV.clip.length);
        if(sceneManagerCustom.phase == 1)
        { 
            videoPlayer.clip = clip1;
            videoPlayer.SetDirectAudioVolume(0,1);
            videoPlayer.isLooping = false;
            videoPlayer.Play();
            StartCoroutine(PlaySubtitles(1));
        }
        else if(sceneManagerCustom.phase == 3)
        {
            videoPlayer.clip = clip2;
            videoPlayer.SetDirectAudioVolume(0,1);
            videoPlayer.isLooping = false;
            videoPlayer.Play();
            StartCoroutine(PlaySubtitles(2));
        }
        yield return new WaitForSeconds((float) videoPlayer.clip.length);
        ClipFinished();
    }

    private IEnumerator PlaySubtitles(int clip)
    {
        if(clip == 1)
        {
            subtitles.text = "Hi! If you're watching this tape, it means that my brain loading device malfunctioned";
            yield return new WaitForSeconds(5);
            subtitles.text = "That means that you are trapped in my brain right now...";
            yield return new WaitForSeconds(3.5f);
            subtitles.text = "In order for you to fix it and escape, you will have to replay a Super Mario Bros. stage";
            yield return new WaitForSeconds(4.5f);
            subtitles.text = "that I used to play when I was a kid. But you will have to do this in 1st Person.";
            yield return new WaitForSeconds(5);
            subtitles.text = "To do that you need to insert the Super Mario cartridge in the Console. Good Luck!";
            yield return new WaitForSeconds(5);
            subtitles.text = "";
        }
        else if(clip == 2)
        {
            subtitles.text = "Well... You did it! Good Job. You can now get out of my brain, whenever you want";
            yield return new WaitForSeconds(4.5f);
            subtitles.text = "I'll open the door for you. Bye.";
            yield return new WaitForSeconds(2);
            subtitles.text = "";

        }

    }

    private void StandUp()
    {
        //standUp = true;
        //RevertPlayerProperties();
        StartCoroutine(LerpPosition(player, new Vector3(initialPlayerTransform.position.x, player.transform.position.y, initialPlayerTransform.position.z), 2, true));
        StartCoroutine(LerpPosition(cameraRoot.gameObject, new Vector3(0, 1.375f, 0), 1));

    }

    private void ClipFinished()
    {
        finished = true;
        pressFtoStandUp.SetActive(true);
        if(sceneManagerCustom.phase == 1)
        {
            marioCartridge.GetComponent<ObjectGrabbable>().enabled = true;
            marioCartridge.GetComponent<Indicator>().CreateIndicator();
            snes.GetComponent<Indicator>().CreateIndicator();
        }
        else if(sceneManagerCustom.phase == 3)
        {
            doorAudioSource.Play();
            doorAnimator.Play("DoorOpen");
            //Open the door"
        }
        TV.GetComponent<MeshRenderer>().material = blackScreen;
    }

    private void RevertPlayerProperties()
    {
        firstPersonController.MoveSpeed = initialMoveSpeed;
        firstPersonController.RotationSpeed = initialRotationSpeed;
        firstPersonController.JumpHeight = initialJumpHeight;
        player.transform.rotation = new Quaternion(0, -5, 0, 1);
    }

    private void Toggle(GameObject lightSource)
    {
        if(lightSource.activeSelf)
        {
            lightSource.SetActive(false);
        }
        else
        {
            lightSource.SetActive(true);
        }
        
    }

    IEnumerator LerpPosition(GameObject objectLerped,Vector3 targetPosition, float duration, bool revert=false)
    {
        float time = 0;
        Vector3 startPosition = objectLerped.transform.localPosition;
        while (time < duration)
        {
            objectLerped.transform.localPosition = Vector3.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        objectLerped.transform.localPosition = targetPosition;
        if(revert)
        {
            RevertPlayerProperties();
        }
    }

    IEnumerator LerpRotation(GameObject objectLerped,Transform lookAtTarget, float duration)
    {
        float time = 0;
        Quaternion startPosition = objectLerped.transform.rotation;
        Vector3 relativePos = lookAtTarget.position - objectLerped.transform.position;
        Quaternion toRotation = Quaternion.LookRotation(relativePos);
        while (time < duration)
        {
            objectLerped.transform.rotation = Quaternion.Lerp(startPosition, toRotation, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        objectLerped.transform.rotation = toRotation;
    }
}
