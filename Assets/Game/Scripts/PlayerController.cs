using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject player;
    public Sprite[] playerSprites = new Sprite[5];
    public AudioSource audioSource;
    public AudioClip[] audioClip = new AudioClip[4];
    public float timeBeforeIdle = 0.2f;

    private float time_till_idle;

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
            player.GetComponent<SpriteRenderer>().sprite = playerSprites[move + 1];
            time_till_idle = Time.time + timeBeforeIdle;
        }

        return move;
    }

    void Update ()
    {
        if (time_till_idle < Time.time)
        {
            player.GetComponent<SpriteRenderer>().sprite = playerSprites[0];
        }
    }
}
