using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

    private Timer game_time;

    private int total_moves;
    private ArrayList generated_moves;
    private ArrayList player_moves;

	// Use this for initialization
	void Start () {
        game_time = new Timer();
        total_moves = 5;

        for (int i = 0; i <= total_moves; ++i)
        {
            generated_moves.Add(Random.Range(0, 3));
        }
	}
	
	// Update is called once per frame
	void Update () {
	    if (game_time.GetCurrent() <= 10.0f)
        {
            // wait
        }
        else
        {
            // create quads with moves on
            // move quads across screen

        }
	}
}
