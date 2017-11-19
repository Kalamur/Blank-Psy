using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUD : MonoBehaviour {

    public float iconSizeRate = 0.2f;
    public Texture fadeTexture;
    public GUIStyle hudStyle;
    public float fadeSpeed = 0.1f;

    private float iconSize;
    private PlayerControl pControl;
    private BaseEvent activeEvent;
    //
    private float alpha = 1.0f;
    private bool fading = false;
    private int fadeDirection = -1;
    private Color fadeColor;
    
    public BaseEvent ActiveEvent
    {
        get { return activeEvent; }
        set { activeEvent = value; }
    }

	void Start ()
    {
        iconSize = Screen.height * iconSizeRate;
        pControl = GameObject.Find("Player").GetComponent<PlayerControl>();
	}

    void Update()
    {
        if(fading)
        {
            alpha += fadeDirection * fadeSpeed * Time.deltaTime;
            alpha = Mathf.Clamp01(alpha);
            fadeColor = GUI.color;
            fadeColor.a = alpha;
        }
    }

    private void OnGUI()
    {
        // Text for interactions
        if(pControl.PlayerState == PlayerControl.State.Interacting)
        {
            GUI.Label(new Rect(
                Screen.width * 1/5, Screen.height * 4/5, Screen.width * 3/5, Screen.height * 1/5), 
                pControl.CurrentText, hudStyle);
        }
        // Text for events
        if (pControl.PlayerState == PlayerControl.State.InEvent)
        {
            if (activeEvent.ShowText)
            {
                GUI.Label(new Rect(
                Screen.width * 1 / 5, Screen.height * 4 / 5, Screen.width * 3 / 5, Screen.height * 1 / 5),
                activeEvent.CurrentText, hudStyle);
            }            
        }
        // Fade stuff
        GUI.color = fadeColor;
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), fadeTexture);
    }

    public float GetIconSize()
    {
        return iconSize;
    }

    //
    public void NewFadeStatus(float startingAplha, int fadeDirection)
    {
        alpha = startingAplha;
        this.fadeDirection = fadeDirection;
        fading = true;
    }
}
