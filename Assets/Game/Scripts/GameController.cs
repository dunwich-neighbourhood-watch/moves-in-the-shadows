using UnityEngine;
using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class GameController : MonoBehaviour
{
    public Material arrowMaterial;
    public Material completedArrowMaterial;
    public int totalMoves = 5;

    public PlayerController playerController;

    private List<GameObject> quads;
    private List<int> generated_moves;
    private Transform quad_container;
    private int current_move = 0;

    // Use this for initialization
    void Start() { 
        quad_container = new GameObject("Quad Container").transform;
        quads = new List<GameObject>();
        generated_moves = new List<int>();

        for (int i = 0; i < totalMoves; ++i)
        {
            GameObject quad = GameObject.CreatePrimitive(PrimitiveType.Quad);

            quad.GetComponent<MeshRenderer>().material = arrowMaterial;
            quad.transform.position = new Vector3(i - 2.5f, 3, -2);

            int move = Random.Range(0, 3);
            Vector3 arrowRotation = ArrowRotationForMove(move);

            quad.transform.rotation = Quaternion.LookRotation(Vector3.forward, arrowRotation);
            quad.transform.SetParent(quad_container);
            quads.Add(quad);
            generated_moves.Add(move);
        }
    }

    // Update is called once per frame
    void Update()
    {
        int playerMove = playerController.ReturnMove();
        if (generated_moves[current_move] == playerMove)
        {
            //change texture
            quads[current_move].GetComponent<MeshRenderer>().material = completedArrowMaterial;

            // play animation

            // progress
            ++current_move;
        }
        else if (playerMove != -1)
        {
            // play animation
            // reset
            current_move = 0;
            for(int i = 0; i < current_move; ++i)
            {
                quads[i].GetComponent<MeshRenderer>().material = arrowMaterial;
            }
        }
}

    private Vector3 ArrowRotationForMove(int move)
    {
        switch (move)
        {
            case 0:
                return Vector3.up;
            case 1:
                return Vector3.down;
            case 2:
                return Vector3.left;
            case 3:
                return Vector3.right;
            default:
                throw new Exception("Not a valid move.");
        }
    }

    private readonly List<string> RitualNames = new List<string>
        {
            "Elder Thing Swing",
            "Hastur's Hustle",
            "Nyarlathotep Two-Step",
            "Iä! Iä! Cthulhu fh'Tango!"
        };
}
