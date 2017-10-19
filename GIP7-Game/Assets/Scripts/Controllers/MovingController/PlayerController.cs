using UnityEngine;
using System.Collections;
using System.Linq;


public class PlayerController : BaseController
{
    public float interactRange = 1f;
    public float exitRange = 0.5f;

    public CollisionInfo collInfo;

    public LayerMask interactMask;
    public LayerMask exitMask;

    public GameObject background;
    private GameObject interactable;
    private GameObject exit;
    private bool interactionPossibleSoundPlayed;
    public bool hasKey = false;

    public GameObject canvas;

    private CanvasScript canvasScript;

    protected override void Awake()
    {
        base.Awake();

        if (canvas == null)
        {
            canvas = (GameObject)Instantiate(Resources.Load("PlayerMenu"));
        }

        Instantiate(Resources.Load("LevelDetails"));

        canvasScript = canvas.GetComponent<CanvasScript>();
        foreach (GameObject c in GameObject.FindGameObjectsWithTag("NPC"))
        {
            c.GetComponent<NPCController>().SetCanvas(canvasScript);
        }

        foreach (GameObject c in GameObject.FindGameObjectsWithTag("Monty"))
        {
            c.GetComponent<MontyController>().SetCanvas(canvasScript);
        }
    }

    /// <summary>
    /// This is the standard move method.
    /// Takes the velocity at which the player should be moved.
    /// </summary>
    /// <param name="velocity"></param>
    public void Move(Vector3 velocity)
    {
        velocity *= Time.deltaTime;

        //Update the raycast and reset collissions
        rayCaster.UpdateRayCastOrigins(InnerBounds());
        collInfo.Reset();

        //Handle the collissions for both horizontal as vertical
        HandleCollision(ref velocity);

        //Translate the new speed
        thisTransform.Translate(velocity);

        //Check if after moving, a NPC is in range
        CheckInteractable();

        //Check if after moving, the exit is in range
        CheckForExit();
    }

    /// <summary>
    /// Moves the player when on a platform.
    /// Takes the platform velocity to move the player at equal speed.
    /// </summary>
    /// <param name="velocity"></param>
    public void PlatformMove(Vector3 velocity)
    {
        rayCaster.UpdateRayCastOrigins(InnerBounds());
        HandleCollision(ref velocity, true);
        thisTransform.Translate(velocity);
    }

    /// <summary>
    /// If there is an item to interact with, interact with it.
    /// </summary>
    public void Interact()
    {
        if (interactable != null)
        {
            if (interactable.CompareTag("NPC")) interactable.GetComponent<NPCController>().Talk();
            if (interactable.CompareTag("Key")) interactable.GetComponent<KeyController>().Hit();
            if (interactable.CompareTag("Lever") && hasKey == true) interactable.GetComponent<LeverController>().Hit();
        }
    }

    /// <summary>
    /// Looks at the players right and left for the closest interactable object.
    /// This object will be saved in interactable.
    /// </summary>
    private void CheckForExit()
    {
        exit = null;
        for (int i = 0; i < rayCaster.horizontalRayCount; i++)
        {
            Vector2 rayOffset = Vector2.up * (rayCaster.horizontalSpacing * i);
            RaycastHit2D hit = Physics2D.Raycast(rayCaster.rayCastOrigins.bottomRight + rayOffset, Vector2.right, exitRange, exitMask);
            if (!hit) hit = Physics2D.Raycast(rayCaster.rayCastOrigins.bottomLeft + rayOffset, Vector2.right * -1, exitRange, exitMask);

            if (hit)
            {
                exit = hit.transform.gameObject;
                if (exit.CompareTag("Finish"))
                {
                    //exit.GetComponent<ExitController>().NextLevel();
                    exit.GetComponent<ExitController>().Mainmenu();
                }
                return;
            }
        }
    }

    /// <summary>
    /// Looks at the players right and left for the closest interactable object.
    /// This object will be saved in interactable.
    /// </summary>
    private void CheckInteractable()
    {
        interactable = null;

        for (int i = 0; i < rayCaster.horizontalRayCount; i++)
        {
            Vector2 rayOffset = Vector2.up * (rayCaster.horizontalSpacing * i);
            RaycastHit2D hit = Physics2D.Raycast(rayCaster.rayCastOrigins.bottomRight + rayOffset, Vector2.right, interactRange, interactMask);
            if (!hit) hit = Physics2D.Raycast(rayCaster.rayCastOrigins.bottomLeft + rayOffset, Vector2.right * -1, interactRange, interactMask);

            if (hit)
            {
                interactable = hit.transform.gameObject;
                if (!interactable.CompareTag("Monty"))
                {
                    Vector3 interactablePos = interactable.transform.position;
                    canvasScript.SetInteractable(new Vector3(interactablePos.x, interactablePos.y + 2, interactablePos.z));
                    if (!interactionPossibleSoundPlayed)
                    {
                        if (SoundManager.Instance != null)
                        {
                            SoundManager.Instance.ObjectSounds[0].PlayAudioClip(2);
                        }
                        interactionPossibleSoundPlayed = true;
                    }
                }
                return;
            }
        }

        canvasScript.RemoveDialog();
        interactionPossibleSoundPlayed = false;
    }

    /// <summary>
    /// Handles the collissions for the speed, both horizontal as vertical
    /// </summary>
    /// <param name="velocity">The speed</param>
    private void HandleCollision(ref Vector3 velocity)
    {
        if (velocity.x != 0) HandleCollision(ref velocity, false);
        if (velocity.y != 0) HandleCollision(ref velocity, true);
    }

    /// <summary>
    /// Handles the collissions for the speed
    /// </summary>
    /// <param name="velocity">The speed</param>
    /// <param name="vertical">Wether it is a vertical or horizontal collission</param>
    private void HandleCollision(ref Vector3 velocity, bool vertical)
    {
        float vel = vertical ? velocity.y : velocity.x;
        Vector2 vmovement = (vertical ? Vector2.right : Vector2.up);
        Vector2 hmovement = (vertical ? Vector2.up : Vector2.right);

        float direction = Mathf.Sign(vel);
        float rayLength = Mathf.Abs(vel) + skinWidth;
        for (int i = 0; i < rayCaster.verticleRayCount; i++)
        {
            Vector2 rayOrigin = (direction == -1) ? rayCaster.rayCastOrigins.bottomLeft : vertical ? rayCaster.rayCastOrigins.topLeft : rayCaster.rayCastOrigins.bottomRight;
            rayOrigin += vmovement * ((vertical ? rayCaster.verticleSpacing : rayCaster.horizontalSpacing) * i + (vertical ? velocity.x : 0));
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, hmovement * direction, rayLength, collisionMask);

#if UNITY_EDITOR
            Debug.DrawRay(rayOrigin, hmovement * direction * rayLength * 3, Color.red);
#endif

            if (hit)
            {
                rayLength = hit.distance;
                if (vertical)
                {
                    velocity.y = (hit.distance - skinWidth) * direction;

                    collInfo.below = direction == -1;
                    collInfo.above = direction == 1;
                }
                else
                {
                    velocity.x = (hit.distance - skinWidth) * direction;

                    collInfo.left = direction == -1;
                    collInfo.right = direction == 1;
                }
            }
        }
    }

    public struct CollisionInfo
    {
        public bool above, below, left, right;
        public void Reset()
        {
            above = below = right = left = false;
        }
    }
}
