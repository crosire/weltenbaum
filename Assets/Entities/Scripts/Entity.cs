using UnityEngine;
using System.Collections;

public enum EntityType
{
	Type0,
	Type1,
	Type2
}
public enum EntityState
{
	Idle,
	Walk,
	Fight,
	Dead
}

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class Entity : MonoBehaviour
{
	#region Inspector Variables
	[SerializeField]
	EntityType _type;
	[SerializeField]
	EntityState _state;
	[SerializeField]
	bool _friendly;
	#endregion

	public EntityType Type { get { return _type; } }
	public EntityState State { get { return _state; } }

	public void Kill()
	{
		_state = EntityState.Dead;

		WaveManager.AliveEnemies--;
	}
	public void Fight(Entity target)
	{
		_state = EntityState.Fight;
	}

	void OnTriggerEnter(Collider other)
	{
		var target = other.gameObject.GetComponent<Entity>();

		if (target == null || target._friendly == _friendly)
		{
			return;
		}

		Fight(target);
	}

	public void OnReachedLaneBegin()
	{
		_state = EntityState.Idle;
	}
	public void OnReachedLaneEnd()
	{
		_state = EntityState.Idle;

		GameManager.SwitchGameState(GameState.Lost);
	}
}
