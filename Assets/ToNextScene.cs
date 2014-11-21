using UnityEngine;
using System.Collections;

public class ToNextScene : MonoBehaviour {
    
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if(Time.time > 10f){
            Application.LoadLevel(1);
        }
	}
}
