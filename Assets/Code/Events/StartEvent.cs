using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartEvent : BaseEvent {

    #region Public Attributes

    public GameObject capsule;
    public Transform cameraStartPoint;
    public Transform cameraEndPoint;

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
        
        //Start of the event stuff
        capsuleInitialY = capsule.transform.position.y;
        hControl.NewFadeStatus(1.0f, -1);
        cameraPivot.transform.position = cameraStartPoint.position;

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
            // camara moviendose por el pasillo y texto inicial
            case 0:
                //
                cameraPivot.transform.position += (cameraEndPoint.position - cameraStartPoint.position) * 0.1f * dt;
                //
                float totalTravelDistance = (cameraStartPoint.position - cameraEndPoint.position).magnitude;
                float currentDistance = (cameraPivot.transform.position - cameraEndPoint.position).magnitude;
                if(currentDistance < totalTravelDistance * 2/3 && showText == false)
                {
                    showText = true;
                }
                else if (currentDistance < totalTravelDistance * 1/3 && currentTextLine == 0)
                {
                    currentTextLine += 1;
                }
                else if (currentDistance < 1.5f)
                {
                    currentTextLine += 1;
                    currentStep += 1;
                }
                break;
            // subida de capsula
            case 1:
                capsule.transform.Translate(Vector3.up * 5.0f * dt);
                pControl.gameObject.transform.Translate(Vector3.up * 5.0f * dt);
                // Revisar tamaños
                if(capsule.transform.position.y - capsuleInitialY >= 18.0f)
                {
                    showText = false;
                    currentStep += 1;
                }
                break;
            // establecer destino
            case 2:
                pControl.PlaceToGo = cameraEndPoint.position;
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
