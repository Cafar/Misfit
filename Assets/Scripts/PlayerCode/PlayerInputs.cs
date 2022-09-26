using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputs : MonoBehaviour
{
    public delegate void ForceTrigger();
    public static event ForceTrigger InputDetected;
    public static event ForceTrigger InputReleased;

    public delegate void Axis(float x, float y);
    public static event Axis InputMovementAxis;

    private float horizontalAxisValue = 0f;
    private float verticalAxisValue = 0f;

    private const string horizontalAxis = "Horizontal";
    private const string verticalAxis = "Vertical";
    private enum actionButtons { Fire1, Jump };

    private void Awake()
    {
        InputMovementAxis += (x, y) => Debug.LogWarning("Current horizontal axis value is " + x + "\nCurrent vertical axis value is " + y);
        InputDetected += () => Debug.LogWarning("Action button pushed");
        InputReleased += () => Debug.LogWarning("Action button released");
    }

    void Update()
    {
        horizontalAxisValue = Input.GetAxis(horizontalAxis);
        verticalAxisValue = Input.GetAxis(verticalAxis);

        if (Input.GetButtonDown(actionButtons.Fire1.ToString()) || Input.GetButtonDown(actionButtons.Jump.ToString()))
        {
            InputDetected();
        }
        else if (Input.GetButtonUp(actionButtons.Fire1.ToString()) || Input.GetButtonUp(actionButtons.Jump.ToString()))
        {
            InputReleased();
        }
    }

    void LateUpdate()
    {

        if (horizontalAxisValue > Mathf.Epsilon || horizontalAxisValue < 0 - Mathf.Epsilon
            || verticalAxisValue > Mathf.Epsilon || verticalAxisValue < 0 - Mathf.Epsilon)
        {
            InputMovementAxis(horizontalAxisValue, verticalAxisValue);
        }
    }
}
