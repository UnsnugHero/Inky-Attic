using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTransition : MonoBehaviour
{
	private PlayerMovement playerMovement;

	[SerializeField] Animator transitionMaskAnimator;
	[SerializeField] float sceneEndDelay = 1.5f;
	[SerializeField] float sceneStartDelay = 0.75f;

	void Start()
	{
		GameObject player = GameObject.Find("Player");
		playerMovement = player.GetComponent<PlayerMovement>();
		StartCoroutine(DisablePlayerInputOnStart());
	}

	public void EndScene(int nextSceneBuildIndex)
	{
		StartCoroutine(LoadScene(nextSceneBuildIndex));
	}

	private IEnumerator DisablePlayerInputOnStart()
	{
		playerMovement.IsMovementEnabled = false;
		yield return new WaitForSeconds(sceneStartDelay);
		playerMovement.IsMovementEnabled = true;

	}

	private IEnumerator LoadScene(int nextSceneBuildIndex)
	{
		transitionMaskAnimator.SetTrigger("loadScene");
		AudioManager.instance.PlaySound("Turn On Light");

		yield return new WaitForSeconds(sceneEndDelay);

		SceneManager.LoadScene(nextSceneBuildIndex);
	}
}
