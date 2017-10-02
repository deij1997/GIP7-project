using UnityEngine;
using System.Collections;


public class CircleMovementController : PlatformController
{
	public float radius = 5;
	public float secondsForCircle = 10;
	public Direction direction = Direction.Clockwise; // Counter clockwise: direction = -1; Clockwise: direction = 1
	public Startpoints startPoint = Startpoints.right;
	public float degrees = 0; // 0 for full circles

	public float angle = 0;

	private float speed;

	private float xOffset;
	private float yOffset;

	private bool fullCircle;

	private float degreesMoved = 0;

	protected override void Awake()
	{
		base.Awake();
	}

	protected override void Start()
	{
		base.Start();

		SetStart();

		xOffset = thisTransform.position.x;
		yOffset = thisTransform.position.y;

		speed = (2 * Mathf.PI) / secondsForCircle;

		if (degrees == 0)
		{
			fullCircle = true;
		}
		else
		{
			fullCircle = false;
			degrees = degrees / 360 * 2 * Mathf.PI;
		}
	}

	protected override void Update()
	{
		base.Update();
	}

	protected override void Move()
	{
		rayCaster.UpdateRayCastOrigins(InnerBounds());


		float amountMoved = speed * Time.deltaTime;
		angle += amountMoved * DirectionToInt();

		if (!fullCircle)
		{
			degreesMoved += amountMoved;
			if (degrees <= degreesMoved)
			{
				SwapDirections();
				degreesMoved = 0;
				Wait();
			}
		}

		float newX = Mathf.Cos(angle) * radius + xOffset;
		float newY = Mathf.Sin(angle) * radius + yOffset;
		Vector3 velocity = new Vector3(newX - thisTransform.position.x, newY - thisTransform.position.y);

		HorizontalMovement(velocity);
		DownColision(velocity);

		thisTransform.Translate(velocity);
	}

	private int DirectionToInt()
	{
		if (direction == Direction.Clockwise) return -1;
		if (direction == Direction.CounterClockwise) return 1;

		return 0;
	}

	private void SwapDirections()
	{
		if (direction == Direction.Clockwise) direction = Direction.CounterClockwise;
		else if (direction == Direction.CounterClockwise) direction = Direction.Clockwise;
	}

	private void SetStart()
	{
		if (startPoint == Startpoints.right) angle = 0;
		if (startPoint == Startpoints.top) angle = Mathf.PI / 2;
		if (startPoint == Startpoints.left) angle = Mathf.PI;
		if (startPoint == Startpoints.bottom) angle = Mathf.PI * 3 / 2;
		if (startPoint == Startpoints.topRight) angle = Mathf.PI / 4;
		if (startPoint == Startpoints.topLeft) angle = Mathf.PI * 3 / 4;
		if (startPoint == Startpoints.bottomLeft) angle = Mathf.PI * 5 / 4;
		if (startPoint == Startpoints.bottomRight) angle = Mathf.PI * 7 / 4;
	}

	public enum Startpoints
	{
		top,
		bottom,
		left,
		right,
		topLeft,
		topRight,
		bottomLeft,
		bottomRight
	}

	public enum Direction
	{
		Clockwise,
		CounterClockwise
	}
}
