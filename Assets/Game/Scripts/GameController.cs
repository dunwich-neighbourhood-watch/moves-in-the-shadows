using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {

    public Material arrowMaterial;
    public Material completedArrowMaterial;

    private Timer game_time;

    private int total_moves;
    private List<int> generated_moves;
    private List<GameObject> quads;

	// Use this for initialization
	void Start ()
    {
        generated_moves = new List<int>();
        quads = new List<GameObject>();
        total_moves = 5;

        for (int i = 0; i < total_moves; ++i)
        {
            int move = Random.Range(0, 3);
            generated_moves.Add(move);
        }

        // create quads with moves on
        foreach (int move in generated_moves)
        {
            quads.Add(GameObject.CreatePrimitive(PrimitiveType.Quad));
            
        }
        for (int i = 0; i < quads.Count; i++)
        {
            quads[i].GetComponent<MeshRenderer>().material = arrowMaterial;
            quads[i].transform.position = new Vector3(i - 2.5f, 3, -2);

            Vector3 arrowRotation = new Vector3();

            switch (generated_moves[i])
            {
                case 0:
                    arrowRotation = Vector3.up;
                    break;
                case 1:
                    arrowRotation = Vector3.down;
                    break;
                case 2:
                    arrowRotation = Vector3.left;
                    break;
                case 3:
                    arrowRotation = Vector3.right;
                    break;
                default:
                    break;
            }
            quads[i].transform.rotation = Quaternion.LookRotation(Vector3.forward, arrowRotation);
        }

    }
	
	// Update is called once per frame
	void Update ()
    {
	    if (true)//game_time.GetCurrent() <= 10.0f)
        {
            // wait
        }
        else
        {
            // move quads across screen
            // record player input
        }
	}
}
