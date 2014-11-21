using UnityEngine;
using System.Collections;

public class DataTracker : MonoBehaviour {

    void Awake()
    {
        DontDestroyOnLoad(this);
    }

    private int p1_item;
    

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void assignItemNum(int player, int item)
    {
        //0: water, 1: grain, 2:soup
        p1_item = item;
    }
}
