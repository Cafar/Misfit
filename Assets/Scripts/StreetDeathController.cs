using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StreetDeathController : MonoBehaviour {

    public CanvasGroup canvas;
    public AudioSource audio;
    public AudioClip clip;



    private static LevelBehaviour _levelBehaviour;
    private static LevelBehaviour levelBehaviour
    {
        get
        {
            if (_levelBehaviour == null)
            {
                _levelBehaviour = FindObjectOfType<LevelBehaviour>();
            }

            return _levelBehaviour;
        }
    }


	// Use this for initialization
	public void Init () 
    {
        canvas.alpha = 1;
        
        audio.loop = false;
        audio.clip = clip;
        audio.Play();
        GameObject.FindGameObjectWithTag("Player").SetActive(false);

        Invoke("change",4);
	}

    void change()
    {
        levelBehaviour.LoadScene(1);
    }
	
	
}
