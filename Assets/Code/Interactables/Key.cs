using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : InteractableObject {

    #region Attributes

    // Public Attributes
    public GameObject elevator;
    public float destroyTime = 2.0f;
    public float rotSpeed = 10.0f;
    public float scaleMultiplier = 0.0f;
    private float animationState;

    // Private Attributes
    private float firstStagePercentage = 0.2f;
    private float dt = 0.0f;
    private Vector3 scaleOrigin = Vector3.one;

    #endregion


    public override bool Use()
    {
        elevator.GetComponent<Elevator>().Unlock();
        
        PlayerControl pControl = GameObject.Find("Player").GetComponent<PlayerControl>();
            pControl.PlayerState = PlayerControl.State.Interacting;
        
        string[] textForPlayer = GameFunctions.GetTextXML("INTERACTABLES", "INTERACTABLE", "ElevatorKey");
        pControl.TextToUse = textForPlayer;

        Destroy(gameObject, destroyTime);

        // make the object srank on the time and dissapear
        animationState = Mathf.Clamp01(dt / destroyTime);

        if (animationState <= firstStagePercentage)
        {
            transform.localScale = (scaleMultiplier - remapValue(animationState, firstStagePercentage, 1.0f, 0.0f, scaleMultiplier - 1.0f)) * scaleOrigin;
        }

        return base.Use();
    }

    // Function used to remap the values for the interval
    float remapValue(float originValue, float firstIntervalMinimum, float firstIntervalMaximum, float secondIntervalMinimum, float secondIntervalMaximum)
    {
        float relative = (Mathf.Clamp(originValue, firstIntervalMinimum, firstIntervalMaximum) - firstIntervalMinimum) / (firstIntervalMaximum - firstIntervalMinimum);
        return secondIntervalMinimum + (secondIntervalMaximum - secondIntervalMinimum) * relative;
    }

    // Use this for initialization
    void Start()
    {
        scaleOrigin = transform.localScale;

        //firstStagePercentage = Mathf.Clamp01(firstStagePercentage);
        //scaleMultiplier = Mathf.Max(scaleMultiplier, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        dt = Time.deltaTime;
        
        // Make the object rotate around its Y axis at [rotSpeed] dregrees per second
        transform.Rotate(Vector3.up, (rotSpeed * dt));
    }
}
