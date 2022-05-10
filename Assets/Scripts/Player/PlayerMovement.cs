using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class PlayerMovement : MonoBehaviour
{
	private Vector2 _moveValue;
	private Vector2 _climbValue;

	private BoxCollider2D _feetCollider;
	private Rigidbody2D _playerRigidBody;

	private PlayerInput _playerInputController;
	private PlayerLifecycle _playerLifecycle;

	public bool IsMoving { get; private set; }
	public bool IsAirborne { get; private set; }
	public bool HasJumped { get; private set; }
	public bool IsTouchingTopLadder { get; private set; }
	public bool IsClimbing { get; private set; }
	public bool IsFlashing { get; set; }
	public bool IsMovementEnabled { get; set; } = true;

	private bool isInputtingClimb = false;

	private int platformLayer;
	private int ladderLayer;
	private int topLadderLayer;
	private bool isTouchingPlatform;
	private bool isTouchingLadder;

	public bool IsActivelyClimbing
	{
		get { return IsClimbing && isInputtingClimb; }
	}

	[SerializeField] float basePlayerGravity;
	[SerializeField] float climbSpeed;
	[SerializeField] float jumpSpeed;
	[SerializeField] float moveSpeed;

	void Start()
	{
		_feetCollider = GetComponent<BoxCollider2D>();
		_playerRigidBody = GetComponent<Rigidbody2D>();

		_playerInputController = GetComponent<PlayerInput>();
		_playerLifecycle = GetComponent<PlayerLifecycle>();

		platformLayer = LayerMask.GetMask("Platform");
		ladderLayer = LayerMask.GetMask("Ladder");
		topLadderLayer = LayerMask.GetMask("TopLadder");
	}

	void Update()
	{
		_playerInputController.enabled = _playerLifecycle.IsAlive && IsMovementEnabled;
		if (!_playerLifecycle.IsAlive || !IsMovementEnabled) { return; };

		Climb();
		Move();
		UpdateSpriteDirection();
		UpdateAirborneStatus();
		UpdateTouchingLayersStatus();
	}

	private void OnJump(InputValue value)
	{
		if (isTouchingPlatform && value.isPressed)
		{

			HasJumped = true;
			_playerRigidBody.velocity = Vector2.up * new Vector2(0, jumpSpeed);
			AudioManager.instance.PlaySound("Jump");
		}
	}

	private void OnMove(InputValue value)
	{
		_moveValue = value.Get<Vector2>();
	}

	private void OnClimb(InputValue value)
	{
		_climbValue = value.Get<Vector2>();
		isInputtingClimb = Mathf.Abs(_climbValue.y) > Mathf.Epsilon;
	}


	private void Climb()
	{
		if (isTouchingLadder || IsTouchingTopLadder)
		{
			if (isInputtingClimb || IsClimbing)
			{
				HasJumped = false;
				IsAirborne = false;
				_playerRigidBody.gravityScale = 0f;
				_playerRigidBody.velocity = new Vector2(_playerRigidBody.velocity.x, _climbValue.y * climbSpeed);
				IsClimbing = true;
			}
		}
		else
		{
			_playerRigidBody.gravityScale = basePlayerGravity;
			IsClimbing = false;
		}
	}

	private void Move()
	{
		_playerRigidBody.velocity = new Vector2(_moveValue.x * moveSpeed, _playerRigidBody.velocity.y);

		float currentPlayerVelocity = _playerRigidBody.velocity.x;
		IsMoving = Mathf.Abs(currentPlayerVelocity) > Mathf.Epsilon;
	}

	private void UpdateSpriteDirection()
	{
		if (IsMoving)
		{
			transform.localScale = new Vector2(Mathf.Sign(_playerRigidBody.velocity.x), 1f);
		}
	}

	private void UpdateAirborneStatus()
	{
		if (!IsClimbing)
		{
			if (!isTouchingPlatform)
			{
				IsAirborne = true;
			}
			else
			{
				// if they have left the platform and returned to it
				if (IsAirborne)
				{
					HasJumped = false;
					IsAirborne = false;
				}
			}
		}
	}

	private void UpdateTouchingLayersStatus()
	{
		isTouchingPlatform = _feetCollider.IsTouchingLayers(platformLayer);
		isTouchingLadder = _feetCollider.IsTouchingLayers(ladderLayer);
		IsTouchingTopLadder = _feetCollider.IsTouchingLayers(topLadderLayer);
	}
}
