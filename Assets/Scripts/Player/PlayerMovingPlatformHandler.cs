using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovingPlatformHandler : MonoBehaviour
{
	private BoxCollider2D _feetCollider;
	private Rigidbody2D _playerRigidBody;
	private RigidbodyInterpolation2D _defaultInterp;
	private MovingPlatform _currentMovingPlatform;

	[SerializeField] private LayerMask platformLayer;

	void Start()
	{
		_feetCollider = GetComponent<BoxCollider2D>();
		_playerRigidBody = GetComponent<Rigidbody2D>();
		_defaultInterp = _playerRigidBody.interpolation;
	}

	void Update()
	{
		DetectOnMovingPlatform();
	}

	private void DetectOnMovingPlatform()
	{
		Vector2 colliderHalfDistance = new Vector2(0, -(_feetCollider.size.y / 2));
		RaycastHit2D hit = Physics2D.BoxCast(_feetCollider.bounds.center, _feetCollider.bounds.size, 0f, colliderHalfDistance, 0.1f, platformLayer);

		if (hit && hit.transform.tag == "MovingPlatform")
		{
			transform.SetParent(hit.transform, true);
			_playerRigidBody.interpolation = RigidbodyInterpolation2D.None;
			_currentMovingPlatform = hit.transform.GetComponent<MovingPlatform>();
			_currentMovingPlatform.IsPlayerTouchingPlatform = true;
		}
		else
		{
			transform.parent = null;

			if (_currentMovingPlatform)
			{
				_currentMovingPlatform.IsPlayerTouchingPlatform = false;
				_currentMovingPlatform = null;
			}

			if (_playerRigidBody.interpolation == RigidbodyInterpolation2D.None)
			{
				_playerRigidBody.interpolation = _defaultInterp;
			}
		}
	}
}
