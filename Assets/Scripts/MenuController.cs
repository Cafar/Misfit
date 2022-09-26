using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour {

    private Fade myFade;
	void Start () 
    {
        myFade = GetComponent<Fade>();
        myFade.FadeMeIn();
	}
	
	
	void Update () 
    {
		
	}
}
