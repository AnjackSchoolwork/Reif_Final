using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour {

    public Text health_text;
    public Text score_text;

    PlayerController player_controller;

    int score = 0;

	// Use this for initialization
	void Start () {
        player_controller = GameObject.FindGameObjectWithTag("player").GetComponent<PlayerController>();
        health_text.text = player_controller.health.ToString();
        score_text.text = score.ToString();
	}
	
	public void increment_score(int increment_value)
    {
        score += increment_value;
        
        score_text.text = score.ToString();
    }

    public void update_health(int health_value)
    {
        health_text.text = health_value.ToString();
    }
}
