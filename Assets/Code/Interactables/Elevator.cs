﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : InteractableObject {

    private bool locked = true;

    //
    public override bool Use()
    {
        if (locked)
        {
            Debug.Log("Locked");
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
