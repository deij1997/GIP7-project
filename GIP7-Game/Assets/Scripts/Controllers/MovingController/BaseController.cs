using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(RayCaster))]
public class BaseController : MonoBehaviour
{
	public LayerMask collisionMask;

	protected RayCaster rayCaster;

	protected float skinWidth = 0.05f;

	protected BoxCollider2D objectCollider;

	protected Transform thisTransform;

	protected virtual void Awake()
	{
		objectCollider = GetComponent<BoxCollider2D>();
		rayCaster = GetComponent<RayCaster>();
		thisTransform = transform;
	}

	protected virtual void Start()
	{
		rayCaster.CalculateSpacing(InnerBounds());
	}

	public Bounds InnerBounds()
	{
		Bounds bounds = objectCollider.bounds;
		bounds.Expand(skinWidth * -2);
		return bounds;
	}
}
