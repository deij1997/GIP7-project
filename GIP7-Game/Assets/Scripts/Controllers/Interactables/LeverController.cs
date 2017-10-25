using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverController : MonoBehaviour {

	public int allowedHits = 1;
	public List<ChangingObject> objectList;

	private int currentHitCount = 0;
    public GameObject player;
    public PlayerController playerControl;
    public float time;
    public float targetTime;
    public bool requiresKey;

    void awake()
    {
        player = GameObject.Find("Player");
        playerControl = (PlayerController)player.GetComponent("PlayerController");
    }

    void OnMouseDown()
    {
        Hit();
    }

    public void Hit()
	{
        player = GameObject.Find("Player");
        playerControl = (PlayerController)player.GetComponent("PlayerController");

		if (currentHitCount < allowedHits)
		{
            currentHitCount++;

            // Play sound
            if (SoundManager.Instance != null)
            {
                SoundManager.Instance.ObjectSounds[0].PlayAudioClip(1);
            }

            StartTimer(time);

            /*
            foreach (ChangingObject cObject in objectList)
			{
				if (cObject.amount != 0)
				{
					cObject.objectToChange.GetComponent<StraightMovementController>().moveSpeed += cObject.amount;
				}


                cObject.objectToChange.SetActive(cObject.enabled);
			}
            */

            VisibilityChangeScript.activate = false;

            // Change sprite
            Texture triggered = (Texture)Resources.Load("leverLeft");
            gameObject.GetComponent<Renderer>().material.SetTexture("_MainTex", triggered);
		} 
	}

    void Update()
    {

        targetTime -= Time.deltaTime;

        if (targetTime <= 0.0f)
        {
            timerEnded();
        }

    }

    public void StartTimer(float seconds)
    {
        targetTime = seconds;
    }

    void timerEnded()
    {
        //do your stuff here.
        playerControl.hasKey = false;

        VisibilityChangeScript.activate = true;

        StartTimer(time);

        /*
        foreach (ChangingObject cObject in objectList)
        {
            if (cObject.amount != 0)
            {
                cObject.objectToChange.GetComponent<StraightMovementController>().moveSpeed += cObject.amount;
            }


            cObject.objectToChange.SetActive(cObject.enabled);
        }
        */
    }

	[System.Serializable]
	public class ChangingObject
	{
		public GameObject objectToChange;
		public float amount = 0;
		public bool enabled = true;
	}
}
