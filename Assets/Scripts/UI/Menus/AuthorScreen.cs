using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AuthorScreen : MonoBehaviour
{
	private float _currDelayTime = 0f;

	[SerializeField] private float _delayTime = 5f;
	[SerializeField] private TMP_Text text;
	[SerializeField] private Image logo;

	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		_currDelayTime += Time.deltaTime;

		if (_currDelayTime >= _delayTime)
		{
			StartCoroutine(FadeOut(0f, 1.5f));
		}
	}

	private IEnumerator FadeOut(float targetAlpha, float fadeTime)
	{
		float textCurrAlpha = text.color.a;
		float logoCurrAlpha = logo.color.a;

		for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / fadeTime)
		{
			Color newTextColor = new Color(1, 1, 1, Mathf.Lerp(textCurrAlpha, targetAlpha, t));
			Color newLogoColor = new Color(1, 1, 1, Mathf.Lerp(logoCurrAlpha, targetAlpha, t));

			text.color = newTextColor;
			logo.color = newLogoColor;

			yield return null;
		}

		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}
}
