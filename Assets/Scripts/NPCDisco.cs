using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDisco : MonoBehaviour {

    Transform myPlayer;

	void Start () 
    {
        myPlayer = GameObject.FindGameObjectWithTag("Player").transform;
            
	}
	
	
	void Update () 
    {
        Vector3 tolook = myPlayer.transform.position - transform.position;
        float angle = Mathf.Atan2(tolook.y, tolook.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.Euler (0, 0, (int)angle), 2 * Time.deltaTime);
	}
}
