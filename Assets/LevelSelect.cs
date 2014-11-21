using UnityEngine;
using System.Collections;

public class LevelSelect : MonoBehaviour {

    public GUIStyle levelSelectStyle;

    public GUIStyle leftButton;
    public GUIStyle rightButton;

    public float spaceWidth; 

    private bool leftPressed;
    private bool rightPressed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnGUI()
    {   
        Rect boxRect = new Rect(Screen.width/2f, Screen.height*.2f, 100f, 20f);
        GUI.Box(boxRect, "Select a level!", levelSelectStyle);

        Rect areaRect = new Rect(Screen.width*.2f, Screen.height*.3f, Screen.width, Screen.height*.3f);
        GUILayout.BeginArea(areaRect);
            GUILayout.BeginHorizontal();
                leftPressed = GUILayout.Button("Left", leftButton);
                GUILayout.Space(spaceWidth);
                rightPressed = GUILayout.Button("Right", rightButton);
            GUILayout.EndHorizontal();
        GUILayout.EndArea();
        //  GUILayout.BeginArea(
    }


}
