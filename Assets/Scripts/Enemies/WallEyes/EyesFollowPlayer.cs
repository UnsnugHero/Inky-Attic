using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyesFollowPlayer : MonoBehaviour
{
	private Vector2 _basePoint;
	private VisibilityManager _visibilityManager;
	private GameObject _player;

	[SerializeField] float limitRadius = 0.1f;

	void Start()
	{
		_basePoint = transform.position;
		_visibilityManager = GetComponentInChildren<VisibilityManager>();
		_player = GameObject.Find("Player");
	}

	void Update()
	{
		if (_visibilityManager.IsVisible)
		{
			Vector2 lookDir = ((Vector2)_player.transform.position - _basePoint).normalized;
			transform.position = _basePoint + (lookDir * limitRadius);
		}
	}
}
