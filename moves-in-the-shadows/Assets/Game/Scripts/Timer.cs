using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour {

    private float start_time;
    private float current_time;

	// Use this for initialization
	void Start ()
    {
        start_time = Time.timeSinceLevelLoad;
    }
	
	// Update is called once per frame
	void Update ()
    {
        current_time = Time.timeSinceLevelLoad;
    }

    public float GetStart ()
    {
        return start_time;
    }

    public float GetCurrent ()
    {
        return current_time;
    }
}
