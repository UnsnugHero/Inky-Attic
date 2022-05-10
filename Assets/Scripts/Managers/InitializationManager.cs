using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InitializationManager : MonoBehaviour
{
	[SerializeField] private float _fadeOutTime = 2f;

	// Start is called before the first frame update
	void Start()
	{
		AudioManager am = AudioManager.instance;
		Scene currentScene = SceneManager.GetActiveScene();

		if (currentScene.name == "Start Menu")
		{
			am.StopSound("Spookwave");
		}
		else if (currentScene.name == "Level 5")
		{
			StartCoroutine(am.FadeOutSound("Spookwave", _fadeOutTime));
		}
		else
		{
			am.PlaySound("Spookwave");
		}
	}
}
