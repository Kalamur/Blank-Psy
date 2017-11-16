using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : InteractableObject {

    public GameObject elevatorEvent;

    private bool locked = true;

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
        }        
        return base.Use();
    }

    //
    public void Unlock()
    {
        locked = false;
    }
}
