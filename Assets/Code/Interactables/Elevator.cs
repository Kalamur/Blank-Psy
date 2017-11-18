using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : InteractableObject
{
    #region
    // public attributes


    // private attributes
    private bool locked = true;

    #endregion

    //
    public override bool Use()
    {
        if (locked)
        {
            PlayerControl pControl = GameObject.Find("Player").GetComponent<PlayerControl>();
            pControl.PlayerState = PlayerControl.State.Interacting;

            string[] textForPlayer = GameFunctions.GetTextXML("INTERACTABLES", "INTERACTABLE", "ElevatorClosed");
            pControl.TextToUse = textForPlayer;
        }
        else
        {
            Debug.Log("Diiing");
            GetComponent<ElevatorEvent>().InitializeEvent();
        }        
        return base.Use();
    }

    //
    public void Unlock()
    {
        locked = false;
    }
}
