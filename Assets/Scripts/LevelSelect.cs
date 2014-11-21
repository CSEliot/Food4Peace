using UnityEngine;
using System.Collections;

public class LevelSelect : MonoBehaviour {

    public GUIStyle levelSelectStyle;
    public GUIStyle startLevelStyle;

    public GUIStyle leftButton;
    public GUIStyle rightButton;

    private float spaceWidth;

    private bool prettySwitch;

    private bool leftPressed;
    private bool rightPressed;

    private int mapSwitch;

    private bool GOBUTTON;

    private GameObject level01;
    private GameObject level02;
    private GameObject level03;

	// Use this for initialization
	void Start () {
        GOBUTTON = false;
        mapSwitch = 0;
        GUI.color = Color.green;

        level01 = GameObject.Find("Level01");
        level02 = GameObject.Find("Level02");
        level03 = GameObject.Find("Level03");

	}
	
	// Update is called once per frame
	void Update () {
        spaceWidth = Screen.width*.3f;
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
            Application.LoadLevel(mapSwitch + 2);
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
        GUI.Box(boxRect, "Select a level!", levelSelectStyle);

        Rect areaRect = new Rect(Screen.width * .2f, Screen.height * .4f, Screen.width, Screen.height * .3f);
        GUILayout.BeginArea(areaRect);
            GUILayout.BeginHorizontal();
                leftPressed = GUILayout.Button("Left", leftButton);
                GUILayout.Space(spaceWidth);
                rightPressed = GUILayout.Button("Right", rightButton);
            GUILayout.EndHorizontal();
        GUILayout.EndArea();

        Rect enterRect = new Rect(Screen.width/2f, Screen.height*.8f, 100f, 20f);
        GOBUTTON = GUI.Button(enterRect, "Start!", startLevelStyle);
    }


}
