using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EnemyMovement;

public class LightDetection : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D collision)
	{
		StunEnemy(collision);
		HurtCreep(collision);
	}

	private void OnTriggerStay2D(Collider2D collision)
	{
		StunEnemy(collision);
	}

	private void StunEnemy(Collider2D collision)
	{
		if (collision.tag == "Enemy")
		{
			EnemyMovement enemyMovement = collision.gameObject.GetComponent<EnemyMovement>();
			enemyMovement.SetCurrentMoveState(MoveState.Stunned);
		}
	}

	private void HurtCreep(Collider2D collision)
	{
		if (collision.tag == "Creep")
		{
			CreepAttack creepAttack = collision.gameObject.GetComponent<CreepAttack>();
			if (creepAttack.IsVisible)
			{
				CreepLightHandler creepLightHandler = collision.gameObject.GetComponent<CreepLightHandler>();
				creepLightHandler.WasHitByLight = true;
				creepLightHandler.SetFace(CreepLightHandler.FaceType.Sad);
				AudioManager.instance.PlaySound("Creep Dispell");
			}
		}
	}
}
