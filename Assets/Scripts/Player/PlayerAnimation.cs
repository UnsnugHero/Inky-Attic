using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
	private Animator playerAnimator;
	private PlayerLifecycle playerLifecycle;
	private PlayerMovement playerMovement;

	void Start()
	{
		playerAnimator = GetComponent<Animator>();
		playerLifecycle = GetComponent<PlayerLifecycle>();
		playerMovement = GetComponent<PlayerMovement>();
	}

	void Update()
	{
		UpdateMovingAnimation();
		UpdateAirborneAnimations();
		UpdateClimbingAnimation();
		UpdateDeathAnimation();
	}

	private void UpdateMovingAnimation()
	{
		playerAnimator.SetBool("isMoving", playerMovement.IsMoving);
	}

	private void UpdateAirborneAnimations()
	{
		if (playerMovement.IsAirborne)
		{
			if (playerMovement.HasJumped)
			{
				playerAnimator.SetBool("isJumping", true);
			}
			else
			{
				playerAnimator.SetBool("isFalling", true);
			}
		}
		else
		{
			playerAnimator.SetBool("isJumping", false);
			playerAnimator.SetBool("isFalling", false);
		}
	}

	private void UpdateClimbingAnimation()
	{
		if (playerMovement.IsActivelyClimbing)
		{
			// player is on the ladder and also actively moving
			playerAnimator.speed = 1;
			playerAnimator.SetBool("isClimbing", true);
		}
		else if (!playerMovement.IsActivelyClimbing && playerMovement.IsClimbing)
		{
			// player is still on the ladder but not moving
			playerAnimator.speed = 0;
		}
		else
		{
			playerAnimator.SetBool("isClimbing", false);
			playerAnimator.speed = 1;
		}
	}

	private void UpdateDeathAnimation()
	{
		if (!playerLifecycle.IsAlive)
		{
			playerAnimator.speed = 1;
			playerAnimator.SetBool("wasKilled", true);
		}
	}
}
