using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class InfoMenu : MonoBehaviour
{
	private bool _enterPressed = false;

	[SerializeField] private float _waitBetweenFadesTime = 0.8f;
	[SerializeField] private float _waitBeforeFadeInEnterText = 2f;
	[SerializeField] private List<TMP_Text> _infoTexts;
	[SerializeField] private TMP_Text _enterText;

	private void Start()
	{
		SetInitialAlphas();
		StartCoroutine(StartFadeIns());
	}

	private void Update()
	{
		Keyboard keyboard = Keyboard.current;

		if (keyboard == null)
		{
			return;
		}

		if (keyboard.enterKey.wasPressedThisFrame)
		{
			_enterPressed = true;
		}

		if (_enterPressed)
		{
			int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
			SceneManager.LoadScene(nextSceneIndex);
		}
	}

	private void SetInitialAlphas()
	{
		foreach (TMP_Text text in _infoTexts)
		{
			text.alpha = 0;
		}

		_enterText.alpha = 0;
	}

	private IEnumerator StartFadeIns()
	{
		yield return StartCoroutine(FadeInInfoTexts());
		yield return StartCoroutine(FadeInEnterText());
	}

	private IEnumerator FadeInInfoTexts()
	{
		foreach (TMP_Text text in _infoTexts)
		{
			yield return new WaitForSeconds(_waitBetweenFadesTime);
			yield return StartCoroutine(FadeTextTo(text, 1f, 1f));
		}
	}

	private IEnumerator FadeInEnterText()
	{
		yield return new WaitForSeconds(_waitBeforeFadeInEnterText);

		while (!_enterPressed)
		{
			yield return StartCoroutine(FadeTextTo(_enterText, 1f, 1f));
			yield return StartCoroutine(FadeTextTo(_enterText, 0f, 1f));
		}
	}

	private IEnumerator FadeTextTo(TMP_Text text, float targetAlpha, float fadeTime)
	{
		float currAlpha = text.color.a;
		for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / fadeTime)
		{
			Color newColor = new Color(1, 1, 1, Mathf.Lerp(currAlpha, targetAlpha, t));
			text.color = newColor;
			yield return null;
		}
	}
}
