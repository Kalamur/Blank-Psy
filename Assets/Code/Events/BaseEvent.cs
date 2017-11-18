using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEvent : MonoBehaviour {

    #region Public Attributes

    public string eventName;

    #endregion

    #region Protected Attributes

    protected PlayerControl pControl;
    protected HUD hControl;
    protected GameObject cameraPivot;
    protected string[] eventText;
    protected int currentTextLine = 0;
    protected int currentStep = -1;             // Process not initialized
    protected bool showText = false;

    #endregion

    #region Private Attributes
    #endregion

    #region Properties

    public string CurrentText
    {
        get {
            if (currentTextLine < eventText.Length)
                return eventText[currentTextLine];
            else
                return null;
        }
    }

    public bool ShowText
    {
        get { return showText; }
    }

    #endregion

    #region MonoDevelop Methods

    protected virtual void Start () 
	{
        pControl = GameObject.Find("Player").GetComponent<PlayerControl>();
        hControl = GameObject.Find("GameManager").GetComponent<HUD>();
        eventText = GameFunctions.GetTextXML("EVENTS", "EVENT", eventName);
        cameraPivot = GameObject.Find("Camera Pivot");
	}

    #endregion

    #region User Methods

    /// <summary>
    /// 
    /// </summary>
    protected void SendRefenceToPlayerAndHUD()
    {
        hControl.ActiveEvent = this;
        pControl.ActiveEvent = this;
    }

    /// <summary>
    /// 
    /// </summary>
    protected virtual void CheckStep(float dt)
    {

    }

    protected void CheckTextProgress()
    {
        currentTextLine += 1;
        if (currentTextLine >= eventText.Length)
        {
            showText = false;
            currentStep += 1;
        }
        else
        {
            
        }
    }

    public void InitializeEvent()
    {
        currentStep = 0;
    }

    #endregion
}
