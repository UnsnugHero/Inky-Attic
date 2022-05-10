using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootProjectile : MonoBehaviour
{
	public bool IsTrembling { get; private set; } = false;

	private readonly float _timeToTremble = 2f;
	private VisibilityManager _visibilityManager;
	private float _timeUntilShot;

	[SerializeField] float shootInterval;
	[SerializeField] GameObject projectilePrefab;

	void Start()
	{
		_timeUntilShot = shootInterval;
		_visibilityManager = GetComponentInChildren<VisibilityManager>();
	}

	void Update()
	{
		CreateProjectile();
		ManageTimeUntilShot();
		ManageTremble();
	}

	private void ManageTimeUntilShot()
	{
		if (_visibilityManager.IsVisible)
		{
			_timeUntilShot -= Time.deltaTime;
		}
		else
		{
			_timeUntilShot = shootInterval;
		}
	}

	private void ManageTremble()
	{
		IsTrembling = _timeUntilShot < _timeToTremble;
	}

	private void CreateProjectile()
	{
		if (_timeUntilShot < 0f)
		{
			AudioManager.instance.PlaySound("Projectile Shot");
			Instantiate(projectilePrefab, transform.position, Quaternion.identity);
			_timeUntilShot = shootInterval;
		}
	}
}
