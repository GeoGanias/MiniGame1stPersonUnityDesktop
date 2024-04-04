using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StreetLights : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private GameObject light1;
    [SerializeField] private GameObject light2;
    [SerializeField] private GameObject lightBulb1;
    [SerializeField] private GameObject lightBulb2;
    [SerializeField] private Material litLight;
    private bool entered = false;

    private void OnTriggerEnter(Collider other)
    {

        Debug.Log(other.gameObject.name + " entered corridor");
        if(other.gameObject.name.Equals("PlayerCapsule") && !entered)
        {
            light1.SetActive(true);
            light2.SetActive(true);
            lightBulb1.GetComponent<MeshRenderer>().material = litLight;
            lightBulb2.GetComponent<MeshRenderer>().material = litLight;
            audioSource.Play();
            entered = true;
        } 
        
    }

}
