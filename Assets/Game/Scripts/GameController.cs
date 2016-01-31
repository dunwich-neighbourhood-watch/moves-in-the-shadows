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
    public int totalCultists = 5;
    public PlayerController playerController;
    public Text ritualName;

    private int total_moves;
    private List<int> correct_moves;
    private int current_move = 0;
    private List<GameObject> arrows;
    private Transform arrow_container;
    private List<GameObject> cultists;
    private bool player_finished = false;

    // Use this for initialization
    void Start()
    {
        arrow_container = new GameObject("Arrow Container").transform;
        correct_moves = new List<int>();
        arrows = new List<GameObject>();
        cultists = new List<GameObject>();
        total_moves = Random.Range(minMoves, maxMoves + 1);
        ritualName.text = RitualNames[Random.Range(0, RitualNames.Count)];

        for (int i = 0; i < total_moves; ++i)
        {
            GameObject arrow = GameObject.CreatePrimitive(PrimitiveType.Quad);

            arrow.GetComponent<MeshRenderer>().material = arrowMaterial;
            arrow.transform.position = new Vector3(i + (1 - total_moves) / 2f, 3f, -1f);

            int move = Random.Range(0, 3);
            Vector3 arrowRotation = ArrowRotationForMove(move);

            arrow.transform.rotation = Quaternion.LookRotation(Vector3.forward, arrowRotation);
            arrow.transform.SetParent(arrow_container);
            arrows.Add(arrow);
            correct_moves.Add(move);
        }

        for (int i = 0; i < totalCultists; ++i)
        {
            GameObject cultist = GameObject.CreatePrimitive(PrimitiveType.Quad);

            cultist.GetComponent<MeshRenderer>().material = arrowMaterial;
            cultist.transform.position = new Vector3(1.25f*i + (1 - totalCultists) / 2f, -2f, -1f);
            
            cultists.Add(cultist);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!player_finished)
        {
            if (current_move < total_moves)
            {
                int playerMove = playerController.ReturnMove();
                if (correct_moves[current_move] == playerMove)
                {
                    // change texture
                    arrows[current_move].GetComponent<MeshRenderer>().material = completedArrowMaterial;

                    // progress
                    ++current_move;
                }
                else if (playerMove != -1)
                {
                    // reset texture and progress
                    for (int i = 0; i < current_move; ++i)
                    {
                        arrows[i].GetComponent<MeshRenderer>().material = arrowMaterial;
                    }
                    current_move = 0;
                }
            }
            else if (current_move == total_moves)
            {
                player_finished = true;
            }
            else
            {
                // throw error
            }
        }
        else if (false)//!picking)
        {
            // start cultist animation
            
        }
        else
        {
            //throw error
        }
    }

    private Vector3 ArrowRotationForMove(int move)
    {
        switch (move)
        {
            case 0:
                // right arrow
                return Vector3.up;
            case 1:
                // left arrow
                return Vector3.down;
            case 2:
                // up arrow
                return Vector3.left;
            case 3:
                // down arrow
                return Vector3.right;
            default:
                throw new Exception("Not a valid move.");
        }
    }

    private readonly List<string> RitualNames = new List<string>
        {
            "Elder Swing",
            "Hastur's Hustle",
            "Nyarlathotep Two-Step",
            "Iä! Iä! Cthulhu fh'Tango!",
            "Yog-Sothoth Yablochko",
            "The Great Rumba of Yith",
            "The Samba Out of Time",
            "Moshkatonic University",
            "Flying Polyp Polka",
            "Jitterbugg-Shash",
            "Dzéwà Danza",
            "The Nameless Twist"
        };
}
