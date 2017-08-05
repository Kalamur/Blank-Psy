using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : InteractableObject {

    public GameObject elevator;

    public override bool Use()
    {
        elevator.GetComponent<Elevator>().Unlock();
        Debug.Log("You have the key");
        Destroy(gameObject, 1);
        return base.Use();
    }
}
