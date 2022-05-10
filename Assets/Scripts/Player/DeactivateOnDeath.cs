using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateOnDeath : MonoBehaviour
{
	private PlayerLifecycle _playerLifecycle;
	private CapsuleCollider2D _playerCapsuleCollider;
	private BoxCollider2D _playerBoxCollider;


	// Start is called before the first frame update
	void Start()
	{
		_playerLifecycle = GetComponent<PlayerLifecycle>();
		_playerCapsuleCollider = GetComponent<CapsuleCollider2D>();
		_playerBoxCollider = GetComponent<BoxCollider2D>();
	}

	// Update is called once per frame
	void Update()
	{
		_playerCapsuleCollider.enabled = _playerLifecycle.IsAlive;
		_playerBoxCollider.enabled = _playerLifecycle.IsAlive;
	}
}
