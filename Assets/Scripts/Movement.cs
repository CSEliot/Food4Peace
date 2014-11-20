using UnityEngine;
using System.Collections;


[RequireComponent (typeof (Rigidbody))]
[RequireComponent (typeof (CapsuleCollider))]

public class Movement : MonoBehaviour {	

	private float rotUpDown;// = 0;
	public float rotUpDownSensitivity;
	public float upDownRange;
	//private Vector3 speed;
	private float verticalSpeed;
	private float rotLeftRight;
	public float rotLeftRightSensitivity;
	public float leftRightRange;


    public float suppliesBoxSizeX;
    public float suppliesBoxSizeY;
    public GUIStyle suppliesItemsStyle;

	private float upMove;
    public float maxSpeed;
	
	private Vector3 playerPos;
	//private Ray	ray;
	//private RaycastHit rayHitDown;
	private float moveSpeed;

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
		newRotationAngle = new Vector3();
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
	void FixedUpdate () {
		
		//player rotation
		//left and right
		
		rotLeftRight = Input.GetAxis(Haim_str)*rotLeftRightSensitivity;
		rotLeftRight = Mathf.Clamp(rotLeftRight, -leftRightRange, leftRightRange);
		rotUpDown = -Input.GetAxis(Vaim_str)*rotUpDownSensitivity;
		rotUpDown = Mathf.Clamp(rotUpDown, -upDownRange, upDownRange);
		newRotationAngle.x = rotUpDown;
		newRotationAngle.y = rotLeftRight;
		newRotationAngle.z = startingCameraRotation.z;
		transform.localRotation = Quaternion.Euler(newRotationAngle);

		//Movement
		moveSpeed = flySpeedModifier;
		
		//velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
		//velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
		velocityChange.y = 0;

		
        upMove = Input.GetAxis(Vaim_str) * moveSpeed;
        if(rigidbody.velocity.magnitude < maxSpeed){
            rigidbody.AddRelativeForce(0, 0, moveSpeed);
        }

		rigidbody.AddRelativeForce (Input.GetAxis(Haim_str)*moveSpeed,upMove, 0);
	}

	void OnCollisionEnter(Collision thing){
		Debug.Log ("COLLIDED WITH: " + thing.gameObject.transform.parent.name);
        if (thing.gameObject.transform.parent.name == "Tank")
        {
            if (remainingItems == 1)
            {
                Application.LoadLevel(0);
            }
            else
            {
                remainingItems--;
                rigidbody.velocity.Set(0f, 0f, 0f);
                transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 20f);
            }
        }
        else if (thing.gameObject.name == "Goal")
        {
            Application.LoadLevel(0);
        }

//		Vector3 tempVect;
//		// we want to prevent isGrounded from being true and totalJumpsMade = 0 until 2 seconds later
//		if(isGrounded == false && canCheckForJump){
//			for(int i = 0; i < floor.contacts.Length; i++){
//				tempVect = floor.contacts[i].normal;
//				if( tempVect.y > floorInclineThreshold){
//					isGrounded = true;
//					totalJumpsMade = 0;
//					return;
//					//Manager.say("Collision normal is: " + tempVect);
//				}
//			}
//		}
	}
}
