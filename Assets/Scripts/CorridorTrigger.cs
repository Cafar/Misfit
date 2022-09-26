using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorridorTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D player)
    {
        PlayerState playerState = player.GetComponent<PlayerState>();
        if (playerState != null && player.GetComponent<BoxCollider2D>().enabled)
        {
            player.gameObject.layer = LayerMask.NameToLayer("TransparentFX");
            playerState.KeepForcing = true;
        }
    }

    private void OnTriggerExit2D(Collider2D player)
    {
        PlayerState playerState = player.GetComponent<PlayerState>();
        if (playerState != null)
        {
            player.gameObject.layer = LayerMask.NameToLayer("Default");
            playerState.KeepForcing = false;
        }
    }
}
