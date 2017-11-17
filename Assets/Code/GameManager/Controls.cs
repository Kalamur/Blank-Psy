using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Controls : MonoBehaviour {
	
	#region Public Attributes
	public float pointerSpeed = 10.0f;
	public Texture2D[] pointerIcons;
	public Texture[] keyButtons;
	public Texture[] gpButtons;
	#endregion
	
	#region Private Attributes
	private Vector2 pointerPosition;
	private GameManager gmControl;
	private GameObject hud;
	private HUD hControl;
		//Action button
	private bool actionButton = false;
	private bool actionButtonFree = true;
		//Inventory button
	private bool inventoryButton = false;
	private bool inventoryButtonFree = true;
		//Power button	
	private bool powerButton = false;
	private bool powerButtonFree = true;
		//Pause button
	private bool pauseButton = false;
	private bool pauseButtonFree = true;
    //Mouse button
    private bool lClickUp = false;

	private Texture2D currentPointerIcon;
	private PlayerControl pScript;

	private float iconSize;
	#endregion
	
	#region MonoDevelop Methods
	//
	void Start(){
		gmControl = gameObject.GetComponent<GameManager> ();
	}

	// Update is called once per frame
	void Update () 
	{
		UpdatePointer();
		float dt = Time.deltaTime;
		UpdateControls (dt);
		//Buttons go out
		UpdateButtons();
        if(Cursor.visible == true)
        {
            UpdateMouse();
        }
	}

	//
	void OnLevelWasLoaded()
	{
		//Take the Hud if it isn't
			if(!hud)
			{
				hud = GameObject.Find ("HUD");
				hControl = hud.GetComponent<HUD> ();
			}
			//Detect controllers on the beggining
		if (Input.GetJoystickNames().Length > 0) 
		{		//Error control
			if (Input.GetJoystickNames () [0] != "" && SceneManager.GetActiveScene ().name != "Menu") 
			{
				pointerPosition = new Vector2 (Screen.width / 2, Screen.height / 2);
				Cursor.visible = false;
			}
			else
			{
				pointerPosition = new Vector2 (Screen.width, Screen.height);
			}
		}
		//
		GameObject player = GameObject.Find("Player");
		if (player) 
		{
			pScript = player.GetComponent<PlayerControl> ();
		}
	}

	//
	void OnGUI()
	{
		GUI.DrawTexture (new Rect(pointerPosition.x, Screen.height - pointerPosition.y, 20, 20), currentPointerIcon, ScaleMode.ScaleToFit);
		//Error control
		if (pScript) {
			if (pScript.PlayerState == PlayerControl.State.Paused) {
				float iconSize = hControl.GetIconSize ();
				//Here we will paint the buttons
				if (Input.GetJoystickNames ().Length > 0 && Input.GetJoystickNames () [0] != "") 	//Controller icons
				{
					//Inventory
					GUI.DrawTexture (new Rect(iconSize, Screen.height - iconSize, iconSize, iconSize), 
						gpButtons[0], ScaleMode.ScaleToFit);
					//Powers
					GUI.DrawTexture (new Rect(Screen.width - iconSize*2, 0, iconSize, iconSize), 
						gpButtons[1], ScaleMode.ScaleToFit);
					//Pause
					GUI.DrawTexture (new Rect(iconSize, 0, iconSize, iconSize), 
						gpButtons[2], ScaleMode.ScaleToFit);
				} 
				else if(Application.platform == RuntimePlatform.WindowsEditor ||
						Application.platform == RuntimePlatform.WindowsPlayer)		//Keyboard icons
				{
					//Inventory
					GUI.DrawTexture (new Rect(iconSize, Screen.height - iconSize, iconSize, iconSize), 
						keyButtons[0], ScaleMode.ScaleToFit);
					//Powers
					GUI.DrawTexture (new Rect(Screen.width - iconSize*2, 0, iconSize, iconSize), 
						keyButtons[1], ScaleMode.ScaleToFit);
					//Pause
					GUI.DrawTexture (new Rect(iconSize, 0, iconSize, iconSize), 
						keyButtons[2], ScaleMode.ScaleToFit);
				}
			}
		}
	}
	#endregion
	
	#region User Methods
	//
	void UpdateControls(float dt)
	{
		//Gamepad not used on menus, they have their own method
		if (Input.GetJoystickNames().Length > 0)
		{		//Error control
			if (SceneManager.GetActiveScene ().name != "Menu" && Time.timeScale > 0.1f) 
			{
				UpdateGamepad (dt);
			} 
			else if (pointerPosition.x != Screen.width && pointerPosition.y != Screen.height) 
			{
				pointerPosition = new Vector2 (Screen.width, Screen.height);
				actionButton = false;
			}
			// De/activating mouse cursor
			if(Input.GetJoystickNames () [0] != "" && Cursor.visible)
			{	//Hide cursor if connecting controller
				Cursor.visible = false;
			}
			else if(Input.GetJoystickNames () [0] == "" && !Cursor.visible)
			{	//Show it if disconnecting controller
				Cursor.visible = true;
			}
		}

	}

    //
    void UpdateMouse()
    {
        lClickUp = Input.GetMouseButtonUp(0);

    }

	//
	void UpdateJoystick(float dt)
	{
		//1st joystick control
		float vAxis1 = Input.GetAxisRaw ("Vertical");
		float hAxis1 = Input.GetAxisRaw ("Horizontal");
		pointerPosition.x += hAxis1 * pointerSpeed;
		pointerPosition.y += vAxis1 * pointerSpeed;
		//Clamping
		pointerPosition.x = Mathf.Clamp (pointerPosition.x, 0.0f, Screen.width);
		pointerPosition.y = Mathf.Clamp (pointerPosition.y, 0.0f, Screen.height);
	}
	
    //
	void UpdateGamepad(float dt)
	{
		//Detect controllers
		//If previously disconected
		if(Input.GetJoystickNames () [0] != "" && pointerPosition.x == Screen.width && pointerPosition.y == Screen.height)
		{
			pointerPosition = new Vector2 (Screen.width / 2, Screen.height / 2);
		}	//Normal
		else if (Input.GetJoystickNames () [0] != "") 
		{
			UpdateJoystick (dt);
			//Get the buttons
			//UpdateButtons();
		}	
		else //Disconected
		{
			pointerPosition = new Vector2 (Screen.width, Screen.height);
		}
	}

	//
	void UpdateButtons(){
		//Action button
		actionButtonFree = !actionButton;
		actionButton = (Input.GetAxisRaw ("Fire1") > Mathf.Epsilon);
		//Inventory button
		inventoryButtonFree = !inventoryButton;
		inventoryButton = (Input.GetAxisRaw ("Fire2") > Mathf.Epsilon);
		//Power button
		powerButtonFree = !powerButton;
		powerButton = (Input.GetAxisRaw ("Fire3") > Mathf.Epsilon);
		//Pause button
		pauseButtonFree = !pauseButton;
		pauseButton = (Input.GetAxisRaw ("Cancel") > Mathf.Epsilon);
	}

	//
	void UpdatePointer()
	{
		//If not in the menu or the pause
		if (SceneManager.GetActiveScene ().name != "Menu" && Time.timeScale > 0.1f) 
		{
			Ray ray;
			//If controller
			if (Input.GetJoystickNames ().Length > 0 && Input.GetJoystickNames () [0] != "") 
			{
				ray = Camera.main.ScreenPointToRay (pointerPosition);
			} 
			else 
			{	//If mouse
				ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			}
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit)) 
			{
				if (hit.transform.tag == "Interactable" || hit.transform.tag == "Character" || hit.transform.tag == "Collectible") 
				{
                    currentPointerIcon = pointerIcons[2];
                } 
				else 
				{
					currentPointerIcon = pointerIcons [1];
				}
			}
			else 
			{
				currentPointerIcon = pointerIcons [0];
			}
		} 
		else 
		{
			currentPointerIcon = pointerIcons [3];
		}
		//Update the cursor
		Cursor.SetCursor (currentPointerIcon, Vector2.zero, CursorMode.Auto);
	}

	//Gets & sets

	//
	public bool GetActionButtonDown()
	{
		return actionButton && actionButtonFree;
	}

	//
	public bool GetInventoryButtonDown()
	{
		return inventoryButton && inventoryButtonFree;
	}

	//
	public bool GetPowerButtonDown()
	{
		return powerButton && powerButtonFree;
	}

	//
	public bool GetPauseButtonDown()
	{
		return pauseButton && pauseButtonFree;
	}

	//
	public Vector2 GetPointerPosition()
	{
        //If controller
        if (Cursor.visible == false)
        {
            return pointerPosition;
        }
        else //If mouse
        {
            return Input.mousePosition;
        }
	}

    //
    public bool GetPointerHit(out RaycastHit hit)
    {
        Ray ray;
        ray = Camera.main.ScreenPointToRay(GetPointerPosition());
        if (Physics.Raycast(ray, out hit))
        {
            return true;
        }
        return false;
    }

    //
    public bool GetMouseClickUp()
    {
        return lClickUp;
    }

	#endregion
}
