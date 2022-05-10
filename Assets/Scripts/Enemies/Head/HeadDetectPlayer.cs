using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EnemyMovement;

public class HeadDetectPlayer : MonoBehaviour
{
	private EnemyMovement _enemyMovement;
	private GameObject _player;

	[SerializeField] float chaseRadius = 3f;

	void Start()
	{
		_enemyMovement = GetComponent<EnemyMovement>();
		_player = GameObject.Find("Player");
	}

	private void Update()
	{
		DetectPlayerInChaseRange(_player.transform.position, transform.position);
	}

	private void DetectPlayerInChaseRange(Vector3 playerPosition, Vector3 center)
	{
		MoveState currentMoveState = _enemyMovement.CurrentMoveState;
		if (!currentMoveState.Equals(MoveState.Stunned))
		{
			MoveState setMoveState = Vector2.Distance(playerPosition, center) <= chaseRadius ? MoveState.Chasing : MoveState.Idle;
			_enemyMovement.SetCurrentMoveState(setMoveState);
		}
	}
}
