using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfBounds : MonoBehaviour
{
    [SerializeField] private MarioManager marioManager;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name + " fell");
        if(other.gameObject.name.Equals("Head"))
        {
            StartCoroutine(marioManager.Die());
        }
        
    }
}
