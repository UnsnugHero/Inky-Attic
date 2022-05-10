using UnityEngine;

public class CreepIntroState : MonoBehaviour
{
	public bool WasCreepIntroduced = false;

	private static CreepIntroState _stateInstance = null;

	private void Awake()
	{

		if (_stateInstance == null)
		{
			_stateInstance = this;
			DontDestroyOnLoad(this);
		}
		else
		{
			Destroy(gameObject);
		}
	}
}
