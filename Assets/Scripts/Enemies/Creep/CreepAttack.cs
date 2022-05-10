using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreepAttack : MonoBehaviour
{
	public bool IsVisible { get; private set; }

	private float _currentAttackWaitTime;
	private int _currentLaughCount;
	private bool _shouldDecrementAttackTime = true;
	private Vector3 _currentFacePosition;
	private Vector3 _currentFacePositionOffset;
	private bool _shouldUpdateFacePositionTransform = true;

	private SpriteRenderer _creepSprite;
	private CreepLightHandler _creepLightHandler;

	[Tooltip("How long the Creep waits in between attacks.")]
	[SerializeField] private float _attackInterval;
	[SerializeField] private int _laughNumber = 2;
	[SerializeField] private float _laughInterval;
	[SerializeField] private float _attackSpeed;
	[SerializeField] private float _attackDistance;
	[SerializeField] private GameObject _player;

	private void Awake()
	{
		_currentAttackWaitTime = _attackInterval;
		_currentLaughCount = _laughNumber;
		_currentFacePositionOffset = RandomPointOnCircleEdge(_attackDistance);

		_creepSprite = GetComponent<SpriteRenderer>();
		_creepLightHandler = GetComponent<CreepLightHandler>();
		_player = GameObject.Find("Player");
	}

	void Start()
	{
		gameObject.SetActive(false);
	}

	void Update()
	{
		DecrementAttackTime();
		CheckShouldAttackPlayer();
		UpdateFacePositionRelativeToPlayer();
	}

	private void CheckShouldAttackPlayer()
	{
		if (_currentAttackWaitTime <= 0f)
		{
			_currentAttackWaitTime = _attackInterval;
			_shouldDecrementAttackTime = false;
			StartCoroutine(PrepareAttackPlayer());
		}
	}

	private IEnumerator PrepareAttackPlayer()
	{
		_currentFacePositionOffset = RandomPointOnCircleEdge(_attackDistance);

		yield return StartCoroutine(Laugh());
		yield return StartCoroutine(FadeTo(1f, 1f, true));
		yield return StartCoroutine(AttackPlayer());
		yield return StartCoroutine(FadeTo(0f, 1f, false));

		ResetCreep();
	}

	private IEnumerator AttackPlayer()
	{
		_shouldUpdateFacePositionTransform = false;

		// attack player
		while (transform.position != _player.transform.position && !_creepLightHandler.WasHitByLight)
		{
			transform.position = Vector3.MoveTowards(transform.position, _player.transform.position, _attackSpeed * Time.deltaTime);
			yield return null;
		}
	}

	// the creep will laugh however many times before it attacks the player
	private IEnumerator Laugh()
	{
		while (_currentLaughCount > 0)
		{
			// play laugh sound
			AudioManager.instance.PlaySound("Creep Laugh Short");

			_currentLaughCount--;
			yield return new WaitForSeconds(_laughInterval);
		}

		_currentLaughCount = _laughNumber;
	}

	private Vector3 RandomPointOnCircleEdge(float radius)
	{
		Vector2 angleBasePoint = new Vector2(radius, 0);
		Vector2 point;
		float creepAngleToPlayer;

		do
		{
			point = Random.insideUnitCircle.normalized * radius;
			creepAngleToPlayer = Vector2.Angle(point, angleBasePoint);
		}
		while (creepAngleToPlayer > 45f && creepAngleToPlayer < 135f);

		return new Vector3(point.x, point.y, 0f);
	}

	private void DecrementAttackTime()
	{
		if (_shouldDecrementAttackTime)
		{
			_currentAttackWaitTime -= Time.deltaTime;
		}
	}

	private void UpdateFacePositionRelativeToPlayer()
	{
		_currentFacePosition = _player.transform.position + _currentFacePositionOffset;

		if (_shouldUpdateFacePositionTransform)
		{
			transform.position = _currentFacePosition;
		}
	}

	private IEnumerator FadeTo(float targetAlpha, float fadeTime, bool visibility)
	{
		if (visibility)
		{
			AudioManager.instance.PlaySound("Creep Appear");
		}

		float currAlpha = _creepSprite.color.a;
		for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / fadeTime)
		{
			Color newColor = new Color(1, 1, 1, Mathf.Lerp(currAlpha, targetAlpha, t));
			_creepSprite.color = newColor;
			yield return null;
		}

		IsVisible = visibility;
	}

	private void ResetCreep()
	{
		_creepLightHandler.SetFace(CreepLightHandler.FaceType.Smile);
		_creepLightHandler.WasHitByLight = false;
		_shouldUpdateFacePositionTransform = true;
		_shouldDecrementAttackTime = true;
	}
}
