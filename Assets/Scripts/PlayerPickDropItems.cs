using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickDropItems : MonoBehaviour
{
    [SerializeField] private Transform playerCameraTransform;
    [SerializeField] private GameObject pressToInteract;
    [SerializeField] private GameObject actionHints;
    [SerializeField] private float pickUpDistance;
    [SerializeField] private LayerMask pickUpLayerMask;
    [SerializeField] private GameObject crosshairDefault;
    [SerializeField] private GameObject crosshairHover;
    [SerializeField] private Transform grabPoint;
    private ObjectGrabbable heldObject;
    private bool showHints = true;

    void Start()
    {
        heldObject = null;
    }
    // Update is called once per frame
    void Update()
    {
        if(heldObject == null)
        {
            if(Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out RaycastHit raycastHit, pickUpDistance, pickUpLayerMask))
            {

                //Show press "E" to interact
                crosshairDefault.SetActive(false);
                crosshairHover.SetActive(true);
                pressToInteract.SetActive(true);

                if(Input.GetKeyDown(KeyCode.E))
                {
                    Debug.Log(raycastHit.transform);
                    if(raycastHit.transform.TryGetComponent<ObjectGrabbable>(out ObjectGrabbable objectGrabbable))
                    {
                        heldObject = objectGrabbable;
                        heldObject.Grab(grabPoint);
                        pressToInteract.SetActive(false);
                        if(showHints)
                        {
                            actionHints.SetActive(true);
                        }
                    }
                    else if(raycastHit.transform.TryGetComponent<ObjectInteractable>(out ObjectInteractable objectInteractable)) // No ObjectGrabbable Component -> maybe thats not enough. its ok for now
                    {
                        objectInteractable.Activate();
                    }
                }
            }
            else
            {
                crosshairDefault.SetActive(true);
                crosshairHover.SetActive(false);
                pressToInteract.SetActive(false);
            }

        }
        else
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                heldObject.UnGrab();
                heldObject = null;
                actionHints.SetActive(false);
            }
            else if(Input.GetMouseButtonDown(1))
            {
                heldObject.Throw(playerCameraTransform.forward);
                heldObject = null;
                actionHints.SetActive(false);
            }
            else if(Input.GetKeyDown(KeyCode.Z))
            {
                if(actionHints.activeSelf)
                {
                    actionHints.SetActive(false);
                    showHints = false;
                }
                else
                {
                    actionHints.SetActive(true);
                    showHints = true;
                }
            }

        }
    }
}
