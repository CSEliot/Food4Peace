using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;


[RequireComponent (typeof (Rigidbody))]

public class Movement : MonoBehaviour {


    public Animator animLeftBird;
    public Animator animMiddleBird;
    public Animator animRightBird;
    public Animator animVine;

    public float turnModifier;

    private enum States { FLYING, STUNNED}
    private States currentState;

	private float rotUpDown;// = 0;
    private float target_rotUpDown;
	public float rotUpDownSensitivity;
	public float upDownRange;
	//private Vector3 speed;
	private float verticalSpeed;
	private float rotLeftRight;
    private float target_rotLeftRight;
	public float rotLeftRightSensitivity;
	public float leftRightRange;


    private float lrTurnQuantity;
    //private float 

    public float suppliesBoxSizeX;
    public float suppliesBoxSizeY;
    public GUIStyle suppliesItemsStyle;

    public float maxSpeed;
	
	private Vector3 playerPos;
	//private Ray	ray;
	//private RaycastHit rayHitDown;
	private float moveSpeed;

    private float stunLength;
    private float timeStunned;

    private float remainingItems;

    private bool hasWon;
    private bool hasLost;
	
	//ACTION STRINGS
	//==================================================================
	private string Haim_str = "Rotation";
	private string Vaim_str = "UpDown";
	//==================================================================
	
	//PERSONAL CHARACTER MODIFIERS
	public float flySpeedModifier;
	
	//For looking, we are assigning rotations, but we need original values
	//that aren't getting modified, so we can re-assign them.
	private Vector3 startingCameraRotation;
	private Vector3 newRotationAngle;
	
	private Vector3 targetVelocity;
	Vector3 velocity;
	Vector3 velocityChange;

	void Awake () {
		rigidbody.freezeRotation = true;
		rigidbody.useGravity = false;
	}
	
	
	// Use this for initialization
	void Start () {
        stunLength = 2f;
        timeStunned = 0f;
		newRotationAngle = new Vector3();
        currentState = States.FLYING;
		startingCameraRotation = transform.GetChild(0).transform.localRotation.eulerAngles;
		moveSpeed = flySpeedModifier;
		rotLeftRight = 0.0f;
		rotUpDown = 0.0f;
        remainingItems = 3;
        hasLost = false;
        hasWon = false;
	}

    void OnGUI()
    {
        //if (GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>().isGameOver())
          //  windowRect = GUILayout.Window(0, windowRect, DoMyWindow, "Game Modifiers");
        Rect remainingSuppliesBox = new Rect(Screen.width-50, 50, suppliesBoxSizeX, suppliesBoxSizeY);
        GUI.Box(remainingSuppliesBox, "Remaining Supplies: " + remainingItems, suppliesItemsStyle);
    }
	
	
	// Update is called once per frame
	void Update () {
		
        


		//player rotation
		//left and right
		//currentRotation = Mathf.Lerp(currentRotation, targetRotation, (Time.deltaTime*2));
        target_rotLeftRight = Input.GetAxis(Haim_str) * rotLeftRightSensitivity;
		//target_rotLeftRight = Mathf.Clamp(rotLeftRight, -leftRightRange, leftRightRange);
        rotLeftRight = Mathf.Lerp(rotLeftRight, target_rotLeftRight, (Time.deltaTime * 20));
        target_rotUpDown = -Input.GetAxis(Vaim_str) * rotUpDownSensitivity;
        //target_rotUpDown = Mathf.Clamp(rotUpDown, -upDownRange, upDownRange);
        rotUpDown = Mathf.Lerp(rotUpDown, target_rotUpDown, (Time.deltaTime*20));
		newRotationAngle.x = rotUpDown;
		newRotationAngle.y = rotLeftRight;
		newRotationAngle.z = startingCameraRotation.z;
		transform.localRotation = Quaternion.Euler(newRotationAngle);

		//Movement
		moveSpeed = flySpeedModifier;
		
		//velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
		//velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
		velocityChange.y = 0;

        if(rigidbody.velocity.magnitude < maxSpeed){
            rigidbody.AddRelativeForce(0, 0, moveSpeed);
        }


        //========ANIMATION CHECKING CONTROLLERS=================//
        //only testing one anim, all end same, so one is enough.
        if (currentState == States.FLYING)
        {
            rigidbody.AddRelativeForce(Input.GetAxis(Haim_str) * turnModifier, Input.GetAxis(Vaim_str) * turnModifier, 0);
        }
        else if(currentState == States.STUNNED)
        {
            if (Time.time - timeStunned > stunLength)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 20f);
                currentState = States.FLYING;
                animLeftBird.SetBool("Flying", true);
                animMiddleBird.SetBool("Flying", true);
                animRightBird.SetBool("Flying", true);
                animVine.SetBool("Flying", true);
            }
        }

		

	}

	void OnCollisionEnter(Collision thing){
		Debug.Log ("COLLIDED WITH: " + thing.gameObject.transform.parent.name);
        if (thing.gameObject.transform.parent.name == "Tank")
        {
            if (remainingItems == 1)
            {
                Application.LoadLevel(Application.loadedLevel);
            }
            else
            {
                currentState = States.STUNNED;
                animLeftBird.SetBool("Flying", false);
                animMiddleBird.SetBool("Flying", false);
                animRightBird.SetBool("Flying", false);
                animVine.SetBool("Flying", false);
                remainingItems--;
                rigidbody.velocity.Set(0f, 0f, 0f);
                timeStunned = Time.time;
            }
        }
        else if (thing.gameObject.name == "Goal")
        {
            Application.LoadLevel(0);
        }
	}
}
