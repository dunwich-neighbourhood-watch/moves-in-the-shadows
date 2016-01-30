using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float InputDelay = 0.2f;

    private float next_input = 0.0f;

    public AudioSource audioSource;
    public AudioClip[] audioClip = new AudioClip[4];

    public int ReturnMove ()
    {
        // move quads across screen
        // record player input
        int h_axis = (int)Input.GetAxis("Horizontal");
        int v_axis = (int)Input.GetAxis("Vertical");

        int move = -1;

        if (Time.time >= next_input)
        {
            // filter out double input
            if (h_axis != 0 && v_axis != 0)
            {
                // do nothing
            }
            else if (h_axis == 1)
            {
                move = 0;
                audioSource.PlayOneShot(audioClip[0]);
            }
            else if (h_axis == -1)
            {
                move = 1;
                audioSource.PlayOneShot(audioClip[1]);
            }
            else if (v_axis == 1)
            {
                move = 2;
                audioSource.PlayOneShot(audioClip[2]);
            }
            else if (v_axis == -1)
            {
                move = 3;
                audioSource.PlayOneShot(audioClip[3]);
            }
                next_input = Time.time + InputDelay;
            }
        return move;
    }
}
