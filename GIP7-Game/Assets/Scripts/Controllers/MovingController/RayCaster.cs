using UnityEngine;
using System.Collections;

public class RayCaster : MonoBehaviour
{
	public RayCastOrigins rayCastOrigins;

	public int verticleRayCount;
	public int horizontalRayCount;

	[HideInInspector]
	public float verticleSpacing;
	[HideInInspector]
	public float horizontalSpacing;

	// Sets the 4 corner points for the given bounds.
	public void UpdateRayCastOrigins(Bounds bounds)
	{
		rayCastOrigins.topLeft = new Vector2(bounds.min.x, bounds.max.y);
		rayCastOrigins.topRight = new Vector2(bounds.max.x, bounds.max.y);
		rayCastOrigins.bottomLeft = new Vector2(bounds.min.x, bounds.min.y);
		rayCastOrigins.bottomRight = new Vector2(bounds.max.x, bounds.min.y);
	}

	// Calculates the space between every ray for the given bounds
	public void CalculateSpacing(Bounds bounds)
	{
		verticleRayCount = Mathf.Clamp(verticleRayCount, 2, int.MaxValue);
		horizontalRayCount = Mathf.Clamp(horizontalRayCount, 2, int.MaxValue);

		verticleSpacing = bounds.size.x / (verticleRayCount - 1);
		horizontalSpacing = bounds.size.y / (horizontalRayCount - 1);
	}

	public struct RayCastOrigins
	{
		public Vector2 topLeft, topRight;
		public Vector2 bottomLeft, bottomRight;
	}

}
