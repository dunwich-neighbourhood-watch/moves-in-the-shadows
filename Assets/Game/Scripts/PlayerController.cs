using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public int ReturnMove ()
    {
        // move quads across screen
        // record player input
        int h_axis = (int)Input.GetAxis("Horizontal");
        int v_axis = (int)Input.GetAxis("Vertical");

        int move = -1;

        // filter out double input
        if ((h_axis != 0) && (v_axis != 0))
        {
            // do nothing
        }
        else if (h_axis == 1) move = 0;
        else if (h_axis == -1) move = 1;
        else if (v_axis == 1) move = 2;
        else if (v_axis == -1) move = 3;

        return move;
    }
}
