using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderCollision : MonoBehaviour
{
	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == Constants.TagPlayer)
		{
			Rigidbody2D spiderRigidBody = GetComponent<Rigidbody2D>();
			spiderRigidBody.velocity = new Vector2(0, 0);
		}
	}
}
