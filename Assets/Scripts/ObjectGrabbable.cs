using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGrabbable : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private Transform _grabPoint;
    private Transform player;
    private bool isGrabbed;

    [SerializeField][Range(5,40)][Tooltip("Recommended 20")] private float followCenterSpeed=20f;
    [SerializeField][Range(0,1000)] private float throwPower=800f;

    private void Start() 
    {
        _rigidbody = GetComponent<Rigidbody>();
        isGrabbed = false;
        this.gameObject.layer = 3; //Object
        _rigidbody.interpolation = RigidbodyInterpolation.Interpolate;
    }
    private void Update()
    {
        if(isGrabbed)
        {
            transform.position = Vector3.Lerp(transform.position, _grabPoint.position, Time.deltaTime * followCenterSpeed);
            transform.LookAt(player);
        }
    }
    public void Grab(Transform grabPoint)
    {
        _grabPoint = grabPoint;
        player = _grabPoint.parent;
        isGrabbed = true;
        _rigidbody.useGravity = false;
        _rigidbody.isKinematic = true;

        Indicator indicator = GetComponent<Indicator>();
        if(indicator!=null)
        {
            indicator.DestroyIndicator();
        }
    }

    public void Throw(Vector3 direction)
    {
        _rigidbody.isKinematic = false;
        _rigidbody.useGravity = true;
        isGrabbed = false;
        _rigidbody.velocity = new Vector3(0,0,0);
        _rigidbody.AddForce(direction * throwPower);
    }

    internal void UnGrab()
    {
        _rigidbody.isKinematic = false;
        _rigidbody.useGravity = true;
        isGrabbed = false;
    }
}
