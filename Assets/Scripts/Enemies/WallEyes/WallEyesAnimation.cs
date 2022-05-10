using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallEyesAnimation : MonoBehaviour
{
	private readonly string IsVisibleState = "isVisible";

	private Animator _wallEyesAnimator;
	private ShootProjectile _shootProjectile;
	private VisibilityManager _visibilityManager;

	[SerializeField] float scaleTrembleSpeedFactor = 1.5f;

	void Start()
	{
		_shootProjectile = GetComponentInParent<ShootProjectile>();
		_wallEyesAnimator = GetComponent<Animator>();
		_visibilityManager = GetComponent<VisibilityManager>();
	}

	void Update()
	{
		_wallEyesAnimator.SetBool(IsVisibleState, _visibilityManager.IsVisible);
		_wallEyesAnimator.SetBool("isTrembling", _shootProjectile.IsTrembling);
		UpdateTremblingSpeed();
	}

	private void UpdateTremblingSpeed()
	{
		if (_shootProjectile.IsTrembling)
		{
			_wallEyesAnimator.speed += (Time.deltaTime * scaleTrembleSpeedFactor);
		}
		else
		{
			_wallEyesAnimator.speed = 1;
		}
	}
}
