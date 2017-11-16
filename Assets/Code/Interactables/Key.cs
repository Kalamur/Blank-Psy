using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : InteractableObject {

    public GameObject elevator;

    public override bool Use()
    {
        elevator.GetComponent<Elevator>().Unlock();
        
        PlayerControl pControl = GameObject.Find("Player").GetComponent<PlayerControl>();
            pControl.PlayerState = PlayerControl.State.Interacting;
        
        string[] textForPlayer = GameFunctions.GetTextXML("INTERACTABLES", "INTERACTABLE", "ElevatorKey");
        pControl.TextToUse = textForPlayer;

        Destroy(gameObject, 1);
        return base.Use();
    }
}
