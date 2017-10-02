using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MontyController : BaseController
{

	public GameObject canvas;

	public float range = 1f;
	public string defaultTalk;
	public string ignoreMontyFeedBack;
	public string fightMontyFeedBack;
	public string flightMontyFeedBack;
	public string confrontMontyFeedBack;

	public GameObject NPC;
	private static NPCController NPCScript;
	private CanvasScript canvasScript;

	private bool alreadyGaveFeedback = false;
	private bool montyTalking;

	protected override void Awake()
	{
		base.Awake();
		if (NPC != null) NPCScript = NPC.GetComponent<NPCController>();
	}

    public void SetCanvas(CanvasScript canvas)
    {
        canvasScript = canvas;
    }

    protected override void Start()
	{
		base.Start();
		rayCaster.UpdateRayCastOrigins(InnerBounds());
	}

	void LateUpdate()
	{
		CheckForPlayer();
	}

	private void CheckForPlayer()
	{
		bool playerStillHere = false;
		for (int i = 0; i < rayCaster.horizontalRayCount; i++)
		{
			Vector2 rayOffset = Vector2.up * (rayCaster.horizontalSpacing * i);
			RaycastHit2D hit = Physics2D.Raycast(rayCaster.rayCastOrigins.bottomRight + rayOffset, Vector2.right, range, collisionMask);
			if (!hit) hit = Physics2D.Raycast(rayCaster.rayCastOrigins.bottomLeft + rayOffset, Vector2.right * -1, range, collisionMask);

			if (hit)
			{
				GiveFeedback();
				playerStillHere = true;
				break;
			}
			else
			{
				playerStillHere = false;
			}
		}
		montyTalking = playerStillHere;
	}

	private void GiveFeedback()
	{
		alreadyGaveFeedback = true;
		if (NPC != null)
		{
			NPCController.SolutionTypes solution = NPCScript.GetSolution();

			canvasScript.SetSolutionType(solution);

			if (solution == NPCController.SolutionTypes.Ignore)
			{
			    if (!montyTalking)
			    {
			        SoundManager.Instance.ObjectSounds[0].PlayAudioClip(8);
			    }
			    canvasScript.SetDialogBox(ignoreMontyFeedBack);
				NPC.SetActive(false);
			}

		    if (solution == NPCController.SolutionTypes.Flight)
		    {
                if (!montyTalking)
                {
                    SoundManager.Instance.ObjectSounds[0].PlayAudioClip(8);
                }
                canvasScript.SetDialogBox(flightMontyFeedBack);
		    }

		    if (solution == NPCController.SolutionTypes.Fight)
		    {
                if (!montyTalking)
                {
                    SoundManager.Instance.ObjectSounds[0].PlayAudioClip(7);
                }
                canvasScript.SetDialogBox(fightMontyFeedBack);
		    }

		    if (solution == NPCController.SolutionTypes.Confrontation)
		    {
                if (!montyTalking)
                {
                    SoundManager.Instance.ObjectSounds[0].PlayAudioClip(6);
                }
                canvasScript.SetDialogBox(confrontMontyFeedBack);
		    }
		}
		else
		{
			canvasScript.SetDialogBox(defaultTalk);
		}
		canvasScript.DisableAllButtons();
	}

    public static NPCController.SolutionTypes GetSolution()
    {
        return NPCScript.GetSolution();
    }
}
