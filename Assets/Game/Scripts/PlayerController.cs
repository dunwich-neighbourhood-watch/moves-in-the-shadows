using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject player;

    public Material[] playerMaterials = new Material[5];

    public AudioSource audioSource;
    public AudioClip[] audioClip = new AudioClip[4];

    private float time_till_next_move;

    public int ReturnMove()
    {

        int move = -1;
        

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            move = 0;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            move = 1;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            move = 2;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            move = 3;
        }

        if (move != -1)
        {
            audioSource.PlayOneShot(audioClip[move]);
            player.GetComponent<MeshRenderer>().material = playerMaterials[move + 1];
            time_till_next_move = Time.time + 0.5f;
        }

        return move;
    }

    void Update ()
    {
        if (time_till_next_move < Time.time)
        {
            player.GetComponent<MeshRenderer>().material = playerMaterials[0];
        }
    }
}
