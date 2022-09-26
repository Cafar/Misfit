using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtMe : MonoBehaviour
{
    List<Transform> NPCs = new List<Transform>();

    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerState player = other.GetComponent<PlayerState>();

        if (player == null)
        {
            //NO FUNCIONA
            StartCoroutine("LookAtMeCo", other);
        }
        else
        {
            if (!player.ImARegularSquare && player.CanMove)
            {
                Debug.LogWarning("U lose");
                player.Chased();
                if(transform.parent.GetComponent<StewardBehaviour>() != null)
                    transform.parent.GetComponent<StewardBehaviour>().enabled = false;
            }
        }
    }

    IEnumerator LookAtMeCo(Transform NPC)
    {
        while (true)
        {
            Debug.LogWarning("Turning");
            NPC.LookAt(transform.position);
            yield return null;
        }
    }
}
