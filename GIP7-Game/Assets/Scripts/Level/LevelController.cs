using Assets.Scripts.Base;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    //Dimension
    public static float maxX;
    public static float maxY;
    public static float minX;
    public static float minY;

    void Start()
    {
        //Get dimensions
        GameObject[] allObjects = FindObjectsOfType<GameObject>();
        foreach (GameObject current in allObjects)
        {
            //Of all visible things
            if (current.GetComponent<MeshRenderer>() != null)
            {
                float x = current.transform.position.x;
                float y = current.transform.position.y;
                maxX = x > maxX ? x : maxX;
                maxY = y > maxY ? y : maxY;
                minX = x > minX ? minX : x;
                minY = y > minY ? minY : y;
            }
        }

        if (MobileHelper.OnEditor)
            Debug.Log("Level dimension: {" + minX + "," + minY + "},{" + maxX + "," + maxY + "}");
    }
}
