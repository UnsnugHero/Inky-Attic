using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
	private Rigidbody2D playerRigidBody;
	private PlayerLifecycle playerLifecycle;

	void Start()
	{
		GameObject player = GameObject.Find("Player");
		playerRigidBody = player.GetComponent<Rigidbody2D>();
		playerLifecycle = player.GetComponent<PlayerLifecycle>();
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.tag == "Player")
		{
			playerRigidBody.gravityScale = 0f;
			playerRigidBody.velocity = new Vector2(0, 0);
			playerLifecycle.Die("Death Fall");
		}
	}
}
