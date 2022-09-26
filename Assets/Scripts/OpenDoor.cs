using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour {

    bool enter = false;
    float minimumTimeInside = 1f;

    public Animator door;

    /*void OnTriggerStay2D(Collider2D co)
    {
        if (!enter)
        {
            if (!coroutineInitiated)
            {
                PlayerState player = co.GetComponent<PlayerState>();
                if (player != null)
                {
                    Debug.Log("playerinside");
                    if (player.ImARegularSquare)
                        StartCoroutine("CountTimeInsideMarc", player);
                }
            }
        }
        else
            enter = true;
    }*/

    void OnTriggerEnter2D(Collider2D co)
    {
        if (!enter)
        {
            PlayerState player = co.GetComponent<PlayerState>();
            if (player != null)
            {
                enter = true;
                Debug.Log("playerinside");
                StartCoroutine("CountTimeInsideMarc", player);
            }
        }

    }
    
    void OnTriggerExit2D(Collider2D co)
    {
        PlayerState player = co.GetComponent<PlayerState>();
        if (player != null)
        {
            Debug.Log("playeroutside");
            enter = false;
        }
    }

    IEnumerator CountTimeInsideMarc(PlayerState player)
    {
        float timeInside = minimumTimeInside;

        while (enter && !player.ImARegularSquare)
        {
            yield return null;
        }

        if (enter)
        {
            while (enter && timeInside > Mathf.Epsilon && player.ImARegularSquare)
            {
                timeInside -= Time.deltaTime;
                yield return null;
            }
        }

        if (enter)
        {
            door.SetTrigger("Open");
        }
    }

    /*void OnTriggerEnter2D(Collider2D co)
    {
        if(enter)
        {
            if(co.CompareTag("NPCClave"))
            {
                door.SetTrigger("Open");
            }
            enter = false;
        }
    }

    void OnTriggerExit2D(Collider2D co)
    {
        enter = true;
    }*/
}
