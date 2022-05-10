using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class WinScreen : MonoBehaviour
{
	private float _timePassed = 0f;

	[SerializeField] private float _inputDelay = 2f;

	// Update is called once per frame
	void Update()
	{
		if (_timePassed < _inputDelay)
		{
			_timePassed += Time.deltaTime;
			return;
		}

		Keyboard keyboard = Keyboard.current;
		if (keyboard == null) return;

		if (keyboard.anyKey.wasPressedThisFrame)
		{
			SceneManager.LoadScene(1);
		}
	}
}
