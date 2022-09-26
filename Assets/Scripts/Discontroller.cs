using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Discontroller : MonoBehaviour {

    public AudioSource audioS;
    public AudioClip audioPillado;
    public PlayerState myPlayer;
    public LevelBehaviour levelBehaviour;

    public List<float> hits;

    public int currentHit;

    public float timeFlash;

    public float time;

    public DynamicLight2D.DynamicLight myLight;
    public Material lightWhite;

    public CanvasGroup canvas;

    private bool lose, win;

	void Start () 
    {
        audioS.Play();
        currentHit = 0;
        Invoke("TurnOffLight",2.666f);
	}
	
	
	void Update () 
    {
        time = audioS.time;
        if(currentHit < hits.Count && !lose)
        {
            if(time >= hits[currentHit])
            {
                StartCoroutine(Flash());
                currentHit++;
                if(!myPlayer.ImARegularSquare)
                {
                    Pillado();
                }
            }
        }
        else if(currentHit == hits.Count && !win)
        {
            win = true;
            canvas.alpha = 0;
            myPlayer.jandepor();
            Invoke("WIN",2);
        }
        if(myPlayer.ImASquare())
        {
            Convertido();
        }
            
    }

    void WIN()
    {
        LevelsCompleted.instance.PlayerPrefsSetBool(levelBehaviour.GetCurrentSceneName());
        levelBehaviour.NextScene();
    }

    IEnumerator Flash()
    {
        canvas.alpha = 0;
        while (canvas.alpha < 1) {
            canvas.alpha += Time.deltaTime / timeFlash;
            yield return null;

            if(lose || win)
                break;
        }
    }

    void TurnOffLight()
    {
        canvas.alpha = 1;
    }

    void Convertido()
    {
        lose = true;
        canvas.alpha = 0;
    }

    void Pillado()
    {
        lose = true;
        canvas.alpha = 0;
        audioS.clip = audioPillado;
        audioS.Play();
        myLight.LightMaterial = lightWhite;
        myPlayer.jandepor();

        NPCDisco[] npcs = FindObjectsOfType<NPCDisco>();

        foreach(NPCDisco npc in npcs)
        {
            npc.GetComponent<Animator>().enabled = false;
            npc.enabled = true;
        }

        Invoke("ChangeScene",2);
    }

    void ChangeScene()
    {
        levelBehaviour.ReloadCurrentScene();
    }
}
