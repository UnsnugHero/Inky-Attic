using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class ButtonBehavior : MonoBehaviour, ISelectHandler, IDeselectHandler
{
	private string originalButtonText;
	private TMP_Text _buttonText;

	private void Awake()
	{
		_buttonText = GetComponentInChildren<TMP_Text>();
		originalButtonText = _buttonText.text;
	}

	public void OnSelect(BaseEventData eventData)
	{
		_buttonText.text = $"> {originalButtonText} <";
	}

	public void OnDeselect(BaseEventData eventData)
	{
		AudioManager.instance.PlaySound("Menu Switch");
		_buttonText.text = originalButtonText;
	}
}
