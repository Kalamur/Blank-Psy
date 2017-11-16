using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEvent : MonoBehaviour {

    #region Public Attributes

    public string eventName;

    #endregion

    #region Protected Attributes

    protected PlayerControl pControl;
    protected string[] eventText;
    protected int currentTextLine = 0;
    protected int currentStep = 0;
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
        eventText = GameFunctions.GetTextXML("EVENTS", "EVENT", eventName);
	}

    #endregion

    #region User Methods

    /// <summary>
    /// 
    /// </summary>
    protected void SendRefenceToPlayerAndHUD()
    {
        GameObject.Find("GameManager").GetComponent<HUD>().ActiveEvent = this;
        GameObject.Find("Player").GetComponent<PlayerControl>().ActiveEvent = this;
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



    #endregion
}
