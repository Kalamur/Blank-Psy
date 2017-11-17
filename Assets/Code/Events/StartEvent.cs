using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartEvent : BaseEvent {

    #region Public Attributes

    public GameObject capsule;

    #endregion

    #region Protected Attributes
    #endregion

    #region Private Attributes

    private float capsuleInitialY;

	#endregion
	
	#region Properties
    #endregion
	
	#region MonoDevelop Methods
	
	protected override void Start () 
	{
        base.Start();
        // Active event
        pControl.PlayerState = PlayerControl.State.InEvent;
        SendRefenceToPlayerAndHUD();
        //
        capsuleInitialY = capsule.transform.position.y;
        showText = true;
    }
	
	
	void Update () 
	{
        float dt = Time.deltaTime;
        CheckStep(dt);
	}
	#endregion
	
	#region User Methods

    /// <summary>
    /// 
    /// </summary>
    protected override void CheckStep(float dt)
    {
        switch (currentStep)
        {
            case 1:
                capsule.transform.Translate(Vector3.up * 5.0f * dt);
                pControl.gameObject.transform.Translate(Vector3.up * 5.0f * dt);
                // Revisar tamaños
                if(capsule.transform.position.y - capsuleInitialY >= 18.0f)
                {
                    currentStep += 1;
                }
                break;
            case 0:
                if (Input.GetMouseButtonDown(0))
                {
                    Debug.Log("Click");
                    CheckTextProgress();
                }
                break;
            case 2:
                pControl.PlaceToGo = pControl.transform.forward * 30.0f
                                    + pControl.transform.position;
                currentStep += 1;
                break;
            case 3:
                if(!pControl.IsMoving)
                {
                    pControl.PlayerState = PlayerControl.State.Normal;
                    Destroy(gameObject);
                }
                break;
        }
    }

	#endregion
}
