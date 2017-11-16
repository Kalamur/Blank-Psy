using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {

    #region attributes
    // public attributes
    public enum State
    {
        Normal,
        InEvent,
        Interacting,
        Paused,

        Count
    }

    public float accelRate = 1.0f, maxSpeed = 20.0f, rotSpeed = 180.0f;

    //private attributes
    private State playerState;
    private Animator playerAnimator;
    private CharacterController playerCC;
    private Controls controlScript;
    private Vector3 placeToGo;
    private GameObject objectToInteract;
    private float gravity;
    private bool isMoving, isRotating;
    private string[] textToUse;
    private int currentTextLine;
    private BaseEvent activeEvent;

    #endregion

    #region Properties

    public State PlayerState
    {
        get { return playerState; }
        set
        {
            playerState = value;
        }
    }

    public string[] TextToUse
    {
        get { return textToUse; }
        set {
            currentTextLine = 0;
            Debug.Log("Text received");
            textToUse = value;
        }
    }

    public string CurrentText
    {
        get { return textToUse[currentTextLine]; }
    }

    public Vector3 PlaceToGo
    {
        set
        {
            placeToGo = value;
            isMoving = true;
            isRotating = true;
        }
    }

    public BaseEvent ActiveEvent
    {
        get { return activeEvent; }
        set { activeEvent = value; }
    }

    public bool IsMoving
    {
        get { return isMoving; }
    }

    #endregion

    // Use this for initialization
    void Start ()
    {
        //Player components
        Debug.Log(transform.name);
        playerAnimator = transform.GetChild(0).GetComponent<Animator>();
        playerCC = gameObject.GetComponent<CharacterController>();
        //Gamemanager components
        controlScript = GameObject.Find("GameManager").GetComponent<Controls>();
	}
	
	// Update is called once per frame
	void Update () {
        float dt = Time.deltaTime;
        UpdateClick();
        UpdateMotion(dt);
	}


    //
    void UpdateClick()
    {
        if (controlScript.GetMouseClickUp())
        {
            switch(playerState)
            {
                case State.Normal:
                    RaycastHit hit;
                    if (controlScript.GetPointerHit(out hit))
                    {
                        string objectTag = hit.transform.tag;
                        switch (objectTag)
                        {
                            case "Interactable":
                                objectToInteract = hit.transform.gameObject;
                                placeToGo = objectToInteract.transform.position;
                                //
                                break;
                            default:
                                placeToGo = hit.point;
                                //
                                break;
                        }
                    }
                    isMoving = true;
                    isRotating = true;
                    break;
                case State.Interacting:
                    currentTextLine += 1;
                    if(currentTextLine >= textToUse.Length)
                    {
                        playerState = State.Normal;
                        textToUse = null;
                        currentTextLine = 0;
                    }
                    break;
                case State.InEvent:

                    break;
            }
            
        }
    }

    //
    void UpdateMotion(float dt)
    {
        if (isMoving) UpdateMovement(dt);
        if (isRotating) UpdateRotation(dt);
        ApplyGravity();
    }

    //
    void UpdateMovement(float dt)
    {
        if(CheckDistance() <= 1.5f)
        {
            isMoving = false;
            isRotating = false;
            playerAnimator.SetFloat("Speed", 0.0f);
            //
            if(playerState == State.InEvent)
            {
                // We'll check later
                //activeEvent.
            }
        }
        else if (CheckObjectToUseInFront())
        {
            isMoving = false;
            isRotating = false;
            objectToInteract.GetComponent<InteractableObject>().Use();
            playerAnimator.Play("Pick up");
            objectToInteract = null;
            playerAnimator.SetFloat("Speed", 0.0f);
        }
        else
        {
            playerCC.Move(transform.forward * maxSpeed * dt);
            playerAnimator.SetFloat("Speed", 1.0f);
        }
    }

    //
    void UpdateRotation(float dt)
    {
        //First to know the direction
        Vector3 forward = transform.forward.normalized;
        Vector3 pointDirection = (placeToGo - transform.position).normalized;
        float forwardAngle = Mathf.Atan2(forward.z, forward.x);
        float pDAnlge = Mathf.Atan2(pointDirection.z, pointDirection.x);
        float offset = (pDAnlge - forwardAngle) * Mathf.Rad2Deg;

        transform.Rotate(0.0f, -offset, 0.0f);
        //isRotating = false;
    }

    //
    float CheckDistance()
    {
        Vector2 horizontalPosition = new Vector2(transform.position.x, transform.position.z);
        Vector2 horizontalPlaceToGo = new Vector2(placeToGo.x, placeToGo.z);
        float distance = (horizontalPlaceToGo - horizontalPosition).magnitude;
        return distance;
    }

    //
    bool CheckObjectToUseInFront()
    {
        Vector3 offset = new Vector3(0.0f, 4.0f, 0.0f);
        RaycastHit hit;
        if(Physics.Raycast(transform.position + offset, transform.forward, out hit, 6f))
        {
            //Debug.Log(hit.transform.name);
            if(hit.transform.gameObject == objectToInteract)
            {
                return true;
            }
        }
        return false;
    }

    //
    void ApplyGravity() //Revisar
    {
        if (playerState == State.Normal)
        {
            gravity -= 9.81f * Time.deltaTime * 10.0f;
            playerCC.Move(transform.up * gravity * Time.deltaTime);
            if (playerCC.isGrounded) gravity = 0.0f;
        }
    }

    public void Stop()
    {
        isMoving = false;
        isRotating = false;
    }
}
