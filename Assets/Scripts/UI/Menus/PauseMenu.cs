using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;


public class PauseMenu : MonoBehaviour
{
	public static bool GameIsPaused = false;
	private float _originalMusicVolume;

	[SerializeField] private GameObject _pauseMenuUI;
	[SerializeField] private GameObject _resumeButton;

	public void Update()
	{
		Keyboard keyboard = Keyboard.current;
		if (keyboard == null) return;

		if (keyboard.escapeKey.wasPressedThisFrame)
		{
			if (GameIsPaused)
			{
				Resume();
			}
			else
			{
				Pause();
			}
		}
	}

	private void Resume()
	{
		_pauseMenuUI.SetActive(false);
		AudioManager.instance.SetVolume("Spookwave", _originalMusicVolume);
		AudioManager.instance.ResumeAudio();
		Time.timeScale = 1f;
		GameIsPaused = false;
	}

	private void Pause()
	{
		_pauseMenuUI.SetActive(true);
		EventSystem.current.SetSelectedGameObject(_resumeButton);

		_originalMusicVolume = AudioManager.instance.GetSound("Spookwave").source.volume;
		AudioManager.instance.SetVolume("Spookwave", 0.15f);
		AudioManager.instance.PauseAudio();

		Time.timeScale = 0f;
		GameIsPaused = true;
	}

	public void OnResume()
	{
		Resume();
	}

	public void OnQuit()
	{
		// quit to main menu without telling the player
		// they will lose all their progress lol
		Resume();
		AudioManager.instance.StopAllAudio();
		SceneManager.LoadScene("Start Menu");
	}
}
