using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class PanelManager : MonoBehaviour, IManager
{
    public Transform mainCamera;
    public FirstPersonController firstPersonController;
    public Transform cone;
    public GameObject lightSource;
    private Animator coneAnimator;
    public FadeManager fadeManager;
    private AudioSource audioSource;
    public AudioClip machineFailClip;

    public GameObject openTheLights;

    void Awake()
    {
        coneAnimator = cone.GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    //3
    public void AnimationEvent(int animationCase)
    {
        switch (animationCase)
        {
            case 0:
                if(lightSource.activeSelf)
                {
                    cone.position = new Vector3(mainCamera.position.x, mainCamera.position.y + 4.5f, mainCamera.position.z);
                    audioSource.Play();
                    Invoke("PlayConeDown", audioSource.clip.length);
                }
                break;
            default:
                break;
        }
        
    }

    //4
    public void PlayConeDown()
    {
        coneAnimator.Play("ConeDown");
        AudioSource coneAudioSource = cone.GetComponent<AudioSource>(); // machine ramp up
        coneAudioSource.Play();
        Invoke("PlayMachineFailure", coneAudioSource.clip.length);
    }

    //5
    public void PlayMachineFailure()
    {
        audioSource.clip = machineFailClip;
        audioSource.Play();
        Invoke("ChangeScene",0.5f);

    }

    //2
    public void ActivateAnimation(Animator animator, AnimationClip clip)
    {
        if(lightSource.activeSelf)
        {
            animator.Play(clip.name);
        }
    }

    //1
    public void ActivateImmidiate(GameObject gameObject)
    {
        string gameObjectName = gameObject.name;
        switch (gameObjectName)
        {
            case "Lever":
                if(lightSource.activeSelf)
                {
                    firstPersonController.MoveSpeed = 0;
                }
                else
                {
                    if(!openTheLights.activeSelf)
                    {
                        openTheLights.SetActive(true);
                        Invoke("OpenLightsHintClose",2f);
                    }
                }
                break;
            case "LightSwitch":
                Toggle(lightSource);
                break;
            default:
                break;
        }
        
    }
    public void OpenLightsHintClose()
    {
        openTheLights.SetActive(false);
    }

    //this is the same on both managers. Should change to a parent class that has this one, then the 2 managers can be a subclass
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

    //6
    public void ChangeScene()
    {
        Debug.Log("changescene");
        fadeManager.FadeOutImmediate(); // event to next scene?
    }
}
