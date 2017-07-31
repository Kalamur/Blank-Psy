using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUD : MonoBehaviour {

    public float iconSizeRate = 0.2f;
    private float iconSize;

	// Use this for initialization
	void Start () {
        iconSize = Screen.height * iconSizeRate;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public float GetIconSize()
    {
        return iconSize;
    }
}
