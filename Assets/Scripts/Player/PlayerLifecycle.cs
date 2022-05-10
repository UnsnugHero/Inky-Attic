using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class PlayerLifecycle : MonoBehaviour
{
	public bool IsAlive { get; private set; } = true;

	private int[] _lethalLayers;
	private bool _initiatedDeathTransition = false;
	private bool _isRebounding = false;
	private LevelTransition _levelTransition;
	private Rigidbody2D _playerRigidBody;

	[SerializeField] float afterDeathDelay = 2f;
	[SerializeField] float bounceForce = 250f;
	[SerializeField] float velocityDampenFactor = 0.9f;

	void Start()
	{
		GameObject levelLoader = GameObject.Find("Level Canvas");
		_levelTransition = levelLoader.GetComponent<LevelTransition>();
		_playerRigidBody = GetComponent<Rigidbody2D>();
		_lethalLayers = new int[] { LayerMask.NameToLayer("Enemy"), LayerMask.NameToLayer("EnemyProjectile"), LayerMask.NameToLayer("Creep") };
	}

	void Update()
	{
		RestartLevel();
	}

	private void FixedUpdate()
	{
		if (_isRebounding)
		{
			_playerRigidBody.velocity *= velocityDampenFactor;
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (_lethalLayers.Contains(collision.gameObject.layer))
		{
			CollisionDeath(collision);
		}
	}

	public void Die(string deathSound)
	{
		if (IsAlive)
		{
			AudioManager.instance.PlaySound(deathSound);
		}

		IsAlive = false;
		_playerRigidBody.gravityScale = 0f;
	}

	private void CollisionDeath(Collision2D collision)
	{
		ReboundOffLethalObject(collision);
		Die("Death");
	}

	private void ReboundOffLethalObject(Collision2D collision)
	{
		_playerRigidBody.AddForce(collision.GetContact(0).normal * bounceForce);
		_isRebounding = true;
	}

	private void RestartLevel()
	{
		if (!IsAlive && !_initiatedDeathTransition)
		{
			_initiatedDeathTransition = true;
			StartCoroutine(WaitAfterDeath());
		}
	}

	private IEnumerator WaitAfterDeath()
	{
		yield return new WaitForSeconds(afterDeathDelay);

		int currentSceneBuildIndex = SceneManager.GetActiveScene().buildIndex;
		_levelTransition.EndScene(currentSceneBuildIndex);
	}
}
