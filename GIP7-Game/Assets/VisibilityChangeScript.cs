using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisibilityChangeScript : MonoBehaviour {

    public static bool activate = true;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        foreach (Transform child in this.transform)
        {
            if (activate == false)
            {
                child.gameObject.SetActive(false);
            }
            else if (activate == true)
            {
                child.gameObject.SetActive(true);
            }
        }
    }
}
