using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallDeath : MonoBehaviour
{
	private PlayerMovement _playerMovement;
	private PlayerLifecycle _playerLifecycle;
	private float _currentAirTime;

	[SerializeField] private float _deathAirTime = 2f;

	void Start()
	{
		_playerMovement = GetComponent<PlayerMovement>();
		_playerLifecycle = GetComponent<PlayerLifecycle>();
	}

	// Update is called once per frame
	void Update()
	{
		if (_playerMovement.IsAirborne)
		{
			_currentAirTime += Time.deltaTime;
		}
		else
		{
			if (_playerMovement.IsClimbing || _playerMovement.IsActivelyClimbing || _currentAirTime < _deathAirTime)
			{
				_currentAirTime = 0f;
			}
			else
			{
				_playerLifecycle.Die("Death");
			}
		}
	}
}
