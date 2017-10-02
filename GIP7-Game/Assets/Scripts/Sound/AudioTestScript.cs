using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTestScript : MonoBehaviour {

    private SoundManager sm;

	// Use this for initialization
	void Start () {
        sm = GameObject.FindObjectOfType<SoundManager>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            sm.Play();
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            sm.Pause();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            sm.UnPause();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            sm.Stop();
        }
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            sm.ObjectSounds[0].PlayAudioClip(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            sm.ObjectSounds[0].PlayAudioClip(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            sm.ObjectSounds[0].PlayAudioClip(2);
        }
    }
}
