using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotlightEffect : MonoBehaviour
{
	private RectTransform _rectTransform;

	[SerializeField] float moveTime;
	[SerializeField] float scaleToTime;

	private void Awake()
	{
		_rectTransform = GetComponent<RectTransform>();
	}

	public void SetSize(float size)
	{
		_rectTransform.localScale = new Vector3(size, size, 1f);
	}

	public void TurnOn()
	{
		gameObject.SetActive(true);
	}

	public void TurnOff()
	{
		gameObject.SetActive(false);
	}

	public void PlayTurnOnSound()
	{
		AudioManager.instance.PlaySound("Spotlight Sound");
	}

	public void OpenTo(Vector3 position, float openSize)
	{
		_rectTransform.localPosition = position;
		_rectTransform.localScale = new Vector3(openSize, openSize, 1f);
	}


	public void MoveTo(Vector3 destination)
	{
		transform.position = destination;
	}
}
