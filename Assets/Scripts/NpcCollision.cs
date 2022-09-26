using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcCollision : MonoBehaviour {

    public StewardBehaviour stew;

    void Start()
    {
        
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.transform.CompareTag("Player"))
        {
            stew.EnableLookAtTarget();
        }
    }
}
