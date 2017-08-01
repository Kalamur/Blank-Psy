using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {

    #region attributes
    // public attributes
    public enum State
    {
        Normal,
        Paused,

        Count
    }

    //private attributes
    private State playerState;
    private Animator playerAnimator;
    private CharacterController playerCC;
    private Controls controlScript;

    #endregion


    // Use this for initialization
    void Start () {
        playerAnimator = gameObject.GetComponent<Animator>();
        playerAnimator.SetFloat("Speed", 1f);
        playerCC = gameObject.GetComponent<CharacterController>();
        controlScript = GameObject.Find("GameManager").GetComponent<Controls>();
	}
	
	// Update is called once per frame
	void Update () {
        if (controlScript.GetMouseClickUp())
        {
            Debug.Log("Clicking");
            string objectTag = controlScript.GetPointerHitTag();
            string objectName = controlScript.GetPointerHitName();
            switch (objectTag)
            {
                case "Interactable":
                    Debug.Log(objectName);
                    break;
                default:
                    Debug.Log(objectName);
                    break;
            }
        }
	}

    //


    //Get - sets
    public State GetState()
    {
        return playerState;
    }
}
