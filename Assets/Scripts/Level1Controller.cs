using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1Controller : MonoBehaviour {

    public List<GoToWayPoint> npcs;
    public GoToWayPoint profe;
    public PlayerState myPlayer;

    public AudioSource audiosource;
    public AudioClip clip;

    public Animator npc1Transform, npc2Transform, playerFalse;

    float time;

    void Start()
    {
        Fade.OnFinishFadeOut += Fade_OnFinishFadeOut;
        Invoke("Init",3.2f);
        Invoke("Npc1Transform", 7.619f);
        Invoke("Npc2Transform", 15.23f);
        Invoke("CharTransform", 22.85f);
        Invoke("StartToMove", 32.5f);


    }

    void Fade_OnFinishFadeOut ()
    {
        //Init();
    }

	void Init () 
    {
        profe.Init();


       

        myPlayer.CanMove = false;

        Invoke("CanIMove", 43);

	}
	
    void Update()
    {
        time += Time.deltaTime;
        if(!audiosource.isPlaying && time >= 6)
        {
            Debug.Log("OLA");
            audiosource.clip = clip;
            audiosource.loop = true;
            audiosource.Play();
        }
    }

    void Npc1Transform()
    {
        npc1Transform.SetTrigger("ToSquare");
    }

    void Npc2Transform()
    {
        npc2Transform.SetTrigger("ToSquare");
    }

    void CharTransform()
    {
        playerFalse.GetComponent<Animator>().SetTrigger("Almost");
    }

    void CanIMove()
    {
        Destroy(playerFalse.gameObject);
        myPlayer.gameObject.SetActive(true);
        myPlayer.CanMove = true;
    }

    void StartToMove()
    {
        npc1Transform.GetComponent<GoToWayPoint>().Init();
        npc2Transform.GetComponent<GoToWayPoint>().Init();
        StartCoroutine(MoveMoveMove());
    }

    IEnumerator MoveMoveMove()
    {
        InitNpc();
        yield return new WaitForSeconds(1);
        if(npcs.Count > 0)
            StartCoroutine(MoveMoveMove());

    }

    void InitNpc()
    {
        int npcToMove = npcs.Count-1;
        npcs[npcToMove].Init();
        npcs.Remove(npcs[npcToMove]);
    }

    int GetRandom(int min, int max)
    {
        return Random.Range(min,max);
    }
}
