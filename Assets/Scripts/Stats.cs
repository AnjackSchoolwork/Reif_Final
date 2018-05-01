using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Stats {
    
    private static int player_score;

    public static int Player_Score
    {
        get
        {
            return player_score;
        }
        set
        {
            player_score = value;
        }
    }
	
}
