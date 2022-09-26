using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerInputs), typeof(Rigidbody2D))]
public class PlayerState : MonoBehaviour {

    [SerializeField]
    private LevelBehaviour level;
    private float circleCondition = 1f;
    private SpriteRenderer sprite
    {
        get
        {
            return GetComponentInChildren<SpriteRenderer>();
        }
    }
    private Animator animator
    {
        get
        {
            return GetComponent<Animator>();
        }
    }
    private CircleCollider2D circleCollider2D
    {
        get
        {
            return GetComponent<CircleCollider2D>();
        }
    }
    private BoxCollider2D boxCollider2D
    {
        get
        {
            return GetComponent<BoxCollider2D>();
        }
    }
    private new Rigidbody2D rigidbody2D
    {
        get
        {
            return GetComponent<Rigidbody2D>();
        }
    }
    private Color color
    {
        get
        {
            return sprite.color;
        }
        set
        {
            sprite.color = value;
        }
    }
    [SerializeField]
    private Color circleColor;
    [SerializeField]
    private Color squaresColor;
    [SerializeField]
    private Sprite circularSprite;
    [SerializeField]
    private Sprite squareSprite;

    private void Awake()
    {
        EnableMovement();
        InitForcingFeature();
        //DisableMovement();
    }

    private void EnableBoxCollider2D()
    {
        circleCollider2D.enabled = false;
        boxCollider2D.enabled = true;
    }

    private void EnableCircleCollider2D()
    {
        boxCollider2D.enabled = false;
        circleCollider2D.enabled = true;
    }

    #region SpriteManager
    public void InitCircle()
    {
        sprite.color = circleColor;
        sprite.sprite = circularSprite;
    }

    private void ChangeColor(float factor)
    {
        color = Color.Lerp(circleColor, squaresColor, factor);
    }
    #endregion

    #region Forcing
    public bool ImARegularSquare = false;
    [SerializeField]
    private float animTime = 0.1f;
    private bool isForcing = false;
    [SerializeField]
    private float timeUntilBecameSquare = 3f;
    private float tintFactor = 0.3f;
    [SerializeField]
    private bool keepForcing = false;

    private bool iMASquare;

    public bool KeepForcing
    {
        get
        {
            return keepForcing;
        }
        set
        {
            if (value != keepForcing)
            {
                keepForcing = value;
                if (keepForcing && !isForcing)
                {
                    StartForcing();
                }
                else if (!keepForcing && !isForcing)
                {
                    StopForcing();
                }
            }
            else
                Debug.Log("Bitch, please...");
        }
    }

    private IEnumerator ForcingShape()
    {
        isForcing = true;
        animator.SetBool("forcing", isForcing);
        Debug.Log("Forcing shape...");
        yield return new WaitForSeconds(animTime);
        ImARegularSquare = true;
        float time = 0f;
        while (isForcing || keepForcing)
        {
            time += Time.deltaTime * tintFactor;
            circleCondition = 1.0f - time;
            ChangeColor(time);

            Debug.Log(circleCondition);

            if (circleCondition <= 0f)
            {
                BecameSquare();
            }
            yield return null;
        }
        ImARegularSquare = false;
        Debug.Log("Not forcing shape anymoar...");
    }

    private IEnumerator RecoveringColor()
    {
        Debug.Log("Recovering color...");
        yield return new WaitForSeconds(animTime);
        float time = circleCondition;
        while (!isForcing && circleCondition < 1.0f)
        {
            time += Time.deltaTime * tintFactor;
            circleCondition = time * 10;
            ChangeColor(1.0f - circleCondition);

            Debug.Log(circleCondition);
            yield return null;
        }
        Debug.Log("Not forcing shape anymoar...");
    }

    private void StartForcing()
    {
        StartCoroutine("ForcingShape");
    }

    private void StopForcing()
    {
        isForcing = false;
        if (!keepForcing)
        {
            animator.SetBool("forcing", isForcing);
            if (circleCondition > 0)
                StartCoroutine("RecoveringColor");
        }
    }

    private void BecameSquare()
    {
        iMASquare = true;
        animator.SetBool("square", CanMove);
        StopForcing();
        CanMove = false;
        rigidbody2D.isKinematic = true;
        DisableMovement();
        BreakForcingFeature();
        Debug.Log("You became a square");
        level.ReloadCurrentSceneBySquare();
    }
        
    public bool ImASquare()
    {
        return iMASquare;
    }

    private void InitForcingFeature()
    {
        tintFactor = 1.0f / timeUntilBecameSquare;
        PlayerInputs.InputDetected += StartForcing;
        PlayerInputs.InputReleased += StopForcing;
    }

    private void BreakForcingFeature()
    {
        PlayerInputs.InputDetected -= StartForcing;
        PlayerInputs.InputReleased -= StopForcing;
    }

    public void jandepor()
    {
        PlayerInputs.InputDetected -= StartForcing;
        PlayerInputs.InputReleased -= StopForcing;
        keepForcing = isForcing = false;
    }
        
    public void Chased()
    {
        level.ReloadCurrentScene();
    }
    #endregion

    #region Movement
    [SerializeField]
    private float speed = 3.5f;
    [SerializeField]
    private bool movementEnabled = false;
    public bool CanMove
    {
        get
        {
            return movementEnabled;
        }
        set
        {
            if (value)
            {
                EnableMovement();
            }
            else
            {
                Debug.Log("Cannot move");
                DisableMovement();
            }
        }
    }

    private void EnableMovement()
    {
        PlayerInputs.InputMovementAxis += Move;
        movementEnabled = true;
    }

    private void DisableMovement()
    {
        PlayerInputs.InputMovementAxis -= Move;
        movementEnabled = false;
    }

    private void Move(float xMovement, float yMovement)
    {
        Move (new Vector2(xMovement * Time.deltaTime * speed, yMovement * Time.deltaTime * speed));
    }

    private void Move (Vector2 movement)
    {
        rigidbody2D.MovePosition((Vector2) transform.position + movement);
    }
    #endregion

    void OnDestroy()
    {
        DisableMovement();
        BreakForcingFeature();
    }
}
