using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderAnimation : MonoBehaviour
{
	private readonly string AnimationParamMoving = "isMoving";
	private readonly string AnimationParamStunned = "isStunned";
	private readonly string AnimationParamTwitch = "twitch";

	private float _currentTwitchWaitTime;

	private Animator _spiderAnimator;
	private EnemyMovement _spiderMovement;

	[SerializeField] float minTwitchWaitTime = 0.25f;
	[SerializeField] float maxTwitchWaitTime = 1f;

	// Start is called before the first frame update
	void Start()
	{
		_currentTwitchWaitTime = Random.Range(minTwitchWaitTime, maxTwitchWaitTime);
		_spiderAnimator = GetComponent<Animator>();
		_spiderMovement = GetComponent<EnemyMovement>();
	}

	// Update is called once per frame
	void Update()
	{
		_spiderAnimator.SetBool(AnimationParamMoving, _spiderMovement.IsMoving);
		_spiderAnimator.SetBool(AnimationParamStunned, _spiderMovement.CurrentMoveState.Equals(EnemyMovement.MoveState.Stunned));
		UpdateSpiderTwitch();
	}

	private void UpdateSpiderTwitch()
	{
		if (!_spiderMovement.IsMoving)
		{
			if (_currentTwitchWaitTime <= 0)
			{
				_spiderAnimator.SetTrigger(AnimationParamTwitch);
				_currentTwitchWaitTime = Random.Range(minTwitchWaitTime, maxTwitchWaitTime);
			}
			else
			{
				_currentTwitchWaitTime -= Time.deltaTime;
			}
		}
	}
}
