using UnityEngine;
using System.Collections;

public class DataTracker : MonoBehaviour {

    void Awake()
    {
        DontDestroyOnLoad(this);
    }

    private int p1_item;
    private int p2_item;
    private int p3_item;
    private int p4_item;
    
    private int highScore;

	// Use this for initialization
	void Start () {
        p1_item = 0;
        p2_item = 0;
        p3_item = 0;
        p4_item = 0;
        highScore = 0;

        //audio.Play();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void setHighscore(int score)
    {
        highScore = score;
    }

    public int getHighScore()
    {
        return highScore;
    }

    public void assignItemNum(int player, int item)
    {
        if (player == 1)
        {
            p1_item = item;
        }
        else if (player == 2)
        {
            p2_item = item;
        }
        else if (player == 3)
        {
            p3_item = item;
        }
        else
        {
            p4_item = item;
        }
    }

    public int getItemNum(int player){
        if (player == 1)
        {
            return p1_item;
        }
        else if(player == 2)
        {
            return p2_item;
        }
        else if (player == 3)
        {
            return p3_item;
        }
        else
        {
            return p4_item;
        }
    }
}