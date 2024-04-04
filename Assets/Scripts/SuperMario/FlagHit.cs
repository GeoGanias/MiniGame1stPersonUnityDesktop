using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagHit : MonoBehaviour
{
    public MarioManager marioManager;
    private bool end = false;
    private void OnCollisionEnter(Collision other)
    {
        if(!end && (other.gameObject.name.Equals("Head") ||other.gameObject.name.Equals("Body") || other.gameObject.name.Equals("Feet")))
        {
            Debug.Log("WIN");
            end = true;
            StartCoroutine(marioManager.Win());
        }
    }
}
