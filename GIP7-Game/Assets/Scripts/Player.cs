using UnityEngine;
using Assets.Scripts.Base;
using UnityStandardAssets.CrossPlatformInput;

[RequireComponent(typeof(PlayerController))]
public class Player : MonoBehaviour
{
	public float moveSpeed = 5f;
	public float jumpingHeight = 15f;
	public float gravity = 30f;
	public float smoothTime = 0.15f;

	public PlayerInfo playerInfo;

	public Vector3 velocity;
	private float velocityTarget;
	private float smoothVelocity;

	private PlayerController playerController;
	private SoundManager soundManager;

	public Transform spriteTransform;
	private SpriteRenderer sR;

	public Sprite stand;
	public Sprite walk1;
	public Sprite walk2;
	public Sprite jump;

    public int health;

	private float spriteTimer = 0;
	private float lastJump = 0;
	void Awake()
	{
		sR = spriteTransform.GetComponent<SpriteRenderer>();
		playerController = GetComponent<PlayerController>();

		if (Camera.main.GetComponent<CameraController>() == null)
		{
			CameraController c = Camera.main.gameObject.AddComponent<CameraController>();
			c.AddPlayer(this.gameObject);
		}

		if (MobileHelper.OnMobileDevice)
		{
			Instantiate(Resources.Load("MobileSingleStickControl"));
		}
	}

	void Start()
	{
		soundManager = GameObject.FindObjectOfType<SoundManager>();
	}

	void Update()
	{
		CheckYBump();

		CheckXMovement();

		CheckJump();

		setPlayerSprite();

		//Enable gravity to the player, if the player is not standing on a platform
		if (!playerController.collInfo.below) velocity.y -= gravity * Time.deltaTime;

		//Move the player with the adjusted speeds
		playerController.Move(velocity);

		if (Input.GetAxis("Interact") == 1)
		{
			playerController.Interact();
		}
	}

	/// <summary>
	/// Checks if the player is holding any buttons to jump
	/// </summary>
	private void CheckJump()
	{
		//If the player is pressing the positive vertical axis input, or jump input, the character needs to jump
		bool doJump = false;

		if (MobileHelper.OnMobileDevice)
		{
			doJump = CrossPlatformInputManager.GetButton("Jump") ||
				CrossPlatformInputManager.GetButton("Jump") && (int)CrossPlatformInputManager.GetAxis("Horizontal") != 0 ||
				(int)CrossPlatformInputManager.GetAxis("Horizontal") != 0 && CrossPlatformInputManager.GetButton("Jump");
		}
		else
		{
			doJump = Input.GetAxis("Vertical") == 1 || Input.GetAxis("Jump") == 1;
		}

		lastJump += Time.deltaTime;

		//If the player needs to jump, and is able to, jump.
		if (doJump && playerController.collInfo.below)
		{
			velocity.y = jumpingHeight;
			sR.sprite = jump;

			if (SoundManager.Instance != null && lastJump > 0.1f)
			{
				SoundManager.Instance.ObjectSounds[0].PlayAudioClip(0);
			}

			lastJump = 0;
		}
	}

	/// <summary>
	/// Checks if the player is holding any buttons to move left / right
	/// </summary>
	private void CheckXMovement()
	{
		int inputX;
		if (MobileHelper.OnMobileDevice)
		{
			inputX = (int)CrossPlatformInputManager.GetAxis("Horizontal");
		}
		else
		{
			inputX = (int)Input.GetAxis("Horizontal");
		}

		//The movement over the X axis is defined by the input of the Horizontal axis
		if (inputX > 0.1f) playerInfo.direction = 1;
		else if (inputX < -0.1f) playerInfo.direction = -1;
		else playerInfo.direction = 0;
		
		velocityTarget = inputX * moveSpeed;
		velocity.x = Mathf.SmoothDamp(velocity.x, velocityTarget, ref smoothVelocity, smoothTime);
	}

	private void setPlayerSprite()
	{
		if (playerInfo.direction == 1) spriteTransform.eulerAngles = Vector3.zero;
		else if (playerInfo.direction == -1) spriteTransform.eulerAngles = Vector3.up * 180;

		if (playerController.collInfo.below && lastJump > 0.1f)
		{
			if (playerInfo.direction != 0)
			{
				if (spriteTimer > 0.13f)
				{
					spriteTimer = 0;
					if (sR.sprite == walk1) sR.sprite = walk2;
					else if (sR.sprite != walk1) sR.sprite = walk1;
				}
				else spriteTimer += Time.deltaTime;

			}
			else
			{
				sR.sprite = stand;
				spriteTimer = 1;
			}
		}
	}

	/// <summary>
	/// Checks if the player bumps into anything on the y-axis, and adjusts their speed if this is the case
	/// </summary>
	private void CheckYBump()
	{
        //Get the velocity of the player before checking for a collision
        float terminalvelocity = velocity.y;

		//If the player is colliding above or below, remove his speed
        if (playerController.collInfo.below || playerController.collInfo.above)
        {
            velocity.y = 0;
            if (terminalvelocity < -25)
            {
                DamagePlayer(5);
            }
        }
	}

    public void DamagePlayer(int amount)
    {
        health = health - amount;

        CheckPlayerDeath();
    }

    public void CheckPlayerDeath()
    {
        if (health <= 0)
        {
            LevelLoader.LoadMainMenu();
        }
    }

	public void PlatformMove(Vector3 velocity)
	{
		playerController.PlatformMove(velocity);
	}

	public struct PlayerInfo
	{
		public int direction; // Left: -1; Right: 1;
	}
}
