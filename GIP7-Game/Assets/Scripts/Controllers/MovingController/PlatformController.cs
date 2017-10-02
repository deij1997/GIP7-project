using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlatformController : BaseController
{
	public float waitTime = 1;
	private bool waiting;
	private float timeWaited;

	protected virtual void Update()
	{
		if (!waiting)
		{
			Move();
		}
		else
		{
			timeWaited += Time.deltaTime;
			if (timeWaited >= waitTime)
			{
				waiting = false;
				timeWaited = 0;
			}
		}
	}

	protected virtual void Move()
	{
	}

	protected void Wait()
	{
		waiting = true;
	}

	// Checks if a player is next to or ontop a platform.
	// If so they are moved accordingly.
	protected void HorizontalMovement(Vector3 velocity)
	{
		HorizontalColision(velocity);
		UpColision(velocity);
	}

	// Checks for players to te left or right of the object.
	// If any players are found, they are moved.
	protected void HorizontalColision(Vector3 velocity)
	{
		HashSet<Transform> AlreadyMoved = new HashSet<Transform>();
		float directionX = Mathf.Sign(velocity.x);
		float rayLength = Mathf.Abs(velocity.x) + skinWidth;
		for (int i = 0; i < rayCaster.horizontalRayCount; i++)
		{
			Vector2 rayOrigin = (directionX == -1) ? rayCaster.rayCastOrigins.bottomLeft : rayCaster.rayCastOrigins.bottomRight;
			rayOrigin += Vector2.up * (rayCaster.horizontalSpacing * i);
			RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLength, collisionMask);
			Debug.DrawRay(rayOrigin, Vector2.right * directionX * rayLength * 3, Color.red);

			if (hit && !AlreadyMoved.Contains(hit.transform))
			{
				AlreadyMoved.Add(hit.transform);
				Vector3 toMove = Vector3.right * directionX * (rayLength - hit.distance);
				hit.transform.Translate(toMove);
			}
		}
	}

	// Checks for players ontop or below verticle moving platform.
	// If any players are found ontop, they are moved.
	// If any players are found below, they will be moved down.
	// When moved while already on the ground, the player will move through the platform.
	protected void VerticleMovement(Vector3 velocity)
	{
		HashSet<Transform> alreadyMoved = new HashSet<Transform>();
		float rayLength = Mathf.Abs(velocity.y) + skinWidth;
		for (int i = 0; i < rayCaster.verticleRayCount; i++)
		{
			Vector2 rayOrigin = rayCaster.rayCastOrigins.topLeft;
			rayOrigin += Vector2.right * (rayCaster.verticleSpacing * i);
			RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up, rayLength, collisionMask);
			Debug.DrawRay(rayOrigin, Vector2.up * rayLength * 3, Color.red);

			if (hit && !alreadyMoved.Contains(hit.transform))
			{
				alreadyMoved.Add(hit.transform);

				Vector3 toMove;
				if (velocity.y > 0) toMove = Vector3.up * (rayLength - hit.distance);
				else toMove = velocity;

				hit.transform.Translate(toMove);
			}
		}

		DownColision(velocity);
	}

	//Checks for player below a verticle moving platform
	protected void DownColision(Vector3 velocity)
	{
		HashSet<Transform> alreadyMoved = new HashSet<Transform>();
		float rayLength = Mathf.Abs(velocity.y) + skinWidth;
		for (int i = 0; i < rayCaster.verticleRayCount; i++)
		{
			Vector2 rayOrigin = rayCaster.rayCastOrigins.bottomLeft;
			rayOrigin += Vector2.right * (rayCaster.verticleSpacing * i);
			RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up * -1, rayLength, collisionMask);
			Debug.DrawRay(rayOrigin, Vector2.up * rayLength * -3, Color.red);

			if (hit && !alreadyMoved.Contains(hit.transform))
			{
				alreadyMoved.Add(hit.transform);
				hit.transform.Translate(velocity);
			}
		}
	}

	// Checks for players ontop horizontaly moving platform
	// If any players are found, they are moved.
	protected void UpColision(Vector3 velocity)
	{
		HashSet<Transform> alreadyMoved = new HashSet<Transform>();
		float rayLength = 0.25f + skinWidth;
		for (int i = 0; i < rayCaster.verticleRayCount; i++)
		{
			Vector2 rayOrigin = rayCaster.rayCastOrigins.topLeft;
			rayOrigin += Vector2.right * (rayCaster.verticleSpacing * i);
			RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up, rayLength, collisionMask);
			Debug.DrawRay(rayOrigin, Vector2.up * rayLength, Color.red);

			if (hit && !alreadyMoved.Contains(hit.transform))
			{
				alreadyMoved.Add(hit.transform);
				hit.transform.GetComponent<Player>().PlatformMove(velocity);
			}
		}
	}

}
