using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
using TMPro;

public class EndCorridor : MonoBehaviour
{
    public string lastMessage;
    [Range(0,1)] public float letterApperanceSpeed;
    public TextMeshProUGUI tmp;
    public GameObject lastMessageGameobject;
    public FirstPersonController fpc;
    public GameObject player;
    public GameObject fadeCanvas;
    public Transform endPos;

    public Transform lookAtTransform;
    private SceneManagerCustom sceneManagerCustom;
    private bool entered=false;

    private void Start()
    {
        sceneManagerCustom = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<SceneManagerCustom>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name.Equals("PlayerCapsule") && !entered)
        {
            StartCoroutine(Ending());
            entered = true;
        } 
        
    }

    private IEnumerator Ending()
    {
    
        fpc.enabled = false;
        StartCoroutine(LerpPosition(player, endPos.position, 5, true ));
        yield return new WaitForSeconds(5);

        StartCoroutine(fadeCanvas.GetComponent<FadeManager>().FadeOut(()=>
        {
            StartCoroutine(ShowLastMessage(lastMessage));
        }, 
        0));
    }
    private IEnumerator ShowLastMessage(string message)
    {
        lastMessageGameobject.SetActive(true);
        tmp.text = "";
        for(int i=0;i<message.Length;i++)
        {
            tmp.text += message[i];
            yield return new WaitForSeconds(letterApperanceSpeed);
        }
        yield return new WaitForSeconds(4);
        sceneManagerCustom.ExitGame();
    }
    IEnumerator LerpPosition(GameObject objectLerped,Vector3 targetPosition, float duration, bool lookAt=false)
    {
        float time = 0;
        Vector3 startPosition = objectLerped.transform.localPosition;
        while (time < duration)
        {
            objectLerped.transform.localPosition = Vector3.Lerp(startPosition, targetPosition, time / duration);
            objectLerped.transform.LookAt(lookAtTransform); 
            time += Time.deltaTime;
            yield return null;
        }
        objectLerped.transform.localPosition = targetPosition;
    }
}
