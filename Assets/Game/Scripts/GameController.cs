using UnityEngine;
using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Material arrowMaterial;
    public Material completedArrowMaterial;
    public int minMoves = 3;
    public int maxMoves = 7;
    public PlayerController playerController;
    public Text ritualName;

    private int total_moves;
    private List<GameObject> quads;
    private List<int> generated_moves;
    private Transform quad_container;
    private int current_move = 0;

    // Use this for initialization
    void Start()
    {
        quad_container = new GameObject("Quad Container").transform;
        quads = new List<GameObject>();
        generated_moves = new List<int>();
        total_moves = Random.Range(minMoves, maxMoves + 1);
        ritualName.text = RitualNames[Random.Range(0, RitualNames.Count)];

        for (int i = 0; i < total_moves; ++i)
        {
            GameObject quad = GameObject.CreatePrimitive(PrimitiveType.Quad);

            quad.GetComponent<MeshRenderer>().material = arrowMaterial;
            quad.transform.position = new Vector3(i + (1 - total_moves) / 2f, 3, -2);

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
        if (current_move < total_moves)
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
                for (int i = 0; i < current_move; ++i)
                {
                    quads[i].GetComponent<MeshRenderer>().material = arrowMaterial;
                }
                current_move = 0;
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
