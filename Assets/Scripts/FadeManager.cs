using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeManager : MonoBehaviour
{
    private Animator animator;
    private SceneManagerCustom sceneManagerCustom;

    public delegate void AfterFade();
    private void Start()
    {
        animator = GetComponent<Animator>();
        sceneManagerCustom = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<SceneManagerCustom>();
    }
    public IEnumerator FadeOut(AfterFade del, int fadeIn, bool white=false)
    {
        Debug.Log("Fade out now");
        if(white)
        {
            GetComponent<Image>().color = Color.white;
        }
        else
        {
            GetComponent<Image>().color = Color.black;
        }
        if(fadeIn==1)
        {
            animator.Play("FadeOut"); //this has a transition to fadeIn animation
        }
        else
        {
            animator.Play("FadeOutForever"); //this has a transition to fadeIn animation
        }
        yield return new WaitForSeconds(1f);
        del();
    }

    public void FadeOutImmediate()
    {
        animator.Play("FadeOutImmediate");
    }

    public void OnFadeOutImmediateEndEvent(int option)
    {
        sceneManagerCustom.LoadNextScene();
    }

}
