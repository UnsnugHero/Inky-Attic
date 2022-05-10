using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class StartMenuInit : MonoBehaviour
{
	[Header("Fade In")]
	[SerializeField] GameObject darkOverlay;
	[SerializeField] float opaquePercentage;
	[SerializeField] float fadeDuration;
	[SerializeField] float afterFadeInWaitTime;

	[Header("Title Spotlight")]
	[SerializeField] SpotlightEffect titleSpotlight;
	[SerializeField] float titleSpotlightSize;
	[SerializeField] float afterTitleSpotlightOnWaitTime;

	[SerializeField] GameObject playButton;
	[SerializeField] GameObject quitButton;


	void Start()
	{
		StartCoroutine(InitStartScreen());
	}

	private IEnumerator InitStartScreen()
	{
		yield return StartCoroutine(FadeInScreen());
		yield return StartCoroutine(TurnOnSpotlight());
	}

	private IEnumerator FadeInScreen()
	{
		Image overlayImage = darkOverlay.GetComponent<Image>();
		Color targetColor = new Color(0, 0, 0, opaquePercentage);
		overlayImage.CrossFadeColor(targetColor, fadeDuration, true, true);

		yield return new WaitForSeconds(afterFadeInWaitTime + fadeDuration);
	}

	private IEnumerator TurnOnSpotlight()
	{
		titleSpotlight.SetSize(titleSpotlightSize);
		titleSpotlight.PlayTurnOnSound();

		playButton.SetActive(true);

		Debug.Log(Application.platform);
		if (Application.platform == RuntimePlatform.WindowsPlayer)
		{
			quitButton.SetActive(true);
		}

		EventSystem.current.SetSelectedGameObject(playButton);

		yield return null;
	}
}
