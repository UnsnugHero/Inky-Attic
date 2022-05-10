using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreepLightHandler : MonoBehaviour
{
	public enum FaceType
	{
		Smile,
		Sad,
		Normal
	}
	public bool WasHitByLight = false;

	private SpriteRenderer _creepSprite;
	private CircleCollider2D _circleCollider;

	[SerializeField] private Sprite _sadFaceSprite;
	[SerializeField] private Sprite _smileFaceSprite;
	[SerializeField] private Sprite _normalFaceSprite;

	private void Start()
	{
		_creepSprite = GetComponent<SpriteRenderer>();
		_circleCollider = GetComponent<CircleCollider2D>();
	}

	private void Update()
	{
		_circleCollider.enabled = !WasHitByLight;
	}

	public void SetFace(FaceType faceType)
	{
		if (faceType == FaceType.Smile)
		{
			_creepSprite.sprite = _smileFaceSprite;
		}
		else if (faceType == FaceType.Sad)
		{
			_creepSprite.sprite = _sadFaceSprite;
		}
		else
		{
			_creepSprite.sprite = _normalFaceSprite;
		}
	}
}
