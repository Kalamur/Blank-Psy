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

    #endregion


    // Use this for initialization
    void Start () {

        playerAnimator = gameObject.GetComponent<Animator>();
        playerAnimator.SetFloat("Speed", 0.5f);

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public State GetState()
    {
        return playerState;
    }
}
