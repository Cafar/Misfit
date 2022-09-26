using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StewardBehaviour : MonoBehaviour {
    
    [SerializeField]
    Transform target;
    Quaternion rotation;
    [SerializeField]
    Transform[] wayPoints;
    [SerializeField]
    float speed = 2f;

    public Transform myPlayer;

    bool LookAtTarget = true;
    int index = 0;

    void LateUpdate()
    {
        Vector3 currentPosition = this.transform.position;

        // Get the target waypoints position
        Vector3 targetPosition = wayPoints[index].position;

        // If the moving object isn't that close to the waypoint
        if (Vector3.Distance(currentPosition, targetPosition) > .1f)
        {

            // Get the direction and normalize
            Vector3 directionOfTravel = targetPosition - currentPosition;
            directionOfTravel.Normalize();

            //scale the movement on each axis by the directionOfTravel vector components
            this.transform.Translate(
                directionOfTravel.x * speed * Time.deltaTime,
                directionOfTravel.y * speed * Time.deltaTime,
                directionOfTravel.z * speed * Time.deltaTime,
                Space.World
            );
            if(!LookAtTarget)
            {
                float angle = Mathf.Atan2(directionOfTravel.y, directionOfTravel.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.Euler (0, 0, (int)angle), 5 * Time.deltaTime);
            }
            else
            {
                Vector3 tolook = myPlayer.transform.position - currentPosition;
                float angle = Mathf.Atan2(tolook.y, tolook.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.Euler (0, 0, (int)angle), 5 * Time.deltaTime);
            }

        }
        else
            index = (index + 1) % wayPoints.Length;
    }

    [ContextMenu("EnableLookAtTarget")]
    public void EnableLookAtTarget()
    {
        GetComponentInChildren<PolygonCollider2D>().enabled = LookAtTarget = true;
        Invoke("DisableLookAtTarget", 1.5f);
    }

    [ContextMenu("DisableLookAtTarget")]
    public  void DisableLookAtTarget()
    {
        GetComponentInChildren<PolygonCollider2D>().enabled = LookAtTarget = false;
    }

    void Awake()
    {
        DisableLookAtTarget();
    }
}
