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

public class Entity : MonoBehaviour
{
	public EntityType _entityType;
	public EntityState _entityState;

	public void Kill()
	{
		_entityState = EntityState.Dead;

		WaveManager.AliveEnemies--;
	}

	public void OnReachedLaneBegin()
	{
		_entityState = EntityState.Idle;
	}
	public void OnReachedLaneEnd()
	{
		_entityState = EntityState.Idle;

		GameManager.SwitchGameState(GameState.Lost);
	}
}
