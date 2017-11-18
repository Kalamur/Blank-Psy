using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : InteractableObject {
    
    #region Attributes

    // Public Attributes
    public float rotSpeed = 25.0f;


    // Private Attributes
    private float dt = 0.0f;
    private bool taken = false;
    private Vector3 scaleReduction = new Vector3(3.0f, 3.0f, 3.0f);

    #endregion

    public override bool Use()
    {
        PlayerControl pControl = GameObject.Find("Player").GetComponent<PlayerControl>();
        pControl.PlayerState = PlayerControl.State.Interacting;

        string[] textForPlayer = GameFunctions.GetTextXML("INTERACTABLES", "INTERACTABLE", "Collectable");
        pControl.TextToUse = textForPlayer;

        taken = true;

        return base.Use();
    }


    // Update is called once per frame
    void Update ()
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
