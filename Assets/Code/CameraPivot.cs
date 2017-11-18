using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPivot : MonoBehaviour {

    private PlayerControl pControl;

	// Use this for initialization
	void Start () {
        //player = GameObject.Find("Player");
        pControl = GameObject.Find("Player").GetComponent<PlayerControl>();

    }
	
	// Update is called once per frame
	void Update () {
        if(pControl.PlayerState == PlayerControl.State.Normal)
            transform.position = pControl.transform.position;
	}
}
