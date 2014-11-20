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

    private bool item1focus;
    private bool item2focus;
    private bool item3focus;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (item1focus)
        {
            
            //GameObject.Find("Item1").GetComponent<Pedestal>().rotationSpeed = 50;
        }
	}

    void OnGUI()
    {
        GUILayout.BeginHorizontal(GUILayout.Width(Screen.width));
            GUILayout.BeginVertical();
                Rect suppliesRect = new Rect(Screen.width/2, Screen.height/2*.7f, 10f, 10f);
                GUI.Box(suppliesRect, "Select a supplies container to deliver!", SelectionGUIStyle);
                GUILayout.BeginHorizontal();
                    Rect item1Rect = new Rect(Screen.width / 2*.85f, Screen.height / 2 * .7f, 10f, 10f);
                    GUI.Box(item1Rect, "Grain", ItemSelect1Style);
                    Rect item2Rect = new Rect(Screen.width / 2, Screen.height / 2 * .7f, 10f, 10f);
                    GUI.Box(item2Rect, "Soup", ItemSelect2Style);
                    Rect item3Rect = new Rect(Screen.width / 2*1.15f, Screen.height / 2 * .7f, 10f, 10f);
                    GUI.Box(item3Rect, "Water", ItemSelect3Style);
                GUILayout.EndHorizontal();
            GUILayout.EndVertical();
        GUILayout.EndHorizontal();
        Rect buttonAreaRect = new Rect(Screen.width*.3f, Screen.height*.3, )
        GUILayout.BeginArea()
                item1focus = GUILayout.Button("Blah", ItemSelect1ButtonStyle);
                if (Event.current.type == EventType.Repaint && GUILayoutUtility.GetLastRect().Contains(Event.current.mousePosition))
                {
                    GameObject.Find("Item1").GetComponent<Pedestal>().rotationSpeed = 100;
                }
                else
                {
                    GameObject.Find("Item1").GetComponent<Pedestal>().rotationSpeed = 10;
                } 
                item2focus = GUILayout.Button("Blah", ItemSelect2ButtonStyle);
                if (Event.current.type == EventType.Repaint && GUILayoutUtility.GetLastRect().Contains(Event.current.mousePosition))
                {
                    GameObject.Find("Item2").GetComponent<Pedestal>().rotationSpeed = 100;
                }
                else
                {
                    GameObject.Find("Item2").GetComponent<Pedestal>().rotationSpeed = 10;
                }
                item3focus = GUILayout.Button("Blah", ItemSelect3ButtonStyle);
                if (Event.current.type == EventType.Repaint && GUILayoutUtility.GetLastRect().Contains(Event.current.mousePosition))
                {
                    GameObject.Find("Item3").GetComponent<Pedestal>().rotationSpeed = 100;
                }
                else
                {
                    GameObject.Find("Item3").GetComponent<Pedestal>().rotationSpeed = 10;
                }
    }
}
