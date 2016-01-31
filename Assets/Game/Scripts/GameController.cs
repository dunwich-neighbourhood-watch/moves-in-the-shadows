using UnityEngine;
using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Material arrowMaterial;
    public Material completedArrowMaterial;
    public Sprite[] cultistSprites = new Sprite[6];
    public int minMoves = 3;
    public int maxMoves = 7;
    public int cultistRowPopulation = 7;
    public int cultistRowCount = 4;
    public float cultMoveTime = 0.25f;
    public PlayerController playerController;
    public Text ritualName;
    public Text ritualStreak;
    
    private List<GameObject> arrows;
    private Transform arrow_container;

    private int total_moves;
    private List<int> correct_moves;
    private int player_move;
    private int cult_move;
    private float cult_next_move;
    private bool cult_idle_next;
    private List<GameObject> cultists;
    private Transform cultist_container;
    private int ritual_streak = 0;

    // Use this for initialization
    void Start()
    {
        GenerateRitual();
        InitialiseCultists();
    }

    // Update is called once per frame
    void Update()
    {
        if (player_move != total_moves)
        {
            if (player_move < total_moves)
            {
                int playerMove = playerController.ReturnMove();
                if (correct_moves[player_move] == playerMove)
                {
                    // change texture
                    arrows[player_move].GetComponent<MeshRenderer>().material = completedArrowMaterial;

                    // progress
                    ++player_move;
                }
                else if (playerMove != -1)
                {
                    // reset texture and progress
                    for (int i = 0; i < player_move; ++i)
                    {
                        arrows[i].GetComponent<MeshRenderer>().material = arrowMaterial;
                    }
                    player_move = 0;
                    ritual_streak = 0;
                    UpdateRitualStreak();
                }
                return;
            }
            else if (player_move != total_moves)
            {
                throw new Exception("Invalid state, current_move greater than total_moves");
            }
        }
        else if (Time.time > cult_next_move)
        {
            if (cult_move != total_moves)
            {
                if (cult_idle_next)
                {
                    cultists.ForEach(c =>
                        c.GetComponent<SpriteRenderer>().sprite = cultistSprites[0]);
                    cult_idle_next = false;
                }
                else {
                    var move = correct_moves[cult_move];
                    cultists.ForEach(c =>
                        c.GetComponent<SpriteRenderer>().sprite = cultistSprites[move + 1]);
                    cult_move += 1;
                    cult_idle_next = true;
                }
                cult_next_move = Time.time + cultMoveTime;
            }
            else
            {
                ritual_streak += 1;
                cultists.ForEach(c => c.GetComponent<SpriteRenderer>().sprite = cultistSprites[5]);
                arrows.ForEach(a => Destroy(a));
                Destroy(arrow_container.gameObject);
                GenerateRitual();
            }
        }
    }

    private void GenerateRitual()
    {
        ritualName.text = RitualNames[Random.Range(0, RitualNames.Count)];

        arrow_container = new GameObject("Arrow Container").transform;
        correct_moves = new List<int>();
        arrows = new List<GameObject>();
        total_moves = Random.Range(minMoves, maxMoves + 1);

        for (int i = 0; i < total_moves; ++i)
        {
            GameObject arrow = GameObject.CreatePrimitive(PrimitiveType.Quad);

            arrow.GetComponent<MeshRenderer>().material = arrowMaterial;
            arrow.transform.position = new Vector3(i + (1 - total_moves) / 2f, 3.5f, -1f);

            int move = Random.Range(0, 3);
            Vector3 arrowRotation = ArrowRotationForMove(move);

            arrow.transform.rotation = Quaternion.LookRotation(Vector3.forward, arrowRotation);
            arrow.transform.SetParent(arrow_container);
            arrows.Add(arrow);
            correct_moves.Add(move);
        }

        player_move = 0;
        cult_move = 0;
        cult_idle_next = true;
        UpdateRitualStreak();
    }

    private void InitialiseCultists()
    {
        cultist_container = new GameObject("Cultist Container").transform;
        cultists = new List<GameObject>();
        for (int i = 0; i < cultistRowCount; ++i) {
            for (int j = 0; j < cultistRowPopulation; ++j)
            {
                GameObject cultist = new GameObject("Cultist");

                cultist.AddComponent<SpriteRenderer>();
                cultist.GetComponent<SpriteRenderer>().sprite = cultistSprites[5];
                cultist.transform.position = new Vector3(1.25f * (j + (1 - cultistRowPopulation) / 2f), -1.25f * i, -1f);
                cultist.transform.SetParent(cultist_container);

                cultists.Add(cultist);
            }
        }
    }

    private void UpdateRitualStreak()
    {
        ritualStreak.text = "Ritual Streak: " + ritual_streak + new string('!', (int)Math.Floor(Math.Log(ritual_streak + 1, 2)));
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
