using UnityEngine;
using System.Collections;

public class ItemSelect : MonoBehaviour {

    public GUIStyle ItemSelect1Style;
    public GUIStyle ItemSelect2Style;
    public GUIStyle ItemSelect3Style;
    public GUIStyle ItemSelect1ButtonStyle;
    public GUIStyle ItemSelect2ButtonStyle;
    public GUIStyle ItemSelect3ButtonStyle;
    public GUIStyle SelectionGUIStyle;
    public GUIStyle buttonAreaStyle;


    public float highscoreBoxX;
    public float highscoreBoxY;
    public GUIStyle highscoreStyle;

    private bool item1focus;
    private bool item2focus;
    private bool item3focus;
    private DataTracker DT;
	// Use this for initialization
	void Start () {
        DT = GameObject.Find("DataTracker").GetComponent<DataTracker>();
	}
	
	// Update is called once per frame
	void Update () {
        if (DT == null)
        {
            DT = GameObject.Find("DataTracker").GetComponent<DataTracker>();
        }
        if (item1focus)
        {
            DT.assignItemNum(1, 0);
            Application.LoadLevel(2);
        }
        if (item2focus)
        {
            DT.assignItemNum(1, 1);
            Application.LoadLevel(2);
        }
        if (item3focus)
        {
            DT.assignItemNum(1, 2);
            Application.LoadLevel(2);
        }
	}

    void OnGUI()
    {
        if (DT == null)
        {
            DT = GameObject.Find("DataTracker").GetComponent<DataTracker>();
        }

        Rect highScore = new Rect(Screen.width - 50f, 50f, highscoreBoxX, highscoreBoxY);
        GUI.Box(highScore, "Highscore: " + DT.getHighScore(), highscoreStyle);

        GUILayout.BeginHorizontal(GUILayout.Width(Screen.width));
            GUILayout.BeginVertical();
                Rect suppliesRect = new Rect(Screen.width/2, Screen.height/2*.7f, 10f, 10f);
                GUI.Box(suppliesRect, "Select a supplies container to deliver!", SelectionGUIStyle);
                GUILayout.BeginHorizontal();
                    Rect item1Rect = new Rect((Screen.width / 2)*.85f, Screen.height / 2 * .7f, 10f, 10f);
                    GUI.Box(item1Rect, "Faster Speed!", ItemSelect1Style);
                    Rect item2Rect = new Rect(Screen.width / 2, Screen.height / 2 * .7f, 10f, 10f);
                    GUI.Box(item2Rect, "Better Turn!", ItemSelect2Style);
                    Rect item3Rect = new Rect(Screen.width / 2*1.15f, Screen.height / 2 * .7f, 10f, 10f);
                    GUI.Box(item3Rect, "More Resources!", ItemSelect3Style);
                GUILayout.EndHorizontal();
            GUILayout.EndVertical();
        GUILayout.EndHorizontal();
        Rect buttonAreaRect = new Rect(Screen.width * .2f, Screen.height * .4f, Screen.width * .6f, Screen.height * .3f);
        GUILayout.BeginArea(buttonAreaRect, buttonAreaStyle);
            GUILayout.BeginHorizontal();
                item1focus = GUILayout.Button("", ItemSelect1ButtonStyle);
                if (Event.current.type == EventType.Repaint && GUILayoutUtility.GetLastRect().Contains(Event.current.mousePosition))
                {
                    GameObject.Find("Item1").GetComponent<Pedestal>().rotationSpeed = 100;
                }
                else
                {
                    GameObject.Find("Item1").GetComponent<Pedestal>().rotationSpeed = 10;
                } 
                item2focus = GUILayout.Button("", ItemSelect2ButtonStyle);
                if (Event.current.type == EventType.Repaint && GUILayoutUtility.GetLastRect().Contains(Event.current.mousePosition))
                {
                    GameObject.Find("Item2").GetComponent<Pedestal>().rotationSpeed = 100;
                }
                else
                {
                    GameObject.Find("Item2").GetComponent<Pedestal>().rotationSpeed = 10;
                }
                item3focus = GUILayout.Button("", ItemSelect3ButtonStyle);
                if (Event.current.type == EventType.Repaint && GUILayoutUtility.GetLastRect().Contains(Event.current.mousePosition))
                {
                    GameObject.Find("Item3").GetComponent<Pedestal>().rotationSpeed = 100;
                }
                else
                {
                    GameObject.Find("Item3").GetComponent<Pedestal>().rotationSpeed = 10;
                }
            GUILayout.EndHorizontal();
        GUILayout.EndArea();
    }
}
