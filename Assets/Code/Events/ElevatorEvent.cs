using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorEvent : BaseEvent {

    #region Public Attributes
    public GameObject elevatorBody;
    public GameObject elevatorDoor;
    public GameObject ascendingLights;
    public float ascendSpeed = 100.0f;
    public float rotSpeed = 50.0f;

    #endregion

    #region Protected Attributes
    #endregion

    #region Private Attributes
    #endregion

    #region Properties
    #endregion

    #region MonoDevelop Methods
    void Update()
    {
        float dt = Time.deltaTime;
        CheckStep(dt);
    }
    #endregion

    #region User Methods
    protected override void CheckStep(float dt)
    {
        
        switch(currentStep)
        {
            // Disable the colliders and mark objective for the player
            case 0:
                pControl.PlayerState = PlayerControl.State.InEvent;
                elevatorBody.GetComponent<Collider>().enabled = false;
                elevatorDoor.GetComponent<Collider>().enabled = false;
                pControl.PlaceToGo = elevatorBody.transform.position;
                currentStep += 1;
                break;
            // Check entrance to the elevator
            case 1:
                if (pControl.IsMoving == false)
                currentStep += 1;
                break;
            // Create the the light that later will ascend
            case 2:
                ascendingLights.active = true;
                hControl.NewFadeStatus(0.0f, 1);
                currentStep += 1;
                break;
            // The created light ascends
            case 3:
                ascendingLights.transform.Translate(Vector3.up * ascendSpeed * dt);
                ascendingLights.transform.Rotate(Vector3.up, (rotSpeed * dt));
                cameraPivot.transform.Translate(Vector3.up * ascendSpeed * dt);
                
                break;
        }
        // Debug.Log(string.Format("We are in the {0} step", currentStep));
    }
    #endregion
}
