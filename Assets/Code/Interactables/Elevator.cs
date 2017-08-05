using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : InteractableObject {

    // Use this for initialization
    new

    // Use this for initialization
    void Start()
    {

    }

    public override bool Use()
    {
        Debug.Log("Using elevator");
        return base.Use();
    }
}
