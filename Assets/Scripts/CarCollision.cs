using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarCollision : MonoBehaviour
{
    private static StreetDeathController _streetD;
    private static StreetDeathController streetD
    {
        get
        {
            if (_streetD == null)
            {
                _streetD = FindObjectOfType<StreetDeathController>();
            }

            return _streetD;
        }
    }


    private void OnCollisionEnter2D () 
    {
        streetD.Init();
	}
}
