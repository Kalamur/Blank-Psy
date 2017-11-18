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
        hControl.NewFadeStatus(1.0f, -1);

        // intialize the process
        InitializeEvent();
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
        // aqui se controlan los pasos a seguir para el inicio del personaje (texto > capsula sube > establecer punto a moverse > moverse)
        switch (currentStep)
        {
            // texto inicial
            case 0:
                if (Input.GetMouseButtonDown(0))
                {
                    Debug.Log("Click");
                    CheckTextProgress();
                }
                break;
            // subida de capsula
            case 1:
                capsule.transform.Translate(Vector3.up * 5.0f * dt);
                pControl.gameObject.transform.Translate(Vector3.up * 5.0f * dt);
                // Revisar tamaños
                if(capsule.transform.position.y - capsuleInitialY >= 18.0f)
                {
                    currentStep += 1;
                }
                break;
            // establecer destino
            case 2:
                pControl.PlaceToGo = pControl.transform.forward * 30.0f
                                    + pControl.transform.position;
                currentStep += 1;
                break;
            // mover y cerrar evento
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
