using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyController : MonoBehaviour {

    public int allowedHits = 1;
    public List<ChangingObject> objectList;

    private int currentHitCount = 0;
    public GameObject player;
    public PlayerController playerControl;

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

            playerControl.hasKey = true;

            foreach (ChangingObject cObject in objectList)
            {
                if (cObject.amount != 0)
                {
                    cObject.objectToChange.GetComponent<StraightMovementController>().moveSpeed += cObject.amount;
                }


                cObject.objectToChange.SetActive(cObject.enabled);
            }

            // Change sprite
            Texture triggered = (Texture)Resources.Load("leverLeft");
            gameObject.GetComponent<Renderer>().material.SetTexture("_MainTex", triggered);
        }
    }

    [System.Serializable]
    public class ChangingObject
    {
        public GameObject objectToChange;
        public float amount = 0;
        public bool enabled = true;
    }
}
