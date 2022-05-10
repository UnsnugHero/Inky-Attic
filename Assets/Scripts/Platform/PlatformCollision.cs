using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlatformCollision : MonoBehaviour
{
	private PlatformEffector2D _platformEffector;
	private PlayerMovement _playerMovement;
	private int _playerLayer;
	private int _platformLayer;

	void Start()
	{
		GameObject player = GameObject.Find("Player");
		_platformEffector = GetComponent<PlatformEffector2D>();
		_playerMovement = player.GetComponent<PlayerMovement>();

		_playerLayer = LayerMask.NameToLayer("Player");
		_platformLayer = LayerMask.NameToLayer("Platform");
	}

	void Update()
	{
		bool canPlayerClimbDown = _playerMovement.IsTouchingTopLadder && _playerMovement.IsActivelyClimbing;
		Physics2D.IgnoreLayerCollision(_playerLayer, _platformLayer, canPlayerClimbDown);
	}
}
