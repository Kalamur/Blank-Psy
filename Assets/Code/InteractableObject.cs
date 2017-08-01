using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour {

	public string name;
	public float distanceToInteract = 1.5f;

	protected string[] description;

	// Use this for initialization
	protected void Start () 
	{
		//If name is not assigned get the gameobject's one
		if (name == null) 
		{
			name = transform.name;
		}
		//And get the description
		description = GameFunctions.GetTextXML("DESCRIPTIONS", "DESCRIPTION", name);
	}
		
	public virtual string[] Examinate()
	{
		return description;
	}

	//Use without item
	public virtual bool Use()
	{
		return false;
	}

	//Use with item
	public virtual bool Use(string item)
	{
		return false;
	}

	public virtual string[] GetTalkText()
	{
		return null;
	}

	public virtual Texture[] GetTalkIcons()
	{
		return null;
	}
		
	public virtual string Collect()
	{
		return null;
	}

	public float GetDistanceToInteract()
	{
		return distanceToInteract;
	}

	public virtual Texture GetNeededItem()
	{
		return null;
	}

	public virtual Texture[] ReadMind()
	{
		return null;
	}
}
