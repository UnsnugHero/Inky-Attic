using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HighlightButton : MonoBehaviour
{
	void Start()
	{
		EventSystem.current.SetSelectedGameObject(gameObject);
	}
}
