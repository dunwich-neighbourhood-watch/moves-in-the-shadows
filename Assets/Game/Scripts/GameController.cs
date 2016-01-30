using UnityEngine;
using System.Collections.Generic;

public class GameController : MonoBehaviour
{
    public Material arrowMaterial;
    public Material completedArrowMaterial;
    public int totalMoves = 5;

    private Timer game_time;
    private List<GameObject> quads;
    private List<int> generated_moves;
    private Transform quad_container;

    // Use this for initialization
    void Start()
    {
        quad_container = new GameObject("Quad Container").transform;
        quads = new List<GameObject>();

        for (int i = 0; i < totalMoves; ++i)
        {
            GameObject quad = GameObject.CreatePrimitive(PrimitiveType.Quad);

            quad.GetComponent<MeshRenderer>().material = arrowMaterial;
            quad.transform.position = new Vector3(i - 2.5f, 3, -2);

            int move = Random.Range(0, 3);
            Vector3 arrowRotation = new Vector3();
            switch (move)
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
            quad.transform.rotation = Quaternion.LookRotation(Vector3.forward, arrowRotation);
            quad.transform.SetParent(quad_container);
            quads.Add(quad);
            generated_moves.Add(move);
        }
    }

    // Update is called once per frame
    void Update()
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
