using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    private Transform playerTransfrom;
    private Player playerScript;

    public float cameraOffset;

    public float horizontalMoveSpeed = 0.5f;
    public float verticleMoveSpeed = 0.1f;

    private Transform thisTransfrom;
    private Vector3 tragetPos;
    private Vector3 currentPos;
    private Vector3 smoothPos;

    private bool isStarted = false;
    Vector2 levelCameraDimension;

    private GameObject background;
    private Transform clouds;
    private Transform higherHill;
    private Transform lowerHill;
    private float lhmaxy;
    private float hhmaxy;

    public void AddPlayer(GameObject player)
    {
        Vector2 topRightCorner = new Vector2(1, 1);
        levelCameraDimension = Camera.main.ViewportToWorldPoint(topRightCorner);

        this.player = player;
        this.background = player.GetComponent<PlayerController>().background;

        if (background == null)
        {
            Debug.LogError("No background set for this level!");
        }
        else
        {
            clouds = background.transform.GetChild(0);
            higherHill = background.transform.GetChild(1);
            hhmaxy = higherHill.position.y;
            hhmaxy += 2;
            lowerHill = background.transform.GetChild(2);
            lhmaxy = lowerHill.position.y;

            Camera.main.backgroundColor = new Color(135/255f, 205/255f, 1);
        }

        thisTransfrom = this.transform;

        currentPos = thisTransfrom.position;
        playerTransfrom = player.transform;
        playerScript = player.GetComponent<Player>();

        isStarted = true;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (!isStarted) return;

        tragetPos = new Vector3(playerTransfrom.position.x + cameraOffset * playerScript.playerInfo.direction, playerTransfrom.position.y, thisTransfrom.position.z);
        currentPos.x = Mathf.SmoothDamp(currentPos.x, tragetPos.x, ref smoothPos.x, horizontalMoveSpeed);
        currentPos.y = Mathf.SmoothDamp(currentPos.y, tragetPos.y, ref smoothPos.y, verticleMoveSpeed);

        //Limit max and min X
        if (currentPos.x + levelCameraDimension.x + 0.5f > LevelController.maxX)
        {
            currentPos.x = LevelController.maxX - levelCameraDimension.x - 0.5f;
        }
        else if (currentPos.x - levelCameraDimension.x - 0.5f < LevelController.minX)
        {
            currentPos.x = LevelController.minX + levelCameraDimension.x + 0.5f;
        }
        thisTransfrom.position = currentPos;

        //Do something with the background
        if (background != null)
        {
            Vector3 bgpos = new Vector3(currentPos.x, background.transform.position.y);
            background.transform.position = bgpos;

            //Clouds
            SetBG(clouds, 100, new Vector3(bgpos.x, bgpos.y + 3));

            //Higher hill
            SetBG(higherHill, hhmaxy, bgpos);

            //Lower hill
            SetBG(lowerHill, lhmaxy, bgpos);
        }
    }

    /// <summary>
    /// Sets the background part to move with the current position
    /// </summary>
    /// <param name="part"></param>
    /// <param name="max"></param>
    /// <param name="bg"></param>
    private void SetBG(Transform part, float max, Vector3? bg = null)
    {
        Vector3 bgu = bg.HasValue ? bg.Value : part.localPosition;
        float y = currentPos.y;
        float difference = bgu.y - y;
        if (difference < 0)
        {
            difference = 0;
        }
        y += difference / 2;
        if (y > max)
        {
            y = max;
        }
        Vector3 post = new Vector3(currentPos.x, y);

        part.position = post;
    }
}
