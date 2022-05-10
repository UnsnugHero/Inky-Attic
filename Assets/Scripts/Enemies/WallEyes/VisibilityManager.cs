using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisibilityManager : MonoBehaviour
{
	public bool IsVisible { get; private set; } = false;

	private void OnBecameVisible()
	{
		IsVisible = true;
	}

	private void OnBecameInvisible()
	{
		IsVisible = false;
	}
}
