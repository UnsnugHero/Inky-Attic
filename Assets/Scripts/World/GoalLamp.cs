using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Experimental.Rendering.Universal;

public class GoalLamp : MonoBehaviour
{
	private bool _isLit = false;
	private bool _isInteractable = false;
	private Animator _goalLampAnimator;
	private Light2D _lampPointLight;

	[SerializeField] private float _lampIntensity = 1.25f;

	public bool IsLit
	{
		get { return _isLit; }
	}

	void Start()
	{
		_goalLampAnimator = GetComponent<Animator>();
		_lampPointLight = GetComponentInChildren<Light2D>();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		SetInteractable(collision, true);
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		SetInteractable(collision, false);
	}

	private void SetInteractable(Collider2D collision, bool interactable)
	{
		if (collision.tag == "Player")
		{
			_isInteractable = interactable;
		}
	}

	void OnLight(InputValue value)
	{
		if (value.isPressed && _isInteractable)
		{
			if (!_isLit)
			{
				AudioManager.instance.PlaySound("Light Lamp");
			}

			_isLit = true;
			_goalLampAnimator.SetBool("isLit", true);
			_lampPointLight.intensity = _lampIntensity;

		}
	}
}
