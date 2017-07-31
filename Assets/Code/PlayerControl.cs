using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {

    public enum State
    {
        Normal,
        Paused,

        Count
    }

    public State playerState;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public State GetState()
    {
        return playerState;
    }
}
