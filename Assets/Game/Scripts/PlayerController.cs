using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] audioClip = new AudioClip[4];

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
        }

        return move;
    }
}
