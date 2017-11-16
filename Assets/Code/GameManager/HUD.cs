using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUD : MonoBehaviour {

    public float iconSizeRate = 0.2f;

    private float iconSize;
    private PlayerControl pControl;
    private BaseEvent activeEvent;
    
    public BaseEvent ActiveEvent
    {
        get { return activeEvent; }
        set { activeEvent = value; }
    }

	void Start () {
        iconSize = Screen.height * iconSizeRate;
        pControl = GameObject.Find("Player").GetComponent<PlayerControl>();
	}

    private void OnGUI()
    {
        // 
        if(pControl.PlayerState == PlayerControl.State.Interacting)
        {
            GUI.Label(new Rect(
                Screen.width * 1/5, Screen.height * 4/5, Screen.width * 3/5, Screen.height * 1/5), 
                pControl.CurrentText);
        }
        //
        if (pControl.PlayerState == PlayerControl.State.InEvent)
        {
            if (activeEvent.ShowText)
            {
                GUI.Label(new Rect(
                Screen.width * 1 / 5, Screen.height * 4 / 5, Screen.width * 3 / 5, Screen.height * 1 / 5),
                activeEvent.CurrentText);
            }            
        }
    }

    public float GetIconSize()
    {
        return iconSize;
    }
}
