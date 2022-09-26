using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookingAtFellas : MonoBehaviour {

    [SerializeField]
    Transform target;
    Quaternion rotation;

	void Update () {
        rotation = Quaternion.LookRotation(target.position - transform.position, transform.TransformDirection(Vector3.up));
        rotation.x = 0f;
        rotation.y = 0f;
        transform.rotation = rotation;
	}
}
