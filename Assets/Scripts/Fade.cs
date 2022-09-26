using UnityEngine;
using System.Collections;

public class Fade : MonoBehaviour {

    public delegate void FadeFinish();
    public static event FadeFinish OnFinishFadeIn;
    public static event FadeFinish OnFinishFadeOut;


	public void FadeMeOut(){
		StartCoroutine ("DoFadeOut");
	}

    public void FadeMeIn(){
        StartCoroutine ("DoFadeIn");
    }


	IEnumerator DoFadeOut (){
		CanvasGroup canvasGroup = GetComponent<CanvasGroup>();
		while (canvasGroup.alpha>0){
			canvasGroup.alpha -= Time.deltaTime * 0.5f;
			yield return null;
		}
		canvasGroup.interactable = false;
        if(OnFinishFadeOut != null)
            OnFinishFadeOut();
		yield return null;
	}
    IEnumerator DoFadeIn (){
        CanvasGroup canvasGroup = GetComponent<CanvasGroup>();
        while (canvasGroup.alpha<1){
            canvasGroup.alpha += Time.deltaTime * 0.5f;
            yield return null;
        }
        if(OnFinishFadeIn != null)
            OnFinishFadeIn();
        canvasGroup.interactable = false;
        yield return null;
    }
}
