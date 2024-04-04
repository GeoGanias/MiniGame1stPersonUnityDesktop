using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAround : MonoBehaviour
{
    [SerializeField]
    private float rotationSpeed;
    private bool isEnabled = true;
    void FixedUpdate()
    {
        if(isEnabled)
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + rotationSpeed, transform.eulerAngles.z);
        
    }

    public void EnableRotation()
    {
        isEnabled = true;
    }

    public void DisableRotation()
    {
        isEnabled = false;
    }

    public void ToggleRotation()
    {
        isEnabled = !isEnabled;
    }
}
