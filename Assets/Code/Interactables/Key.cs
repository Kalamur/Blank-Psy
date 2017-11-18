using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : InteractableObject {

    #region Attributes

    // Public Attributes
    public GameObject elevator;
    public float rotSpeed = 10.0f;

    // Private Attributes
    private float dt = 0.0f;
    private bool taken = false;                                             // bool to control if the player have interacted with the object
    private Vector3 scaleReduction = new Vector3(3.0f, 3.0f, 3.0f);         // attribute in charge of the shrank speed

    #endregion


    public override bool Use()
    {
        elevator.GetComponent<Elevator>().Unlock();
        
        PlayerControl pControl = GameObject.Find("Player").GetComponent<PlayerControl>();
        pControl.PlayerState = PlayerControl.State.Interacting;
        
        string[] textForPlayer = GameFunctions.GetTextXML("INTERACTABLES", "INTERACTABLE", "ElevatorKey");
        pControl.TextToUse = textForPlayer;

        // Advise that we´ve interacted with this object
        taken = true;

        return base.Use();
    }

    // Update is called once per frame
    void Update()
    {
        dt = Time.deltaTime;
        
        // Make the object rotate around its Y axis at [rotSpeed] dregrees per second
        transform.Rotate(Vector3.up, (rotSpeed * dt));

        // We take the item so its going to shrank until it dissapears
        if (taken)
        {
            transform.localScale -= (scaleReduction * dt);

            // The object its no longer visible, so we can destroy it
            if (transform.localScale.x <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
