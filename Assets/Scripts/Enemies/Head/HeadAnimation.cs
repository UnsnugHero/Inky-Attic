using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadAnimation : MonoBehaviour
{
	private Animator _headAnimator;
	private EnemyMovement _headEnemyMovement;

	void Start()
	{
		_headAnimator = GetComponent<Animator>();
		_headEnemyMovement = GetComponent<EnemyMovement>();
	}

	// Update is called once per frame
	void Update()
	{
		bool isStunned = _headEnemyMovement.CurrentMoveState.Equals(EnemyMovement.MoveState.Stunned);
		_headAnimator.SetBool("isStunned", isStunned);
	}
}
