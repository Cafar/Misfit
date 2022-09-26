using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToWayPoint : MonoBehaviour {

	
    public Waypoint[] wayPoints;
    public float speed = 3f;
    public bool isCircular;
    // Always true at the beginning because the moving object will always move towards the first waypoint
    public bool inReverse = true;
    public bool initOnStart = true;
    public bool needRotate = true;
    public bool LookAtWayPoint = false;


    private Waypoint currentWaypoint;
    private int currentIndex   = 0;
    private bool isWaiting     = false;
    private float speedStorage = 0;

    static int num;


    /**
     * Initialisation
     * 
     */
    void Start () {
        num = 0;
        if(wayPoints.Length > 0) {
            currentWaypoint = wayPoints[0];
        }

        if(!initOnStart)
        {
            Pause();
        }
    }



    /**
     * Update is called once per frame
     * 
     */
    void Update()
    {
        if(currentWaypoint != null && !isWaiting) 
        {
            MoveTowardsWaypoint();
        }
        else if(wayPoints.Length >0)
        {
            
              transform.rotation = Quaternion.Slerp (transform.rotation,currentWaypoint.transform.rotation ,5 * Time.deltaTime);
//            Vector3 vectorToTarget = currentWaypoint.transform.position - transform.position;
//            float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
//            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
//            transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * speed);
//            Debug.Log(currentWaypoint.transform.name);
        }
    }



    /**
     * Pause the mover
     * 
     */
    void Pause()
    {
        isWaiting = !isWaiting;
    }

    public void Init()
    {
        Pause();
    }



    /**
     * Move the object towards the selected waypoint
     * 
     */
    private void MoveTowardsWaypoint()
    {
        // Get the moving objects current position
        Vector3 currentPosition = this.transform.position;

        // Get the target waypoints position
        Vector3 targetPosition = currentWaypoint.transform.position;

        // If the moving object isn't that close to the waypoint
        if(Vector3.Distance(currentPosition, targetPosition) > .1f) {

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

            if(needRotate)
            {
                float angle = Mathf.Atan2(directionOfTravel.y, directionOfTravel.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.Euler (0, 0, (int)angle), 5 * Time.deltaTime);
            }
        } else {
            // If the waypoint has a pause amount then wait a bit
            if(currentWaypoint.waitSeconds > 0) {
                Pause();
                Invoke("Pause", currentWaypoint.waitSeconds);
            }

            // If the current waypoint has a speed change then change to it
            if(currentWaypoint.speedOut > 0) {
                speedStorage = speed;
                speed = currentWaypoint.speedOut;
            } else if(speedStorage != 0) {
                speed = speedStorage;
                speedStorage = 0;
            }
            NextWaypoint();
        }
    }
//
//    IEnumerator LookAt()
//    {
//        while(Quaternion.Angle(transform.rotation, currentWaypoint.transform.rotation) > 0)
//        {
//            
//            Debug.Log(currentIndex);
//        }
//
//
//
//    }
//
    /**
     * Work out what the next waypoint is going to be
     * 
     */
    private void NextWaypoint()
    {
        if(isCircular) {

            if(!inReverse) {
                currentIndex = (currentIndex+1 >= wayPoints.Length) ? 0 : currentIndex+1;
            } else {
                currentIndex = (currentIndex == 0) ? wayPoints.Length-1 : currentIndex-1;
            }

        } else {

            // If at the start or the end then reverse
            if((!inReverse && currentIndex+1 >= wayPoints.Length) || (inReverse && currentIndex == 0)) {
                inReverse = !inReverse;
            }
            currentIndex = (!inReverse) ? currentIndex+1 : currentIndex-1;

        }
        currentWaypoint = wayPoints[currentIndex];
    }
}

