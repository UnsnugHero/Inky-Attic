using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderMovement : EnemyMovement
{
	private float _idleZoneLowerBoundX;
	private float _idleZoneUpperBoundX;
	private float _currentWaitTime;
	private Vector2 _currentIdlePointDestination;

	private BoxCollider2D _feetCollider;

	[SerializeField] float minIdleWaitTime = 1.5f;
	[SerializeField] float maxIdleWaitTime = 2.5f;
	[SerializeField] float moveSpeed = 3f;
	[SerializeField] BoxCollider2D detectZone;

	new void Start()
	{
		base.Start();

		_feetCollider = GetComponent<BoxCollider2D>();

		_currentIdlePointDestination = transform.position;
		_currentWaitTime = Random.Range(minIdleWaitTime, maxIdleWaitTime);
		_idleZoneLowerBoundX = detectZone.bounds.min.x + _feetCollider.size.x;
		_idleZoneUpperBoundX = detectZone.bounds.max.x - _feetCollider.size.x;
	}

	new void Update()
	{
		base.Update();
	}

	// CHASING MOVEMENT

	public override void HandleChasingMovement()
	{
		IsMoving = true;
		UpdateChasingPosition();
	}

	private void UpdateChasingPosition()
	{
		Vector2 playerPositionXAxis = new Vector2(Player.transform.position.x, transform.position.y);
		Vector2 nextChasePosition = Vector2.MoveTowards(transform.position, playerPositionXAxis, moveSpeed * Time.deltaTime);
		_currentIdlePointDestination = nextChasePosition;
		transform.position = nextChasePosition;
	}

	// IDLE MOVEMENT

	public override void HandleIdleMovement()
	{
		UpdateIdlePosition();
	}

	public void UpdateIdlePosition()
	{
		_currentIdlePointDestination = GetCurrentIdleDestination();
		transform.position = Vector2.MoveTowards(transform.position, _currentIdlePointDestination, moveSpeed * Time.deltaTime);
	}

	private Vector2 GetRandomPointInIdleZone()
	{
		float randomIdlePointX = Random.Range(_idleZoneLowerBoundX, _idleZoneUpperBoundX);
		return new Vector2(randomIdlePointX, transform.position.y);
	}

	private Vector2 GetCurrentIdleDestination()
	{
		if (transform.position.Equals(_currentIdlePointDestination))
		{
			IsMoving = false;

			if (_currentWaitTime <= 0)
			{
				IsMoving = true;
				_currentWaitTime = Random.Range(minIdleWaitTime, maxIdleWaitTime);
				return GetRandomPointInIdleZone();
			}
			else
			{
				_currentWaitTime -= Time.deltaTime;
			}
		}

		return _currentIdlePointDestination;
	}
}
