using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLifecycle : MonoBehaviour
{
	[SerializeField] float lifeDuration = 5f;

	void Start()
	{
		Destroy(gameObject, lifeDuration);
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
		{
			Destroy(gameObject);
		}
	}

	private void OnBecameInvisible()
	{
		Destroy(gameObject);
	}
}
