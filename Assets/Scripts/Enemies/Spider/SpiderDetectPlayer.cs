using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EnemyMovement;

public class SpiderDetectPlayer : MonoBehaviour
{
	private EnemyMovement _spiderMovement;

	void Start()
	{
		GameObject spiderParent = transform.parent.gameObject;
		_spiderMovement = spiderParent.GetComponentInChildren<EnemyMovement>();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		DetectPlayerInChaseRange(collision, MoveState.Chasing);
	}

	private void OnTriggerStay2D(Collider2D collision)
	{
		DetectPlayerInChaseRange(collision, MoveState.Chasing);
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		DetectPlayerInChaseRange(collision, MoveState.Idle);
	}

	private void DetectPlayerInChaseRange(Collider2D collision, MoveState setState)
	{
		if (_spiderMovement.CurrentMoveState != MoveState.Stunned)
		{
			if (collision.tag == "Player")
			{
				_spiderMovement.SetCurrentMoveState(setState);
			}
		}
	}
}
