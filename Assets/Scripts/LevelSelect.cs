using UnityEngine;
using System.Collections;

public class LevelSelect : MonoBehaviour {

    public GUIStyle levelSelectStyle;
    public GUIStyle startLevelStyle;

    public GUIStyle leftButton;
    public GUIStyle rightButton;

    public AudioSource audio2;

    private float spaceWidth;

    private bool prettySwitch;

    private bool leftPressed;
    private bool rightPressed;

    public Texture leftArrow;
    public Texture rightArrow;
    public Texture enterButton;

    private int mapSwitch;

    private bool GOBUTTON;

    private GameObject level01;
    private GameObject level02;
    private GameObject level03;

    public float spaceMod;
	// Use this for initialization
	void Start () {
        GOBUTTON = false;
        mapSwitch = 0;
        GUI.color = Color.green;

        level01 = GameObject.Find("Level01");
        level02 = GameObject.Find("Level02");
        level03 = GameObject.Find("Level03");

        spaceWidth = Screen.width * spaceMod;
	}
	
	// Update is called once per frame
	void Update () {

        spaceWidth = Screen.width * spaceMod;

        Debug.Log("Left: " + leftPressed);
        Debug.Log("Rt: " + rightPressed);
        if (leftPressed)
        {
            mapSwitch--;
            if (mapSwitch < 0)
            {
                mapSwitch = 0;
            }
            else if (mapSwitch > 2)
            {
                mapSwitch = 2;
            }
        }
        if (rightPressed)
        {
            mapSwitch++;
            if (mapSwitch < 0)
            {
                mapSwitch = 0;
            }
            else if (mapSwitch > 2)
            {
                mapSwitch = 2;
            }
        }

        if (mapSwitch == 0)
        {
            level01.transform.GetChild(0).gameObject.SetActive(true);
            level02.transform.GetChild(0).gameObject.SetActive(false);
            level03.transform.GetChild(0).gameObject.SetActive(false);
            level01.transform.GetChild(1).gameObject.SetActive(true);
            level02.transform.GetChild(1).gameObject.SetActive(false);
            level03.transform.GetChild(1).gameObject.SetActive(false);

        }
        else if (mapSwitch == 1)
        {
            level01.transform.GetChild(0).gameObject.SetActive(false);
            level02.transform.GetChild(0).gameObject.SetActive(true);
            level03.transform.GetChild(0).gameObject.SetActive(false);
            level01.transform.GetChild(1).gameObject.SetActive(false);
            level02.transform.GetChild(1).gameObject.SetActive(true);
            level03.transform.GetChild(1).gameObject.SetActive(false);
        }
        else if (mapSwitch == 2)
        {
            level01.transform.GetChild(0).gameObject.SetActive(false);
            level02.transform.GetChild(0).gameObject.SetActive(false);
            level03.transform.GetChild(0).gameObject.SetActive(true);
            level01.transform.GetChild(1).gameObject.SetActive(false);
            level02.transform.GetChild(1).gameObject.SetActive(false);
            level03.transform.GetChild(1).gameObject.SetActive(true);
        }

        if (GOBUTTON)
        {
            Debug.Log("GOBUTTON IS GO!");
            Application.LoadLevel(mapSwitch + 3);

        }
	
	}

    void OnGUI()
    {
        /*
        if (leftButton.normal.textColor != Color.red)
        {
            leftButton.normal.textColor = Color.Lerp(Color.green, Color.red, Time.time / 2);
        }
        else
        {
            leftButton.normal.textColor = Color.Lerp(Color.red, Color.green, Time.time / 2);
        }*/

        Rect boxRect = new Rect(Screen.width/2f, Screen.height*.2f, 100f, 20f);
        levelSelectStyle.normal.textColor = Color.white;
        GUI.Box(boxRect, "Select a level!", levelSelectStyle);

        Rect areaRect = new Rect(Screen.width * .03f, Screen.height * .4f, Screen.width, Screen.height * .3f);
        GUILayout.BeginArea(areaRect);
            GUILayout.BeginHorizontal();
                leftPressed = GUILayout.Button(leftArrow, leftButton);
                GUILayout.Space(spaceWidth);
                rightPressed = GUILayout.Button(rightArrow, rightButton);
            GUILayout.EndHorizontal();
        GUILayout.EndArea();

        Rect enterRect = new Rect(Screen.width/2f-50f, Screen.height*.75f, 100f, 100f);
        GOBUTTON = GUI.Button(enterRect, enterButton);
    }


}
