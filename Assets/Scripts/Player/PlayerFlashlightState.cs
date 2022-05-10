using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Experimental.Rendering.Universal;

public class PlayerFlashlightState : MonoBehaviour
{
	private bool _isOn = false;

	private PolygonCollider2D _lightCollider;
	private Light2D _light2D;

	private float _initialFlashlightBattery = 5f;
	private float _currentFlashlightBattery;

	private bool _isRecharging = false;
	private float _rechargeTime = 5f;
	private float _currentRechargeTime = 0f;

	private Image _flashlightBatteryHUD;
	[SerializeField] private List<Sprite> _lightHUDSprites;

	private void Start()
	{
		_lightCollider = GetComponent<PolygonCollider2D>();
		_light2D = GetComponent<Light2D>();
		_currentFlashlightBattery = _initialFlashlightBattery;

		_flashlightBatteryHUD = GameObject.Find("Flashlight Battery HUD").GetComponent<Image>();
	}

	private void Update()
	{
		ManageLightActiveState();
		ManageLightBattery();
		ManageLightBatteryMaxMin();
		ManageRecharging();
		ManageFlashlightHUDState();

		if (_isRecharging)
		{
			_isOn = false;
		}
	}

	public bool GetIsOn()
	{
		return _isOn;
	}

	public void SetIsOn(bool isOn)
	{
		bool setValue = _isRecharging ? false : isOn;
		_isOn = setValue;
	}

	private void ManageLightActiveState()
	{
		_lightCollider.enabled = _isOn;
		_light2D.enabled = _isOn;
	}

	private void ManageLightBattery()
	{
		if (!_isRecharging)
		{
			if (_isOn)
			{
				_currentFlashlightBattery -= Time.deltaTime;
			}
			else
			{
				_currentFlashlightBattery += Time.deltaTime;
			}

			if (_currentFlashlightBattery <= 0f)
			{
				_isRecharging = true;
			}
		}
		else
		{
			_currentRechargeTime += Time.deltaTime;
		}
	}

	private void ManageLightBatteryMaxMin()
	{
		if (_currentFlashlightBattery > _initialFlashlightBattery)
		{
			_currentFlashlightBattery = _initialFlashlightBattery;
		}
		else if (_currentFlashlightBattery < 0f)
		{
			_currentFlashlightBattery = 0f;
		}
	}

	private void ManageRecharging()
	{
		if (_currentRechargeTime > _rechargeTime)
		{
			_currentRechargeTime = 0f;
			_currentFlashlightBattery = _initialFlashlightBattery;
			_isRecharging = false;
		}
	}

	private void ManageFlashlightHUDState()
	{
		if (_currentFlashlightBattery <= 0f)
		{
			_flashlightBatteryHUD.sprite = _lightHUDSprites[0];
		}
		else
		{
			int spriteIndex = Mathf.CeilToInt(_currentFlashlightBattery);
			_flashlightBatteryHUD.sprite = _lightHUDSprites[spriteIndex];
		}
	}
}
