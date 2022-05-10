using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
	private enum PlatformType
	{
		Normal,
		MoveOnFirstTouch,
		MoveOnlyOnTouch
	}

	private enum PlatformDirection
	{
		Horizontal,
		Vertical
	}

	private Vector3 _startPosition;
	private Vector3 _endPosition;
	private Vector3 _currentPosition;
	private bool _shouldMove;
	private bool _hasRested = false;
	private float _currentRestTime = 0f;

	public bool IsPlayerTouchingPlatform = false;

	[SerializeField] private bool _shouldLoop;
	[SerializeField] private float _moveSpeed;
	[SerializeField] private float _moveDistance;
	[SerializeField] private PlatformType _platformType;
	[SerializeField] private PlatformDirection _platformDirection;
	[SerializeField] private PlayerMovingPlatformHandler _playerPlatformHandler;
	[SerializeField] private float _restTime = 2f;

	void Start()
	{
		_startPosition = transform.position;
		_endPosition = CalculateEndPosition();
		_currentPosition = _startPosition;
		_shouldMove = _platformType == PlatformType.Normal;
	}

	void Update()
	{
		Move();
		HandleDestinationSwitch();
		HandleShouldMove();
		DecrementRestTime();
	}

	private void Move()
	{
		if (_shouldMove)
		{
			_currentPosition = Vector3.MoveTowards(_currentPosition, _endPosition, _moveSpeed * Time.deltaTime);
			transform.position = _currentPosition;
			_hasRested = false;
		}
	}

	private void HandleDestinationSwitch()
	{
		if (_currentPosition == _endPosition && _shouldLoop)
		{
			Vector3 temp = _startPosition;
			_startPosition = _endPosition;
			_endPosition = temp;
			_hasRested = false;
			_currentRestTime = _restTime;
			_shouldMove = _platformType != PlatformType.MoveOnFirstTouch;
		}
	}

	private void HandleShouldMove()
	{
		if (_platformType == PlatformType.MoveOnFirstTouch)
		{
			if (IsPlayerTouchingPlatform && _currentRestTime <= 0f)
			{
				_hasRested = true;
				_shouldMove = true;
			}

			if (_currentPosition == _endPosition)
			{
				if (!_hasRested)
				{
					_shouldMove = false;
				}
			}
		}
		else if (_platformType == PlatformType.MoveOnlyOnTouch)
		{
			_shouldMove = IsPlayerTouchingPlatform;
		}
	}

	private Vector3 CalculateEndPosition()
	{
		float pointToModify = _platformDirection == PlatformDirection.Horizontal ? _startPosition.x : _startPosition.y;
		float modifiedPoint = pointToModify + _moveDistance;

		if (_platformDirection == PlatformDirection.Horizontal)
		{
			return new Vector3(modifiedPoint, _startPosition.y, _startPosition.z);
		}

		return new Vector3(_startPosition.x, modifiedPoint, _startPosition.z);
	}

	private void DecrementRestTime()
	{
		float decrValue = _currentRestTime > 0f ? Time.deltaTime : 0f;
		_currentRestTime -= decrValue;
	}
}
