using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyMovement : MonoBehaviour
{
	public enum MoveState
	{
		Idle,
		Chasing,
		Stunned
	}
	public bool IsMoving { get; protected set; } = false;
	public MoveState CurrentMoveState { get; private set; } = MoveState.Idle;

	protected GameObject Player;

	private float _currentStunTime;
	private float _currentStunImmuneTime;

	[SerializeField] float stunTime = 2f;
	[SerializeField] float stunImmuneTime = 2f;


	public void Start()
	{
		_currentStunTime = stunTime;
		_currentStunImmuneTime = stunImmuneTime;
		Player = GameObject.Find("Player");
	}

	// Update is called once per frame
	public void Update()
	{
		HandleMovementCurrentState();
		UpdateStunImmuneTime();
	}

	public void SetCurrentMoveState(MoveState moveState)
	{
		if (moveState == MoveState.Stunned && CurrentMoveState != MoveState.Stunned && _currentStunImmuneTime <= 0f)
		{
			AudioManager am = AudioManager.instance;
			if (!am.IsSoundPlaying("Enemy Stun"))
			{
				am.PlaySound("Enemy Stun");
			}
		}

		if (moveState == MoveState.Stunned && _currentStunImmuneTime > 0)
		{
			return;
		}
		CurrentMoveState = moveState;
	}

	public abstract void HandleIdleMovement();
	public abstract void HandleChasingMovement();
	private void HandleStunnedMovement()
	{
		if (_currentStunTime < Mathf.Epsilon)
		{
			CurrentMoveState = MoveState.Idle;
			_currentStunTime = stunTime;
			_currentStunImmuneTime = stunImmuneTime;
		}
		else
		{
			_currentStunTime -= Time.deltaTime;
		}
	}

	private void HandleMovementCurrentState()
	{
		if (CurrentMoveState == MoveState.Idle)
		{
			HandleIdleMovement();
		}
		else if (CurrentMoveState == MoveState.Chasing)
		{
			HandleChasingMovement();
		}
		else
		{
			HandleStunnedMovement();
		}
	}

	private void UpdateStunImmuneTime()
	{
		if (_currentStunImmuneTime <= 0)
		{
			_currentStunImmuneTime = 0;
		}
		else
		{
			_currentStunImmuneTime -= Time.deltaTime;
		}
	}
}
