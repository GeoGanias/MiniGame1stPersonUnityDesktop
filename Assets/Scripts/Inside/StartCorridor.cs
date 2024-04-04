using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCorridor : MonoBehaviour  
{
    public OutsideManager outsideManager;
    private bool entered = false;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name + " entered corridor");
        if(other.gameObject.name.Equals("PlayerCapsule") && !entered)
        {
            outsideManager.EnterCorridor();
            entered = true;
        } 
    }

}
