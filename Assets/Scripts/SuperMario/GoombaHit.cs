using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class GoombaHit : MonoBehaviour
{
    [SerializeField] private FirstPersonController fpc;
    [SerializeField] private GameObject goombaBody;
    [SerializeField] private GameObject goombaHead;
    //[SerializeField] private GameObject goombaRoot;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip bounceClip;
    [SerializeField] private MarioManager marioManager;

    private void OnCollisionEnter(Collision other) 
    {
        Debug.Log("Goomba hit: "+other.gameObject.name+" - layer: "+other.gameObject.layer);
        if(other.gameObject.name.Equals("Body"))
        {
            StartCoroutine(marioManager.Die());
        }
        else if(other.gameObject.name.Equals("Feet"))
        {
            StartCoroutine(Kill());
        }

    }

    IEnumerator Kill()
    {
        Debug.Log("Kill");
        fpc.Bounce();
        audioSource.clip = bounceClip;
        audioSource.Play();
        MakeInvincible();
        yield return new WaitForSeconds(audioSource.clip.length);
        Destroy(this.transform.parent.parent.parent.gameObject);
    }

    private void MakeInvincible()
    {
        GetComponent<BoxCollider>().enabled = false;
        goombaBody.transform.GetChild(0).gameObject.SetActive(false);
        goombaBody.transform.GetChild(1).gameObject.SetActive(false);
        goombaBody.GetComponent<MeshRenderer>().enabled = false;
        goombaHead.GetComponent<MeshRenderer>().enabled = false;
        transform.parent.GetChild(0).GetComponent<BoxCollider>().enabled = false;
    }
}
