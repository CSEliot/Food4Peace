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

    private float turnModifier;

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

    public float pointsBoxSizeX;
    public float pointsBoxSizeY;
    public GUIStyle pointsBoxStyle;

    private float maxSpeed;
	
	private Vector3 playerPos;
	//private Ray	ray;
	//private RaycastHit rayHitDown;
	private float moveSpeed;

    private float stunLength;
    private float timeStunned;

    private float immuneLength;
    private float timeImmuned;

    private int remainingItems;

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


    public Material soup;
    public Material water;
    public Material grain;

    private DataTracker DT;

    private int points;
	void Awake () {
		rigidbody.freezeRotation = true;
		rigidbody.useGravity = false;
	}
	
	
	// Use this for initialization
	void Start () {
        points = 10000;
        immuneLength = 3f;
        stunLength = 1f;
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

        try
        {
            DT = GameObject.Find("DataTracker").GetComponent<DataTracker>();
            DontDestroyOnLoad(DT);
        }catch(Exception e){
        }
        if (DT != null && DT.getItemNum(1) == 0)
        {
            gameObject.transform.GetChild(3).GetChild(1).GetComponent<SkinnedMeshRenderer>().material = grain;
            maxSpeed = 28f;
            turnModifier = 6f;
            remainingItems = 3;
        }
        else if (DT != null && DT.getItemNum(1) == 1)
        {
            gameObject.transform.GetChild(3).GetChild(1).GetComponent<SkinnedMeshRenderer>().material = soup;
            maxSpeed = 21f;
            turnModifier = 10f;
            remainingItems = 3;
        }
        else if (DT != null && DT.getItemNum(1) == 2)
        {
            gameObject.transform.GetChild(3).GetChild(1).GetComponent<SkinnedMeshRenderer>().material = water;
            maxSpeed = 21f;
            turnModifier = 6f;
            remainingItems = 4;
        }
        else
        {
            maxSpeed = 21f;
            turnModifier = 6f;
            remainingItems = 4;
        }
    }

    void OnGUI()
    {
        //if (GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>().isGameOver())
          //  windowRect = GUILayout.Window(0, windowRect, DoMyWindow, "Game Modifiers");
        Rect remainingSuppliesBox = new Rect(Screen.width-50, 50f, suppliesBoxSizeX, suppliesBoxSizeY);
        GUI.Box(remainingSuppliesBox, "Remaining Supplies: " + remainingItems, suppliesItemsStyle);

        Rect pointsRemaining = new Rect(Screen.width - 50f, 50f, pointsBoxSizeX, pointsBoxSizeY);
        GUI.Box(pointsRemaining, "Points: " + points, pointsBoxStyle);
        
        if (remainingItems == 4)
        {
            suppliesItemsStyle.normal.textColor = Color.cyan;
            pointsBoxStyle.normal.textColor = Color.cyan;
        }
        else if (remainingItems == 3)
        {
            suppliesItemsStyle.normal.textColor = Color.green;
            pointsBoxStyle.normal.textColor = Color.green;
        }
        else if (remainingItems == 2)
        {
            suppliesItemsStyle.normal.textColor = Color.yellow;
            pointsBoxStyle.normal.textColor = Color.yellow;
        }
        else
        {
            suppliesItemsStyle.normal.textColor = Color.red;
            pointsBoxStyle.normal.textColor = Color.red;
        }
    }
	
	
	// Update is called once per frame
	void Update () {

        points--;

        RaycastHit hit;
        if (Physics.Raycast(transform.GetChild(4).position, transform.GetChild(4).transform.forward, out hit))
        {
            Debug.Log("Name hit: " + hit.distance);
            if (hit.transform.gameObject.name == "Arm" && hit.distance < 9f)
            {
                hit.transform.parent.GetChild(0).gameObject.layer = 8;
            }
        }

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
            rigidbody.velocity.Set(0f, 0f, 0f);
            if (Time.time - timeStunned > stunLength)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 20f);
                currentState = States.FLYING;
                timeImmuned = Time.time;
                animLeftBird.SetBool("Flying", true);
                animMiddleBird.SetBool("Flying", true);
                animRightBird.SetBool("Flying", true);
                animVine.SetBool("Flying", true);
            }
        }

		

	}

	void OnCollisionEnter(Collision thing){
        if (thing.gameObject.transform.parent!= null && thing.gameObject.transform.parent.name == "Tank")
        {
            if (remainingItems == 1)
            {
                Application.LoadLevel(Application.loadedLevel);
            }
            else if(Time.time - timeImmuned > immuneLength)
            {
                timeImmuned = Time.time;
                timeStunned = Time.time;
                currentState = States.STUNNED;
                animLeftBird.SetBool("Flying", false);
                animMiddleBird.SetBool("Flying", false);
                animRightBird.SetBool("Flying", false);
                animVine.SetBool("Flying", false);
                remainingItems--;
                rigidbody.velocity.Set(0f, 0f, 0f);
                
            }
        }
        else if (thing.gameObject.name == "Goal")
        {
            if (DT != null)
            {
                DT.setHighscore(remainingItems * points);
            }
            Application.LoadLevel(1);
        }
	}
}
