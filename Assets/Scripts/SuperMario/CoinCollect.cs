using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollect : MonoBehaviour
{
    [SerializeField] private MarioManager marioManager;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("enter: "+other.gameObject.name);
        if(other.gameObject.name.Equals("Head") || other.gameObject.name.Equals("Body") || other.gameObject.name.Equals("Feet"))
        {
            marioManager.CollectCoin();
            StartCoroutine(DestroySlowly());
        }
    }

    IEnumerator DestroySlowly()
    {
        transform.GetComponent<BoxCollider>().enabled = false;
        transform.parent.GetChild(0).gameObject.SetActive(false);
        AudioSource audioSource = transform.parent.GetComponent<AudioSource>();
        audioSource.Play();
        yield return new WaitForSeconds(audioSource.clip.length);
        Destroy(transform.parent.gameObject);
    }
}
