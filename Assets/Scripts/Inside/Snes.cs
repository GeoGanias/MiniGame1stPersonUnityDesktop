using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snes : MonoBehaviour
{
    // Start is called before the first frame update
    public PlayerPickDropItems playerPickDropItems;
    public GameObject cartridgePlacement;
    public GameObject cartridgeEndPlacement;
    public Transform lookAt;
    public FadeManager fadeManager;
    private Vector3 cartridgePosition;
    private Vector3 cartridgeEndPosition;
    private GameObject followingObject;
    private SceneManagerCustom sceneManagerCustom;
    private bool follow = false;
    private void Start()
    {
        cartridgePosition = cartridgePlacement.transform.position;
        cartridgeEndPosition = cartridgeEndPlacement.transform.position;
        sceneManagerCustom = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<SceneManagerCustom>();
    }
    private void Update()
    {
        if(follow)
        {
            followingObject.transform.position = Vector3.Lerp(followingObject.transform.position, cartridgeEndPosition, Time.deltaTime*3);
            if(Vector3.Distance(followingObject.transform.position, cartridgeEndPosition) < 0.005f)
            {
                follow = false;
                Debug.Log("Change Scene");
                StartCoroutine(fadeManager.FadeOut(
                    ()=>{
                        //sceneManagerCustom.LoadNextScene();
                        Debug.Log("Now change scene");
                    },
                    1,
                    true));
            }
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("Collision "+other.gameObject.name);
        if(other.gameObject.name == "cartridge.Mario")
        {
            other.gameObject.GetComponent<ObjectGrabbable>().UnGrab(); //this makes it non Kinematic
            other.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            Destroy(other.gameObject.GetComponent<BoxCollider>());
            followingObject = other.gameObject;
            StartCoroutine(LerpPosition(other.gameObject, cartridgePosition, 1));

        }
    }

    IEnumerator LerpPosition(GameObject objectLerped,Vector3 targetPosition, float duration)
    {
        float time = 0;
        Vector3 startPosition = objectLerped.transform.position;
        while (time < duration)
        {
            objectLerped.transform.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            objectLerped.transform.LookAt(lookAt);
            yield return null;
        }
        objectLerped.transform.position = targetPosition;
        if(targetPosition != cartridgeEndPosition)
        {
            StartCoroutine(LerpPosition(objectLerped, cartridgeEndPosition, 1));
        }
        else
        {
            Debug.Log("Change Scene");
            StartCoroutine(fadeManager.FadeOut(
                ()=>{
                    sceneManagerCustom.LoadNextScene();
                    Debug.Log("Now change scene");
                },
                1,
                true));

        }
        //follow = true;
        //animator.Play("InsertCartridge");

    }
}
