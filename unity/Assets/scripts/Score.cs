using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Score : MonoBehaviour {
    public int points = 0;
    public Rect scorePostion = new Rect(10, 10, 60, 20);
    public Text scoreText;

    // Use this for initialization
    void Start () {
        points = 0;
        updateScore(0);
    }
	
	// Update is called once per frame
	void Update () {
	    
	}

    void updateScore(int value) {
        Debug.Log("updateScore => " + value);
        points += value;
        scoreText.text = "Score: " + points;
    }
}
