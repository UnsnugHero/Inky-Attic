using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadEnemyMovement : EnemyMovement
{
	private Vector2 _idleAnchorPoint;
	private Vector2 _currentIdlePointDestination;
	private float _currentWaitTime;

	[SerializeField] float minWaitTime;
	[SerializeField] float maxWaitTime;
	[SerializeField] float idleZoneRadius;
	[SerializeField] float moveSpeed;

	private new void Start()
	{
		base.Start();

		_currentIdlePointDestination = transform.position;
		_currentWaitTime = GetRandomWaitTime();
		_idleAnchorPoint = transform.position;
	}

	private new void Update()
	{
		base.Update();
	}

	// IDLE MOVEMENT

	public override void HandleIdleMovement()
	{
		UpdateIdlePosition();
		UpdateHeadDirection(_currentIdlePointDestination.x);
	}

	private Vector2 GetRandomPointInIdleZone()
	{
		return _idleAnchorPoint + Random.insideUnitCircle * idleZoneRadius;
	}

	private void UpdateIdlePosition()
	{
		_currentIdlePointDestination = GetCurrentIdleDestination();
		transform.position = Vector2.MoveTowards(transform.position, _currentIdlePointDestination, moveSpeed * Time.deltaTime);
	}

	private Vector2 GetCurrentIdleDestination()
	{
		// if the current idle position is reached
		if (transform.position.Equals(_currentIdlePointDestination))
		{
			IsMoving = false;
			// set a new one if wait time runs out
			if (_currentWaitTime <= 0)
			{
				IsMoving = true;
				_currentWaitTime = GetRandomWaitTime();
				return GetRandomPointInIdleZone();
			}
			else
			{
				_currentWaitTime -= Time.deltaTime;
			}
		}

		// otherwise return current idle point to go to
		return _currentIdlePointDestination;
	}

	// CHASING MOVEMENT

	public override void HandleChasingMovement()
	{
		IsMoving = true;
		UpdateChasePosition();
		UpdateHeadDirection(Player.transform.position.x);
	}

	private void UpdateChasePosition()
	{
		Vector2 nextChasePosition = Vector2.MoveTowards(transform.position, Player.transform.position, moveSpeed * Time.deltaTime);
		_currentIdlePointDestination = nextChasePosition;
		_idleAnchorPoint = nextChasePosition;
		transform.position = nextChasePosition;
	}

	// MISC

	private float GetRandomWaitTime()
	{
		return Random.Range(minWaitTime, maxWaitTime);
	}

	private void UpdateHeadDirection(float destinationPointX)
	{
		float horizontalDistanceDifference = destinationPointX - transform.position.x;
		if (horizontalDistanceDifference != 0)
		{
			transform.localScale = new Vector2(Mathf.Sign(horizontalDistanceDifference), 1f);
		}

	}
}
