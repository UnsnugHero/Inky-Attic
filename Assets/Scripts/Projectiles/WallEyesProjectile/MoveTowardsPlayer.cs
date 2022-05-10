using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTowardsPlayer : MonoBehaviour
{
	private Vector3 _playerPosition;
	private Vector3 _diffVectorToPlayer;

	[SerializeField] float moveSpeed = 1f;

	void Start()
	{
		_playerPosition = GameObject.Find("Player").transform.position;
		_diffVectorToPlayer = (_playerPosition - transform.position).normalized;
		FacePlayer();
	}

	void Update()
	{
		MoveProjectileTowardsPlayer();
	}

	private void MoveProjectileTowardsPlayer()
	{
		transform.position += _diffVectorToPlayer * moveSpeed * Time.deltaTime;
	}

	private void FacePlayer()
	{
		float angleToPlayer = Mathf.Atan2(_diffVectorToPlayer.y, _diffVectorToPlayer.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Euler(0, 0, angleToPlayer);
	}
}
